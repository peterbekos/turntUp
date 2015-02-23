using UnityEngine;
using System.Collections;

public abstract class BeatObject : MonoBehaviour {
	
	//Overridable method to make each object react to beats
	public void onBeat(GD type) {
		switch (type) {
		case GD.MELODY:
			onMelody();
			break;
		case GD.HARMONY:
			onHarmony();
			break;
		case GD.KICK:
			onKick();
			break;
		case GD.SNARE:
			onSnare();
			break;
		case GD.HAT:
			onHat();	
			break;
		case GD.BASS:
			onBass();
			break;
		case GD.TREBLE:
			onTreble();
			break;
		default:
			break;
		}
	}
	
	public void onMelody() {
		
	}
	
	public void onHarmony() {
		
	}
	
	public void onKick() {
		
	}
	
	public void onSnare() {
		
	}
	
	public void onHat() {
		
	}
	
	public void onBass() {
		
	}
	
	public void onTreble() {
		
	}
	
	public void move(float x, float y) {
		//TODO - use rigid body velocity
		float dt = Time.deltaTime;
		transform.Translate(x * dt, y * dt, 0);
	}
}
