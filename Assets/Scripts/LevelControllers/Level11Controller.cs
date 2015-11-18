using UnityEngine;
using System.Collections;

public class Level11Controller : LevelController {
	public override void Invoke(){
		string[][] cursorInstructions = new string[6][];
		
		cursorInstructions[1] = new string[] { "MrTarget" };
		cursorInstructions[2] = new string[] { "MrTarget", "MrBomb" };
		cursorInstructions[3] = new string[] { "MrTile" };
		cursorInstructions[4] = new string[] { "MrTile" };
		cursorInstructions[5] = new string[] { "MrBomb", "MrTarget" };
	
		SpeechBubble speechBubble = SpeechBubble.mainBubble;
		
		speechBubble.textToDisplay = new string[6];
		speechBubble.textToDisplay[0] = "hello world! have you come to help us?";
		speechBubble.textToDisplay[1] = "our mission is to rescue my imprisoned comrades.";
		speechBubble.textToDisplay[2] = "to free the target, it must become connected to a bomb. the whirly thing.";
		speechBubble.textToDisplay[3] = "these connections are made by placing tiles.";
		speechBubble.textToDisplay[4] = "each turn, you get a new set of tiles to place on the board.";
		speechBubble.textToDisplay[5] = "go ahead and try creating a road of tiles between the target and the bomb.";
		speechBubble.dismissable = true;
		speechBubble.cursorInstructions = cursorInstructions;
		speechBubble.Activate();
	}
}

