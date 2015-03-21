using UnityEngine;
using System.Collections;

public class ShotOrigin : BeatObject {

	public enum AngleMethod { IDENTIY, PITCH, TIME, VELOCITY, MINUS30, MINUS45, PLUS30, PLUS45 };

	//Bullets for each firing type
	public GameObject melodyShot, harmonyShot, kickShot, snareShot, hatShot, bassShot, trebleShot;
	public AngleMethod melodyAngle, harmonyAngle, kickAngle, snareAngle, hatAngle, bassAngle, trebleAngle;
	
	//damage multipliers
	private double mMod = 1d, hMod = 1d, kMod = 1d, sMod = 1d, hatMod = 1d, bMod = 1d, tMod = 1d; 
	
	
	//Fire a bullet.  What else?
	public void fireBullet(GameObject shot, double mod, float interp){
        GameObject bullet = (GameObject)GameObject.Instantiate(shot, this.transform.position, this.transform.rotation);
		
		
		bullet.SendMessage("interpolate", interp);
		
		if(mod != 1.0)
			bullet.SendMessage("amplify", mod);
	}
	
	//Fire a bullet.  Handles duration
	public void fireBullet(GameObject shot, double mod, float interp, float duration){
		
		GameObject bullet = (GameObject)GameObject.Instantiate(shot, this.transform.position, this.transform.rotation);
		
		bullet.SendMessage("setDuration", duration);
		bullet.SendMessage("interpolate", interp);
		
		if(mod != 1.0)
			bullet.SendMessage("amplify", mod);
	}
	
	//Override beat functions
	new public void onMelody(float interp){
		if(melodyShot == null) return;
		fireBullet(melodyShot, mMod, interp);
	}
	new public void onHarmony(float interp) {
		if(harmonyShot == null) return;
		fireBullet(harmonyShot, hMod, interp);
	}
	new public void onKick(float interp) {
		if(kickShot == null) return;
		fireBullet(kickShot, kMod, interp);
	}
	new public void onSnare(float interp) {
		if(snareShot == null) return;
		fireBullet(snareShot, sMod, interp);
	}
	new public void onHat(float interp) {
		if(hatShot == null) return;
		fireBullet(hatShot, hatMod, interp);
	}
	new public void onBass(float interp) {
		if(bassShot == null) return;
		fireBullet(bassShot, bMod, interp);
	}
	new public void onTreble(float interp) {
		if(trebleShot == null) return;
		fireBullet(trebleShot, tMod, interp);
	}
	
	//Override beat functions
	new public void onMelody(float[]args){
		if(melodyShot == null) return;
		fireBullet(melodyShot, mMod, args[0], args[1]);
	}
	new public void onHarmony(float[] args) {
		if(harmonyShot == null) return;
		fireBullet(harmonyShot, hMod, args[0], args[1]);
	}
	new public void onKick(float[] args) {
		if(kickShot == null) return;
		fireBullet(kickShot, kMod, args[0], args[1]);
	}
	new public void onSnare(float[] args) {
		if(snareShot == null) return;
		fireBullet(snareShot, sMod, args[0], args[1]);
	}
	new public void onHat(float[] args) {
		if(hatShot == null) return;
		fireBullet(hatShot, hatMod, args[0], args[1]);
	}
	new public void onBass(float[] args) {
		if(bassShot == null) return;
		fireBullet(bassShot, bMod, args[0], args[1]);
	}
	new public void onTreble(float[] args) {
		if(trebleShot == null) return;
		fireBullet(trebleShot, tMod, args[0], args[1]);
	}
}
