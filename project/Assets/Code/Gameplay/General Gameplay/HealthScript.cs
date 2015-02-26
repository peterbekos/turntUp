using UnityEngine;
using System.Collections;

public class HealthScript : MonoBehaviour {

	public int health;
	
	void takeDamage(int dmg){
		health -= dmg;
		
		if(health <= 0 )
			GameObject.Destroy (gameObject);
	}
}
