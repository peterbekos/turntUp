using UnityEngine;
using System.Collections;

public class Minion : EnemyObject {

	public Vector3 targetPosition;
	public GameObject zergling;
	public GameObject laser;
	
	bool arrived;

	// Use this for initialization
	new void Start () {
		base.Start ();
	}
	
	// Update is called once per frame
	new void Update () {
		base.Update();
		if(!arrived){
			transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
			if(transform.position == targetPosition) arrived = true;
		}
		else {
			if(GameManager.player != null)
				rotateToward(GameManager.player.gameObject);
		}
	}
	
	public void setTargetPos(Vector2 pos){
		targetPosition = new Vector3(pos.x, pos.y, transform.position.z);
		rotateToward(targetPosition);
	}
	
	public void spawnZergling(){
		Instantiate (zergling, transform.position + transform.up * 2, transform.rotation);
	}
	
	public void fireLaser(){
		GameObject newlaser = (GameObject)Instantiate (laser, transform.position + transform.up * 2, transform.rotation);
		newlaser.transform.SetParent(gameObject.transform);
	}
}
