using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/*
 * This class is used to manage gameplay and keep track of data
 */
public static class GameManager {

	public static void ping() {}

	static GameManager() {
		init ();
	}
	
	//variables
    public static NoteBar notebar;
	public static PlayShip player;
	public static List<EnemyObject> enemies;
	public static EnemySpawnerController spawnController;
	public static int score = 0;
	public static int durationForHold = 500;
	public static GameTimer gameTimer;
	public static float realTimeStageStarted = 0;

	
	//initalization function
	private static void init() {
		
	}
	
	public static float getTime(){
		return gameTimer.getTime();
	}
}

[System.Serializable]
public static class PlayerConfig {

	public static GameObject body;
	public static GameObject wing;
	public static GameObject booster;

}