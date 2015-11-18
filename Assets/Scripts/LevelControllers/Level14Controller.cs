using UnityEngine;
using System.Collections;

public class Level14Controller : LevelController {
	public override void Invoke(){
		SpeechBubble speechBubble = SpeechBubble.mainBubble;
		speechBubble.textToDisplay = new string[3];
		speechBubble.textToDisplay[0] = "a green target this time, though you won't see any green tiles.";
		speechBubble.textToDisplay[1] = "did you know that tile explosions can cascade to other colors?";
		speechBubble.textToDisplay[2] = "it depends on the color, though. for a hint, think about the colors of the rainbow.";
		speechBubble.dismissable = true;
		speechBubble.freezesGameOnDisplay = true;
		speechBubble.Activate();
	}
}


