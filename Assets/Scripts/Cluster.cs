﻿using UnityEngine;
using System.Collections;

public class Cluster : MonoBehaviour {
	public ArrayList tiles = new ArrayList();
	private ArrayList tilesToDestroy = new ArrayList();
	private ArrayList tilesToRandomize = new ArrayList();
	
	public static float[] colorProbability = new float[] { 1.666f, 1.666f, 1.666f, 1.666f, 1.666f, 1.666f };
	public static float whiteProbability = 0;
	public static float blackProbability = 0;
	public static int[] forcedPattern;
	
	public static float colorStability;
	private int replaceIndex;

	// Use this for initialization
	void Start () {
		transform.parent = GameObject.Find ("Clusters").transform;
		int lastColorIndex = -1;
		foreach(Transform child in transform.Find ("Tiles")){
			GameObject newTile = Instantiate (Resources.Load ("Tile"), child.position, Quaternion.identity) as GameObject;
			tiles.Add (newTile.transform);
			tilesToRandomize.Add (newTile.transform);
			tilesToDestroy.Add(child);
		}
		
		foreach(Transform tile in tilesToDestroy){
			Destroy (tile.gameObject);
		}
		
		foreach(Transform tile in tiles){
			tile.parent = transform.Find ("Tiles");
		}
	
		if(forcedPattern == null){	
			RandomizeTiles();
			
			foreach(Transform tile in tilesToRandomize){
				if(lastColorIndex != -1 && Random.value <= colorStability){
					tile.GetComponent<GridElement>().colorIndex = lastColorIndex;
					tile.GetComponent<GridElement>().colorSet = true;
				}else{
					float random = Random.value;
					if(random < whiteProbability){
						tile.GetComponent<GridElement>().white = true;
					}
					if(random >= whiteProbability && random < (whiteProbability + blackProbability)){
						tile.GetComponent<GridElement>().black = true;
					}
					tile.GetComponent<GridElement>().colorIndex = GridElement.RandomizedColorIndex(colorProbability);
					tile.GetComponent<GridElement>().colorSet = true;
				}
				lastColorIndex = tile.GetComponent<GridElement>().colorIndex;
			}
		}else{
			int i = 0;
			foreach(Transform tile in tiles){
				tile.GetComponent<GridElement>().colorIndex = forcedPattern[i];
				tile.GetComponent<GridElement>().colorSet = true;
				i++;
			}
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
}
