using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

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
	
	static PlayerConfig pConfig = new PlayerConfig();
	
	//initalization function
	private static void init() {
		
	}
	
	public static float getTime(){
		return gameTimer.getTime();
	}
	
	public static GameObject getBody(){
		return pConfig.getBody();
	}
	public static void setBody(GameObject o){
		pConfig.setBody(o);
	}
	public static GameObject getWing(){
		return pConfig.getWing();
	}
	public static void setWing(GameObject o){
		pConfig.setWing(o);
	}
	public static GameObject getBooster(){
		return pConfig.getBooster();
	}
	public static void setBooster(GameObject o){
		pConfig.setBooster(o);
	}
	
	public static void SavePlayer(){
		BinaryFormatter bf = new BinaryFormatter();
		FileStream file = File.Create(Application.persistentDataPath + "/savegame.dat");
		
		bf.Serialize(file, pConfig);
		file.Close();
		
		Debug.Log ("Saved!");
	}
	
	public static void LoadPlayer(){
		if(File.Exists(Application.persistentDataPath + "/savegame.dat"))
		{
			BinaryFormatter bf = new BinaryFormatter();
			FileStream file = File.Open(Application.persistentDataPath + "/savegame.dat", FileMode.Open);
			
			pConfig = (PlayerConfig)bf.Deserialize(file);
			file.Close();
		}
	}
}

[Serializable]
class PlayerConfig {

	GameObject body;
	GameObject wing;
	GameObject booster;

	public GameObject getBody(){
		return body;
	}
	
	public void setBody (GameObject b){
		body = b;
	}

	public GameObject getWing(){
		return wing;
	}
	
	public void setWing(GameObject w){
		wing = w;
	}
	
	public GameObject getBooster(){
		return booster;
	}
	
	public void setBooster(GameObject b){
		booster = b;
	}
}