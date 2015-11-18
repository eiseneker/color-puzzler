using UnityEngine;
using System.Collections;

public class Level17Controller : LevelController {
	public override void Invoke(){
		SpeechBubble speechBubble = SpeechBubble.mainBubble;
		speechBubble.textToDisplay = new string[3];
		speechBubble.textToDisplay[0] = "some colors are considered opposites, such as red vs. green";
		speechBubble.textToDisplay[1] = "interesting things happen when opposites are arranged together.";
		speechBubble.textToDisplay[2] = "try surrounding tiles with their opposites to see the effects.";
		speechBubble.dismissable = true;
		speechBubble.freezesGameOnDisplay = true;
		speechBubble.Activate();
	}
}


