using UnityEngine;
using System.Collections;

public class Target : MonoBehaviour {
	private Color tileColor;
	private int colorIndex;
	
	public static float[] colorProbability = new float[] { 1.666f, 1.666f, 1.666f, 1.666f, 1.666f, 1.666f };
	
	void Start(){
		if(!GetComponent<GridElement>().colorSet){
			GetComponent<GridElement>().colorIndex = GridElement.RandomizedColorIndex(colorProbability);
			GetComponent<GridElement>().colorSet = true;
		}
		
		if(GameObject.Find ("Targets").transform != transform.parent){
			transform.parent = GameObject.Find ("Targets").transform;
		}
		
		GameController.remainingTargetCount++;
	}
	
}
