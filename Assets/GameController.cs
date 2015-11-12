using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour {

	Matrix matrix;
	private GFGrid grid;
	
	public static bool frozen = true;
	public static bool finished = false;
	public static bool textActive = false;
	private static float timeSinceLastEvent = 0;
	public static float remainingEnergy = 100;
	public static float energyRequirement = 4;
	public GameObject[] clusterPrefabs;
	
	private static GameController instance;
	public static int randomizedTargetCount = 0;
	public static int remainingTargetCount = 0;
	public static int randomizedTileCount = 0;
	public static ArrayList clusters = new ArrayList();
	
	private float minX = -2.8f;
	private float maxX = 2.8f;
	private float minY = -3f;
	private float maxY = 4.3f;
	
	public void ReplayLevel(){
		Application.LoadLevel ("Game");
	}
	
	// Use this for initialization
	void Start () {
		finished = false;
		textActive = false;
		instance = this;
		matrix = GameObject.Find ("Grid").GetComponent<Matrix>();
		grid = GameObject.Find ("Grid").GetComponent<GFGrid>();
		
		LevelLoader.matrix = matrix;
		LevelLoader.LoadLevel (ApplicationController.levelToLoad);
		
		ArrayList tiles = CreateRandomizedObjects((GameObject)Resources.Load ("Tile"), randomizedTileCount);
		ArrayList targets = CreateRandomizedObjects((GameObject)Resources.Load ("Target"), randomizedTargetCount);
		
		foreach(GameObject tile in tiles){
			tile.transform.parent = GameObject.Find ("Tiles").transform;
		}
		
		foreach(GameObject target in targets){
			target.transform.parent = GameObject.Find ("Targets").transform;
		}
		
		clusters.Add (GenerateNextCluster());
		clusters.Add (GenerateNextCluster());
		clusters.Add (GenerateNextCluster());
	}
	
	// Update is called once per frame
	void Update () {
		timeSinceLastEvent += Time.deltaTime;
	
		if(timeSinceLastEvent > 1){
			if(remainingTargetCount > 0){
				frozen = false;
			}else{
				LoadWinScreen();
			}
		}
	}
	
	public static bool Frozen(){
		return(frozen || finished || textActive);
	}
	
	public static void ResetEventTimer(){
		timeSinceLastEvent = 0;
	}
	
	public static void LoadLoseScreen(){
		Camera.main.GetComponent<Animator>().Play("Desaturate");
		finished = true;
	}
	
	public static void LoadWinScreen(){
		Camera.main.GetComponent<Animator>().Play("WinGlow");
		finished = true;
	}
	
	public static GameObject GetNextCluster(){
		if(clusters.Count >= 4) clusters.RemoveAt (0);
		clusters.Add(instance.GenerateNextCluster());
		GameObject clusterToReturn = clusters[0] as GameObject;
		return(clusterToReturn);		
	}
	
	private void InsertChildrenIntoMatrix(GameObject parentObject){
		foreach(Transform child in parentObject.transform){
			matrix.InsertIntoMatrix(child.gameObject);		
		}
	}
	
	private GameObject GenerateNextCluster(){
		return(Instantiate(clusterPrefabs[0], new Vector3(-100, -100, 0), Quaternion.identity) as GameObject);
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
