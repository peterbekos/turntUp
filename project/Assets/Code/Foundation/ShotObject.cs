using UnityEngine;
using System.Collections;

public class ShotObject : PlayObject {

	//Stores what the bullet should be damaging
	public enum hit {enemy, player};
	public hit target = hit.player;
	
	public float lifeSpan = 10;
	
	public float duration = 0;
	
	new protected void Start(){
		
	}
	
	// Update is called once per frame
	new protected void Update () {
		if(lifeSpan != -99 && lifeSpan > 0){
			lifeSpan -= Time.deltaTime;
		}
		else
		{
			Destroy(gameObject);
		}
	}
	
	public void interpolate(float interp){
		transform.position += Vector3.up * interp * speed;
	}
	
	public void setDuration(float dur){
		this.duration = dur;
	}
	
	public void amplify(double mod){
		strength = (int)(strength * mod);
	}
	
	//Handles collisions without affecting the transform
	new public void OnTriggerEnter2D(Collider2D coll){
	
		//check that it's colliding with something it's supposed to damage, and if so damage it
		if( target == hit.enemy && coll.tag.Equals("Enemy") || target == hit.player && coll.tag == "Player"){
			//Debug.Log (coll.gameObject.name);
			coll.gameObject.SendMessage("takeDamage", strength);
			takeDamage(1);
		}
	}
}
