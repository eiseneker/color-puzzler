using UnityEngine;
using System.Collections;
using SimpleJSON;

public class LevelLoader : MonoBehaviour {

	public static void LoadLevel(string levelNumber){
		TextAsset levelText = Resources.Load ("Levels/" + levelNumber) as TextAsset;
		JSONNode json = JSON.Parse (levelText.text);
		
		GameController.randomizedTileCount = json["randomizedTileCount"].AsInt;
		GameController.targetCount = json["randomizedTargetCount"].AsInt;
		GameController.remainingEnergy = json["remainingEnergy"].AsFloat;
		
	}

}
