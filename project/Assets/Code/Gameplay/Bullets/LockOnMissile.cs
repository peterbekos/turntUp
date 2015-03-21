using UnityEngine;
using System.Collections;

public class LockOnMissile : ShotObject {

	private enum phase { exit, lockOn, attacking }
	private phase currPhase = phase.exit;
	
	public float launchSpeed = 3f;
	
	public float maxRotationPerSecond = 180f;
	
	private float zRot;
	private GameObject lockTarget;
	
	// Update is called once per frame
	new void Update () {
		base.Update();
	
		switch(currPhase){
		case phase.exit: // missile has just been launched and is delayed before targeting
			transform.position += transform.up * launchSpeed * Time.deltaTime;
			duration -= Time.deltaTime * 1000;
			
			if(duration <= 0){
				currPhase = phase.lockOn;
			}
			break;
		case phase.lockOn: //find a target
			GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
			
			if (enemies.Length == 0) break;
			
			lockTarget = enemies[Random.Range(0, enemies.Length)];
			if(lockTarget != null) currPhase = phase.attacking;
			break;
		case phase.attacking: //rotate and move toward target
			if(lockTarget == null)
			{
				currPhase = phase.lockOn;
				break;
			}
			
			rotateToward(lockTarget);
			
			transform.position += transform.up * speed * Time.deltaTime;
			
			break;
		}
	}
	
	new public void setDuration(float dur){
		base.setDuration(dur);
		duration *= .75f;
	}
}
