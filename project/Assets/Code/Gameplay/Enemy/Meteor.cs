using UnityEngine;
using System.Collections;

public class Meteor : Asteroid {
	
	new void takeDamage(int dmg){
		base.takeDamage(dmg);
		
		transform.position += Vector3.up;
	}
	
	new void OnTriggerEnter2D(Collider2D coll){
		base.OnTriggerEnter2D(coll);
	}
}
