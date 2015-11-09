using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour {

	Matrix matrix;
	private GFGrid grid;
	
	public static bool frozen = true;
	private static float timeSinceLastEvent = 0;
	public static float remainingEnergy = 100;
	public GameObject[] clusters;
	public static GameObject nextCluster;
	
	private static GameController instance;
	public static int targetCount = 0;
	public static int randomizedTileCount = 0;
	
	private float minX = -2.8f;
	private float maxX = 2.8f;
	private float minY = -3f;
	private float maxY = 4.3f;
	

	// Use this for initialization
	void Start () {
		instance = this;
		matrix = GameObject.Find ("Grid").GetComponent<Matrix>();
		grid = GameObject.Find ("Grid").GetComponent<GFGrid>();
		
		LevelLoader.LoadLevel ("1-1");
		
		InitializeTiles();
		
		ArrayList tiles = CreateRandomizedObjects((GameObject)Resources.Load ("Tile"), randomizedTileCount);
		ArrayList targets = CreateRandomizedObjects((GameObject)Resources.Load ("Target"), targetCount);
		
		foreach(GameObject tile in tiles){
			tile.transform.parent = GameObject.Find ("Tiles").transform;
		}
		
		foreach(GameObject target in targets){
			target.transform.parent = GameObject.Find ("Targets").transform;
		}
//		
		nextCluster = GenerateNextCluster();
	}
	
	// Update is called once per frame
	void Update () {
		timeSinceLastEvent += Time.deltaTime;
	
		if(timeSinceLastEvent > 1){
			frozen = false;
		}
	}
	
	public static void ResetEventTimer(){
		timeSinceLastEvent = 0;
	}
	
	public static void LoadLoseScreen(){
		Application.LoadLevel ("Lose");
	}
	
	public static GameObject GetNextCluster(){
		GameObject clusterToReturn = nextCluster;
		remainingEnergy -= clusterToReturn.GetComponent<Cluster>().TileCount();
		nextCluster = instance.GenerateNextCluster();
		return(clusterToReturn);		
	}
	
	private void InitializeTiles(){
		InsertChildrenIntoMatrix(GameObject.Find ("Tiles"));
		InsertChildrenIntoMatrix(GameObject.Find ("Bombs"));
	}
	
	private void InsertChildrenIntoMatrix(GameObject parentObject){
		foreach(Transform child in parentObject.transform){
			matrix.InsertIntoMatrix(child.gameObject);		
		}
	}
	
	private GameObject GenerateNextCluster(){
		return(Instantiate(clusters[0], new Vector3(-100, -100, 0), Quaternion.identity) as GameObject);
	}	
	private ArrayList CreateRandomizedObjects(GameObject objectToCreate, int numberOfObjects){
		ArrayList gameObjects = new ArrayList();
		for (int i = 0; i < numberOfObjects; i++)
		{
			bool success = false;
			while(success == false){
				Vector3 position = new Vector3(Random.Range (minX, maxX), Random.Range (minY, maxY), 5);
				GameObject tile = Instantiate (objectToCreate, position, Quaternion.identity) as GameObject;
				grid.AlignTransform(tile.transform);
				success = matrix.InsertIntoMatrix(tile);
				if(success){
					gameObjects.Add (tile);
				}else{
					Destroy (tile);
				}
			}
		}
		return(gameObjects);
	}
}
