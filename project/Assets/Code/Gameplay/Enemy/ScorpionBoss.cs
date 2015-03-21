using UnityEngine;
using System.Collections;

public class ScorpionBoss : EnemyObject {

   
    public Vector3 startPoint = new Vector3(0.2f, 35.91f, 10.00f);
    public Vector3 endPoint = new Vector3(0.2f, 7.2f, 10.0f);
    private Vector3 target;

    //Used for when boss enters
    private bool startMoving;
    private bool doneInitialMoving;

    //When boss moves after entering
    private short movingLeftAndRight;

    private float secondsPassed;
    
    private GameObject cockpit;

    // Use this for initialization
    new public void Start()
    {
    	cockpit = GameObject.Find ("Cockpit");
        secondsPassed = 0;
		startMoving = true;
		doneInitialMoving = false;
		movingLeftAndRight = 0;
		transform.position = startPoint;
		rotateToward(endPoint);
		target = GameManager.player.gameObject.transform.position;
        
    }

    // Update is called once per frame
    new public void Update()
    {
    	if(cockpit == null) Destroy (gameObject);
        base.Update();
        
        if (!doneInitialMoving && startMoving) { enterTheScreen(); }
        if (doneInitialMoving) { getCray(); }
        secondsPassed += Time.deltaTime;
        //Debug.Log("Seconds passed = " + secondsPassed);
    }

    //Initial moving
    protected void enterTheScreen()
    {
        transform.position += transform.up * (Time.deltaTime * 5);
        if (transform.position.y < endPoint.y)
            doneInitialMoving = true;
    }

    protected void getCray()
    {
        moveLeftAndRight();
        rotateToward(GameManager.player.transform.position);
    }

    protected void moveLeftAndRight()
    {
        if (movingLeftAndRight == 0)
        {
            transform.Translate(new Vector3(-1 * speed, 0, 0) * Time.deltaTime * 5, Space.World);
            if(transform.position.x < -20)
            {
                movingLeftAndRight = 1;
            }
        }
        else
        {
            transform.Translate(new Vector3(1 * speed, 0, 0) * Time.deltaTime * 5, Space.World);
            if (transform.position.x > 20)
            {
                movingLeftAndRight = 0;
            }
        }
    }

    new public void OnTriggerEnter2D(Collider2D coll)
    {
        base.OnTriggerEnter2D(coll);

        if (coll.tag.Equals("Player"))
        {
            coll.gameObject.SendMessage("takeDamage", strength);
            takeDamage(strength);
        }
    }
}
