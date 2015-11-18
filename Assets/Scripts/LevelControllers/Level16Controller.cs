using UnityEngine;
using System.Collections;

public class Level16Controller : LevelController {
	public override void Invoke(){
		SpeechBubble speechBubble = SpeechBubble.mainBubble;
		speechBubble.textToDisplay = new string[3];
		speechBubble.textToDisplay[0] = "have you noticed the lower-left display? the four blocks with question marks?";
		speechBubble.textToDisplay[1] = "it shows you the tiles you are currently placing, and the next three sets you'll draw.";
		speechBubble.textToDisplay[2] = "use this to help plan your moves!";
		speechBubble.dismissable = true;
		speechBubble.freezesGameOnDisplay = true;
		speechBubble.Activate();
	}
}


