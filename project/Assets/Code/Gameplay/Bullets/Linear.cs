using UnityEngine;
using System.Collections;

public class Linear : ShotObject {
	
	// Update is called once per frame
	new public void Update () {
		base.Update();
		transform.Translate(Vector3.up * speed * Time.deltaTime);
	}
}
