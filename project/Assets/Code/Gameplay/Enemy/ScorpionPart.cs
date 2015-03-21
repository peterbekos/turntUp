using UnityEngine;
using System.Collections;

public class ScorpionPart : EnemyObject {

	// Use this for initialization
	new public void Start () {
        base.Start();
	}
	
	// Update is called once per frame
	new public void Update () {
		base.Update();
	}
	
	new public void OnTriggerEnter2D(Collider2D coll){
		base.OnTriggerEnter2D(coll);
	}
}
