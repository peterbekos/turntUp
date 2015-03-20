using UnityEngine;
using System.Collections;

public class ScorpionPart : EnemyObject {


    private bool flashVar = false;
    private int repeatStart = 2;
    private int repeatTemp = 0;
    protected Color defaultColor;

    protected SpriteRenderer mSprintRenderer;

	// Use this for initialization
	void Start () {
        
       // mSprintRenderer = this.GetComponent(SpriteRenderer);
        mSprintRenderer = (SpriteRenderer)this.gameObject.GetComponent("SpriteRenderer");
        defaultColor = mSprintRenderer.color;
	}
	
	// Update is called once per frame
	void Update () {
        flash();
	}
    new void OnTriggerEnter2D(Collider2D coll)
    {
        base.OnTriggerEnter2D(coll);
        if (coll.tag.Equals("Player"))
        {
            //Debug.Log("" + gameObject.name + " Entered collision");
            
            coll.gameObject.SendMessage("takeDamage", strength);
            takeDamage(strength);
        }
        else if (coll.tag.Equals("Bullet"))
        {
            flashVar = true;
            coll.gameObject.SendMessage("takeDamage", strength);
            takeDamage(strength);
        }
    }

    public void activateTrigger(Collider2D coll)
    {
        OnTriggerEnter2D(coll);
    }
    public void flash()
    {
        if(flashVar)
        {
            mSprintRenderer.color = new Color(1.0f, 0.0f, 0.0f);
            flashVar = false;
            repeatTemp = repeatStart;
        }
        else if(repeatTemp > 0)
        {
            mSprintRenderer.color = new Color(1.0f, 0.0f, 0.0f);
            repeatTemp--;
            flashVar = false;
        }
        else
        {
            mSprintRenderer.color = new Color(1.0f, 1.0f, 1.0f);
        }
    }
}
