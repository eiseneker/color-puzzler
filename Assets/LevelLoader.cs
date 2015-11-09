using UnityEngine;
using System.Collections;
using SimpleJSON;

public class LevelLoader : MonoBehaviour {

	public static Matrix matrix;

	public static void LoadLevel(string levelNumber){
		TextAsset levelText = Resources.Load ("Levels/" + levelNumber) as TextAsset;
		JSONNode json = JSON.Parse (levelText.text);
		
		GameController.randomizedTileCount = json["randomizedTileCount"].AsInt;
		GameController.targetCount = json["randomizedTargetCount"].AsInt;
		GameController.remainingEnergy = json["remainingEnergy"].AsFloat;
		
		if(json["grid"] != null){
			JSONArray elements = json["grid"].AsArray;
			for(int i = 0; i < elements.Count; i++) {
				JSONNode element = elements[i];
				int x = element["x"].AsInt;
				int y = element["y"].AsInt;
				Vector3 newPosition = matrix.PositionToCoordinate(x, y);
				GameObject newObject = Instantiate (Resources.Load (element["prefabName"]), newPosition, Quaternion.identity) as GameObject;
				if(element["colorIndex"] != null){
					newObject.GetComponent<GridElement>().colorIndex = element["colorIndex"].AsInt;
					newObject.GetComponent<GridElement>().colorSet = true;
				}
				if(element["agnostic"] != null){
					newObject.GetComponent<GridElement>().agnostic = element["agnostic"].AsBool;
				}
				matrix.InsertIntoMatrix (newObject);
			}
		}
		
		
	}

}
