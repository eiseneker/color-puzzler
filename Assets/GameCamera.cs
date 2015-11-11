using UnityEngine;
using System.Collections;

public class GameCamera : MonoBehaviour {
	public GameObject gameOver;
	public GameObject win;
	
	public void ShowGameOver(){
		gameOver.SetActive(true);
		
		GameObject bubbleObject = Instantiate (Resources.Load ("SpeechBubble"), Vector3.zero, Quaternion.identity) as GameObject;
		SpeechBubble speechBubble = bubbleObject.GetComponent<SpeechBubble>();
		speechBubble.textToDisplay = "there's always tomorrow,\ni guess...";
		speechBubble.dismissable = false;
		speechBubble.transform.position = GetComponent<Camera>().WorldToScreenPoint(new Vector3(-2, -2, 0));
	}
	
	public void ShowWin(){
		win.SetActive(true);
		
		GameObject bubbleObject = Instantiate (Resources.Load ("SpeechBubble"), Vector3.zero, Quaternion.identity) as GameObject;
		SpeechBubble speechBubble = bubbleObject.GetComponent<SpeechBubble>();
		speechBubble.textToDisplay = "you're pretty good";
		speechBubble.dismissable = false;
		speechBubble.transform.position = GetComponent<Camera>().WorldToScreenPoint(new Vector3(-2, -2, 0));
	}
}
