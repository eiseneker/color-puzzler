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
		GetComponent<Canvas>().enabled = false;
	}
	
	public static void FadeOut(){
		instance.GetComponent<Canvas>().enabled = true;
		instance.GetComponent<Animator>().Play("TransitionFadeOut");
	}
}
