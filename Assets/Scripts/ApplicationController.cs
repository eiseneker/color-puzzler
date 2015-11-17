using UnityEngine;
using System.Collections;

public class ApplicationController : MonoBehaviour {

	public static string levelToLoad = "1-1";
	
	private ApplicationController instance;
	
	void Start(){
		instance = this;
	}
	
	public void GoToLevel(string level){
		levelToLoad = level;
		Application.LoadLevel("Game");
	}
	
	public void GoToLevelSelect(){
		Application.LoadLevel("LevelSelect");
	}
	
}
