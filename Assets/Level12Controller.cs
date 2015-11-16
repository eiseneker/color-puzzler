using UnityEngine;
using System.Collections;

public class Level12Controller : MonoBehaviour, ILevel {
	public void Invoke(){
		GameObject bubbleObject = Instantiate (Resources.Load ("SpeechBubble"), Vector3.zero, Quaternion.identity) as GameObject;
		SpeechBubble speechBubble = bubbleObject.GetComponent<SpeechBubble>();
		speechBubble.setWidth = 400;
		speechBubble.setHeight = 120;
		speechBubble.textToDisplay = new string[3];
		speechBubble.textToDisplay[0] = "this is different. now there are targets with different colors";
		speechBubble.textToDisplay[1] = "normally, targets can only be destroyed using tiles of the same color";
		speechBubble.textToDisplay[2] = "try destroying all targets using red and green tiles";
		speechBubble.dismissable = true;
		speechBubble.freezesGameOnDisplay = true;
		speechBubble.transform.position = Camera.main.WorldToScreenPoint(new Vector3(-2, -2, 0));
	}
}