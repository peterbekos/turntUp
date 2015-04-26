using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class InfinityBoss : EnemyObject {

    float time;
    private GameObject mainBossShip;
    private Animator mAnimator;

    List<Orb> allOfTheOrbs;
    public Orb orbInstance;
    public Vector3 mainBossStartingPoint;
    public Vector3 mainBossEndingPoint;

    public AudioSource growlSound;

    private bool doneWithPhase0;
    private bool doneWithPhase1;

	// Use this for initialization
	void Start () {
        allOfTheOrbs = new List<Orb>();
        doneWithPhase0 = false;
        time = 0;
        transform.localPosition = mainBossStartingPoint;
        mAnimator = GetComponent<Animator>();
        GameManager.infinityBoss = this;
	}

    new public void onKick(float interp)
    {
        base.onKick();
        
        //Create an orb
        if(orbInstance != null)
        {
            Vector3 tempVector = new Vector3(transform.position.x, transform.position.y, transform.position.z + 1);
            Orb newOrb = (Orb)Instantiate(orbInstance, tempVector, transform.rotation);
            allOfTheOrbs.Add(newOrb);
            if(allOfTheOrbs.Count > 7)
            {
                //Move towards player
                foreach(Orb orb in allOfTheOrbs)
                {
                    orb.moveTowardsPlayer = true;
                }
                allOfTheOrbs.Clear();
            }
        }

    }
	
	// Update is called once per frame
	void Update () {
        time += Time.deltaTime;
        
	    if(!doneWithPhase0)
        {
            phase0();
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
            doneWithPhase0 = true;
            mAnimator.Play("InfinityBossShake", -1, 0);
            growlSound.Play();
        }
    }
    

    /* Orb Spawning on every kick
     * Asteroid spawning on melody
     */
    void phase1()
    {
        
    }

    void phase2()
    {

    }

    void phase3()
    {

    }

    void phase4()
    {

    }

    void phase5()
    {

    }

    void phase6()
    {

    }

    void phase7()
    {

    }

    new public void onBeat(GD type, float interp)
    {
        //base.onBeat();
        switch (type)
        {
            case GD.HAT:
                //onHat(interp);
                break; // =)
            case GD.BASS:
                //onBass(interp);
                break; // =)
            case GD.MELODY:
                //onMelody(interp);
                break; // =)
            case GD.KICK:
                onKick(interp);
                break; // =)
            case GD.HARMONY:
                //onHarmony(interp);
                break; // =)
            case GD.SNARE:
                //onSnare(interp);
                break; // =(
            case GD.TREBLE:
                //onTreble(interp);
                break;
        }
    }


}
