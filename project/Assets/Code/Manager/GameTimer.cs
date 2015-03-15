using UnityEngine;
using System.Collections;

public class GameTimer : MonoBehaviour {

	//variables
	float gameTime = 0f; //In mills
	
	bool started = false;

	// Use this for initialization
	void Start () {
		BeatManager.checkBeats (gameTime);
	}

	void FixedUpdate() {
		
	}


	public void initTime(){
		gameTime = 0f;
		started = true;
	}
	
	// Update is called once per frame
	void Update () {
		if(started){
			gameTime += (Time.deltaTime * 1000);
			//Debug.Log (gameTime);
			BeatManager.checkBeats (gameTime);
		}
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
