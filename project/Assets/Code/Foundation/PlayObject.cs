using UnityEngine;
using System.Collections;

public abstract class PlayObject : BeatObject {

	//Variables
	//note: they have to be public so we can tweak them in the editor, 
	// or at least protected so we can see them at all in sub-classes
	public int hitpoints;
	public int strength;
	public float speed;

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
			Destroy(gameObject);
		}
	}
	
	//Use this for collisions, built-in to Unity
	//Triggers don't physically collide (as in, they don't send things flying when you bump into them)
	//they just pass through each other and trigger this method
	protected void OnTriggerEnter2D(Collider2D coll){
		
	}
	
	protected void OnTriggerExit2D(Collider2D coll){
		if (coll.gameObject.tag.Equals("MainCamera")){
			Destroy(gameObject);
		}
	}
}
