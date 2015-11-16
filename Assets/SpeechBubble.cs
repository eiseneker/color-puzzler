using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SpeechBubble : MonoBehaviour {

	[TextArea(3,10)]
	public string[] textToDisplay;
	public float maxTimeBetweenCharacters;
	public bool dismissable;
	public bool dismissesSelf = false;
	public float setWidth;
	public float setHeight;
	public bool freezesGameOnDisplay;
	
	private int textBubbleIndex;
	private float currentTimeBetweenCharacters;
	private Text text;
	private int textIndex;
	private RectTransform bubble;
	private float maxWidth;
	private float maxHeight;
	private float maxPostFinishDelay = 2f;
	private float currentPostFinishDelay;
	private GameObject arrow;
	
	public static bool inFreezeState;

	// Use this for initialization
	void Start () {
		text = transform.Find ("Text").GetComponent<Text>();
		text.text = "";
		bubble = GetComponent<RectTransform>();
		GameObject speech = GameObject.Find ("Speech");
		transform.parent = speech.transform;
		arrow = transform.Find ("Arrow").gameObject;
		inFreezeState = freezesGameOnDisplay;
	}
	
	// Update is called once per frame
	void Update () {
		currentTimeBetweenCharacters += Time.deltaTime;
		if(currentTimeBetweenCharacters >= maxTimeBetweenCharacters && textIndex < textToDisplay[textBubbleIndex].Length){
			text.text += textToDisplay[textBubbleIndex][textIndex];
			textIndex++;
			currentTimeBetweenCharacters = 0;
			if(setWidth == 0 || setHeight == 0){
				if(maxWidth < text.preferredWidth + 40) maxWidth = text.preferredWidth + 40;
				if(maxHeight < text.preferredHeight + 30) maxHeight = text.preferredHeight + 30;
			}else{
				maxWidth = setWidth;
				maxHeight = setHeight;
			}
			bubble.sizeDelta = new Vector2(maxWidth, maxHeight);
		}
		if(dismissesSelf && Finished ()){
			if(currentPostFinishDelay > maxPostFinishDelay){
				DismissMe();
			}else{
				currentPostFinishDelay += Time.deltaTime;		
			}
		}
		UpdateArrow();
	}
	
	void UpdateArrow(){
		arrow.SetActive (DoneWithPage () && !Finished ());
	}
	
	public void DismissMe(){
		if(dismissable && Finished()){
			if(freezesGameOnDisplay) inFreezeState = false;
			Destroy (gameObject);
		}
	}
	
	public void AdvanceMe(){
		if(Finished()){
			DismissMe ();
		}else if(DoneWithPage ()){
			ShowNextPage();
		}
	}
	
	private void ShowNextPage(){
		textIndex = 0;
		textBubbleIndex++;
		text.text = "";
	}
	
	private bool DoneWithPage(){
		return(textIndex >= textToDisplay[textBubbleIndex].Length);
	}
	
	private bool Finished(){
		return(textBubbleIndex == (textToDisplay.Length - 1) && DoneWithPage ());
	}
}
