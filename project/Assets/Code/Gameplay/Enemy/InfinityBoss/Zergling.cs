using UnityEngine;
using System.Collections;

public class Zergling : EnemyObject {

	// Update is called once per frame
	new void Update () {
		base.Update();
		
		if(GameManager.player == null) return;
		
		rotateToward(GameManager.player.gameObject);
		transform.position += transform.up * speed * Time.deltaTime;
	}
}
