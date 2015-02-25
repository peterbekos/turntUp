using UnityEngine;
using System.Collections;

public class BlueDash : MonoBehaviour {

	public int speed = 10;
	
	// Update is called once per frame
	void Update () {
		rigidbody2D.velocity = transform.up * speed;
	}
}
