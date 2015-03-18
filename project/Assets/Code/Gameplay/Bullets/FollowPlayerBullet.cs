using UnityEngine;
using System.Collections;

public class FollowPlayerBullet : ShotObject {

	public GameObject player;
	
	float zRot; //rotation to face player
	
	public enum actions { standStill, moveForward };
	
	public actions ifNoPlayer = actions.standStill;
	
	new void Start(){
		base.Start();
		
		player = GameManager.player.gameObject;
	}
	
	// Update is called once per frame
	new void Update () {
		base.Update ();
		//Debug.Log ("SonicBoom Alive");
		if(player != null) //if there still exists a player
		{
			zRot = Mathf.Atan2(player.transform.position.y - transform.position.y, player.transform.position.x - transform.position.x) * Mathf.Rad2Deg;
			transform.rotation = Quaternion.Euler(0f, 0f, zRot - 90);
			
			//If player is within the movement range of the bullet, match the location
			if(Vector3.Distance(transform.position, player.transform.position) <= speed){
				transform.position = new Vector3(player.transform.position.x, player.transform.position.y, transform.position.z);
			}
			else { //otherwise just move forward
				transform.position += Vector3.up * speed;
			}
		}
		else{ //if there's no player, move according to guideline
			switch(ifNoPlayer){
			case actions.standStill:
				return;
			case actions.moveForward:
				transform.position += Vector3.up * speed;
				break;
			}
		}
	}
}
