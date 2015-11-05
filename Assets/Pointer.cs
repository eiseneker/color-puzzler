using UnityEngine;
using System.Collections;

public class Pointer : MonoBehaviour {

	private Cluster cluster;
	private GFGrid grid;
	
//	private ArrayList tilesToReviewForLevel1 = new ArrayList();
//	private ArrayList tilesToReviewForBombs = new ArrayList();
//	private float timeSinceLastEvaluation;
//	private bool evaluateBombs = false;
//	ArrayList level1Tiles = new ArrayList();
//	ArrayList level2Tiles = new ArrayList();
//	ArrayList level3Tiles = new ArrayList();

	// Use this for initialization
	void Start () {
		grid = GameObject.Find ("Grid").GetComponent<GFGrid>();
	}
	
	// Update is called once per frame
	void Update () {
		if(cluster){
			Vector3 newPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
			newPosition.z = 1;
			cluster.transform.position = newPosition;
			grid.AlignTransform(cluster.transform);
			
			if(Input.GetMouseButtonDown(0)){
				InsertHere();
			}
		}else{
			if(!GameController.frozen){
				Cluster returnedCluster = GameController.GetNextCluster().GetComponent<Cluster>();
				returnedCluster.transform.position = transform.position;
				if(GameController.remainingTileCount >= returnedCluster.TileCount()){
					Vector3 newPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
					newPosition.z = 1;
					cluster = returnedCluster;
					grid.AlignTransform(cluster.transform);
				}else{
					GameController.LoadLoseScreen();
				}
			}
		}
		
//		if(tilesToReviewForLevel1.Count > 0 && timeSinceLastEvaluation > .5f){
//			EvaluateTilesForLevel1();
//			timeSinceLastEvaluation = 0;
//		}else if(evaluateBombs && timeSinceLastEvaluation > .5f){
//			EvaluateBombs();
//		}else{
//			timeSinceLastEvaluation += Time.deltaTime;
//		}
	}
	
//	void EvaluateTilesForLevel1(){
//		GameController.timeSinceLastEvent = 0;
//	
//		ArrayList centerCandidates = new ArrayList();
//		
//		ArrayList tilesToFlip = new ArrayList();
//		
//		foreach(Tile tile in tilesToReviewForLevel1){
//			ArrayList neighbors = tile.GetComponent<GridElement>().AllNeighbors();
//			foreach(GameObject neighbor in neighbors){
//				if(neighbor){
//					if(Mathf.Abs (neighbor.GetComponent<GridElement>().colorIndex - tile.GetComponent<GridElement>().colorIndex) == 3){
//						if(!centerCandidates.Contains(neighbor)) {
//							centerCandidates.Add (neighbor);
//							if(!centerCandidates.Contains(tile.gameObject)) centerCandidates.Add (tile.gameObject);
//						}
//					}
//				}
//			}
//		}
//		
//		foreach(GameObject centerCandidate in centerCandidates){
//			bool[] matchMatrix = new bool[8];
//			for(int i = 0; i < 8; i++){
//				GridElement gridElement = centerCandidate.GetComponent<GridElement>();
//				GameObject neighbor = ((GameObject)gridElement.AllNeighbors()[i]);
//				if(neighbor != null){
//					int difference = gridElement.colorIndex - neighbor.GetComponent<GridElement>().colorIndex;
//					matchMatrix[i] = (Mathf.Abs (difference) == 3 && neighbor.GetComponent<GridElement>().canBeReplaced);
//				}
//			}
//			if(matchMatrix[0] && matchMatrix[1] && matchMatrix[2] && matchMatrix[3] && matchMatrix[4] && matchMatrix[5] && matchMatrix[6] && matchMatrix[7]) level3Tiles.Add (centerCandidate.GetComponent<Tile>());
//			if(matchMatrix[0] && matchMatrix[2] && matchMatrix[4] && matchMatrix[6] && !level3Tiles.Contains(centerCandidate.GetComponent<Tile>())) {
//				level2Tiles.Add (centerCandidate.GetComponent<Tile>());
//			}
//			
//			if(!level1Tiles.Contains(centerCandidate.GetComponent<Tile>()) && !level2Tiles.Contains (centerCandidate.GetComponent<Tile>()) && !level3Tiles.Contains (centerCandidate.GetComponent<Tile>())){
//				if((matchMatrix[0] && matchMatrix[4]) || (matchMatrix[2] && matchMatrix[6])){
//					level1Tiles.Add(centerCandidate.GetComponent<Tile>());
//					tilesToFlip.Add(centerCandidate.GetComponent<Tile>());
//				}
//			}
//		}
//		
//		centerCandidates.Clear();
//		
//		foreach(Tile tile in tilesToFlip){
//			int factor = 1;
//			if(tile.GetComponent<GridElement>().colorIndex >= 3){
//				factor *= -1;
//			}
//			int difference = Mathf.Abs (tile.GetComponent<GridElement>().colorIndex + (3 * factor));
//			tile.GetComponent<GridElement>().UpdateColorByIndex(difference);
//			GameController.timeSinceLastEvent = 0;
//		}
//		
//		tilesToReviewForLevel1 = tilesToFlip;
//		foreach(Tile tile in tilesToFlip){
//			if(!tilesToReviewForBombs.Contains(tile)) tilesToReviewForBombs.Add (tile);
//		}
//		
//		foreach(Tile tile in level2Tiles){
//			if(!tilesToReviewForBombs.Contains(tile)) tilesToReviewForBombs.Add (tile);
//		}
//		
//		foreach(Tile tile in level3Tiles){
//			if(!tilesToReviewForBombs.Contains(tile)) tilesToReviewForBombs.Add (tile);
//		}
//		
//		if(tilesToReviewForLevel1.Count <= 0){
//			level1Tiles.Clear ();
//			level2Tiles.Clear ();
//			level3Tiles.Clear ();
//			evaluateBombs = true;
//		}
//	}
//	
//	void EvaluateBombs(){
//		GameController.timeSinceLastEvent = 0;
//		//1-0-1 combos
//		foreach(Tile tile in tilesToReviewForBombs){
//			if(!level2Tiles.Contains(tile) && !level3Tiles.Contains (tile)){
//				bool[] matchMatrix = new bool[8];
//				for(int i = 0; i < 8; i++){
//					GridElement gridElement = tile.GetComponent<GridElement>();
//					GameObject neighbor = ((GameObject)gridElement.AllNeighbors()[i]);
//					if(neighbor != null){
//						int difference = gridElement.colorIndex - neighbor.GetComponent<GridElement>().colorIndex;
//						matchMatrix[i] = (Mathf.Abs (difference) == 3 && neighbor.GetComponent<GridElement>().canBeReplaced);
//					}
//				}
//				if(matchMatrix[0] && matchMatrix[1] && matchMatrix[2] && matchMatrix[3] && matchMatrix[4] && matchMatrix[5] && matchMatrix[6] && matchMatrix[7]) level3Tiles.Add (tile);
//				if(matchMatrix[0] && matchMatrix[2] && matchMatrix[4] && matchMatrix[6] && !level3Tiles.Contains(tile)) {
//					level2Tiles.Add (tile);
//				}
//			}
//		}
//		
//		foreach(Tile tile in level2Tiles){
//			int originalColorIndex = tile.GetComponent<GridElement>().colorIndex;
//			Destroy(tile.gameObject);
//			GameObject bomb = Instantiate (Resources.Load ("Bomb"), tile.transform.position, Quaternion.identity) as GameObject;
//			bomb.GetComponent<GridElement>().colorSet = true;
//			int factor = 1;
//			if(originalColorIndex >= 3){
//				factor *= -1;
//			}
//			bomb.GetComponent<GridElement>().UpdateColorByIndex(Mathf.Abs (originalColorIndex + (3 * factor)));
//			bomb.transform.parent = GameObject.Find ("Bombs").transform;
//			grid.GetComponent<Matrix>().InsertIntoMatrix(bomb);
//		}
//		
//		foreach(Tile tile in level3Tiles){
//			foreach(GameObject neighbor in tile.GetComponent<GridElement>().AllNeighbors()){
//				neighbor.GetComponent<GridElement>().UpdateColorByIndex(6);
//				neighbor.GetComponent<GridElement>().agnostic = true;
//			}
//			int originalColorIndex = tile.GetComponent<GridElement>().colorIndex;
//			Destroy(tile.gameObject);
//			GameObject bomb = Instantiate (Resources.Load ("Bomb"), tile.transform.position, Quaternion.identity) as GameObject;
//			bomb.GetComponent<GridElement>().colorSet = true;
//			int factor = 1;
//			if(originalColorIndex >= 3){
//				factor *= -1;
//			}
//			bomb.GetComponent<GridElement>().UpdateColorByIndex(Mathf.Abs (originalColorIndex + (3 * factor)));
//			bomb.transform.parent = GameObject.Find ("Bombs").transform;
//			grid.GetComponent<Matrix>().InsertIntoMatrix(bomb);
//		}
//		
//		ArrayList fieldBombs = new ArrayList();
//		
//		foreach(Transform child in GameObject.Find ("Bombs").transform){
//			fieldBombs.Add (child.GetComponent<Bomb>());
//		}
//		
//		foreach(Bomb bomb in fieldBombs){
//			bomb.Explode ();
//		}
//		
//		evaluateBombs = false;
//		level3Tiles.Clear ();
//		level2Tiles.Clear ();
//		tilesToReviewForBombs.Clear ();
//	}
	
	void InsertHere(){
		GameController.frozen = true;
		ArrayList children = new ArrayList();
		bool canMove = true;
		foreach(Transform child in cluster.transform){
			children.Add (child);
		}
		foreach(Transform child in children){
			if(canMove){
				canMove = grid.GetComponent<Matrix>().CanInsertIntoMatrix(child.gameObject);
			}
		}
		if(canMove){
			ArrayList tiles = new ArrayList();
		
			foreach(Transform child in children){
				grid.GetComponent<Matrix>().InsertIntoMatrix(child.gameObject);
				child.parent = GameObject.Find ("Tiles").transform;
			}
			Destroy (cluster.gameObject);
			
			foreach(Transform child in children){
				Tile tile = child.GetComponent<Tile>();
				if(tile){
					tiles.Add (tile);
				}
			}
			
			EventController.tilesToReviewForLevel1 = tiles;
			EventController.tilesToReviewForBombs = tiles;
		}
	}
	
}
