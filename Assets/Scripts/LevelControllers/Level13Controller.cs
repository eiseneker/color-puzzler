using UnityEngine;
using System.Collections;

public class Level13Controller : LevelController {
	public override void Invoke(){
		string[][] cursorInstructions = new string[2][];
		
		cursorInstructions[0] = new string[] { "MrBomb" };
		
		SpeechBubble speechBubble = SpeechBubble.mainBubble;
		speechBubble.textToDisplay = new string[2];
		speechBubble.textToDisplay[0] = "looks like the bomb is orange this time. i wonder what that means?";
		speechBubble.textToDisplay[1] = "you've probably noticed by now that you can overlap color tiles. it's pretty handy!";
		speechBubble.dismissable = true;
		speechBubble.freezesGameOnDisplay = true;
		speechBubble.cursorInstructions = cursorInstructions;
		speechBubble.Activate();
	}
}


