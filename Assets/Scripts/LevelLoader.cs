using UnityEngine;
using System.Collections;
using SimpleJSON;

public class LevelLoader : MonoBehaviour {

	public static ArrayList levels = new ArrayList();

	public static Matrix matrix;
	
	void Start(){
		if(levels.Count == 0){
			levels.Add ("1-1");
			levels.Add ("1-2");
			levels.Add ("1-3");
			levels.Add ("1-4");
			levels.Add ("1-5");
			levels.Add ("1-6");
			levels.Add ("1-7");
			levels.Add ("1-8");
			levels.Add ("1-9");
			levels.Add ("1-10");
			levels.Add ("2-1");
			levels.Add ("2-2");
			levels.Add ("2-3");
			levels.Add ("2-4");
			levels.Add ("2-5");
			levels.Add ("2-6");
			levels.Add ("2-7");
			levels.Add ("2-8");
		}
	}
	
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
		}else{
			throw new UnityException("No manifest entry for clusterColorFrequency found");
		}
		
		print ("cluster probability values set");
		
		if(json["clusterForcedPattern"] != null){
			JSONArray pattern = json["clusterForcedPattern"].AsArray;
			Cluster.forcedPattern = new int[] { pattern[0].AsInt, pattern[1].AsInt, pattern[2].AsInt, pattern[3].AsInt };
		}else{
			Cluster.forcedPattern = null;
		}
		
		if(json["clusterColorStability"] != null){
			Cluster.colorStability = json["clusterColorStability"].AsFloat;
		}else{
			Cluster.colorStability = 1;
		}
		
		if(json["tileWhiteFrequency"] != null){
			Tile.whiteProbability = json["tileWhiteFrequency"].AsFloat;
		}else{
			Tile.whiteProbability = 0;
		}
		
		if(json["clusterWhiteFrequency"] != null){
			Cluster.whiteProbability = json["clusterWhiteFrequency"].AsFloat;
		}else{
			Cluster.whiteProbability = 0;
		}
		
		if(json["tileBlackFrequency"] != null){
			Tile.blackProbability = json["tileBlackFrequency"].AsFloat;
		}else{
			Tile.blackProbability = 0;
		}
		
		if(json["clusterBlackFrequency"] != null){
			Cluster.blackProbability = json["clusterBlackFrequency"].AsFloat;
		}else{
			Cluster.blackProbability = 0;
		}
		
		if(json["energyRequirementFactor"] != null){
			GameController.energyRequirementFactor = json["energyRequirementFactor"].AsFloat;
		}else{
			GameController.energyRequirementFactor = 1;
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
					newObject.GetComponent<GridElement>().colorIndex = element["colorIndex"].AsInt;
					newObject.GetComponent<GridElement>().colorSet = true;
				}
				if(element["white"] != null){
					newObject.GetComponent<GridElement>().white = element["white"].AsBool;
				}
				if(element["black"] != null){
					newObject.GetComponent<GridElement>().black = element["black"].AsBool;
				}
				if(element["brown"] != null){
					newObject.GetComponent<GridElement>().brown = element["brown"].AsBool;
				}
				if(element["gray"] != null){
					newObject.GetComponent<GridElement>().gray = element["gray"].AsBool;
				}
				if(element["survivesExplosion"] != null){
					newObject.GetComponent<GridElement>().survivesExplosion = element["survivesExplosion"].AsBool;
				}
				if(element["friendlyName"] != null){
					newObject.GetComponent<GridElement>().friendlyName = element["friendlyName"];
					GameController.namedElements.Add (newObject.GetComponent<GridElement>());
				}
				matrix.InsertIntoMatrix (newObject);
			}
		}
		
		GameObject levelController = Instantiate(Resources.Load ("Levels/Level"+levelNumber+"Controller"), Vector3.zero, Quaternion.identity) as GameObject;
		levelController.transform.parent = GameObject.Find ("LevelController").transform;
		
		LevelCard.SetLevel(levelNumber, json["name"]);
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
