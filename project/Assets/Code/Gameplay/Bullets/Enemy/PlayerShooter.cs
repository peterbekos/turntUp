using UnityEngine;
using System.Collections;

public class PlayerShooter : MonoBehaviour {

	public GameObject player;
	float zRot; //z rotation to face player
	
	public float timeBetweenShots = 1;
	float timeSinceShot = 0;

	// Use this for initialization
	void Start () {
		player = GameObject.FindGameObjectWithTag("Player");
	}
	
	// Update is called once per frame
	void Update () {
		timeSinceShot += Time.deltaTime;
	
		if(player != null){
			zRot = Mathf.Atan2(player.transform.position.y - transform.position.y, player.transform.position.x - transform.position.x) * Mathf.Rad2Deg;
			transform.rotation = Quaternion.Euler(0f, 0f, zRot - 90);
		}
		
		if(timeSinceShot >= timeBetweenShots){
			gameObject.SendMessage("fireBullet");
			timeSinceShot = 0;
		}
	}
}
