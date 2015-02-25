using UnityEngine;
using System.Collections;

public class ShotOrigin : MonoBehaviour {

	public GameObject shot; //stores the bullet to be fired
	
	public int type = 1; //type of shot; m = meody, b = bass, d = drum kit
	
	// Update is called once per frame
	void Update () {
		//fire weapon if button pressed
		switch(type){
		case 1: //if this is of melody type, check for melody button
			if(Input.GetButtonDown("Fire1"))
				fireBullet(1.0);
			break;
		case 2: //if this is of bass type, check for bass button
			if(Input.GetButtonDown("Fire2"))
				fireBullet(1.0);
			break;
		case 3: //if this is of drum kit type, check for kit button
			if(Input.GetButtonDown("Fire3"))
				fireBullet(1.0);
			break;
		case -1: //doesn't shoot with button input
			break;
		default:
			Debug.LogError(this.name + " has invalid firing type");
			break;
		}
	}
	
	//Fire a bullet.  What else?
	public void fireBullet(){
		GameObject.Instantiate(shot, this.transform.position, this.transform.rotation);
		//bullet.GetComponent<BulletScript>().setDamage(intensity);
	}
	
	//Fire a bullet.  What else?
	public void fireBullet(double intensity){
		GameObject bullet = (GameObject)Instantiate(shot, this.transform.position, this.transform.rotation);
		//bullet.GetComponent<BulletScript>().setDamage(intensity);
	}
}
