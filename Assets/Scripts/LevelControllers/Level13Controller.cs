using UnityEngine;
using System.Collections;

public class Level13Controller : LevelController {
	public override void Invoke(){
		string[][] cursorInstructions = new string[2][];
		
		cursorInstructions[0] = new string[] { "MrBomb" };
		
		GameObject bubbleObject = Instantiate (Resources.Load ("SpeechBubble"), Vector3.zero, Quaternion.identity) as GameObject;
		SpeechBubble speechBubble = bubbleObject.GetComponent<SpeechBubble>();
		speechBubble.setWidth = HUDHelpers.speechWidth;
		speechBubble.setHeight = HUDHelpers.speechHeight;
		speechBubble.textToDisplay = new string[2];
		speechBubble.textToDisplay[0] = "looks like the bomb is orange this time. i wonder what that means?";
		speechBubble.textToDisplay[1] = "you've probably noticed by now that you can overlap color tiles. it's pretty handy!";
		speechBubble.dismissable = true;
		speechBubble.freezesGameOnDisplay = true;
		speechBubble.transform.position = Camera.main.WorldToScreenPoint(HUDHelpers.speechPosition);
		speechBubble.cursorInstructions = cursorInstructions;
	}
}


