using UnityEngine;
using System.Collections;

public class EventController : MonoBehaviour {

	public static ArrayList tilesToReviewForLevel1 = new ArrayList();
	public static ArrayList tilesToReviewForBombs = new ArrayList();
	
	private float timeSinceLastEvaluation;
	private bool evaluateBombs = false;
	private ArrayList level2Tiles = new ArrayList();
	private ArrayList level3Tiles = new ArrayList();
	private GFGrid grid;
	private ArrayList tilesToFlip = new ArrayList();
	private float timeSinceLastFlip;
	private float explosionDelay;
	private bool readyToExplode;
	private ArrayList fieldBombs = new ArrayList();
	
	void Start () {
		grid = GameObject.Find ("Grid").GetComponent<GFGrid>();
	}
	
	void Update(){
		if(readyToExplode && explosionDelay > 1){
			Explode ();
		}else if(readyToExplode){
			GameController.ResetEventTimer();
			explosionDelay += Time.deltaTime;
		}else if(tilesToFlip.Count > 0 && timeSinceLastFlip > .3f){
			FlipTiles ();
			timeSinceLastFlip = 0;
		}else if(tilesToReviewForLevel1.Count > 0 && timeSinceLastEvaluation > .5f){
			EvaluateTilesForLevel1();
			timeSinceLastEvaluation = 0;
		}else if(evaluateBombs && timeSinceLastEvaluation > .5f){
			EvaluateBombs();
		}else{
			timeSinceLastEvaluation += Time.deltaTime;
		}
		timeSinceLastFlip += Time.deltaTime;
	}
	
	private void EvaluateTilesForLevel1(){
		GameController.ResetEventTimer();
		
		ArrayList tilesToIgnore = new ArrayList();
		ArrayList centerCandidates = GetTilesWithOppositeNeighbors(tilesToReviewForLevel1);
		
		foreach(GameObject centerCandidate in centerCandidates){
			bool[] matchMatrix = GenerateMatchMatrix(centerCandidate);
			if(IsLevel3Pattern(matchMatrix)) tilesToIgnore.Add (centerCandidate.GetComponent<Tile>());
			if(IsLevel2Pattern(matchMatrix) && !tilesToIgnore.Contains(centerCandidate.GetComponent<Tile>())) {
				tilesToIgnore.Add (centerCandidate.GetComponent<Tile>());
			}
			
			bool notAlreadyUsed = !tilesToFlip.Contains(centerCandidate.GetComponent<Tile>()) &&
				!tilesToIgnore.Contains (centerCandidate.GetComponent<Tile>());
			
			if(notAlreadyUsed){
				if(IsLevel1Pattern(matchMatrix)) {
					tilesToFlip.Add(centerCandidate.GetComponent<Tile>());
				}
			}
		}
		
		centerCandidates.Clear();
		
		tilesToReviewForLevel1 = (ArrayList)tilesToFlip.Clone ();
		
		foreach(Tile tile in tilesToFlip){
			if(!tilesToReviewForBombs.Contains(tile)) tilesToReviewForBombs.Add (tile);
		}
		
		foreach(Tile tile in tilesToIgnore){
			if(!tilesToReviewForBombs.Contains(tile)) tilesToReviewForBombs.Add (tile);
		}
		
		if(tilesToReviewForLevel1.Count <= 0){
			evaluateBombs = true;
		}
	}
	
	private void EvaluateBombs(){
		GameController.ResetEventTimer();
		
		StockLevel2AndLevel3Tiles();
		
		foreach(Tile tile in level2Tiles){
			ConvertToBomb (tile);
		}
		
		foreach(Tile tile in level3Tiles){
			foreach(GameObject neighbor in tile.GetComponent<GridElement>().AllNeighbors()){
				neighbor.GetComponent<GridElement>().agnostic = true;
				neighbor.GetComponent<GridElement>().UpdateColor(1);
				neighbor.GetComponent<Tile>().EnterTransitionState();
			}
			ConvertToBomb (tile);
		}
		
		foreach(Transform child in GameObject.Find ("Bombs").transform){
			fieldBombs.Add (child.GetComponent<Bomb>());
		}
		
		readyToExplode = true;
	}
	
	private void Explode(){
		GameController.ResetEventTimer();
		foreach(Bomb bomb in fieldBombs){
			bomb.Explode ();
		}
		
		evaluateBombs = false;
		readyToExplode = false;
		explosionDelay = 0;
		level3Tiles.Clear ();
		level2Tiles.Clear ();
		tilesToReviewForBombs.Clear ();
		fieldBombs.Clear ();
	}
	
	private void StockLevel2AndLevel3Tiles(){
		foreach(Tile tile in tilesToReviewForBombs){
			if(!level2Tiles.Contains(tile) && !level3Tiles.Contains (tile)){
				bool[] matchMatrix = GenerateMatchMatrix(tile.gameObject);
				if(IsLevel3Pattern(matchMatrix)) level3Tiles.Add (tile);
				if(IsLevel2Pattern(matchMatrix) && !level3Tiles.Contains(tile)) {
					level2Tiles.Add (tile);
				}
			}
		}
	}
	
	private void FlipTiles(){
		foreach(Tile tile in tilesToFlip){
			int factor = 1;
			if(tile.GetComponent<GridElement>().colorIndex >= 3){
				factor *= -1;
			}
			int difference = Mathf.Abs (tile.GetComponent<GridElement>().colorIndex + (3 * factor));
			tile.GetComponent<GridElement>().UpdateColorByIndex(difference, 1);
			GameController.ResetEventTimer();
			tile.EnterTransitionState();
		}
		tilesToFlip.Clear ();
	}
	
	private bool OppositeColors(GridElement color1, GridElement color2){
		return(Mathf.Abs (color1.colorIndex - color2.colorIndex) == 3);
	}
	
	private ArrayList GetTilesWithOppositeNeighbors(ArrayList list){
		ArrayList listToReturn = new ArrayList();
	
		foreach(Tile tile in list){
			GridElement tileElement = tile.GetComponent<GridElement>();
			ArrayList neighbors = tileElement.AllNeighbors();
			foreach(GameObject neighborObject in neighbors){
				if(neighborObject){
					GridElement neighbor = neighborObject.GetComponent<GridElement>();
					if(!neighbor.disabled && neighbor.canBeReplaced && OppositeColors(neighbor, tileElement)){
						if(!listToReturn.Contains(neighbor.gameObject)) {
							listToReturn.Add (neighbor.gameObject);
						}
						if(!listToReturn.Contains(tile.gameObject)) {
							listToReturn.Add (tile.gameObject);
						}
					}
				}
			}
		}
		
		return(listToReturn);
	}
	
	private bool IsLevel3Pattern(bool[] matchMatrix){
		return(
			matchMatrix[0] && matchMatrix[1] && matchMatrix[2] && matchMatrix[3] &&
			matchMatrix[4] && matchMatrix[5] && matchMatrix[6] && matchMatrix[7]
		);
	}
	
	private bool IsLevel2Pattern(bool[] matchMatrix){
		return(matchMatrix[0] && matchMatrix[2] && matchMatrix[4] && matchMatrix[6]);
	}
	
	private bool IsLevel1Pattern(bool[] matchMatrix){
		return((matchMatrix[0] && matchMatrix[4]) || (matchMatrix[2] && matchMatrix[6]));
	}
	
	private void ConvertToBomb(Tile tile){
		int originalColorIndex = tile.GetComponent<GridElement>().colorIndex;
		Destroy(tile.gameObject);
		GameObject bomb = Instantiate (Resources.Load ("Bomb"), tile.transform.position, Quaternion.identity) as GameObject;
		bomb.GetComponent<GridElement>().colorSet = true;
		int factor = 1;
		if(originalColorIndex >= 3){
			factor *= -1;
		}
		bomb.GetComponent<GridElement>().UpdateColorByIndex(Mathf.Abs (originalColorIndex + (3 * factor)));
		bomb.transform.parent = GameObject.Find ("Bombs").transform;
		print ("inserting bomb into matrix");
		grid.GetComponent<Matrix>().InsertIntoMatrix(bomb);
	}
	
	private bool[] GenerateMatchMatrix(GameObject center){
		bool[] matchMatrix = new bool[8];
		for(int i = 0; i < 8; i++){
			GridElement gridElement = center.GetComponent<GridElement>();
			GameObject neighbor = ((GameObject)gridElement.AllNeighbors()[i]);
			if(neighbor != null){
				matchMatrix[i] = (OppositeColors(gridElement, neighbor.GetComponent<GridElement>()) && neighbor.GetComponent<GridElement>().canBeReplaced && !neighbor.GetComponent<GridElement>().disabled);
			}
		}
		return(matchMatrix);
	}
	

}
