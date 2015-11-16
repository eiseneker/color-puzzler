﻿using UnityEngine;
using System.Collections;

public class Level11Controller : MonoBehaviour, ILevel {
	public void Invoke(){
		string[][] cursorInstructions = new string[2][];
		
		cursorInstructions[1] = new string[] { "MrTile" };
	
		GameObject bubbleObject = Instantiate (Resources.Load ("SpeechBubble"), Vector3.zero, Quaternion.identity) as GameObject;
		SpeechBubble speechBubble = bubbleObject.GetComponent<SpeechBubble>();
		speechBubble.setWidth = 400;
		speechBubble.setHeight = 100;
		speechBubble.textToDisplay = new string[2];
		speechBubble.textToDisplay[0] = "now then, let's get started!";
		speechBubble.textToDisplay[1] = "do you know how to play?";
		speechBubble.dismissable = true;
		speechBubble.freezesGameOnDisplay = true;
		speechBubble.transform.position = Camera.main.WorldToScreenPoint(new Vector3(-2, -2, 0));
		speechBubble.cursorInstructions = cursorInstructions;
		
		
		
		
	}
}
