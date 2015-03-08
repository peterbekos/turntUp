using UnityEngine;
using System.Collections;

public class LinearTravel : MonoBehaviour {

	public float speed;
	
	// Update is called once per frame
	void Update () {
		transform.position = transform.position + Vector3.up * speed * Time.deltaTime;
	}
}
