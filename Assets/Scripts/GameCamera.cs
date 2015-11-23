using UnityEngine;
using System.Collections;

public class GameCamera : MonoBehaviour {
	public GameObject gameOver;
	public GameObject win;
	
	public void ShowGameOver(){
		string[] quotes = new string[8];
		
		quotes[0] = "there's always tomorrow, i guess...";
		quotes[1] = "you definitely gave it your all... right?";
		quotes[2] = "it's not that we're angry. just disappointed.";
		quotes[3] = "i'm not sure what i just witnessed...";
		quotes[4] = "have you considered getting into real estate?";
		quotes[5] = "well, that was something.";
		quotes[6] = "this is why we can't have nice things.";
		quotes[7] = "maybe i should just do this myself?";
	
		gameOver.SetActive(true);
		
		SpeechBubble speechBubble = SpeechBubble.mainBubble;
		speechBubble.textToDisplay = new string[1];
		speechBubble.textToDisplay[0] = quotes[Random.Range (0, quotes.Length)];
		speechBubble.dismissable = false;
		speechBubble.Activate ();
		
	}
	
	public void ShowWin(){
		string[] quotes = new string[8];
		
		quotes[0] = "you're... pretty good!";
		quotes[1] = "this is merely the beginning!";
		quotes[2] = "looks like i'm always on the winning side!";
		quotes[3] = "what skill! maybe you've done this one before?";
		quotes[4] = "if this keeps up, i might be able to take that vacation!";
		quotes[5] = "i always had complete faith in you! (heh)";
		quotes[6] = "get ready for the next battle!";
		quotes[7] = "i can't help but wonder if you're cheating...";
	
		win.SetActive(true);
		
		SpeechBubble speechBubble = SpeechBubble.mainBubble;
		
		speechBubble.textToDisplay = new string[1];
		speechBubble.textToDisplay[0] = quotes[Random.Range (0, quotes.Length)];
		speechBubble.dismissable = false;
		speechBubble.Activate ();
		
	}
	
	
	
}
