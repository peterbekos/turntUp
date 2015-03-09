using UnityEngine;
using System.Collections;

public abstract class PlayObject : BeatObject {

	//Variables
	//note: they have to be public so we can tweak them in the editor, 
	// or at least protected so we can see them at all in sub-classes
	public int hitpoints;
	public int strength;
	public float speed;
	
	public GameObject deathSoundPlayer; //object that plays a sound effect after the object dies
	//this is because if we destroy the object while the sound is playing, the sound stops
	//so we need another object to play the sound

	protected void Update(){
	
	}
	
	void onCollideSelf() {

	}

	void onCollideTarget(PlayObject target) {

	}
	
	//reduce health, and if <= 0 die
	public void takeDamage(int dmg){
		hitpoints -= dmg;
		if(hitpoints <= 0)
		{
			//play the death sound and kill the object
			Instantiate(deathSoundPlayer);
			Destroy(gameObject);
		}
	}
	
	//Use this for collisions, built-in to Unity
	//Triggers don't physically collide (as in, they don't send things flying when you bump into them)
	//they just pass through each other and trigger this method
	public void OnTriggerEnter2D(Collider2D coll){
		
	}
	
	public void OnTriggerExit2D(Collider2D coll){
		if (coll.gameObject.tag.Equals("MainCamera")){
			Destroy(gameObject);
		}
	}
}
