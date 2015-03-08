using UnityEngine;
using System.Collections;

public class EnemyObject : PlayObject {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	new void Update () {
	
	}
	
	new void OnTriggerEnter2D(Collider2D coll){
		base.OnTriggerEnter2D(coll);
		
		Debug.Log("" + gameObject.name + "Exited collision");
	}
}
