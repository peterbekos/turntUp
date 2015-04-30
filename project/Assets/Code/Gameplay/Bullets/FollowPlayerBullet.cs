using UnityEngine;
using System.Collections;

public class FollowPlayerBullet : ShotObject {

	public GameObject player;
	
	//float zRot; //rotation to face player
	
	public enum actions { standStill, moveForward };
	
	public actions ifNoPlayer = actions.standStill;
	
	new void Start(){
		base.Start();
		
		if(GameManager.player != null) player = GameManager.player.gameObject;
	}
	
	// Update is called once per frame
	new void Update () {
		base.Update ();
		//Debug.Log ("SonicBoom Alive");
		if(player != null) //if there still exists a player
		{
			rotateToward(player);
			
			//If player is within the movement range of the bullet, match the location
			if(Vector3.Distance(transform.position, player.transform.position) <= speed * Time.deltaTime){
				transform.position = new Vector3(player.transform.position.x, player.transform.position.y, transform.position.z);
			}
			else { //otherwise just move forward
				transform.position += Vector3.up * speed * Time.deltaTime;
			}
		}
		else{ //if there's no player, move according to guideline
			switch(ifNoPlayer){
			case actions.standStill:
				return;
			case actions.moveForward:
				transform.position += Vector3.up * speed * Time.deltaTime;
				break;
			}
		}
	}
}
