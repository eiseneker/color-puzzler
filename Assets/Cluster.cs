using UnityEngine;
using System.Collections;

public class Cluster : MonoBehaviour {
	public ArrayList tiles = new ArrayList();
	
	private float variance = .5f;
	private int replaceIndex;

	// Use this for initialization
	void Start () {
		foreach(Transform child in transform){
			tiles.Add (child);
		}
		
		RandomizeTiles();
	}
	
	private void RandomizeTiles(){
		for (int i = 0; i < tiles.Count; i++) {
			Transform temp = (Transform)tiles[i];
			int randomIndex = Random.Range(i, tiles.Count);
			tiles[i] = tiles[randomIndex];
			tiles[randomIndex] = temp;
		}
	}
	
	private void ColorTiles(){
		int randomColorIndex = RandomColorIndex();
		
		for(int i = 0; i < tiles.Count; i++){
			if(Random.value < variance){
				randomColorIndex = RandomColorIndex ();
			}
			((Transform)tiles[i]).GetComponent<GridElement>().colorIndex = randomColorIndex;
			((Transform)tiles[i]).GetComponent<GridElement>().colorSet = true;
		}
	}
	
	private int RandomColorIndex(){
		int colorIndex;
		float randomValue = Random.value;
		if(randomValue > .8f){
			colorIndex = 0;
		}else if(randomValue > .6f){
			colorIndex = 1;
		}else if(randomValue > .4f){
			colorIndex = 2;
		}else if(randomValue > .3f){
			colorIndex = 3;
		}else if(randomValue > .165f){
			colorIndex = 4;
		}else{
			colorIndex = 5;
		}
		return(colorIndex);
	}
	
	public int TileCount(){
		return(transform.childCount);
	}
}
