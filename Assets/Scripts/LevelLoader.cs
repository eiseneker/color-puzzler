using UnityEngine;
using System.Collections;
using SimpleJSON;

public class LevelLoader : MonoBehaviour {

	public static Matrix matrix;

	public static void LoadLevel(string levelNumber){
		TextAsset levelText = Resources.Load ("Levels/" + levelNumber) as TextAsset;
		JSONNode json = JSON.Parse (levelText.text);
		
		GameController.randomizedTileCount = json["randomizedTileCount"].AsInt;
		GameController.randomizedTargetCount = json["randomizedTargetCount"].AsInt;
		GameController.remainingEnergy = json["remainingEnergy"].AsFloat;
		
		if(json["tileColorFrequency"] != null){
			Tile.colorProbability = GetProbabilityArray(json["tileColorFrequency"].AsArray);
		}
		
		if(json["targetColorFrequency"] != null){
			Target.colorProbability = GetProbabilityArray(json["targetColorFrequency"].AsArray);
		}
		
		if(json["bombColorFrequency"] != null){
			Bomb.colorProbability = GetProbabilityArray(json["bombColorFrequency"].AsArray);
		}
		
		if(json["clusterColorFrequency"] != null){
			Cluster.colorProbability = GetProbabilityArray(json["clusterColorFrequency"].AsArray);
		}
		
		if(json["clusterColorVariance"] != null){
			Cluster.colorVariance = json["clusterColorVariance"].AsFloat;
		}
		
		if(json["tileWhiteFrequency"] != null){
			Tile.whiteProbability = json["tileWhiteFrequency"].AsFloat;
		}
		
		if(json["clusterWhiteFrequency"] != null){
			Cluster.whiteProbability = json["clusterWhiteFrequency"].AsFloat;
		}
		
		if(json["tileBlackFrequency"] != null){
			Tile.blackProbability = json["tileBlackFrequency"].AsFloat;
		}
		
		if(json["clusterBlackFrequency"] != null){
			Cluster.blackProbability = json["clusterBlackFrequency"].AsFloat;
		}
		
		if(json["energyRequirementFactor"] != null){
			GameController.energyRequirementFactor = json["energyRequirementFactor"].AsFloat;
		}
		
		if(json["grid"] != null){
			JSONArray elements = json["grid"].AsArray;
			for(int i = 0; i < elements.Count; i++) {
				JSONNode element = elements[i];
				int x = element["x"].AsInt;
				int y = element["y"].AsInt;
				Vector3 newPosition = matrix.PositionToCoordinate(x, y);
				GameObject newObject = Instantiate (Resources.Load (element["prefabName"]), newPosition, Quaternion.identity) as GameObject;
				if(element["colorIndex"] != null){
					print (element["prefabName"] + "... " + newObject);
					print (newObject.GetComponent<GridElement>());
					newObject.GetComponent<GridElement>().colorIndex = element["colorIndex"].AsInt;
					newObject.GetComponent<GridElement>().colorSet = true;
				}
				if(element["white"] != null){
					newObject.GetComponent<GridElement>().white = element["white"].AsBool;
				}
				if(element["survivesExplosion"] != null){
					newObject.GetComponent<GridElement>().survivesExplosion = element["survivesExplosion"].AsBool;
				}
				if(element["friendlyName"] != null){
					newObject.GetComponent<GridElement>().friendlyName = element["friendlyName"];
					GameController.namedElements.Add (newObject.GetComponent<GridElement>());
					print ("added to friendly list");
				}
				matrix.InsertIntoMatrix (newObject);
			}
		}
		
		GameObject levelController = Instantiate(Resources.Load ("Levels/Level"+levelNumber+"Controller"), Vector3.zero, Quaternion.identity) as GameObject;
		levelController.transform.parent = GameObject.Find ("LevelController").transform;
	}
	
	private static float[] GetProbabilityArray(JSONArray jsonArray){
		return(new float[] {
			jsonArray[0].AsFloat,
			jsonArray[1].AsFloat,
			jsonArray[2].AsFloat,
			jsonArray[3].AsFloat,
			jsonArray[4].AsFloat,
			jsonArray[5].AsFloat
		});
	}

}
