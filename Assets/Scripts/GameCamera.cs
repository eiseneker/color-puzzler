using UnityEngine;
using System.Collections;

public class GameCamera : MonoBehaviour {
	public GameObject gameOver;
	public GameObject win;
	
	public void ShowGameOver(){
		gameOver.SetActive(true);
		
		SpeechBubble speechBubble = SpeechBubble.mainBubble;
		speechBubble.textToDisplay = new string[1];
		speechBubble.textToDisplay[0] = "there's always tomorrow,\ni guess...";
		speechBubble.dismissable = false;
		speechBubble.Activate ();
		
	}
	
	public void ShowWin(){
		win.SetActive(true);
		
		SpeechBubble speechBubble = SpeechBubble.mainBubble;
		
		speechBubble.textToDisplay = new string[1];
		speechBubble.textToDisplay[0] = "you're pretty good";
		speechBubble.dismissable = false;
		speechBubble.Activate ();
		
	}
}
