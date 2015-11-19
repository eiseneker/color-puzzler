using UnityEngine;
using System.Collections;

public class ApplicationController : MonoBehaviour {

	public static string levelToLoad = "1-1";
	
	public static ApplicationController instance;
	
	void Start(){
		instance = this;
	}
	
	public void GoToLevel(string level){
		levelToLoad = level;
		Transition.FadeOut ();
		
		Invoke ("LoadGame", 3);
	}
	
	public void GoToLevelSelect(){
		Transition.FadeOut ();
		
		Invoke ("LoadLevelSelect", 3);
	}
	
	private void LoadGame(){
		Application.LoadLevel("Game");
	}
	
	private void LoadLevelSelect(){
		Application.LoadLevel("LevelSelect");
	}
	
}
