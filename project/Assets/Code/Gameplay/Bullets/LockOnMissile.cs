using UnityEngine;
using System.Collections;

public class LockOnMissile : ShotObject {

	private enum phase { exit, lockOn, attacking }
	private phase currPhase = phase.exit;
	
	public float launchSpeed = 3f;
	public float launchDuration = 2f;
	
	public float maxRotationPerSecond = 180f;
	
	private float zRot;
	private GameObject lockTarget;
	
	// Update is called once per frame
	new void Update () {
		base.Update();
	
		switch(currPhase){
		case phase.exit: // missile has just been launched and is delayed before targeting
			transform.position += transform.up * launchSpeed * Time.deltaTime;
			launchDuration -= Time.deltaTime;
			
			if(launchDuration <= 0){
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
			
			zRot = Mathf.Atan2(lockTarget.transform.position.y - transform.position.y, lockTarget.transform.position.x - transform.position.x) * Mathf.Rad2Deg;
			
			//if( transform.rotation.z - zRot > maxRotationPerSecond / Time.deltaTime ) zRot = maxRotationPerSecond / Time.deltaTime;
			
			transform.rotation = Quaternion.Euler (0f, 0f, zRot - 90);
			
			transform.position += transform.up * speed * Time.deltaTime;
			
			break;
		}
	}
}
