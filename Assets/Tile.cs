﻿using UnityEngine;
using System.Collections;

public class Tile : MonoBehaviour {
	private Color tileColor;
	private int colorIndex;
	
	public static float[] colorProbability = new float[] { 1.666f, 1.666f, 1.666f, 1.666f, 1.666f, 1.666f };
	public static float whiteProbability = 0;
	public static float blackProbability = 0;
	
	void Start(){
		if(!GetComponent<GridElement>().colorSet){
			float random = Random.value;
			if(random < whiteProbability){
				GetComponent<GridElement>().white = true;
			}
			if(random >= whiteProbability && random < (whiteProbability + blackProbability)){
				GetComponent<GridElement>().black = true;
			}
			GetComponent<GridElement>().colorIndex = GridElement.RandomizedColorIndex(colorProbability);
			GetComponent<GridElement>().colorSet = true;
		}
		
		if(GetComponent<GridElement>().insertedIntoMatrix && GameObject.Find ("Tiles").transform != transform.parent){
			transform.parent = GameObject.Find ("Tiles").transform;
		}
	}

	public void ResetTransitionState(){
		GetComponent<Animator>().SetBool ("transitioning", false);
	}
	
	public void EnterTransitionState(){
		GetComponent<Animator>().SetBool ("transitioning", true);
	}
	
}
