using UnityEngine;
using System.Collections;

public class RedBomb : MonoBehaviour {

	public int speed = 10;
	public float timeToStage2 = .5f;
	private float timePassed = 0;
	
	public float timeToSpawnBullets = 0.25f;
	
	private int stage = 0; //what should the bullet be doing? 0 = move forward, 1 = spawn extra bullets
	
	private float zRot;
	
	// Update is called once per frame
	void Update () {
		timePassed += Time.deltaTime;
		
		switch(stage){
		case 0: //stage 0, simply move normally
			if( timePassed > timeToStage2 ){
				stage = 1;
				rigidbody2D.velocity = Vector3.zero;
				timePassed = timeToSpawnBullets;
			}
			else{
				rigidbody2D.velocity = transform.up * speed;
			}
			break;
		case 1: //stage 1, return to player
			if(timePassed > timeToSpawnBullets)
			{
				foreach(Transform child in gameObject.transform){
					child.gameObject.SendMessage("fireBullet");
				}
				timePassed = 0;
			}
			break;
		default:
			Debug.LogError("BlueWave in invalid state! Instance:" + this.GetInstanceID() + " value: " + stage);
			break;
		}
	}
}
