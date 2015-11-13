using UnityEngine;
using System.Collections;

public class HUDDrawer : MonoBehaviour {

	private bool canMove = true;
	private bool movesUpNext = true;
	public static bool drawerOut;

	public void MoveDrawer(){
		if(canMove){
			if(movesUpNext){
				GetComponent<Animator>().Play ("MoveUp");
			}else{
				GetComponent<Animator>().Play ("MoveDown");
			}
			canMove = false;
		}
	}
	
	public void ResetMoveStatus(){
		drawerOut = movesUpNext;
		movesUpNext = !movesUpNext;
		canMove = true;
		
	}
}
