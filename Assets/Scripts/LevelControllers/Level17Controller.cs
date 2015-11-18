using UnityEngine;
using System.Collections;

public class Level17Controller : LevelController {
	public override void Invoke(){
		GameObject bubbleObject = Instantiate (Resources.Load ("SpeechBubble"), Vector3.zero, Quaternion.identity) as GameObject;
		SpeechBubble speechBubble = bubbleObject.GetComponent<SpeechBubble>();
		speechBubble.setWidth = HUDHelpers.speechWidth;
		speechBubble.setHeight = HUDHelpers.speechHeight;
		speechBubble.textToDisplay = new string[3];
		speechBubble.textToDisplay[0] = "Did you know that some colors are opposites? red and green are considered opposites.";
		speechBubble.textToDisplay[1] = "interesting things happen when opposites are arranged together.";
		speechBubble.textToDisplay[2] = "try surrounding tiles with their opposites to see the effects.";
		speechBubble.dismissable = true;
		speechBubble.freezesGameOnDisplay = true;
		speechBubble.transform.position = Camera.main.WorldToScreenPoint(HUDHelpers.speechPosition);
	}
}


