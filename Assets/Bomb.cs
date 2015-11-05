using UnityEngine;
using System.Collections;

public class Bomb : MonoBehaviour {

	void Start(){
		GetComponent<GridElement>().canChain = false;
		GetComponent<GridElement>().canBeReplaced = false;
	}

	public void Explode(){
		GameController.frozen = true;
		GameController.ResetEventTimer();
		GetComponent<GridElement>().SetExplode(GridElement.Direction.None, 0);
	}
	
}
