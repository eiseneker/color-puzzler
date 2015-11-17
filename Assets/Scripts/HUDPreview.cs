using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class HUDPreview : MonoBehaviour {

	public int index;
	public int clusterIndex;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		if(GameController.clusters.Count >= 4){
			GameObject nextCluster = GameController.clusters[clusterIndex] as GameObject;
			if(nextCluster != null && index < nextCluster.GetComponent<Cluster>().tiles.Count){
				ArrayList tiles = nextCluster.GetComponent<Cluster>().tiles;
				Transform tile = (Transform)tiles[index];
				if(tile){
					if(tile.GetComponent<GridElement>().permanentWhite){
						GetComponent<Image>().color = Color.white;
					}else if(tile.GetComponent<GridElement>().permanentBlack){
						GetComponent<Image>().color = Color.black;
					}else{
						GetComponent<Image>().color = GridElement.colors[tile.GetComponent<GridElement>().permanentColorIndex];
					}
				}else{
					GetComponent<Image>().color = Color.black;
				}
			}
		}
	}
}
