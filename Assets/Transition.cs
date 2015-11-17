using UnityEngine;
using System.Collections;

public class Transition : MonoBehaviour {

	public static bool finished;
	private static Transition instance;

	// Use this for initialization
	void Start () {
		finished = false;
		instance = this;
	}
	
	public void MarkFinished(){
		finished = true;
	}
	
	public static void FadeOut(){
		instance.GetComponent<Animator>().Play("TransitionFadeOut");
	}
}
