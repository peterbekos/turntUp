using UnityEngine;
using System.Collections;

public class ScorpionPartCockpit : ScorpionPart {

    public GameObject wholeScorpionBody;
	// Use this for initialization
	void Start () 
    {
        mSprintRenderer = (SpriteRenderer)this.gameObject.GetComponent("SpriteRenderer");
        defaultColor = mSprintRenderer.color;
	}
	
	// Update is called once per frame
	void Update () 
    {
        base.flash();
	}
    
    new void OnTriggerEnter2D(Collider2D coll)
    {
        base.activateTrigger(coll);
        if(hitpoints <= 0)
        {
            Destroy(wholeScorpionBody);
        }
    }
}
