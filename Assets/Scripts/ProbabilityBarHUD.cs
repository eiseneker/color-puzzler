using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ProbabilityBarHUD : MonoBehaviour {

	public int colorIndex;
	private Image image;
	public bool white;
	public bool black;
	private static float probabilityMax;
	private static int barCount;

	void Start(){
		if(barCount >= 8){
			barCount = 0;
			probabilityMax = 0;
		}
		image = GetComponent<Image>();
		if(probabilityMax < Cluster.colorProbability[colorIndex]){
			probabilityMax = Cluster.colorProbability[colorIndex];
		}
		barCount++;
	}

	// Update is called once per frame
	void Update () {
		if(!white && !black){
			image.fillAmount = Cluster.colorProbability[colorIndex]/probabilityMax;
			image.color = GridElement.colors[colorIndex];
		}else if(white){
			image.fillAmount = Cluster.whiteProbability/probabilityMax;
			image.color = Color.white;
		}else if(black){
			image.fillAmount = Cluster.blackProbability/probabilityMax;
			image.color = Color.black;
		}
	}
}
