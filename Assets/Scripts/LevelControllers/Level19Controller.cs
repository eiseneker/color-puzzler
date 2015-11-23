using UnityEngine;
using System.Collections;

public class Level19Controller : LevelController {
	public override void Invoke(){
		string[][] cursorInstructions = new string[4][];
		
		cursorInstructions[0] = new string[] { "MrTile1", "MrTile3" };
		cursorInstructions[1] = new string[] { "MrTile1", "MrTile3" };
		cursorInstructions[2] = new string[] { "MrTile1", "MrTile3" };
		cursorInstructions[3] = new string[] { "MrTile1", "MrTile3" };
		
		SpeechBubble speechBubble = SpeechBubble.mainBubble;
		speechBubble.textToDisplay = new string[4];
		speechBubble.textToDisplay[0] = "combining opposite colors yields a locked tile. they can be brown or gray.";
		speechBubble.textToDisplay[1] = "in most cases, you can't place another tile on top of them.";
		speechBubble.textToDisplay[2] = "however, they can be destroyed, but only with a complete rainbow cascade.";
		speechBubble.textToDisplay[3] = "try it out and see what happens. it could save your life!";
		speechBubble.dismissable = true;
		speechBubble.freezesGameOnDisplay = true;
		speechBubble.cursorInstructions = cursorInstructions;
		
		speechBubble.Activate();
	}
}


