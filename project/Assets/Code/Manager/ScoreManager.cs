using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour {

	Text scoreText;
	
	// Use this for initialization
	void Start () {
		scoreText = gameObject.GetComponent<Text>();
	}
	
	// Update is called once per frame
	void Update () {
		scoreText.text = "" + GameManager.score;
	}
}
