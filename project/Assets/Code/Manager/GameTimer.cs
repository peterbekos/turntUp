using UnityEngine;
using System.Collections;

public class GameTimer : MonoBehaviour {

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void spawnPlayer() {
		//TODO - write code that can spawn a playable ship
		//TODO - pass instance of the player ship to GameManager
	}

	void pingManagers() {
		GameManager.ping ();
		BeatManager.ping ();
		GraphicsManager.ping ();
		StageManager.ping ();
	}
}
