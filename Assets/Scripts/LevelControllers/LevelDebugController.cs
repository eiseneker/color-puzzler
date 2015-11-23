using UnityEngine;
using System.Collections;

public class LevelDebugController : LevelController {
	public override void Invoke(){
		SpeechBubble.inFreezeState = false;
	}
}


