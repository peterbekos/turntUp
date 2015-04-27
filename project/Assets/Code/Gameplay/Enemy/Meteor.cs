using UnityEngine;
using System.Collections;

public class Meteor : EnemyObject {

	//public Vector3 direction;
	public float rotSpeed = 5f;
	
	public Sprite[] skins = new Sprite[2];

	// Use this for initialization
	new void Start () {
		base.Start();
		
		rotSpeed = Random.Range(-rotSpeed, rotSpeed);
		gameObject.GetComponent<SpriteRenderer>().sprite = skins[(int)(Random.Range (0, skins.Length))];
		
		rigidbody2D.velocity = Vector3.down * speed;
	}
	
	// Update is called once per frame
	new void Update () {
		base.Update();
		transform.Rotate(0, 0, rotSpeed);
		if(GameManager.player != null){
			rigidbody2D.velocity = new Vector3(-.5f * GameManager.player.rigidbody2D.velocity.x, rigidbody2D.velocity.y, 0);
		}
		
		if(transform.position.y < Camera.main.transform.position.y - 20 - transform.localScale.y)
			Destroy (gameObject);
	}
	
	new void OnTriggerEnter2D(Collider2D coll){
		base.OnTriggerEnter2D(coll);
		
		if (coll.tag.Equals("Player")){
			coll.gameObject.SendMessage("takeDamage", strength);
			takeDamage(strength);
		}
	}
	
	new void takeDamage(int dmg){
		base.takeDamage(dmg);
		
		transform.position += Vector3.up;
	}
}
