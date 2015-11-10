using UnityEngine;
using System.Collections;

public class Tile : MonoBehaviour {
	private Color tileColor;
	private int colorIndex;
	
	public static float[] colorProbability = new float[] { 1.666f, 1.666f, 1.666f, 1.666f, 1.666f, 1.666f };
	
	void Start(){
		if(!GetComponent<GridElement>().colorSet){
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
