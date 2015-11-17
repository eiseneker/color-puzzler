using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Bonus : MonoBehaviour {

	// Use this for initialization
	void Start () {
		transform.parent = GameObject.Find ("PlayHUD").transform;
	}
	
	public void DestroyMe(){
		Destroy(gameObject);
	}
	
	public void SetValue(int value){
		transform.Find ("Text").GetComponent<Text>().text = value.ToString ();
	}
}
