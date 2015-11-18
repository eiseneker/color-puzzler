using UnityEngine;
using System.Collections;

public class Level12Controller : MonoBehaviour, ILevel {
	public void Invoke(){
		GameObject bubbleObject = Instantiate (Resources.Load ("SpeechBubble"), Vector3.zero, Quaternion.identity) as GameObject;
		SpeechBubble speechBubble = bubbleObject.GetComponent<SpeechBubble>();
		speechBubble.setWidth = HUDHelpers.speechWidth;
		speechBubble.setHeight = HUDHelpers.speechHeight;
		speechBubble.textToDisplay = new string[2];
		speechBubble.textToDisplay[0] = "well, this is different.\nnow there are targets with different colors...";
		speechBubble.textToDisplay[1] = "try using different colored tiles to connect the bombs to the targets!";
		speechBubble.dismissable = true;
		speechBubble.freezesGameOnDisplay = true;
		speechBubble.transform.position = Camera.main.WorldToScreenPoint(HUDHelpers.speechPosition);
	}
}