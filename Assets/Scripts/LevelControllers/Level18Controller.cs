using UnityEngine;
using System.Collections;

public class Level18Controller : LevelController {
	public override void Invoke(){
		GameObject bubbleObject = Instantiate (Resources.Load ("SpeechBubble"), Vector3.zero, Quaternion.identity) as GameObject;
		SpeechBubble speechBubble = bubbleObject.GetComponent<SpeechBubble>();
		speechBubble.setWidth = HUDHelpers.speechWidth;
		speechBubble.setHeight = HUDHelpers.speechHeight;
		speechBubble.textToDisplay = new string[4];
		speechBubble.textToDisplay[0] = "there are no bombs on the field. quite a predicament...";
		speechBubble.textToDisplay[1] = "you can make your own bombs by surrounding a color with its opposites.";
		speechBubble.textToDisplay[2] = "the pattern can either be plus shaped...";
		speechBubble.textToDisplay[3] = "... or shaped like a complete square. try them out!";
		speechBubble.dismissable = true;
		speechBubble.freezesGameOnDisplay = true;
		speechBubble.transform.position = Camera.main.WorldToScreenPoint(HUDHelpers.speechPosition);
	}
}


