using UnityEngine;
using System.Collections;

public class Orb : EnemyObject {

    public bool moveTowardsPlayer;
    private bool foundPlayer;
    private Vector3 firstPlayerPosition;

	// Use this for initialization
	void Start () {
        moveTowardsPlayer = false;
        foundPlayer = false;

	}
	
	// Update is called once per frame
	void Update () {
        float step = speed * Time.deltaTime;
	    if(moveTowardsPlayer)
        {
            if(foundPlayer)
            {
                transform.position = Vector3.MoveTowards(transform.position, firstPlayerPosition, step);
            }
            else
            {
                firstPlayerPosition = GameManager.player.transform.position;
                foundPlayer = true;
            }
        }
        else
        {
            transform.position = transform.forward * step;
        }
	}

    public void flyTowardsPlayer()
    {
        moveTowardsPlayer = true;
    }

}
