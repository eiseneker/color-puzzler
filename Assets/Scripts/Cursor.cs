﻿using UnityEngine;
using System.Collections;

public class Cursor : MonoBehaviour {

	// Use this for initialization
	void Start () {
		GameObject playHud = GameObject.Find ("CursorHUD");
		transform.parent = playHud.transform;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
