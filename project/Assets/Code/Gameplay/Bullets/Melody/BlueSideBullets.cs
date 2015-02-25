using UnityEngine;
using System.Collections;

public class BlueSideBullets : MonoBehaviour {

	public int rotationAngle = 30;
	
	// Update is called once per frame
	void Update () {
		transform.Rotate(new Vector3(0, 0, (Time.time * rotationAngle) % rotationAngle));
	}
}
