using UnityEngine;
using System.Collections;

public class Level16Controller : LevelController {
	public override void Invoke(){
		GameObject bubbleObject = Instantiate (Resources.Load ("SpeechBubble"), Vector3.zero, Quaternion.identity) as GameObject;
		SpeechBubble speechBubble = bubbleObject.GetComponent<SpeechBubble>();
		speechBubble.setWidth = HUDHelpers.speechWidth;
		speechBubble.setHeight = HUDHelpers.speechHeight;
		speechBubble.textToDisplay = new string[3];
		speechBubble.textToDisplay[0] = "have you noticed the lower-left display? the four blocks with question marks?";
		speechBubble.textToDisplay[1] = "it shows you the cluster you are currently placing, and the next three clusters you'll draw.";
		speechBubble.textToDisplay[2] = "as the cluster becomes more imminent, you'll see more of it. use this to help plan your moves!";
		speechBubble.dismissable = true;
		speechBubble.freezesGameOnDisplay = true;
		speechBubble.transform.position = Camera.main.WorldToScreenPoint(HUDHelpers.speechPosition);
	}
}


