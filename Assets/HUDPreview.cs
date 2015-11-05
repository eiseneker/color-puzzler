using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class HUDPreview : MonoBehaviour {

	public int index;
	
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
		if(GameController.nextCluster != null && index < GameController.nextCluster.GetComponent<Cluster>().tiles.Count){
			ArrayList tiles = GameController.nextCluster.GetComponent<Cluster>().tiles;
			Transform tile = (Transform)tiles[index];
			if(tile){
				GetComponent<Image>().color = colors[tile.GetComponent<GridElement>().colorIndex];
			}else{
				GetComponent<Image>().color = Color.black;
			}
		}else{
			GetComponent<Image>().color = Color.black;
		}
	}
}
