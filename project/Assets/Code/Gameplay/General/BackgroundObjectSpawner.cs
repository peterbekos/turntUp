using UnityEngine;
using System.Collections;

public class BackgroundObjectSpawner : MonoBehaviour {


	Vector2 cameraVisibleBounds; //stores half the camera's field of view, to place enemies outside it
	Vector3 cameraLocation; //stores the camera's (x, y) location, to use with cameraVisibleBounds
	
	public float timeBetweenSpawns = 1; //seconds in between spawning a new object
	private float timeSinceSpawn = 0;
	
	public GameObject[] toSpawn = new GameObject[3]; //possible objects to spawn in the background
	
	// Get the camera's bounds on creation
	void Start () {
		cameraVisibleBounds = new Vector2(Camera.main.orthographicSize * Screen.width / Screen.height, Camera.main.orthographicSize * Screen.height / Screen.width);
	}
	
	// Update is called once per frame
	void Update () {
		//if the camera moved, we need to find its new position
		cameraLocation = Camera.main.transform.position;
		
		//increase the time elapsed
		timeSinceSpawn += Time.deltaTime;
		
		//if it's time to spawn an enemy, spawn an enemy and reset the timer
		if(timeSinceSpawn > timeBetweenSpawns){
			spawnObject ();
			timeSinceSpawn = 0;
		}
	}
	
	//spawn a random enemy at a random point outside the camera
	void spawnObject(){
		Vector3 spawnPoint = getRandomPoint();
		GameObject spawn = (GameObject)GameObject.Instantiate(toSpawn[Random.Range(0, toSpawn.Length)], spawnPoint, gameObject.transform.rotation);
	}
	
	//gets a random point outside the camera
	Vector3 getRandomPoint(){
		Vector3 spawnPoint = new Vector3(0, 0, 0);
		
		//otherwise spawn on top
		spawnPoint.y = cameraLocation.y + cameraVisibleBounds.y + Random.Range (10f, 15f); //spawn unit between 5 and 10 units above camera
		spawnPoint.x = cameraLocation.x + Random.Range ( -cameraVisibleBounds.x - 1, cameraVisibleBounds.x + 1); //spawn unit somewhere within the camera's x boundaries
		
		return spawnPoint;
	}
}
