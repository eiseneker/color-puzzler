using UnityEngine;
using System.Collections;

public class Tile : MonoBehaviour {
	private Color tileColor;
	private int colorIndex;

	public void ResetColor(){
	}
	
	public void ResetTransitionState(){
		print ("exiting transition state");
		GetComponent<Animator>().SetBool ("transitioning", false);
	}
	
	public void EnterTransitionState(){
		print ("entering transition state");
		GetComponent<Animator>().SetBool ("transitioning", true);
	}
}
