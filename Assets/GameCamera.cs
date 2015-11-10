using UnityEngine;
using System.Collections;

public class GameCamera : MonoBehaviour {
	public GameObject gameOver;
	
	public void ShowGameOver(){
		gameOver.SetActive(true);
	}
}
