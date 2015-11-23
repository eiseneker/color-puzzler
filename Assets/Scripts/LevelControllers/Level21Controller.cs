using UnityEngine;
using System.Collections;

public class Level21Controller : LevelController {
	public override void Invoke(){
		SpeechBubble speechBubble = SpeechBubble.mainBubble;
		speechBubble.textToDisplay = new string[2];
		speechBubble.textToDisplay[0] = "if you expand the hud, you'll see a colored chart";
		speechBubble.textToDisplay[1] = "this represents the likelihood of which colors will be drawn";
		speechBubble.dismissable = true;
		speechBubble.freezesGameOnDisplay = true;
		speechBubble.Activate();
	}
}


