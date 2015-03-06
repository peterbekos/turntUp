using UnityEngine;
using System.Collections;

public class ShotOrigin : BeatObject {

	//Bullets for each firing type
	public GameObject melodyShot, harmonyShot, kickShot, snareShot, hatShot, bassShot, trebleShot;
	
	//damage multipliers
	private double mMod = 1d, hMod = 1d, kMod = 1d, sMod = 1d, hatMod = 1d, bMod = 1d, tMod = 1d; 
	
	
	//Fire a bullet.  What else?
	public void fireBullet(GameObject shot, double mod){
        var bullet = GameObject.Instantiate(shot, this.transform.position, this.transform.rotation);
		
		if(mod != 1.0)
			shot.SendMessage("amplify", mod);
	}
	
	//Override beat functions
	new public void onMelody(){
		if(melodyShot == null) return;
		fireBullet(melodyShot, mMod);
	}
	new public void onHarmony() {
		if(harmonyShot == null) return;
		fireBullet(harmonyShot, hMod);
	}
	new public void onKick() {
		if(kickShot == null) return;
		fireBullet(kickShot, kMod);
	}
	new public void onSnare() {
		if(snareShot == null) return;
		fireBullet(snareShot, sMod);
	}
	new public void onHat() {
		if(hatShot == null) return;
		fireBullet(hatShot, hatMod);
	}
	new public void onBass() {
		if(bassShot == null) return;
		fireBullet(bassShot, bMod);
	}
	new public void onTreble() {
		if(trebleShot == null) return;
		fireBullet(trebleShot, tMod);
	}
}
