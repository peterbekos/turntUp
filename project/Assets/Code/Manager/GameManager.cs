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
    public static InfinityBoss infinityBoss;
	public static EnemySpawnerController spawnController;
	public static int score = 0;
	public static int durationForHold = 500;
	public static GameTimer gameTimer;
	public static float realTimeStageStarted = 0;
	public static GameObject menuMusic;
	public static HighScoreManager scoretable;
	public static string nextLevel;
    
	
	//initalization function
	private static void init() {
		
	}
	
	public static float getTime(){
		return gameTimer.getTime();
	}
	
	public static void endLevel(){
		GameObject.Find("Random Enemy Spawner").SetActive(false); //stop spawning enemies
		gameTimer.stop ();
		checkScoreAndSave(gameTimer.levelName, score, "DEV");
	}
	
	public static void checkScoreAndSave(string filename, int score, string name){
		scoretable.getScoreTable().loadScores(filename);
		scoretable.getScoreTable().addScore(score, name);
		scoretable.getScoreTable().saveScores(filename);
		scoretable.displayScores();
		Debug.Log("Saved");
	}
}

[System.Serializable]
public static class PlayerConfig {

	public static GameObject body;
	public static GameObject wing;
	public static GameObject booster;

}