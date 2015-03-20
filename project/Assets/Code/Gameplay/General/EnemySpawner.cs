using UnityEngine;
using System.Collections;

public class EnemySpawner : MonoBehaviour {

	Vector2 cameraVisibleBounds; //stores half the camera's field of view, to place enemies outside it
	Vector3 cameraLocation; //stores the camera's (x, y) location, to use with cameraVisibleBounds
	
	public float timeBetweenSpawns = 1; //seconds in between spawning a new enemy
	private float timeSinceSpawn = 0;
	
	public GameObject[] enemiesToSpawn = new GameObject[5]; //possible enemy types to spawn in the level
	
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
			spawnEnemy ();
			timeSinceSpawn = 0;
		}
	}
	
	//spawn a random enemy at a random point outside the camera
	public void spawnEnemy(){
		Vector3 spawnPoint = getRandomPoint();
		GameObject spawn = (GameObject)GameObject.Instantiate(enemiesToSpawn[Random.Range(0, enemiesToSpawn.Length)], spawnPoint, gameObject.transform.rotation);
	}
	
	//spawn a random enemy at a given point outside the camera
	public void spawnEnemy(Vector3 spawnPoint){
		GameObject spawn = (GameObject)GameObject.Instantiate(enemiesToSpawn[Random.Range(0, enemiesToSpawn.Length)], spawnPoint, gameObject.transform.rotation);
	}
	
	//spawn a given enemy at a random point outside the camera
	public void spawnEnemy(GameObject enemy){
		Vector3 spawnPoint = getRandomPoint();
		GameObject spawn = (GameObject)GameObject.Instantiate(enemy, spawnPoint, gameObject.transform.rotation);
	}
	
	//spawn a given enemy at a given point outside the camera
	public void spawnEnemy(GameObject enemy, Vector3 spawnPoint){
		GameObject spawn = (GameObject)GameObject.Instantiate(enemy, spawnPoint, gameObject.transform.rotation);
	}
	
	//spawn a given enemy in the array at a random point outside the camera
	public void spawnEnemy(int enemy){
		Vector3 spawnPoint = getRandomPoint();
		GameObject spawn = (GameObject)GameObject.Instantiate(enemiesToSpawn[enemy], spawnPoint, gameObject.transform.rotation);
	}
	
	//spawn a given enemy in the array at a given point outside the camera
	public void spawnEnemy(int enemy, Vector3 spawnPoint){
		GameObject spawn = (GameObject)GameObject.Instantiate(enemiesToSpawn[enemy], spawnPoint, gameObject.transform.rotation);
	}
	
	//gets a random point outside the camera
	Vector3 getRandomPoint(){
		Vector3 spawnPoint = new Vector3(0, 0, 0);
		
		//determine whether to spawn on side or on top
		if(Random.Range (0F,1F) > .5) //50% chance to spawn on side
		{
			spawnPoint.y = cameraLocation.y + Random.Range (0, cameraVisibleBounds.y + 10);
			if(Random.Range (0f, 1f) > .5)
			{ //50% chance to spawn on left
				spawnPoint.x = cameraLocation.x - cameraVisibleBounds.x - Random.Range (10f, 15f); // spawn unit between 5 and 10 units to left of camera
			}
			else
			{ //otherwise spawn on right
				spawnPoint.x = cameraLocation.x + cameraVisibleBounds.x + Random.Range (10f, 15f); // spawn unit between 5 and 10 units to left of camera
			}
		}
		else{ //otherwise spawn on top
			spawnPoint.y = cameraLocation.y + cameraVisibleBounds.y + Random.Range (10f, 15f); //spawn unit between 5 and 10 units above camera
			spawnPoint.x = cameraLocation.x + Random.Range ( -cameraVisibleBounds.x, cameraVisibleBounds.x); //spawn unit somewhere within the camera's x boundaries
		}
		
		return spawnPoint;
	}
	
}
