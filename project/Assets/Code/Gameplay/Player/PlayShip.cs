using UnityEngine;
using System.Collections;

public class PlayShip : PlayObject {

	//ship's parts
	public GameObject body, wing, booster;
	
	//max rotations on x, y, and z axes, respectively
	public int maxPitch = 15;
	public int maxRoll = 15;
	public int maxYaw = 30;
	
	public float minX, maxX, minY, maxY;
	
	void Start(){
		generateShip();
		
		//get bounds of camera
		maxX = Camera.main.transform.position.x + Camera.main.orthographicSize * Screen.width / Screen.height / 2;
		minX = Camera.main.transform.position.x - Camera.main.orthographicSize * Screen.width / Screen.height / 2;
		maxY = Camera.main.transform.position.x + Camera.main.orthographicSize * Screen.height / Screen.width / 2;
		minY = Camera.main.transform.position.x - Camera.main.orthographicSize * Screen.height / Screen.width / 2;
	}
	
	// Update is called once per frame
	new void Update () {
		base.Update ();
		
		if (Input.GetButtonDown("Fire1")){
			onMelody ();
			Debug.Log("onMelody");
		}
		
		//change the ship's velocity based on input
		if( rigidbody2D.velocity != Vector2.zero || Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0){
			rigidbody2D.velocity = new Vector2 (Input.GetAxis("Horizontal") * speed, Input.GetAxis("Vertical") * speed);
		}
		
		
		//rotate the ship based on its velocity
		gameObject.transform.rotation = Quaternion.Euler(maxPitch * rigidbody2D.velocity.y / speed, -maxRoll * rigidbody2D.velocity.x / speed, -maxYaw * rigidbody2D.velocity.x / speed);
	
	}
	
	//Craft the ship out of the stored parts
	void generateShip(){
		//delete all current ship parts
		foreach(Transform child in gameObject.transform){
			GameObject.Destroy(child.gameObject);
		}
		
		//add parts
		addPart(body);
		addPart(wing);
		addPart(booster);
	}
	
	//Change all parts and craft ship
	private void generateShip(GameObject body, GameObject wing, GameObject booster){
		changePart(0, body);
		changePart(1, wing);
		changePart(2, booster);
		
		//changepart automatically generates ship
	}
	
	//Add a ship part to the sceneand set it as a child, then craft the collider
	private void addPart(GameObject part){
		GameObject temp;
		
		//add part to scene
		temp = (GameObject)Instantiate(part, this.transform.position, this.transform.rotation);
		temp.transform.localScale.Set(this.transform.localScale.x, this.transform.localScale.y, this.transform.localScale.z);
		
		//set this object as parent
		temp.transform.SetParent(this.gameObject.transform);
		
		//move the part's collider
#pragma warning disable 0169
//disable "unused" warnings

		PolygonCollider2D coll = gameObject.GetComponent<PolygonCollider2D>();
		PolygonCollider2D old = temp.GetComponent<PolygonCollider2D>();
		
#pragma warning restore 0169
//re-enable "unused" warnings
		
		
		//TODO: re-create the collider to fit the new shape
		/*
		//edit the correct path
		if(part == body){
			coll.SetPath(0, old.GetPath(0)); //copy body collider
		}
		else if(part == wing){
			coll.SetPath(1, old.GetPath(0)); //copy wing collider
		}
		else if(part == booster){
			coll.SetPath(2, old.GetPath(0)); //copy booster collider
		}
		Destroy(old); //old one not needed; waste of CPU
		*/
		
	}
	
	//swap a part
	private void changePart(int type, GameObject part){
		switch(type){
		case 0: //change body
			body = part;
			break;
		case 1: //change wings
			wing = part;
			break;
		case 2: //change boosters
			booster = part;
			break;
		default:
			Debug.LogError("Invalid type when changing ship parts!");
			return;
		}
		
		generateShip();
	}
	
	new public void onMelody(){
		base.onMelody ();
		
		ShotOrigin[] guns = transform.GetComponentsInChildren<ShotOrigin>();
		foreach(ShotOrigin s in guns){
			s.SendMessage("onMelody");
		}
	}
	
	new public void onHarmony(){
		base.onHarmony ();
		
		ShotOrigin[] guns = transform.GetComponentsInChildren<ShotOrigin>();
		foreach(ShotOrigin s in guns){
			s.SendMessage("onHarmony");
		}
	}
	
	new public void onKick(){
		base.onKick ();
		
		ShotOrigin[] guns = transform.GetComponentsInChildren<ShotOrigin>();
		foreach(ShotOrigin s in guns){
			s.SendMessage("onKick");
		}
	}
	
	new public void onHat(){
		base.onHat ();
		
		ShotOrigin[] guns = transform.GetComponentsInChildren<ShotOrigin>();
		foreach(ShotOrigin s in guns){
			s.SendMessage("onHat");
		}
	}
	
	new public void onSnare(){
		base.onSnare ();
		
		ShotOrigin[] guns = transform.GetComponentsInChildren<ShotOrigin>();
		foreach(ShotOrigin s in guns){
			s.SendMessage("onSnare");
		}
	}
	
	new public void onBass(){
		base.onBass ();
		
		ShotOrigin[] guns = transform.GetComponentsInChildren<ShotOrigin>();
		foreach(ShotOrigin s in guns){
			s.SendMessage("onBass");
		}
	}
	
	new public void onTreble(){
		base.onTreble ();
		
		ShotOrigin[] guns = transform.GetComponentsInChildren<ShotOrigin>();
		foreach(ShotOrigin s in guns){
			s.SendMessage("onTreble");
		}
	}
}
