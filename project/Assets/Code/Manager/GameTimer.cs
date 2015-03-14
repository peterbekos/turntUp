using UnityEngine;
using System.Collections;

public class GameTimer : MonoBehaviour {

	//variables
	float gameTime = 0f; //In mills

	// Use this for initialization
	void Start () {

	}

	void FixedUpdate() {
		gameTime = gameTime + (Time.fixedDeltaTime * 1000);
		//Debug.Log (gameTime);
		BeatManager.checkBeats (gameTime);
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
		GDMethods.init ();
	}
}
