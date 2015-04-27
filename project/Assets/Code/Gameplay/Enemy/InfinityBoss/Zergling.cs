using UnityEngine;
using System.Collections;

public class Zergling : EnemyObject {

	// Update is called once per frame
	void Update () {
		rotateToward(GameManager.player.gameObject);
		transform.position += transform.up * speed * Time.deltaTime;
	}
}
