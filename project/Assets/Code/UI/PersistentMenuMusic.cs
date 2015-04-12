using UnityEngine;
using System.Collections;

public class PersistentMenuMusic : MonoBehaviour {

	// Use this for initialization
	void Awake () {
		if (GameManager.menuMusic != null){
			Destroy (gameObject);
			return;
		}
		GameManager.menuMusic = gameObject;
		DontDestroyOnLoad(transform.gameObject);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
