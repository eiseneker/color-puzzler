using UnityEngine;
using System.Collections;

public class Level12Controller : MonoBehaviour, ILevel {
	public void Invoke(){
		SpeechBubble speechBubble = SpeechBubble.mainBubble;
		speechBubble.textToDisplay = new string[2];
		speechBubble.textToDisplay[0] = "well, this is different. now there are targets with different colors...";
		speechBubble.textToDisplay[1] = "try using different colored tiles to connect the bombs to the targets!";
		speechBubble.dismissable = true;
		speechBubble.freezesGameOnDisplay = true;
		speechBubble.Activate ();
	}
}