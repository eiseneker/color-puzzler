using UnityEngine;
using System.Collections;

public class Level110Controller : LevelController {
	public override void Invoke(){
		SpeechBubble speechBubble = SpeechBubble.mainBubble;
		speechBubble.textToDisplay = new string[5];
		speechBubble.textToDisplay[0] = "did you know that you can expand the hud? touch the arrow at the bottom.";
		speechBubble.textToDisplay[1] = "this will let you go back to level select, see the current turn number...";
		speechBubble.textToDisplay[2] = "it also shows the likelihood of colors that can be drawn...";
		speechBubble.textToDisplay[3] = "and \"stability\", or how likely your drawn tiles will be the same color. the lower";
		speechBubble.textToDisplay[4] = "the number, the more likely draw tiles will be different colors.";
		speechBubble.dismissable = true;
		speechBubble.freezesGameOnDisplay = true;
		
		speechBubble.Activate();
	}
}


