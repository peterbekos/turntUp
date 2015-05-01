using UnityEngine;
using System.Collections;

public class ParabolaMirrorAI : EnemyObject {
	
	public Vector3 target = new Vector3(0, 0, 0);
	Vector2 camBounds;
	bool onLeftSideOfScreen;
	bool flipped = false;
	float curve = 0;
	static float debuf = 0.5f;
	
	// Use this for initialization
	new public void Start () {
		base.Start ();
		
		//get a quarter of camera's field of view
		camBounds = new Vector2(Camera.main.orthographicSize * Screen.width / Screen.height, Camera.main.orthographicSize * Screen.height / Screen.width);
		
		transform.rotation = Quaternion.Euler(0f, 0f, 180f);

		if (this.rigidbody2D.position.x < 0) {
			onLeftSideOfScreen = false;
		} else {
			onLeftSideOfScreen = true;
		}
	}
	
	// Update is called once per frame
	new void Update () {
		base.Update();

		if (!flipped) {
			curve += Time.deltaTime;
		} else {
			curve -= Time.deltaTime;
		}

		if (!flipped) {
			transform.position += transform.up * speed * Time.deltaTime;
		} else {
			transform.position -= transform.up * speed * Time.deltaTime;
		}

		if (onLeftSideOfScreen) {
			transform.position += transform.right * (curve * speed * debuf) * Time.deltaTime;
		} else {
			transform.position -= transform.right * (curve * speed * debuf) * Time.deltaTime;
		}

		if (this.rigidbody2D.position.x < 0 && flipped == false && onLeftSideOfScreen == true) {
			flipped = true;
		} else if (this.rigidbody2D.position.x > 0 && flipped == false && onLeftSideOfScreen == false) {
			flipped = true;
		}

	}

}