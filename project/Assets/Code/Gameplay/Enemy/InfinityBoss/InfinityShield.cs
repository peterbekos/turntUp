using UnityEngine;
using System.Collections;

public class InfinityShield : EnemyObject {
	
	public GameObject pulseInstance;
	
	public void spawnPulse(){
		Instantiate(pulseInstance, transform.position, Quaternion.identity);
	}
	
	new void takeDamage(int dmg){ //we don't want to increase score, just want to kill object &spawn death animation
		if(invincible) return;
	
		hitpoints -= dmg;
		
		if(hitpoints <= 0)
		{
			//play the death sound and kill the object
			if(deathAnimation != null) Instantiate(deathAnimation, transform.position, Quaternion.identity);
			Destroy(gameObject);
		}
	}
}
