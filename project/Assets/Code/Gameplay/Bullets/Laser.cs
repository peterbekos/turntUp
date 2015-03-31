using UnityEngine;
using System.Collections;

public class Laser : ContinuousShotObject {

	// Use this for initialization
	new void Start () {
		base.Start ();
		
		//link this to the player
		transform.SetParent(GameManager.player.gameObject.transform);
	}
	
	new void setDuration(float dur){
		base.setDuration(dur);
		lifeSpan = dur/1000;
	}
}
