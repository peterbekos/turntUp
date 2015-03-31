using UnityEngine;
using System.Collections;

public class EnemyObject : PlayObject {

	
	
	// Update is called once per frame
	public new void Update () {
		base.Update ();
	}
	
	new public void OnTriggerEnter2D(Collider2D coll){
		base.OnTriggerEnter2D(coll);
		
		if (coll.tag.Equals("Player"))
		{
			coll.gameObject.SendMessage("takeDamage", strength);
			takeDamage(strength);
		}
	}
}
