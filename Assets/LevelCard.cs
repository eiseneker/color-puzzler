using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class LevelCard : MonoBehaviour {
	
	public static bool finished;
	private static LevelCard instance;
	private bool readyToFadeOut;
	public static string levelNumber;
	public static string levelName;
	
	// Use this for initialization
	void Start () {
		finished = false;
		instance = this;
		levelNumber = "";
		levelName = "";
	}
	
	void Update(){
		if(readyToFadeOut){
			FadeOut ();
		}
	}
	
	public static void SetLevel(string inputLevelNumber, string inputLevelName){
		levelNumber = inputLevelNumber;
		levelName = inputLevelName;
		instance.transform.Find ("Panel").Find ("LevelNumber").GetComponent<Text>().text = "Level " + levelNumber;
		instance.transform.Find ("Panel").Find ("LevelName").GetComponent<Text>().text = levelName;
	}
	
	public static void FadeOut(){
		instance.transform.Find("Panel").GetComponent<Image>().fillOrigin = (int)Image.OriginHorizontal.Right;
		instance.GetComponent<Animator>().Play("LevelCardFadeOut");
	}
	
	public static void FadeIn(){
		instance.GetComponent<Animator>().Play("LevelCardFadeIn");
	}
	
	public void MarkFinished(){
		finished = true;
	}
	
	public void SetReadyToFadeOut(){
		readyToFadeOut = true;
	}
}