using UnityEngine;
using System.Collections;

public class EnemySpawnerController : MonoBehaviour {

	EnemySpawner spawner;
	
	public enum changetypes { enemySpawnRate, addEnemy, removeEnemy, spawnEnemy, spawnEnemyAtPoint };
	
	public GameObject enemyToSpawn;
	
	public int nextChange = 0;
	
	public Change[] changes = new Change[8];

	// Use this for initialization
	void Start () {
		GameManager.spawnController = this;
		spawner = gameObject.GetComponent<EnemySpawner>();
		
		changes[0] = new Change( 50000f, changetypes.enemySpawnRate, .1f );
		changes[1] = new Change( 60000f, changetypes.enemySpawnRate, 0f);
		changes[2] = new Change( 88000f, changetypes.enemySpawnRate, .25f);
		changes[3] = new Change( 124000f, changetypes.enemySpawnRate, .1f);
		changes[4] = new Change( 138000f, changetypes.spawnEnemy, enemyToSpawn);
		changes[5] = new Change( 138000f, changetypes.enemySpawnRate, .06f);
		changes[6] = new Change( 165000f, changetypes.enemySpawnRate, 1f);
		changes[7] = new Change( 180000f, changetypes.enemySpawnRate, 100f);
	}
	
	//read change list and apply any changes that need to be made
	public void checkChanges(float time){
		while(nextChange < changes.Length && changes[nextChange].time <= time ){
			Change c = changes[nextChange];
		
			switch(c.type)
			{
				case changetypes.enemySpawnRate: //change the spawn rate of base enemies
					spawner.timeBetweenSpawns = c.spawnRate;
					Debug.Log("Changing spawn rate to " + c.spawnRate);
					break;
				
				case changetypes.addEnemy: //add an enemy to the list
					//TODO: add an enemy to the spawnable list
					Debug.Log ("Adding enemy " + c.enemy.name + " to spawn list.");
					break;
				
				case changetypes.removeEnemy: //remove an enemy from the list
					//TODO: remove an enemy from the spawnable list
				Debug.Log ("Removing enemy " + c.enemy.name + " from spawn list.");
					break;
				
				case changetypes.spawnEnemy: //spawn an enemy directly
					spawner.spawnEnemy(c.enemy);
					Debug.Log("Spawning enemy " + c.enemy.name + ".");
					break;
				case changetypes.spawnEnemyAtPoint: //spawn an enemy at a specific point
					spawner.spawnEnemy(c.enemy, c.point);
					Debug.Log("Spawning enemy " + c.enemy.name + " at point " + c.point + ".");
					break;
			}
			
			nextChange++;
		}
	}
}

public class Change {
	public float time;
	public EnemySpawnerController.changetypes type;
	public GameObject enemy;
	public Vector3 point;
	public float spawnRate;
	
	public Change(float f, EnemySpawnerController.changetypes ct, GameObject e, Vector3 v){
		time = f;
		type = ct;
		enemy = e;
		point = v;
	}
	
	public Change(float f, EnemySpawnerController.changetypes ct, GameObject e){
		time = f;
		type = ct;
		enemy = e;
	}
	
	public Change(float f, EnemySpawnerController.changetypes ct, float sr){
		time = f;
		type = ct;
		spawnRate = sr;
	}
}




