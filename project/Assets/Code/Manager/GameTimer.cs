using UnityEngine;
using System.Collections;

public class GameTimer : MonoBehaviour {

	//variables
	public string levelName = "Colors";
	
	float gameTime = 0; //In mills
	
	bool started = false;
	
	public GameObject playerPrefab;
	public Vector3 playerStartPos = new Vector3(0, -10, 0);
	
	public float respawnCD = 1.5f; //cooldown on player respawn after death
	public float respawnTimer = 0; //how much time remains
	
	private Vector3 anchorPoint; //resting point of the camera
	private float anchorScale; //resting scale of the camera
	public float horizShakeTime = 0, vertShakeTime = 0, zoomShakeTime = 0; //stores duration of shake remaining
	private float timeForShake = .1f; //default shake duration
	private Vector2 perlinPoint = new Vector2(0, 0);

	private float realTimeStageStarted = 0;
	
	// Use this for initialization
	void Start () {
		anchorPoint = Camera.main.gameObject.transform.position;
		anchorScale = Camera.main.orthographicSize;
		GameManager.gameTimer = this;
		GameManager.score = 0;
		BeatManager.loadFile("Assets/Art/Music/" + levelName + ".mid");
		//Camera.main.GetComponent<AudioSource>().clip = AssetDatabase.FindAsset("Assets/Art/Music" + levelName + ".mp3");
		BeatManager.checkBeats (gameTime);
	}

	void FixedUpdate() {
		
	}

	//Key pressing stuff
	void OnGUI() {
		if (Input.GetKey (KeyCode.Z)) {
			float pressTime = Time.realtimeSinceStartup - realTimeStageStarted;

			Note nextNote = BeatManager.getNextNote();
			float nextNoteTime = (float) nextNote.startTime / 1000;

			Note thisNote = BeatManager.getCurrentNote();
			float thisNoteTime = (float) thisNote.startTime / 1000;

			Note lastNote = BeatManager.getLastNote();
			float lastNoteTime = (float) lastNote.startTime / 1000;

			Debug.Log("Acc = " + (lastNoteTime - pressTime) + " ; " + (thisNoteTime - pressTime) + " ; " + (nextNoteTime - pressTime));
		}
	}


	public void initTime(){
		gameTime = 0;
		started = true;
		spawnPlayer();
	}

	void detectKeyInput() {

	}
	
	// Update is called once per frame
	void Update () {
		if (started) {

			gameTime += (Time.deltaTime * 1000);
			//Debug.Log (gameTime);
			BeatManager.checkBeats (gameTime);
			GameManager.spawnController.checkChanges (gameTime / 1000);

			if (respawnTimer != -99) {
					respawnTimer -= Time.deltaTime;
					if (respawnTimer <= 0) {
							spawnPlayer ();
					}
			}

			//Handle shaking the camera
			if (horizShakeTime > 0 || vertShakeTime > 0) {
					perlinPoint += new Vector2 (Time.deltaTime * 10, Time.deltaTime * 10);
	
					float pX = .5f;
					if (horizShakeTime > 0) {
							pX = Mathf.PerlinNoise (perlinPoint.x, 0);
							horizShakeTime -= Time.deltaTime;
					}
					float pY = .5f;
					if (vertShakeTime > 0) {
							pY = Mathf.PerlinNoise (0, perlinPoint.y);
							vertShakeTime -= Time.deltaTime;
					}
	
	
					Camera.main.transform.position = anchorPoint + new Vector3 (pX - .5f, pY - .5f, 0);
	
			} else if (Camera.main.transform.position != anchorPoint) {
					Camera.main.transform.position = anchorPoint;
			}
		} else {
			realTimeStageStarted = Time.realtimeSinceStartup;
		}
	}
	
	public void startHorizShake(){
		horizShakeTime = timeForShake;
	}
	public void startVertShake(){
		vertShakeTime = timeForShake;
	}
	public void startZoomShake(){
		zoomShakeTime = timeForShake;
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
	
	public float getTime(){
		return gameTime;
	}

	void pingManagers() {
		GameManager.ping ();
		BeatManager.ping ();
		GraphicsManager.ping ();
		StageManager.ping ();
		GDMethods.init ();
	}
}
