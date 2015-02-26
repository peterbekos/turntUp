using UnityEngine;
using System.Collections;

public class BlueBass : MonoBehaviour {

	public int speed = 10; //how many units to move in one second
	public int rotationSpeed = 180; //how many degrees to turn in one second
	
	// Rotate and move forward (moves in a circle)
	void Update () {
		transform.Rotate(new Vector3(0, 0, rotationSpeed * Time.deltaTime));
		rigidbody2D.velocity = transform.up * speed;
	}
}
