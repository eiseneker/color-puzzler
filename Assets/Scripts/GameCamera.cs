using UnityEngine;
using System.Collections;

public class GameCamera : MonoBehaviour {
	public GameObject gameOver;
	public GameObject win;
	
	public void ShowGameOver(){
		gameOver.SetActive(true);
		
		GameObject bubbleObject = Instantiate (Resources.Load ("SpeechBubble"), Vector3.zero, Quaternion.identity) as GameObject;
		SpeechBubble speechBubble = bubbleObject.GetComponent<SpeechBubble>();
		speechBubble.textToDisplay = new string[1];
		speechBubble.textToDisplay[0] = "there's always tomorrow,\ni guess...";
		speechBubble.dismissable = false;
		speechBubble.transform.position = GetComponent<Camera>().WorldToScreenPoint(HUDHelpers.speechPosition);
		speechBubble.setHeight = HUDHelpers.speechHeight;
		speechBubble.setWidth = HUDHelpers.speechWidth;
	}
	
	public void ShowWin(){
		win.SetActive(true);
		
		GameObject bubbleObject = Instantiate (Resources.Load ("SpeechBubble"), Vector3.zero, Quaternion.identity) as GameObject;
		SpeechBubble speechBubble = bubbleObject.GetComponent<SpeechBubble>();
		speechBubble.textToDisplay = new string[1];
		speechBubble.textToDisplay[0] = "you're pretty good";
		speechBubble.dismissable = false;
		speechBubble.transform.position = GetComponent<Camera>().WorldToScreenPoint(HUDHelpers.speechPosition);
		speechBubble.setHeight = HUDHelpers.speechHeight;
		speechBubble.setWidth = HUDHelpers.speechWidth;
	}
}
