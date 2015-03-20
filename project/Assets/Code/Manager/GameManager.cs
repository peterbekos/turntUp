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
	
	//initalization function
	private static void init() {
		
	}
}
