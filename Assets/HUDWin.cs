using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class HUDWin : MonoBehaviour {

	private static GameObject instance;

	public static void Win(){
		instance.GetComponent<Text>().enabled = true;
	}
	
	void Start(){
		instance = gameObject;
	}
}
