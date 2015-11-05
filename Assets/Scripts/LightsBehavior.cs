using UnityEngine;
using System.Collections;

public class LightsBehavior : MonoBehaviour {
	public bool isOn = false;
	
	private Transform cachedTransform;
	private Renderer cachedRenderer;
	
	void Awake(){
		cachedRenderer = GetComponent<MeshRenderer>();
		
		SwitchLight ();
	}
	
	void OnEnable(){
		LightsManager.onHitSwitch += OnHitSwitch;
	}
	
	void OnDisable(){
		LightsManager.onHitSwitch -= OnHitSwitch;
	}
	
	void OnHitSwitch(Vector3 switchPosition, GFGrid theGrid){
		Vector3 myPosition = theGrid.WorldToGrid(transform.position);
		
		bool isAdjacent = Mathf.Abs (myPosition.x - switchPosition.x) <= 1.1f && Mathf.Abs (myPosition.y - switchPosition.y) <= 1.1f;
		
		bool isDiagonal = Mathf.Abs (myPosition.x - switchPosition.x) > 0.1f && Mathf.Abs (myPosition.y - switchPosition.y) > 0.1f && isAdjacent;
		
		if(isAdjacent && !isDiagonal){
			isOn = !isOn;
		}
		
		SwitchLight ();
	}
	
	void SwitchLight(){
		if(isOn){
			cachedRenderer.material.SetColor ("_Color", Color.red);
		}else{
			cachedRenderer.material.SetColor ("_Color", Color.white);
		}
	}
	
	void OnMouseUpAsButton(){
		LightsManager.SendSignal(transform.position);
	}
	
}
