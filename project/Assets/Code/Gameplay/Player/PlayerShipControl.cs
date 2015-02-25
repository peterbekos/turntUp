using UnityEngine;
using System.Collections;

public class PlayerShipControl : MonoBehaviour {

	public int maxVelocity = 5;
	public int maxYaw = 30;
	public int maxPitch = 15;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetAxis ("Cancel") > 0){
			Application.Quit();
		}
	
		if( rigidbody2D.velocity != Vector2.zero || Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0){
			rigidbody2D.velocity = new Vector2 (Input.GetAxis("Horizontal") * maxVelocity, Input.GetAxis("Vertical") * maxVelocity);
		}
		
		gameObject.transform.rotation = Quaternion.Euler(maxPitch * rigidbody2D.velocity.y / maxVelocity, 0, -maxYaw * rigidbody2D.velocity.x / maxVelocity);
	}
	
}
