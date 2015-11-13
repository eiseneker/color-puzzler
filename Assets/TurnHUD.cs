using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TurnHUD : MonoBehaviour {

	Text number;

	// Use this for initialization
	void Start () {
		number = transform.Find ("Number").GetComponent<Text>();
	}
	
	// Update is called once per frame
	void Update () {
		number.text = (GameController.turnCount + 1).ToString ();
	}
}
