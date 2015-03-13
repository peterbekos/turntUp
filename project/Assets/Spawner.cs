using UnityEngine;
using System.Collections;

public class Spawner : MonoBehaviour {

	public GameObject sprite;
	
	private float timeToSpawn;

	// Use this for initialization
	void Start () {
		timeToSpawn = Random.Range(.5f,2f);
	}
	
	// Update is called once per frame
	void Update () {
		timeToSpawn -= Time.deltaTime;
		if(timeToSpawn <= 0){
			Instantiate(sprite, transform.position, transform.rotation);
			timeToSpawn = Random.Range(.5f,2f);
		}
	}
}
