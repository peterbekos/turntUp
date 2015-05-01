using UnityEngine;
using System.Collections;

public class SnakeAI : EnemyObject {
	
	public Vector3 target = new Vector3(0, 0, 0);

	public float snakePos = 0f;
	public bool snakeFlip = false;
	
	public static float freq = 0.25f;
	public static float amp = 6f * (1/freq);

	
	// Use this for initialization
	new public void Start () {
		base.Start ();
		
		//get a quarter of camera's field of view
		Vector2 camBounds = new Vector2(Camera.main.orthographicSize * Screen.width / Screen.height, Camera.main.orthographicSize * Screen.height / Screen.width);
		
		transform.rotation = Quaternion.Euler(0f, 0f, 180f);
	}
	
	// Update is called once per frame
	new void Update () {
		base.Update();
		
		transform.position += transform.up * speed * Time.deltaTime;

		if (snakePos < freq) {
			if (!snakeFlip) {
				transform.position += transform.right * speed * amp * (freq - Mathf.Abs(snakePos)) * Time.deltaTime;
			} else {
				transform.position -= transform.right * speed * amp * (freq - Mathf.Abs(snakePos)) * Time.deltaTime;
			}
			snakePos += Time.deltaTime;
		} else {
			snakePos = -freq;
			if (snakeFlip) {
				snakeFlip = false;
			} else {
				snakeFlip = true;
			}
		}
	}
}