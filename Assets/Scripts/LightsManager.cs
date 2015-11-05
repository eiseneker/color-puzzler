using UnityEngine;
using System.Collections;

public class LightsManager : MonoBehaviour {

	public delegate void SwitchingHandler(Vector3 switchCoordinate, GFGrid theGrid);
	public static event SwitchingHandler onHitSwitch;
	
	private static GFGrid cachedGrid;
	
	void Awake(){
		cachedGrid = GetComponent<GFRectGrid>();
	}
	
	public static void SendSignal(Vector3 theSwitch){
		if(cachedGrid != null && onHitSwitch != null){
			onHitSwitch(cachedGrid.WorldToGrid(theSwitch), cachedGrid);
		}
	}
}