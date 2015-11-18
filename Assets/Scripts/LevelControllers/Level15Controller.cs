using UnityEngine;
using System.Collections;

public class Level15Controller : LevelController {
	public override void Invoke(){
		SpeechBubble speechBubble = SpeechBubble.mainBubble;
		speechBubble.textToDisplay = new string[3];
		speechBubble.textToDisplay[0] = "did you know that there's a limit to the number of turns you can take?";
		speechBubble.textToDisplay[1] = "be sure to keep an eye on the energy meter, near the bottom-right";
		speechBubble.textToDisplay[2] = "you can reclaim energy by cascading color explosions";
		speechBubble.dismissable = true;
		speechBubble.freezesGameOnDisplay = true;
		speechBubble.Activate();
	}
}


