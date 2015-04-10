using UnityEngine;
using System.Collections;

public class SplashScreen : MonoBehaviour {

	public float timeToMenu = 3f;

	// Use this for initialization
	void Start () {
		//GameManager.LoadPlayer ();
	}
	
	// Update is called once per frame
	void Update () {
		timeToMenu -= Time.deltaTime;
		if(timeToMenu <=0) Application.LoadLevel(1);
	}
}
