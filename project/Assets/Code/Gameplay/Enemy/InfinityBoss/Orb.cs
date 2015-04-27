using UnityEngine;
using System.Collections;

public class Orb : ShotObject {

    public bool moveTowardsPlayer;
    private bool foundPlayer;
    private Vector3 firstPlayerPosition;

	// Use this for initialization
	new void Start () {
        moveTowardsPlayer = false;
        foundPlayer = false;

	}
	
	// Update is called once per frame
	new void Update () {
        float step = speed * Time.deltaTime;
	    if(moveTowardsPlayer)
        {
            if(foundPlayer)
            {
                //transform.position = Vector3.MoveTowards(transform.position, firstPlayerPosition, step);
                transform.position += transform.up * speed;
            }
            else
            {
                //firstPlayerPosition = GameManager.player.transform.position;
                if(GameManager.player != null){
                	rotateToward(GameManager.player.gameObject);
                }
                else{
                	rotateToward(new Vector3(0, -10, 0));
                }
                foundPlayer = true;
            }
        }
	}

    public void flyTowardsPlayer()
    {
        moveTowardsPlayer = true;
    }

}
