using UnityEngine;
using System.Collections;

public class PlayShip : PlayObject {

	//ship's parts
	public GameObject body, wing, booster;
	
	//max rotations on x, y, and z axes, respectively
	public int maxPitch = 15;
	public int maxRoll = 15;
	public int maxYaw = 30;
	
	//for respawning
	private int numBlinks = 4;
	private float timeBetweenBlinks = 0.25f;
	private float blinkDuration = 0.15f;
	private float timeSinceBlink = 0;
	private bool isVisible = true;
	
	void blink(){
		if(isVisible && timeSinceBlink > timeBetweenBlinks){
			//Debug.Log("blink");
			isVisible = false;
			foreach(SpriteRenderer rend in transform.GetComponentsInChildren<SpriteRenderer>()){
				rend.enabled = false;
			}
			timeSinceBlink = 0;
		}
		else if (timeSinceBlink > blinkDuration){
			//Debug.Log("unblink");
			isVisible = true;
			foreach(SpriteRenderer rend in transform.GetComponentsInChildren<SpriteRenderer>()){
				rend.enabled = true;
			}
			timeSinceBlink = 0;
		}
		
		numBlinks--;
	}
	
	void Start(){
		generateShip();
		GameManager.player = this;
	}
	
	// Update is called once per frame
	new void Update () {
		base.Update ();
		
		if(numBlinks > 0)
		{
			//blink();
			timeSinceBlink += Time.deltaTime;
		}
		
		if (Input.GetButton("Fire1")){
			onMelody ();
			//Debug.Log("onMelody");
		}
		
		//change the ship's velocity based on input
		if( rigidbody2D.velocity != Vector2.zero || Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0){
			rigidbody2D.velocity = new Vector2 (Input.GetAxis("Horizontal") * speed, Input.GetAxis("Vertical") * speed);
		}
		
		
		//rotate the ship based on its velocity
		gameObject.transform.rotation = Quaternion.Euler(maxPitch * rigidbody2D.velocity.y / speed, -maxRoll * rigidbody2D.velocity.x / speed, -maxYaw * rigidbody2D.velocity.x / speed);
		
		
		if(Input.GetAxis("Hold") != 0){
			rigidbody2D.velocity = Vector3.zero;
		}
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
		PolygonCollider2D coll = gameObject.GetComponent<PolygonCollider2D>();
		PolygonCollider2D old = temp.GetComponent<PolygonCollider2D>();
		
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

    new public void onBeat(GD type, float interp){
        //base.onBeat();
        switch(type)
        {
        case GD.HAT:
            onHat(interp);
            break; // =)
        case GD.BASS:
			onBass(interp);
            break; // =)
        case GD.MELODY:
			onMelody(interp);
            break; // =)
        case GD.KICK:
			onKick(interp);
            break; // =)
        case GD.HARMONY:
			onHarmony(interp);
            break; // =)
        case GD.SNARE:
			onSnare (interp);
            break; // =(
		case GD.TREBLE:
			onTreble (interp);
			break;
        }
    }
	
	new public void onMelody(float interp){
		base.onMelody ();
		
		ShotOrigin[] guns = transform.GetComponentsInChildren<ShotOrigin>();
		foreach(ShotOrigin s in guns){
			s.SendMessage("onMelody", interp);
		}
	}
	
	new public void onHarmony(float interp){
		base.onHarmony ();
		
		ShotOrigin[] guns = transform.GetComponentsInChildren<ShotOrigin>();
		foreach(ShotOrigin s in guns){
			s.SendMessage("onHarmony", interp);
		}
	}
	
	new public void onKick(float interp){
		base.onKick ();
		
		ShotOrigin[] guns = transform.GetComponentsInChildren<ShotOrigin>();
		foreach(ShotOrigin s in guns){
			s.SendMessage("onKick", interp);
		}
	}
	
	new public void onHat(float interp){
		base.onHat ();
		
		ShotOrigin[] guns = transform.GetComponentsInChildren<ShotOrigin>();
		foreach(ShotOrigin s in guns){
			s.SendMessage("onHat", interp);
		}
	}
	
	new public void onSnare(float interp){
		base.onSnare ();
		
		ShotOrigin[] guns = transform.GetComponentsInChildren<ShotOrigin>();
		foreach(ShotOrigin s in guns){
			s.SendMessage("onSnare", interp);
		}
	}
	
	new public void onBass(float interp){
		base.onBass ();
		
		ShotOrigin[] guns = transform.GetComponentsInChildren<ShotOrigin>();
		foreach(ShotOrigin s in guns){
			s.SendMessage("onBass", interp);
		}
	}
	
	new public void onTreble(float interp){
		base.onTreble ();
		
		ShotOrigin[] guns = transform.GetComponentsInChildren<ShotOrigin>();
		foreach(ShotOrigin s in guns){
			s.SendMessage("onTreble", interp);
		}
	}
}
