using UnityEngine;
using System.Collections;

public class Bomb : MonoBehaviour {
	
	public static float[] colorProbability = new float[] { 1.666f, 1.666f, 1.666f, 1.666f, 1.666f, 1.666f };
	
	void Start(){
		GetComponent<GridElement>().canChain = false;
		GetComponent<GridElement>().canBeReplaced = false;
		
		if(!GetComponent<GridElement>().colorSet){
			GetComponent<GridElement>().colorIndex = GridElement.RandomizedColorIndex(colorProbability);
			GetComponent<GridElement>().colorSet = true;
		}
		
		GetComponent<GridElement>().InitializeColor();
		
		if(GameObject.Find ("Bombs").transform != transform.parent){
			transform.parent = GameObject.Find ("Bombs").transform;
		}
		
	}

	public void Explode(){
		GameController.frozen = true;
		GameController.ResetEventTimer();
		GetComponent<GridElement>().SetExplode(GridElement.Direction.None, 0);
	}
	
}
