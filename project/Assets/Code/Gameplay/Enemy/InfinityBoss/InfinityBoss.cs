﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class InfinityBoss : EnemyObject {

    private GameObject mainBossShip;
    private Animator mAnimator;

    List<GameObject> allOfTheOrbs;
    public GameObject orbInstance;
    public GameObject minionInstance;
    public Vector3 mainBossStartingPoint;
    public Vector3 mainBossEndingPoint;

    public AudioSource growlSound;
    
    private Vector3[] orbPoints = new Vector3[] { new Vector3(-9f, -3.5f, 0), new Vector3(-8, -6, 0), new Vector3(-6f, -8.5f, 0), new Vector3(0, -9, 0), new Vector3(6f, -8.5f, 0), new Vector3(8, -6, 0), new Vector3(9f, -3.5f, 0)};
	private Vector2[] minionPoints = new Vector2[] { new Vector2(0, -5), new Vector2(-29, 6), new Vector2(29, 6), new Vector2(-14,0), new Vector2(14, 0), new Vector2(-18, 13), new Vector2(18,13) };
	private int numMinionsSpawned = 0;
	List<GameObject> minions;
	
	private enum BossPhase { ZERO, ONE, TWO, THREE, FOUR, FIVE, SIX, SEVEN };
	private BossPhase phase = BossPhase.ZERO;
	
	private bool melody, harmony, treble, bass, kick, snare, hat;
	
	// Use this for initialization
	new void Start () {
		base.Start ();
        allOfTheOrbs = new List<GameObject>();
        minions = new List<GameObject>();
        phase = BossPhase.ZERO;
        transform.localPosition = mainBossStartingPoint;
        mAnimator = GetComponent<Animator>();
        GameManager.infinityBoss = this;
	}
	
	// Update is called once per frame
	new void Update () {
		base.Update ();
		
		foreach(GameObject minion in minions){
			if(minion == null) minions.Remove(minion);
		}
		
        
	    switch(phase){
		case BossPhase.ZERO:
			phase0 ();
			break;
		case BossPhase.ONE:
			if(GameManager.gameTimer.getTime() >= 44500)
				startPhase2();
			else
				phase1 ();
			break;
		case BossPhase.TWO:
			if(GameManager.gameTimer.getTime() >= 73400 || minions.Count == 0)
				startPhase3 ();
			else
				phase2 ();
			break;
		case BossPhase.THREE:
			if(GameManager.gameTimer.getTime () >= 98250)
				startPhase4();
			else
				phase3 ();
			break;
		case BossPhase.FOUR:
			if(GameManager.gameTimer.getTime() > 122500)
				startPhase5();
			else
				phase4 ();
			break;
		case BossPhase.FIVE:
			phase5 ();
			break;
		case BossPhase.SIX:
			phase6 ();
			break;
		case BossPhase.SEVEN:
			phase7 ();
			break;
	    }
	    
	    //Reset all beat variables
		melody = harmony = treble = bass = kick = hat = snare = false; //Holy shit nice code Trevor
	}
	
	void startPhase2(){
		
		for( ; numMinionsSpawned < 3; numMinionsSpawned++){
			GameObject temp = (GameObject)Instantiate(minionInstance);
			temp.SendMessage("setTargetPos", minionPoints[numMinionsSpawned]);
			minions.Add(temp);
		}
		
		invincible = true;
		
		phase = BossPhase.TWO;
	}
	
	void startPhase3(){
		invincible = false;
		
		phase = BossPhase.THREE;
	}
	
	void startPhase4(){
		invincible = true;
	
		for( ; numMinionsSpawned < 7; numMinionsSpawned++){
			GameObject temp = (GameObject)Instantiate(minionInstance);
			temp.SendMessage("setTargetPos", minionPoints[numMinionsSpawned]);
		}
		
		phase = BossPhase.FOUR;
	}
	
	void startPhase5(){
		invincible = false;
		
		phase = BossPhase.FIVE;
	}
	
	void spawnOrb(){
		//Create an orb
		if(orbInstance != null)
		{
			if(allOfTheOrbs.Count == 7)
			{
				//Move towards player
				foreach(GameObject orb in allOfTheOrbs)
				{
					if(orb != null)
						orb.GetComponent<Orb>().moveTowardsPlayer = true;
				}
				allOfTheOrbs.Clear();
			}
			else{
				//Vector3 tempVector = new Vector3(transform.position.x, transform.position.y, transform.position.z + 1);
				//Vector3 tempVector = orbPoints[allOfTheOrbs.Count];
				GameObject newOrb = (GameObject)Instantiate(orbInstance, transform.position + orbPoints[allOfTheOrbs.Count], Quaternion.identity);
				allOfTheOrbs.Add (newOrb);
			}
		}
	}


    /* Simply have the mainboss come in
     * And Growl
     */
    void phase0()
    {
        transform.position += transform.up * -1 * (Time.deltaTime * 3);
        if (transform.position.y < mainBossEndingPoint.y)
        {
            phase = BossPhase.ONE;
            mAnimator.Play("InfinityBossShake", -1, 0);
            growlSound.Play();
        }
        
        
    }
    

    /* Orb Spawning on every kick
     * Asteroid spawning on melody
     */
    void phase1()
    {
        if(bass){
			spawnOrb();
        }
        
        if(melody){
        	if(GameManager.spawner != null){
        		GameManager.spawner.SendMessage("spawnEnemy");
        	}
        }
    }

	
    void phase2()
    {
		if(bass){
			spawnOrb ();
		}
		
		if(treble){
			foreach(GameObject minion in minions){
				minion.SendMessage ("spawnZergling");
			}
		}
    }

    void phase3()
    {
		if(bass){
			spawnOrb ();
		}
		if(melody){
			if(GameManager.spawner != null){
				GameManager.spawner.SendMessage("spawnEnemy");
			}
		}
		if(kick){
			foreach(GameObject minion in minions){
				minion.SendMessage ("fireLaser");
			}
		}
    }

    void phase4()
    {
		if(bass){
			spawnOrb ();
			bass = false;
		}
		if(treble){
			foreach(GameObject minion in minions){
				minion.SendMessage ("spawnZergling");
			}
		}
    }

    void phase5()
    {
		if(bass){
			spawnOrb ();
			bass = false;
		}
    }

    void phase6()
    {

    }

    void phase7()
    {

    }

    new public void onBeat(GD type, float interp)
    {
        switch (type)
        {
            case GD.HAT:
                hat = true;
                break; // =)
            case GD.BASS:
                bass = true;
                break; // =)
            case GD.MELODY:
                melody = true;
                break; // =)
            case GD.KICK:
                kick = true;
                break; // =)
            case GD.HARMONY:
                harmony = true;
                break; // =)
            case GD.SNARE:
                snare = true;
                break; // =(
            case GD.TREBLE:
                treble = true;
                break;
        }
    }
}
