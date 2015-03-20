using UnityEngine;
using System.Collections;

public class LinearEnemy : EnemyObject {
	
	public Vector3 target = new Vector3(0, 0, 0);
	
	// Use this for initialization
	void Start () {
		
		//get a quarter of camera's field of view
		Vector2 camBounds = new Vector2(Camera.main.orthographicSize * Screen.width / Screen.height, Camera.main.orthographicSize * Screen.height / Screen.width);
		
		//get a position near the center of the camera to guarantee that the unit will pass through the screen
		target.x = Camera.main.transform.position.x + Random.Range( - camBounds.x /2, camBounds.x /2);
		target.y = Camera.main.transform.position.y + Random.Range( - camBounds.y /2, camBounds.y /2);
		
		
		//rotate to face the target point
		rotateToward(target);
	}
	
	// Update is called once per frame
	new void Update () {
		base.Update();
		
		transform.position += transform.up * speed * Time.deltaTime;
	}
	
	new void OnTriggerEnter2D(Collider2D coll){
		base.OnTriggerEnter2D(coll);
		
		//Debug.Log("" + gameObject.name + " Entered collision");
	
		if (coll.tag.Equals("Player")){
			coll.gameObject.SendMessage("takeDamage", strength);
			takeDamage(strength);
		}
	}
}