using UnityEngine;
using System.Collections;

public class GameTimer : MonoBehaviour {

	//variables
	public string levelName = "Colors";
	
	float gameTime = -5000; //In mills
	
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
	public float maxZoom = 2.5f; //max change to perspective view
	private bool musicStarted = false;
	
	// Use this for initialization
	void Start () {
		anchorPoint = Camera.main.gameObject.transform.position;
		anchorScale = Camera.main.orthographicSize;
		GameManager.gameTimer = this;
		GameManager.score = 0;
		BeatManager.loadFile("Assets/Art/Music/" + levelName + ".mid");
		GameManager.scoretable.displayScores();
		GameManager.scoretable.enable(false);
		//Camera.main.GetComponent<AudioSource>().clip = AssetDatabase.FindAsset("Assets/Art/Music" + levelName + ".mp3");
		BeatManager.checkBeats (gameTime);
		Destroy(GameManager.menuMusic);
	}

	void FixedUpdate() {
		
	}


	public void stop(){
		started = false;
	}
	//Key pressing stuff
	void OnGUI() {
		float acc = 0; 
		if (Input.GetKey (KeyCode.Z)) {
			acc = checkAccuracy(GD.KICK);
		} else if (Input.GetKey (KeyCode.X)) {
			acc = checkAccuracy(GD.BASS);
		} else if (Input.GetKey (KeyCode.C)) {
			acc = checkAccuracy(GD.MELODY);
		} else if (Input.GetKey (KeyCode.V)) {
			acc = checkAccuracy(GD.SNARE);
		}
		if (acc > 0) {
			Debug.Log (acc);
		}

	}

	private float mapValue(float input, float inMin, float inMax, float outMin, float outMax) {
		float inFloored = input - inMin;
		float diffRatio = (outMin - outMax) / (inMin - inMax);
		float result = inFloored * diffRatio + outMin;
		return result;
	}

	float checkAccuracy(GD input) {
		float pressTime = Time.realtimeSinceStartup - GameManager.realTimeStageStarted;
		
		Note thisNote = BeatManager.getCurrentNote(input);

		//if (GDMethods.getBeatType (thisNote.instrumentName) == ) {

		if (thisNote == null) {
			return 0;
		}



		float thisNoteTime = (float) thisNote.startTime / 1000;

		float offset = (thisNoteTime - pressTime - (songStartBuffer / 1000));

		return mapValue(offset, 0.4f, 0, 0, 100);

	}

	public float songStartBuffer = -2000;
	public void initTime(){
		gameTime = songStartBuffer;
		started = true;
		spawnPlayer();
	}

	void detectKeyInput() {

	}
	
	// Update is called once per frame
	void Update () {
	
		if(Input.GetKeyDown(KeyCode.Escape)){
			Application.LoadLevel("SplashScreen");
		}
	
		if (respawnTimer != -99) {
			respawnTimer -= Time.deltaTime;
			if (respawnTimer <= 0) {
				spawnPlayer ();
			}
		}
		
		if (started) {

			gameTime += (Time.deltaTime * 1000);
			if(musicStarted == false && gameTime > 0){
				musicStarted = true;
				AudioSource music = Camera.main.GetComponent<AudioSource>();
				music.time = gameTime/1000;
				music.Play ();
			}
			//Debug.Log (gameTime);
			BeatManager.checkBeats (gameTime);
            if(!object.ReferenceEquals(GameManager.spawnController, null))
			    GameManager.spawnController.checkChanges (gameTime / 1000);

			//Handle shaking the camera
			if (horizShakeTime > 0 || vertShakeTime > 0 || zoomShakeTime > 0) {
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
					float zoom = 0f;
					if (zoomShakeTime > 0){
						zoom = maxZoom * zoomShakeTime / timeForShake;
						zoomShakeTime -= Time.deltaTime;
					}
	
					Camera.main.transform.position = anchorPoint + new Vector3 (pX - .5f, pY - .5f, 0);
					Camera.main.orthographicSize = 20 - zoom;
	
			} else if (Camera.main.transform.position != anchorPoint) {
					Camera.main.transform.position = anchorPoint;
			}
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
