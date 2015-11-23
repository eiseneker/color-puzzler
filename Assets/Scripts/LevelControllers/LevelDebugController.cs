using UnityEngine;
using System.Collections;

public class LevelDebugController : LevelController {
	public override void Invoke(){
		SpeechBubble speechBubble = SpeechBubble.mainBubble;
		speechBubble.textToDisplay = new string[1];
		speechBubble.textToDisplay[0] = "debug room! enjoy!";
		speechBubble.dismissable = true;
		speechBubble.freezesGameOnDisplay = true;
		speechBubble.Activate();
	}
}


