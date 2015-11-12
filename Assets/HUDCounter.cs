using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class HUDCounter : MonoBehaviour {

	private Image underlay;
	private Image overlay;

	// Use this for initialization
	void Start () {
		underlay = transform.Find ("Underlay").GetComponent<Image>();
		overlay = transform.Find ("Overlay").GetComponent<Image>();
	}
	
	// Update is called once per frame
	void Update () {
		float underlayAmount = 1;
		float overlayAmount = 1;
		float measurableEnergy = 100;
		if(GameController.remainingEnergy < 100){
			measurableEnergy = GameController.remainingEnergy;
		}
		
		underlayAmount = measurableEnergy/100;
		overlayAmount = (measurableEnergy - GameController.energyRequirement)/100;
		
		underlay.fillAmount = underlayAmount;
		overlay.fillAmount = overlayAmount;
	}
}
