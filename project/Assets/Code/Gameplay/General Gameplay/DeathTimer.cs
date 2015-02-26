using UnityEngine;
using System.Collections;

public class DeathTimer : MonoBehaviour {

	public float timeToDeath = 3;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		timeToDeath -= Time.deltaTime;
		if(timeToDeath <= 0)
			Destroy(gameObject);
	}
}
