using UnityEngine;
using System.Collections;

public class ShipPartsAdder : MonoBehaviour {

	public GameObject body, wing, booster;
	
	public GameObject[] bodies = new GameObject[3];
	public GameObject[] wings = new GameObject[3];
	public GameObject[] boosters = new GameObject[3];
	
	// Use this for initialization
	void Start () {
		
		GenerateShip();
		
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.Y))
			ChangePart(0, bodies[0]);
		else if(Input.GetKeyDown(KeyCode.U))
			ChangePart(0, bodies[1]);
		//else if(Input.GetKeyDown(KeyCode.Keypad3))
			//ChangePart(0, bodies[2]);
		else if(Input.GetKeyDown(KeyCode.H))
			ChangePart(1, wings[0]);
		else if(Input.GetKeyDown(KeyCode.J))
			ChangePart(1, wings[1]);
		//else if(Input.GetKeyDown(KeyCode.Keypad6))
			//ChangePart(1, wings[2]);
		else if(Input.GetKeyDown(KeyCode.N))
			ChangePart(2, boosters[0]);
		else if(Input.GetKeyDown(KeyCode.M))
			ChangePart(2, boosters[1]);
		//else if(Input.GetKeyDown(KeyCode.Keypad9))
			//ChangePart(2, boosters[2]);
	}
	
	void GenerateShip(){
		//delete all current ship parts
		foreach(Transform child in gameObject.transform){
			GameObject.Destroy(child.gameObject);
		}
	
		GameObject temp;
		
		//add and scale body
		temp = (GameObject)Instantiate(body, this.transform.position, this.transform.rotation);
		temp.transform.localScale.Set(this.gameObject.transform.localScale.x, this.gameObject.transform.localScale.y, this.gameObject.transform.localScale.z);
		temp.transform.SetParent(this.gameObject.transform);
		
		//add and scale wings
		temp = (GameObject)Instantiate(wing, this.transform.position, this.transform.rotation);
		temp.transform.localScale.Set(this.transform.localScale.x, this.transform.localScale.y, this.transform.localScale.z);
		temp.transform.SetParent(this.gameObject.transform);
		
		//add and scale boosters
		temp = (GameObject)Instantiate(booster, this.transform.position, this.transform.rotation);
		temp.transform.localScale.Set(this.transform.localScale.x, this.transform.localScale.y, this.transform.localScale.z);
		temp.transform.SetParent(this.gameObject.transform);
		
		temp = null;
	}
	
	void ChangePart(int type, GameObject part){
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
		
		GenerateShip();
	}
}
