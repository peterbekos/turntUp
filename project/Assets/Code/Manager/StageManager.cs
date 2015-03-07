using UnityEngine;
using System.Collections;

/*
 * This class is used to manage game screens and stages
 */
public static class StageManager {

	public static void ping() {}

	static StageManager() {
		init ();
	}
	
	//variables
	
	
	//initalization function
	private static void init() {
		
	}

	public static void configureEnemies() {
		//TODO - this method will be called at the begining of the stage
		// What it will do is determine what enemies will spawn when
		// from the BeatManager
	}

	public static void spawnEnemies(float gameTime) {
		//TODO - spawn enemies based off enemy configureation
		EnemyObject enemy = new EnemyObject ();
		//TODO - regiester enemies in GameManager
		GameManager.enemies.Add (enemy);
	}
}
