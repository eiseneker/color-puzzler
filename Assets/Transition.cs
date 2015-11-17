using UnityEngine;
using System.Collections;

public class Transition : MonoBehaviour {

	public static bool finished;
	public static Transition instance;

	// Use this for initialization
	void Start () {
		finished = false;
		instance = this;
	}
	
	public void MarkFinished(){
		finished = true;
		GetComponent<Canvas>().sortingOrder = -1;
		
	}
	
	public static void FadeOut(){
		instance.GetComponent<Canvas>().sortingOrder = 2;
		instance.GetComponent<Animator>().Play("TransitionFadeOut");
	}
}
