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
	public string[][] cursorInstructions;
	public bool isMainBubble;
	
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
	private bool cursorStale;
	private ArrayList cursors;
	
	public static SpeechBubble mainBubble;
	
	public static bool inFreezeState;

	// Use this for initialization
	void Start () {
		if(isMainBubble) {
			mainBubble = this;
		}
		text = transform.Find ("Text").GetComponent<Text>();
		text.text = "";
		bubble = GetComponent<RectTransform>();
		GameObject speech = GameObject.Find ("Speech");
		transform.parent = speech.transform;
		arrow = transform.Find ("Arrow").gameObject;
		inFreezeState = freezesGameOnDisplay;
		cursors = new ArrayList();
		if(isMainBubble) gameObject.SetActive (false);
	}
	
	public void Activate(){
		inFreezeState = freezesGameOnDisplay;
		gameObject.SetActive (true);
	}
	
	// Update is called once per frame
	void Update () {
		currentTimeBetweenCharacters += Time.deltaTime;
		if(currentTimeBetweenCharacters >= maxTimeBetweenCharacters && textIndex < textToDisplay[textBubbleIndex].Length){
			text.text += textToDisplay[textBubbleIndex][textIndex];
			textIndex++;
			currentTimeBetweenCharacters = 0;
			if(!mainBubble){
				if(setWidth == 0 || setHeight == 0){
					if(maxWidth < text.preferredWidth + 40) maxWidth = text.preferredWidth + 40;
					if(maxHeight < text.preferredHeight + 30) maxHeight = text.preferredHeight + 30;
				}else{
					maxWidth = setWidth;
					maxHeight = setHeight;
				}
				bubble.sizeDelta = new Vector2(maxWidth, maxHeight);
			}
		}
		if(!cursorStale && cursorInstructions != null && cursorInstructions.Length > textBubbleIndex){
			if(cursorInstructions[textBubbleIndex] != null){
				foreach(string friendlyName in cursorInstructions[textBubbleIndex]){
					GameObject cursorObject = Instantiate (Resources.Load ("Cursor"), Vector3.zero, Quaternion.identity) as GameObject;
					Cursor cursor = cursorObject.GetComponent<Cursor>();
					GridElement foundElement = GameController.GetElementByName(friendlyName);
					if(foundElement == null) throw new UnassignedReferenceException("no friendly object found for " + friendlyName);
					cursor.transform.position = Camera.main.WorldToScreenPoint(GameController.GetElementByName(friendlyName).transform.position);
					cursors.Add (cursor.gameObject);
				}
			}
			cursorStale = true;
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
			textIndex = 0;
			textBubbleIndex = 0;
			text.text = "";
			foreach(GameObject cursor in cursors){
				Destroy (cursor);
			}
			if(freezesGameOnDisplay) inFreezeState = false;
			if(isMainBubble){
				gameObject.SetActive(false);
			}else{
				Destroy (gameObject);
			}
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
		cursorStale = false;
		foreach(GameObject cursor in cursors){
			Destroy (cursor);
		}
		cursors.Clear ();
	}
	
	private bool DoneWithPage(){
		return(textIndex >= textToDisplay[textBubbleIndex].Length);
	}
	
	private bool Finished(){
		return(textBubbleIndex == (textToDisplay.Length - 1) && DoneWithPage ());
	}
}
