using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class StabilityHUD : MonoBehaviour {

	Text text;

	// Use this for initialization
	void Start () {
		text = transform.Find ("Percentage").GetComponent<Text>();
	}
	
	// Update is called once per frame
	void Update () {
		text.text = (Cluster.colorStability * 100).ToString () + "%";
	}
}
