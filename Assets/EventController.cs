using UnityEngine;
using System.Collections;

public class EventController : MonoBehaviour {

	public static ArrayList tilesToReviewForLevel1 = new ArrayList();
	public static ArrayList tilesToReviewForBombs = new ArrayList();
	
	private float timeSinceLastEvaluation;
	private bool evaluateBombs = false;
	private ArrayList level1Tiles = new ArrayList();
	private ArrayList level2Tiles = new ArrayList();
	private ArrayList level3Tiles = new ArrayList();
	private GFGrid grid;
	
	void Start () {
		grid = GameObject.Find ("Grid").GetComponent<GFGrid>();
	}
	
	void Update(){
		if(tilesToReviewForLevel1.Count > 0 && timeSinceLastEvaluation > .5f){
			EvaluateTilesForLevel1();
			timeSinceLastEvaluation = 0;
		}else if(evaluateBombs && timeSinceLastEvaluation > .5f){
			EvaluateBombs();
		}else{
			timeSinceLastEvaluation += Time.deltaTime;
		}
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
					if(OppositeColors(neighbor, tileElement)){
						if(!listToReturn.Contains(neighbor.gameObject)) {
							listToReturn.Add (neighbor.gameObject);
							if(!listToReturn.Contains(tile.gameObject)) listToReturn.Add (tile.gameObject);
						}
					}
				}
			}
		}
		
		return(listToReturn);
	}

	void EvaluateTilesForLevel1(){
		GameController.ResetEventTimer();
		
		ArrayList tilesToFlip = new ArrayList();
		ArrayList tilesToIgnore = new ArrayList();
		ArrayList centerCandidates = GetTilesWithOppositeNeighbors(tilesToReviewForLevel1);
		
		foreach(GameObject centerCandidate in centerCandidates){
			bool[] matchMatrix = new bool[8];
			for(int i = 0; i < 8; i++){
				GridElement gridElement = centerCandidate.GetComponent<GridElement>();
				GameObject neighbor = ((GameObject)gridElement.AllNeighbors()[i]);
				if(neighbor != null){
					int difference = gridElement.colorIndex - neighbor.GetComponent<GridElement>().colorIndex;
					matchMatrix[i] = (Mathf.Abs (difference) == 3 && neighbor.GetComponent<GridElement>().canBeReplaced);
				}
			}
			if(matchMatrix[0] && matchMatrix[1] && matchMatrix[2] && matchMatrix[3] && matchMatrix[4] && matchMatrix[5] && matchMatrix[6] && matchMatrix[7]) tilesToIgnore.Add (centerCandidate.GetComponent<Tile>());
			if(matchMatrix[0] && matchMatrix[2] && matchMatrix[4] && matchMatrix[6] && !level3Tiles.Contains(centerCandidate.GetComponent<Tile>())) {
				tilesToIgnore.Add (centerCandidate.GetComponent<Tile>());
			}
			
			if(!tilesToFlip.Contains(centerCandidate.GetComponent<Tile>()) && !tilesToIgnore.Contains (centerCandidate.GetComponent<Tile>())){
				if((matchMatrix[0] && matchMatrix[4]) || (matchMatrix[2] && matchMatrix[6])){
					tilesToFlip.Add(centerCandidate.GetComponent<Tile>());
				}
			}
		}
		
		centerCandidates.Clear();
		
		foreach(Tile tile in tilesToFlip){
			int factor = 1;
			if(tile.GetComponent<GridElement>().colorIndex >= 3){
				factor *= -1;
			}
			int difference = Mathf.Abs (tile.GetComponent<GridElement>().colorIndex + (3 * factor));
			tile.GetComponent<GridElement>().UpdateColorByIndex(difference);
			GameController.ResetEventTimer();
		}
		
		tilesToReviewForLevel1 = tilesToFlip;
		foreach(Tile tile in tilesToFlip){
			if(!tilesToReviewForBombs.Contains(tile)) tilesToReviewForBombs.Add (tile);
		}
		
		foreach(Tile tile in level2Tiles){
			if(!tilesToReviewForBombs.Contains(tile)) tilesToReviewForBombs.Add (tile);
		}
		
		foreach(Tile tile in level3Tiles){
			if(!tilesToReviewForBombs.Contains(tile)) tilesToReviewForBombs.Add (tile);
		}
		
		if(tilesToReviewForLevel1.Count <= 0){
			evaluateBombs = true;
		}
	}
	
	void EvaluateBombs(){
		GameController.ResetEventTimer();
		foreach(Tile tile in tilesToReviewForBombs){
			if(!level2Tiles.Contains(tile) && !level3Tiles.Contains (tile)){
				bool[] matchMatrix = new bool[8];
				for(int i = 0; i < 8; i++){
					GridElement gridElement = tile.GetComponent<GridElement>();
					GameObject neighbor = ((GameObject)gridElement.AllNeighbors()[i]);
					if(neighbor != null){
						int difference = gridElement.colorIndex - neighbor.GetComponent<GridElement>().colorIndex;
						matchMatrix[i] = (Mathf.Abs (difference) == 3 && neighbor.GetComponent<GridElement>().canBeReplaced);
					}
				}
				if(matchMatrix[0] && matchMatrix[1] && matchMatrix[2] && matchMatrix[3] && matchMatrix[4] && matchMatrix[5] && matchMatrix[6] && matchMatrix[7]) level3Tiles.Add (tile);
				if(matchMatrix[0] && matchMatrix[2] && matchMatrix[4] && matchMatrix[6] && !level3Tiles.Contains(tile)) {
					level2Tiles.Add (tile);
				}
			}
		}
		
		foreach(Tile tile in level2Tiles){
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
			grid.GetComponent<Matrix>().InsertIntoMatrix(bomb);
		}
		
		foreach(Tile tile in level3Tiles){
			foreach(GameObject neighbor in tile.GetComponent<GridElement>().AllNeighbors()){
				neighbor.GetComponent<GridElement>().agnostic = true;
				neighbor.GetComponent<GridElement>().UpdateColor();
			}
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
			grid.GetComponent<Matrix>().InsertIntoMatrix(bomb);
		}
		
		ArrayList fieldBombs = new ArrayList();
		
		foreach(Transform child in GameObject.Find ("Bombs").transform){
			fieldBombs.Add (child.GetComponent<Bomb>());
		}
		
		foreach(Bomb bomb in fieldBombs){
			bomb.Explode ();
		}
		
		evaluateBombs = false;
		level3Tiles.Clear ();
		level2Tiles.Clear ();
		tilesToReviewForBombs.Clear ();
	}
}
