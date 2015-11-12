using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class HUDPreview : MonoBehaviour {

	public int index;
	public int clusterIndex;
	
	private Color[] colors = {
		Color.red,
		new Color(1, .5f, 0),
		Color.yellow,
		Color.green,
		Color.blue,
		new Color(1, 0, 1)
	};

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
						GetComponent<Image>().color = colors[tile.GetComponent<GridElement>().permanentColorIndex];
					}
				}else{
					GetComponent<Image>().color = Color.black;
				}
			}
		}
	}
}
