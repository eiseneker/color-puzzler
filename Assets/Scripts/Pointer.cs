using UnityEngine;
using System.Collections;

public class Pointer : MonoBehaviour {

	private Cluster cluster;
	private GFGrid grid;
	
	// Use this for initialization
	void Start () {
		grid = GameObject.Find ("Grid").GetComponent<GFGrid>();
	}
	
	// Update is called once per frame
	void Update () {
		if(!HUDDrawer.drawerOut && !SpeechBubble.inFreezeState){
			if(cluster){
				Vector3 newPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
				newPosition.z = 1;
				cluster.transform.position = newPosition;
				grid.AlignTransform(cluster.transform);
				
				if(Input.GetMouseButtonDown(0)){
					InsertHere();
				}
			}else{
				if(!GameController.Frozen () && !GameController.finished){
					GameObject nextCluster = GameController.GetNextCluster();
					if(nextCluster){
						Cluster returnedCluster = nextCluster.GetComponent<Cluster>();
						returnedCluster.transform.position = transform.position;
						if(GameController.remainingEnergy >= 1){
							Vector3 newPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
							newPosition.z = 1;
							cluster = returnedCluster;
							grid.AlignTransform(cluster.transform);
						}else{
							Destroy (returnedCluster.gameObject);
							GameController.LoadLoseScreen();
						}
					}
				}
			}
		}
	}
	
	void InsertHere(){
		GameController.frozen = true;
		ArrayList children = new ArrayList();
		bool canMove = true;
		foreach(Transform child in cluster.transform.Find ("Tiles")){
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
			EventController.ResetTiles();
			EventController.tilesToReviewForLevel1 = tiles;
			EventController.tilesToReviewForBombs = tiles;
			GameController.remainingEnergy = Mathf.Clamp (GameController.remainingEnergy - GameController.EnergyRequirement (), 0, 100);
			GameController.turnCount++;
		}
	}
	
}
