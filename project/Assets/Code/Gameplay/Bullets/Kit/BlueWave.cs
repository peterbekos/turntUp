using UnityEngine;
using System.Collections;

public class BlueWave : MonoBehaviour {

	public int speed = 10;
	public int timeToReturn = 1;
	private float timePassed = 0;
	
	private int stage = 0; //what should the bullet be doing? 0 = move forward, 1 = target player
	
	private GameObject player = null;
	
	private float zRot;
	
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		timePassed += Time.deltaTime;
	
		switch(stage){
		case 0: //stage 0, simply move normally
			if( timePassed > (float)timeToReturn ){
		 		stage = 1;
				player = GameObject.Find("Player Constructor");
			}
			break;
		case 1: //stage 1, return to player
			if(player != null){
				zRot = Mathf.Atan2(player.transform.position.y - transform.position.y, player.transform.position.x - transform.position.x) * Mathf.Rad2Deg;
				transform.rotation = Quaternion.Euler(0f, 0f, zRot - 90);
			}
			break;
		default:
			Debug.LogError("BlueWave in invalid state! Instance:" + this.GetInstanceID() + " value: " + stage);
			break;
		}
		
		rigidbody2D.velocity = transform.up * speed;
	}
	
	void OnTriggerEnter2D(Collider2D coll){
		if(stage == 1 && player != null && coll.gameObject == player)
		{
			Destroy(gameObject);
		}
	}
}
