using UnityEngine;
using System.Collections;

public class HealthBar : MonoBehaviour {

	public float emptyPos = -4.7f;
	public float hurtAnimSpeed = 2; //units to move per second when hurt
	public float healAnimSpeed = 4; //units to move per second when healed
	public float missingHealthPercent;
	
	private AudioSource hurt, healed; //sound effects
	private bool hurtPlaying, healPlaying; //stores if a sound is already playing
	
	void Start(){
		hurt = gameObject.GetComponents<AudioSource>()[0];
		healed = gameObject.GetComponents<AudioSource>()[1];
	}

	// Update is called once per frame
	void Update () {
		if(GameManager.player == null){
			missingHealthPercent = 1f;
		}
		else {	
			missingHealthPercent = (5000 - GameManager.player.hitpoints)/5000f;
		}
		
		//DID SOMEBODY SAY DONUTS?!
		
		
		float currpos = transform.localPosition.y; //current position
		float targetpos = missingHealthPercent * emptyPos; //target position
		
		if(currpos > targetpos){ //hurt; move down toward target
			if(!hurtPlaying) {
				hurtPlaying = true;
				hurt.loop = true;
				hurt.Play ();
			}
		
			if( currpos - Time.deltaTime * hurtAnimSpeed > targetpos) //if not close enough to target
				transform.localPosition -= new Vector3(0, Time.deltaTime * hurtAnimSpeed, 0); //move down by animation
			else{
				transform.localPosition = new Vector3(0, targetpos, -.1f); //move to target
				hurt.loop = false;
				hurtPlaying = false;
			}
		}
		else if (currpos < targetpos) { //healed; move up toward target
			if(!healPlaying){
				healPlaying = true;
				healed.loop = true;
				healed.Play();
			}
		
			if( currpos + Time.deltaTime * healAnimSpeed < targetpos) //if not close enough to target
				transform.localPosition += new Vector3(0, Time.deltaTime * healAnimSpeed, 0); //move up by animation
			else{
				transform.localPosition = new Vector3(0, targetpos, -.1f); //move to target
				healed.loop = false;
				healPlaying = false;
			}
		}
		else if(healPlaying || hurtPlaying){
			healed.loop = false;
			hurt.loop = false;
			healPlaying = false;
			hurtPlaying = false;
		}
	}
}
