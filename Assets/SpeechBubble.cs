using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SpeechBubble : MonoBehaviour {

	[TextArea(3,10)]
	public string textToDisplay;
	public float maxTimeBetweenCharacters;
	public bool dismissable;
	public bool dismissesSelf = false;
	
	private float currentTimeBetweenCharacters;
	private Text text;
	private int textIndex;
	private RectTransform bubble;
	private float maxWidth;
	private float maxHeight;
	private float maxPostFinishDelay = 2f;
	private float currentPostFinishDelay;

	// Use this for initialization
	void Start () {
		text = transform.Find ("Text").GetComponent<Text>();
		text.text = "";
		bubble = GetComponent<RectTransform>();
		GameObject speech = GameObject.Find ("Speech");
		transform.parent = speech.transform;
	}
	
	// Update is called once per frame
	void Update () {
		currentTimeBetweenCharacters += Time.deltaTime;
		if(currentTimeBetweenCharacters >= maxTimeBetweenCharacters && textIndex < textToDisplay.Length){
			text.text += textToDisplay[textIndex];
			textIndex++;
			currentTimeBetweenCharacters = 0;
			if(maxWidth < text.preferredWidth + 40) maxWidth = text.preferredWidth + 40;
			if(maxHeight < text.preferredHeight + 30) maxHeight = text.preferredHeight + 30;
			bubble.sizeDelta = new Vector2(maxWidth, maxHeight);
		}
		if(dismissesSelf && Finished ()){
			if(currentPostFinishDelay > maxPostFinishDelay){
				DismissMe();
			}else{
				currentPostFinishDelay += Time.deltaTime;		
			}
		}
	}
	
	public void DismissMe(){
		if(dismissable && Finished()){
			Destroy (gameObject);
		}
	}
	
	private bool Finished(){
		return(textIndex >= textToDisplay.Length);
	}
}
