using UnityEngine;
using System.Collections;

public class CircularCurveBullet : ShotObject {

	public float rotationSpeed = 180f; //how many degrees to turn in one second
	
	// Rotate and move forward (moves in a circle)
	new void Update () {
		base.Update();
	
		transform.Rotate(new Vector3(0, 0, rotationSpeed * Time.deltaTime));
		rigidbody2D.velocity = transform.up * speed;
	}
}
