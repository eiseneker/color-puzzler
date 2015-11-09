using UnityEngine;
using System.Collections;

public class Cluster : MonoBehaviour {
	public ArrayList tiles = new ArrayList();
	private ArrayList tilesToDestroy = new ArrayList();
	private ArrayList tilesToRandomize = new ArrayList();
	
	public static float[] colorProbability = new float[] { 1.666f, 1.666f, 1.666f, 1.666f, 1.666f, 1.666f };
	
	public static float colorVariance = .25f;
	private int replaceIndex;

	// Use this for initialization
	void Start () {
		int lastColorIndex = -1;
		foreach(Transform child in transform){
			GameObject newTile = Instantiate (Resources.Load ("Tile"), child.position, Quaternion.identity) as GameObject;
			tiles.Add (newTile.transform);
			tilesToRandomize.Add (newTile.transform);
			tilesToDestroy.Add(child);
		}
		
		foreach(Transform tile in tilesToDestroy){
			Destroy (tile.gameObject);
		}
		
		foreach(Transform tile in tiles){
			tile.parent = transform;
		}
		
		RandomizeTiles();
		
		foreach(Transform tile in tilesToRandomize){
			print (colorVariance);
			if(lastColorIndex != -1 && Random.value > colorVariance){
				tile.GetComponent<GridElement>().colorIndex = lastColorIndex;
				tile.GetComponent<GridElement>().colorSet = true;
			}else{
				tile.GetComponent<GridElement>().colorIndex = GridElement.RandomizedColorIndex(colorProbability);
				tile.GetComponent<GridElement>().colorSet = true;
			}
			lastColorIndex = tile.GetComponent<GridElement>().colorIndex;
		}
	}
	
	private void RandomizeTiles(){
		for (int i = 0; i < tilesToRandomize.Count; i++) {
			Transform temp = (Transform)tilesToRandomize[i];
			int randomIndex = Random.Range(i, tilesToRandomize.Count);
			tilesToRandomize[i] = tilesToRandomize[randomIndex];
			tilesToRandomize[randomIndex] = temp;
		}
	}
	
	public int TileCount(){
		return(transform.childCount);
	}
}
