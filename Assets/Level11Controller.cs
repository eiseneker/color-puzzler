using UnityEngine;
using System.Collections;

public class Level11Controller : MonoBehaviour, ILevel {
	public void Invoke(){
		string[][] cursorInstructions = new string[6][];
		
		cursorInstructions[1] = new string[] { "MrTarget" };
		cursorInstructions[2] = new string[] { "MrTarget" };
		cursorInstructions[3] = new string[] { "MrBomb" };
		cursorInstructions[4] = new string[] { "MrTile" };
		cursorInstructions[5] = new string[] { "MrTile" };
		cursorInstructions[5] = new string[] { "MrBomb", "MrTarget" };
	
		GameObject bubbleObject = Instantiate (Resources.Load ("SpeechBubble"), Vector3.zero, Quaternion.identity) as GameObject;
		SpeechBubble speechBubble = bubbleObject.GetComponent<SpeechBubble>();
		speechBubble.setWidth = 400;
		speechBubble.setHeight = 120;
		speechBubble.textToDisplay = new string[6];
		speechBubble.textToDisplay[0] = "now then, let's get started!";
		speechBubble.textToDisplay[1] = "the rules are simple. first, let's look at targets. here's one right now!";
		speechBubble.textToDisplay[2] = "the goal of the game is to remove all targets from the board.";
		speechBubble.textToDisplay[3] = "to remove the target, it must become connected to a bomb.";
		speechBubble.textToDisplay[4] = "connect the target to the bomb by placing tiles between them.";
		speechBubble.textToDisplay[5] = "each turn, you get a new set of tiles to place on the board.";
		speechBubble.textToDisplay[5] = "go ahead and try creating a road of tiles between the target and the bomb.";
		speechBubble.dismissable = true;
		speechBubble.freezesGameOnDisplay = true;
		speechBubble.transform.position = Camera.main.WorldToScreenPoint(new Vector3(-2, -2, 0));
		speechBubble.cursorInstructions = cursorInstructions;
	}
}

