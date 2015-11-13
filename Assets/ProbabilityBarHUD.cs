using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ProbabilityBarHUD : MonoBehaviour {

	public int colorIndex;
	private Image image;
	public bool white;
	public bool black;

	void Start(){
		image = GetComponent<Image>();
	}

	// Update is called once per frame
	void Update () {
		if(!white && !black){
			image.fillAmount = Cluster.colorProbability[colorIndex];
			image.color = GridElement.colors[colorIndex];
		}else if(white){
			image.fillAmount = Cluster.whiteProbability;
			image.color = Color.white;
		}else if(black){
			image.fillAmount = Cluster.blackProbability;
			image.color = Color.black;
		}
	}
}
