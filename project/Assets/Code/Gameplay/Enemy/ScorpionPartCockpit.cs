using UnityEngine;
using System.Collections;

public class ScorpionPartCockpit : ScorpionPart {

    public GameObject wholeScorpionBody;
    
	// Use this for initialization
	new public void Start () 
    {
    	base.Start ();
	}
    
    new public void OnTriggerEnter2D(Collider2D coll)
    {
        base.OnTriggerEnter2D(coll);
        if(hitpoints <= 0)
        {
            Destroy(wholeScorpionBody);
        }
    }
}
