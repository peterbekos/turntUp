using UnityEngine;
using System.Collections;

public class ShotOrigin : BeatObject {

	public enum AngleMethod { IDENTIY, PITCH, TIME, VELOCITY, S1, S2, S3, S1PINGPONG, S2PINGPONG, S3PINGPONG, S1MIRROR, S2MIRROR, S3MIRROR, MINUS15, MINUS30, MINUS45, PLUS15, PLUS30, PLUS45, BROKENTIME };
	
	public static AngleMethod[] Sequence1 = {AngleMethod.PLUS45, AngleMethod.IDENTIY, AngleMethod.MINUS45 };
	public int currSeq1Index = 0;
	private bool s1mirror;
	
	public static AngleMethod[] Sequence2 = {AngleMethod.PLUS45, AngleMethod.PLUS30, AngleMethod.IDENTIY, AngleMethod.MINUS30, AngleMethod.MINUS45 };
	public int currSeq2Index = 0;
	private bool s2mirror;
	
	public static AngleMethod[] Sequence3 = {AngleMethod.PLUS45, AngleMethod.PLUS15, AngleMethod.MINUS15, AngleMethod.MINUS45 };
	public int currSeq3Index = 0;
	private bool s3mirror;
	
	public float timeModifier = 1f;
	public float maxTimeAngle = 45;

	//Bullets for each firing type
	public GameObject melodyShot, harmonyShot, kickShot, snareShot, hatShot, bassShot, trebleShot;
	public AngleMethod melodyAngle, harmonyAngle, kickAngle, snareAngle, hatAngle, bassAngle, trebleAngle;
	
	//damage multipliers
	private double mMod = 1d, hMod = 1d, kMod = 1d, sMod = 1d, hatMod = 1d, bMod = 1d, tMod = 1d; 
	
	
	//Fire a bullet.  What else?
	public GameObject fireBullet(GameObject shot, double mod, float interp){
        GameObject bullet = (GameObject)GameObject.Instantiate(shot, this.transform.position, this.transform.rotation);
		
		
		bullet.SendMessage("interpolate", interp);
		
		if(mod != 1.0)
			bullet.SendMessage("amplify", mod);
			
		return bullet;
	}
	
	//Fire a bullet.  Handles duration
	public GameObject fireBullet(GameObject shot, double mod, float interp, float duration){
		
		GameObject bullet = (GameObject)GameObject.Instantiate(shot, this.transform.position, this.transform.rotation);
		
		bullet.SendMessage("setDuration", duration);
		bullet.SendMessage("interpolate", interp);
		
		
		if(mod != 1.0)
			bullet.SendMessage("amplify", mod);
			
		return bullet;
	}
	
	//Rotate a bullet
	protected void rotate(GameObject bullet, AngleMethod method){
		switch(method){
		case AngleMethod.IDENTIY: //no rotation, match shotOrigin's
			return;
		case AngleMethod.MINUS15: //rotate right/out 30 degrees
			bullet.transform.Rotate(new Vector3(0, 0, -15));
			break;	
		case AngleMethod.MINUS30: //rotate right/out 30 degrees
			bullet.transform.Rotate(new Vector3(0, 0, -30));
			break;
		case AngleMethod.MINUS45: //rotate right/out 45 degrees
			bullet.transform.Rotate(new Vector3(0, 0, -45));
			break;
		case AngleMethod.PLUS15: //rotate left/in 45 degrees 
			bullet.transform.Rotate(new Vector3(0, 0, 15));
			break;
		case AngleMethod.PLUS30: //rotate left/in 45 degrees 
			bullet.transform.Rotate(new Vector3(0, 0, 30));
			break;
		case AngleMethod.PLUS45: //rotate left/in 45 degrees
			bullet.transform.Rotate(new Vector3(0, 0, 45));
			break;
		case AngleMethod.PITCH: //rotate left/right based on pitch of note
			//TODO: handle pitch
			
			break;
		case AngleMethod.TIME: //rotate left/right based on gametime
			if(timeModifier == 0) return; //avoid dividing by zero
			bullet.transform.Rotate(new Vector3(0, 0, Mathf.Cos( (GameManager.getTime() * timeModifier / 1000 ) % 360) * maxTimeAngle ));
			break;
		case AngleMethod.BROKENTIME: //rotate left/right based on gametime - logic error made cool pattern
			if(timeModifier == 0) return; //avoid dividing by zero
			bullet.transform.Rotate(new Vector3(0, 0, Mathf.Cos( (GameManager.getTime() / 1000) % (360 / timeModifier) * timeModifier ) * maxTimeAngle ));
			break;
		case AngleMethod.VELOCITY:
			//TODO: handle velocity
			
			break;
		case AngleMethod.S1:
			rotate(bullet, Sequence1[currSeq1Index]);
			currSeq1Index = ++currSeq1Index % Sequence1.Length;
			break;
		case AngleMethod.S2:
			rotate(bullet, Sequence2[currSeq2Index]);
			currSeq2Index = ++currSeq2Index % Sequence2.Length;
			break;
		case AngleMethod.S3:
			rotate(bullet, Sequence3[currSeq3Index]);
			currSeq3Index = ++currSeq3Index % Sequence3.Length;
			break;
		case AngleMethod.S1MIRROR:
			rotate(bullet, Sequence1[Sequence1.Length - currSeq1Index - 1]);
			currSeq1Index = ++currSeq1Index % Sequence1.Length;
			break;
		case AngleMethod.S2MIRROR:
			rotate(bullet, Sequence2[Sequence2.Length - currSeq2Index - 1]);
			currSeq2Index = ++currSeq2Index % Sequence2.Length;
			break;
		case AngleMethod.S3MIRROR:
			rotate(bullet, Sequence3[Sequence3.Length - currSeq3Index - 1]);
			currSeq3Index = ++currSeq3Index % Sequence3.Length;
			break;
		case AngleMethod.S1PINGPONG:
			rotate(bullet, Sequence1[currSeq1Index]);
			if(s1mirror){
				if( --currSeq1Index < 0){
					currSeq1Index += 2;
					s1mirror = false;
				}
			}
			else{
				if( ++currSeq1Index == Sequence1.Length){
					currSeq1Index -= 2;
					s1mirror = true;
				}
			}
			break;
		case AngleMethod.S2PINGPONG:
			rotate(bullet, Sequence2[currSeq2Index]);
			if(s2mirror){
				if( --currSeq2Index < 0){
					currSeq2Index += 2;
					s1mirror = false;
				}
			}
			else{
				if( ++currSeq2Index == Sequence2.Length){
					currSeq2Index -= 2;
					s2mirror = true;
				}
			}
			break;
		case AngleMethod.S3PINGPONG:
			rotate(bullet, Sequence3[currSeq3Index]);
			if(s3mirror){
				if( --currSeq3Index < 0){
					currSeq3Index += 2;
					s3mirror = false;
				}
			}
			else{
				if( ++currSeq3Index == Sequence3.Length){
					currSeq3Index -= 2;
					s3mirror = true;
				}
			}
			break;
		default:
			Debug.Log ("No rotation method for " + method);
			break;
		}
	}
	
	//Override beat functions
	new public void onMelody(float interp){
		if(melodyShot == null) return;
		GameObject bullet = fireBullet(melodyShot, mMod, interp);
		rotate(bullet, melodyAngle);
	}
	new public void onHarmony(float interp) {
		if(harmonyShot == null) return;
		GameObject bullet = fireBullet(harmonyShot, hMod, interp);
		rotate(bullet, harmonyAngle);
	}
	new public void onKick(float interp) {
		if(kickShot == null) return;
		GameObject bullet = fireBullet(kickShot, kMod, interp);
		rotate(bullet, kickAngle);
	}
	new public void onSnare(float interp) {
		if(snareShot == null) return;
		GameObject bullet = fireBullet(snareShot, sMod, interp);
		rotate(bullet, snareAngle);
	}
	new public void onHat(float interp) {
		if(hatShot == null) return;
		GameObject bullet = fireBullet(hatShot, hatMod, interp);
		rotate(bullet, hatAngle);
	}
	new public void onBass(float interp) {
		if(bassShot == null) return;
		GameObject bullet = fireBullet(bassShot, bMod, interp);
		rotate(bullet, bassAngle);
	}
	new public void onTreble(float interp) {
		if(trebleShot == null) return;
		GameObject bullet = fireBullet(trebleShot, tMod, interp);
		rotate(bullet, trebleAngle);
	}
	
	//Override beat functions
	new public void onMelody(float[]args){
		if(melodyShot == null) return;
		GameObject bullet = fireBullet(melodyShot, mMod, args[0], args[1]);
		rotate(bullet, melodyAngle);
	}
	new public void onHarmony(float[] args) {
		if(harmonyShot == null) return;
		GameObject bullet = fireBullet(harmonyShot, hMod, args[0], args[1]);
		rotate(bullet, harmonyAngle);
	}
	new public void onKick(float[] args) {
		if(kickShot == null) return;
		GameObject bullet = fireBullet(kickShot, kMod, args[0], args[1]);
		rotate(bullet, kickAngle);
	}
	new public void onSnare(float[] args) {
		if(snareShot == null) return;
		GameObject bullet = fireBullet(snareShot, sMod, args[0], args[1]);
		rotate(bullet, snareAngle);
	}
	new public void onHat(float[] args) {
		if(hatShot == null) return;
		GameObject bullet = fireBullet(hatShot, hatMod, args[0], args[1]);
		rotate(bullet, hatAngle);
	}
	new public void onBass(float[] args) {
		if(bassShot == null) return;
		GameObject bullet = fireBullet(bassShot, bMod, args[0], args[1]);
		rotate(bullet, bassAngle);
	}
	new public void onTreble(float[] args) {
		if(trebleShot == null) return;
		GameObject bullet = fireBullet(trebleShot, tMod, args[0], args[1]);
		rotate(bullet, trebleAngle);
	}
}
