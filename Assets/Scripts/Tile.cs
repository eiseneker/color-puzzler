using UnityEngine;
using System.Collections;

public class Tile : MonoBehaviour {
	private Color tileColor;
	private int colorIndex;
	
	public static float[] colorProbability = new float[] { 1.666f, 1.666f, 1.666f, 1.666f, 1.666f, 1.666f };
	public static float whiteProbability = 0;
	public static float blackProbability = 0;
	
	public Sprite[] sprites;
	public Sprite spriteDisabled;
	public Sprite spriteWhite;
	public Sprite spriteBlack;
	private SpriteRenderer spriteOverlay;
	
	void Start(){
		if(!GetComponent<GridElement>().colorSet){
			float random = Random.value;
			if(random < whiteProbability){
				GetComponent<GridElement>().white = true;
			}
			if(random >= whiteProbability && random < (whiteProbability + blackProbability)){
				GetComponent<GridElement>().black = true;
			}
			GetComponent<GridElement>().colorIndex = GridElement.RandomizedColorIndex(colorProbability);
			GetComponent<GridElement>().colorSet = true;
		}
		
		if(GetComponent<GridElement>().insertedIntoMatrix && GameObject.Find ("Tiles").transform != transform.parent){
			transform.parent = GameObject.Find ("Tiles").transform;
		}
	}
	
	public void DestroyWithDelay(){
		Invoke ("DestroyMe", .5f);
	}
	
	private void DestroyMe(){
		Destroy (gameObject);
	}

	public void ResetTransitionState(){
		GetComponent<Animator>().SetBool ("transitioning", false);
	}
	
	public void EnterTransitionState(){
		GetComponent<Animator>().SetBool ("transitioning", true);
	}
	
	public void UpdateSprite(){
		GridElement gridElement = GetComponent<GridElement>();
		if(SpriteOverlay () != null){
			if(gridElement.Disabled ()){
				SpriteOverlay().sprite = spriteDisabled;
				SpriteOverlay ().color = Color.black;
			}else if(gridElement.white){
				SpriteOverlay().sprite = spriteWhite;
				SpriteOverlay ().color = Color.white;
			}else if(gridElement.black){
				SpriteOverlay().sprite = spriteBlack;
				SpriteOverlay ().color = new Color(.3f, .3f, .3f);
			}else{
				SpriteOverlay().sprite = sprites[GetComponent<GridElement>().colorIndex];
				SpriteOverlay ().color = Color.black;
			}
		}
	}
	
	private SpriteRenderer SpriteOverlay(){
		if(spriteOverlay == null){
			if(transform.Find ("Overlay")){
				spriteOverlay = transform.Find ("Overlay").GetComponent<SpriteRenderer>();
			}
		}
		return(spriteOverlay);
	}
	
}
