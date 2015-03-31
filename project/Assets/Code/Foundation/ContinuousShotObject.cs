using UnityEngine;
using System.Collections;

public class ContinuousShotObject : ShotObject {

	new protected void Update(){
		if(lifeSpan <=0){
			transform.SetParent(null);
			GetComponent<SpriteRenderer>().enabled = false;
			transform.position = new Vector3(1000, 1000, 0);
		}
		base.Update();
	}

	new protected void OnTriggerEnter2D (Collider2D coll){
		if( target == hit.enemy && coll.tag == "Enemy" || target == hit.player && coll.tag == "Player"){
			coll.gameObject.SendMessage("changeDoT", strength);
		}
	}
	
	new protected void OnTriggerExit2D (Collider2D coll){
		if( target == hit.enemy && coll.tag == "Enemy" || target == hit.player && coll.tag == "Player"){
			coll.gameObject.SendMessage("changeDoT", -strength);
		}
	}
}
