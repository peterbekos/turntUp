using UnityEngine;
using System.Collections;

public class ShotObject : PlayObject {

	//Stores what the bullet should be damaging
	public enum target {enemy, player};
	public target type = target.player;
	
	// Update is called once per frame
	new void Update () {
	
	}
	
	public void amplify(double mod){
		strength = (int)(strength * mod);
	}
	
	//Handles collisions without affecting the transform
	void OnTriggerEnter2D(Collider2D coll){
	
		//check that it's colliding with something it's supposed to damage, and if so damage it
		if(coll.gameObject.tag == "Enemy" && type == target.enemy){
			coll.gameObject.SendMessage("takeDamage", this.strength);
			this.hitpoints -= 1; //reduce number of remaining penetrations
		}
		else if(coll.gameObject.tag == "Player" && type == target.player){
			coll.gameObject.SendMessage("takeDamage", this.strength);
			this.hitpoints -= 1; //reduce number of remaining penetrations
		}
		
		//if this bullet has hit as much as it's allowed to, die
		if(hitpoints <= 0){
			Destroy (gameObject);
		}
	}
}
