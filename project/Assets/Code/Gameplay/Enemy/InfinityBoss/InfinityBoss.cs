using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class InfinityBoss : EnemyObject {

    private GameObject mainBossShip;
    private Animator mAnimator;

    List<GameObject> allOfTheOrbs;
    public GameObject orbInstance;
    public GameObject minionInstance;
    public GameObject chainInstance;
    public GameObject laserInstance;
    public Vector3 mainBossStartingPoint;
    public Vector3 mainBossEndingPoint;

    public AudioSource growlSound;
    public AudioSource murmurSound;
    public AudioSource dieSound;
    
    private Vector3[] orbPoints = new Vector3[] { new Vector3(-9f, -3.5f, 0), new Vector3(-8, -6, 0), new Vector3(-6f, -8.5f, 0), new Vector3(0, -9, 0), new Vector3(6f, -8.5f, 0), new Vector3(8, -6, 0), new Vector3(9f, -3.5f, 0)};
	private Vector2[] minionPoints = new Vector2[] { new Vector2(0, -5), new Vector2(-29, 6), new Vector2(29, 6), new Vector2(-14,0), new Vector2(11, 0), new Vector2(-18, 13), new Vector2(18,13) };
	public SpriteRenderer[] lights = new SpriteRenderer[7];
	private int numMinionsSpawned = 0;
	private int numLaserCharges = 0;
	List<GameObject> minions;
	
	public enum BossPhase { ZERO, ONE, TWO, THREE, FOUR, FIVE, SIX, SEVEN };
	public BossPhase phase = BossPhase.ZERO;
	
	private bool melody, harmony, treble, bass, kick, snare, hat;
	
	private float phase6Cooldown = 0; //cooldown between lasers to reduce performance demand
	
	// Use this for initialization
	new void Start () {
		base.Start ();
        allOfTheOrbs = new List<GameObject>();
        minions = new List<GameObject>();
        phase = BossPhase.ZERO;
        transform.localPosition = mainBossStartingPoint;
        mAnimator = GetComponent<Animator>();
        mAnimator.enabled = false;
        GameManager.infinityBoss = this;
	}
	
	// Update is called once per frame
	new void Update () {
		base.Update ();
        
		if(mAnimator.enabled){
			if(mAnimator.GetCurrentAnimatorStateInfo(0).IsName("EmptyStateDONOTDELETE") && phase != BossPhase.SEVEN){
				mAnimator.StopPlayback();
				mAnimator.enabled = false;
				//Debug.Log ("Animation Stopped");
				transform.rotation = Quaternion.identity;
			}
			else if(mAnimator.GetCurrentAnimatorStateInfo(0).IsTag("Rekt")){
				Destroy (gameObject);
			}
		}
		
		if(minions.Count > 0){
			bool minionsDead = true;
			foreach(GameObject minion in minions){
				if(minion != null) {
					minionsDead = false;
					break;
				}
			}
			if(minionsDead) minions.Clear();
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
			if(GameManager.gameTimer.getTime () >= 118250)
				startPhase4();
			else
				phase3 ();
			break;
		case BossPhase.FOUR:
			if(minions.Count == 0)
				startPhase5();
			else
				phase4 ();
			break;
		case BossPhase.FIVE:
			if(GameManager.gameTimer.getTime() > 203000){
				startPhase6 ();
			}
			else
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
		melody = harmony = treble = bass = kick = hat = snare = false; //Holy shit nice code Trevor //lol
	}
	
	void startPhase2(){
		mAnimator.enabled = true;
        mAnimator.Play("InfinityBossTwist", -1, 0);
        murmurSound.Play();

		for( ; numMinionsSpawned < 3; numMinionsSpawned++){
			spawnMinion ();
			
		}
        
		invincible = true;
		
		phase = BossPhase.TWO;
	}
	
	void spawnMinion(){
		GameObject temp = (GameObject)Instantiate(minionInstance);
		temp.SendMessage("setTargetPos", minionPoints[numMinionsSpawned]);
		minions.Add(temp);
		
		GameObject newChain = (GameObject)Instantiate(chainInstance);
		newChain.GetComponentInChildren<ChainAttacher>().setTarget(temp);
	}
	
	void startPhase3(){
		invincible = false;
		growl ();
		phase = BossPhase.THREE;
	}
	
	void startPhase4(){
		invincible = true;
	
		for( ; numMinionsSpawned < 7; numMinionsSpawned++){
			spawnMinion ();
		}
		
		phase = BossPhase.FOUR;
	}
	
	void startPhase5(){
		invincible = false;
		growl ();
		phase = BossPhase.FIVE;
	}
	
	void startPhase6(){
		phase = BossPhase.SIX;
	}
	
	void startPhase7(){
		phase = BossPhase.SEVEN;
		foreach(GameObject orb in allOfTheOrbs){
			Destroy (orb);
		}
		foreach(GameObject chain in GameObject.FindGameObjectsWithTag("Chain")){
			//SendMessage("DestroyChain");
			Destroy (chain);
		}
		mAnimator.enabled = true;
		mAnimator.Play("InfinityBossDie", -1, 0);
		dieSound.Play();
		collider2D.enabled = false;
		GameObject.Find("Random Enemy Spawner").GetComponent<EnemySpawner>().timeBetweenSpawns = .25f;
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
            growl ();
        }
        
        
    }
    
    void growl(){
		mAnimator.enabled = true;
		mAnimator.Play("InfinityBossShake", -1, 0);
		growlSound.Play();
    }

    /* Orb Spawning on every kick
     * Asteroid spawning on melody
     */
    void phase1()
    {
    	transform.rotation = Quaternion.identity;
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
			Debug.Log ("Spawn Zerglings!");
			foreach(GameObject minion in minions){
				if(minion != null){
					minion.SendMessage ("spawnZergling");
				}
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
				if(minion != null){
					minion.SendMessage ("fireLaser");
				}
			}
		}
    }

    void phase4()
    {
		if(bass){
			spawnOrb ();
		}
		if(treble || bass || harmony){
			foreach(GameObject minion in minions){
				if(minion != null){
					minion.SendMessage ("spawnZergling");
				}
			}
		}
    }

    void phase5()
    {
		if(bass){
			spawnOrb ();
			Debug.Log ("Spawn Orb");
		}
		if(kick){
			numLaserCharges++;
			
			switch(numLaserCharges){
			case 0:
				for(int i = 0; i < lights.Length; i++){
					lights[i].enabled = false;
				}
				break;
			case 32:
				lights[6].enabled = true;
				Instantiate(laserInstance, new Vector3(0, 0, 0), Quaternion.Euler(new Vector3(0,0,180)));
				Instantiate(laserInstance, new Vector3(-3, 0, 1), Quaternion.Euler(new Vector3(0,0,135)));
				Instantiate(laserInstance, new Vector3(3, 0, 1), Quaternion.Euler(new Vector3(0,0,225)));
				Instantiate(laserInstance, new Vector3(-10, 10, 1), Quaternion.Euler(new Vector3(0,0,90)));
				Instantiate(laserInstance, new Vector3(10, 10, 1), Quaternion.Euler(new Vector3(0,0,270)));
				numLaserCharges = 0;
				for(int i = 0; i < lights.Length; i++){
					lights[i].enabled = false;
				}
				break;
			case 24:
				lights[5].enabled = lights[4].enabled = true;
				break;
			case 16:
				lights[3].enabled = lights[2].enabled = true;
				break;
			case 8:
				lights[0].enabled = lights[1].enabled = true;
				break;
			}
		}
    }

    void phase6(){
		if(GameManager.gameTimer.getTime() < 212000){
			if(phase6Cooldown <= 0)
			{
				//spawn a random laser at a random location every frame
				float curveLocation = Random.Range (Mathf.PI,2* Mathf.PI);
				Instantiate(laserInstance, new Vector3(10* Mathf.Cos(curveLocation), 10 * Mathf.Sin(curveLocation) + 10, 1), Quaternion.Euler(new Vector3(0,0, curveLocation * Mathf.Rad2Deg - 90)));
				phase6Cooldown = .05f;
			}
			else {
				phase6Cooldown -= Time.deltaTime;
			}
		}
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

    new public void takeDamage(int dmg)
    {
        //Debug.Log("Took sum dmg broski");
        if (hitpoints - dmg <= 0)
        {
        	
            startPhase7();
            
            hitpoints = 0;
            
			//play the death sound and kill the object
			if(deathAnimation != null) Instantiate(deathAnimation, transform.position, Quaternion.identity);
			GameManager.score += scoreOnKill;
		}
		else {
        	base.takeDamage(dmg);
        }
        
    }
}
