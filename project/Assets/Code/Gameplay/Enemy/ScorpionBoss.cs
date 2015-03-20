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
    
    //Rotational shtuff
    private float zRot;

    private bool test = false;
    private bool test2 = false;

    private float secondsPassed;

    // Use this for initialization
    void Start()
    {
        secondsPassed = 0;
        
    }

    // Update is called once per frame
    new void Update()
    {
        if (secondsPassed > 64 && !test2)
        {
            startMoving = true;
            doneInitialMoving = false;
            movingLeftAndRight = 0;
            transform.position = startPoint;
            target = GameManager.player.gameObject.transform.position;
            test2 = true;
        }
        base.Update();
        if (!doneInitialMoving && startMoving) { enterTheScreen(); }
        if (doneInitialMoving) { getCray(); }
        secondsPassed += Time.deltaTime;
        Debug.Log("Seconds passed = " + secondsPassed);
    }

    //Initial moving
    void enterTheScreen()
    {
        transform.position += transform.up * (Time.deltaTime * 5);
        if (transform.position.y < endPoint.y)
            doneInitialMoving = true;
    }

    void getCray()
    {
        moveLeftAndRight();
        //transform.LookAt(target);
        //rotate to face the target point
        //zRot = Mathf.Atan2(target.y - transform.position.y, target.x - transform.position.x) * Mathf.Rad2Deg;
        //transform.rotation = Quaternion.Euler(0f, 0f, zRot - 90);
        rotateToward(GameManager.player.transform.position);
    }

    void moveLeftAndRight()
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

    new void OnTriggerEnter2D(Collider2D coll)
    {
        base.OnTriggerEnter2D(coll);

        //Debug.Log("" + gameObject.name + " Entered collision");
        if (test)
        {
            renderer.material.color = new Color(1.0f, 0.0f, 0.0f);
            test = false;
        }
        else
        {
            renderer.material.color = new Color(1.0f, 1.0f, 1.0f);
            test = true;
        }

        if (coll.tag.Equals("Player"))
        {
            coll.gameObject.SendMessage("takeDamage", strength);
            takeDamage(strength);
        }

        

    }
}
