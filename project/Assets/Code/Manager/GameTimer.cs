using UnityEngine;
using System.Collections;

public class GameTimer : MonoBehaviour {

	//variables
	float gameTime = 0f; //In mills
	
	bool started = false;
	
	public GameObject playerPrefab;
	public Vector3 playerStartPos = new Vector3(0, -10, 0);
	
	public float respawnCD = 1.5f; //cooldown on player respawn after death
	public float respawnTimer = 0; //how much time remains

	// Use this for initialization
	void Start () {
		BeatManager.checkBeats (gameTime);
	}

	void FixedUpdate() {
		
	}


	public void initTime(){
		gameTime = 0f;
		started = true;
		spawnPlayer();
	}
	
	// Update is called once per frame
	void Update () {
		if(started){
			gameTime += (Time.deltaTime * 1000);
			//Debug.Log (gameTime);
			BeatManager.checkBeats (gameTime);
			GameManager.spawnController.checkChanges(gameTime);
			
			if(respawnTimer != -99){
				respawnTimer -= Time.deltaTime;
				if(respawnTimer <=0 ){
					spawnPlayer();
				}
			}
		}
	}
	
	public void startRespawnTimer(){
		respawnTimer = respawnCD;
		GameManager.player = null;
	}

	void spawnPlayer() {
		GameObject playerObj = (GameObject)Instantiate(playerPrefab, playerStartPos, Quaternion.identity);
		GameManager.player = playerObj.GetComponent<PlayShip>();
		
		respawnTimer = -99f;
	}

	void pingManagers() {
		GameManager.ping ();
		BeatManager.ping ();
		GraphicsManager.ping ();
		StageManager.ping ();
		GDMethods.init ();
	}
}
