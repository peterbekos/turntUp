using UnityEngine;
using System.Collections;

public class CenterAI : EnemyObject {
	
	public Vector3 target = new Vector3(0, 0, 0);
	
	// Use this for initialization
	new public void Start () {
		base.Start ();
		
		//get a quarter of camera's field of view
		Vector2 camBounds = new Vector2(Camera.main.orthographicSize * Screen.width / Screen.height, Camera.main.orthographicSize * Screen.height / Screen.width);
		
		//transform.rotation = Quaternion.Euler(0f, 0f, 180f);
		rotateToward (Vector3.down);
	}
	
	// Update is called once per frame
	new void Update () {
		base.Update();
		
		transform.position += transform.up * speed * Time.deltaTime;
	}
}