using UnityEngine;
using System.Collections;

public class DamageEnemy : MonoBehaviour {
	void OnTriggerEnter2D(Collider2D coll){
		//Debug.Log("Collision!");
		if(coll.gameObject.tag.Equals("Enemy"))
		{
			//Damage the enemy
			coll.gameObject.SendMessage("takeDamage", this.gameObject.GetComponent<DamageScript>().damage);
			
			//reduce this bullet's # of hits by one
			gameObject.SendMessage("takeDamage", 1);
		}
	}
}
