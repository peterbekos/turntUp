using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class HighScoreManager : MonoBehaviour {

	public HighScoreTable hscores = new HighScoreTable();
	
	void Awake(){
		GameManager.scoretable = this;
		enableAll(false);
	}
	
	// Use this for initialization
	public void displayScores () {
		enableAll(true);
		hscores.loadScores(GameManager.gameTimer.levelName);
		GameObject.Find("HighScoreDisplay").GetComponent<Text>().text = "" + hscores.getScore(1);
		for(int i = 1; i <= 10; i++){
			GameObject.Find("Score" + i).GetComponent<Text>().text = "" + hscores.getScore(i);
			GameObject.Find("Name" + i).GetComponent<Text>().text = hscores.getName(i);
		}
	}
	
	public void enableAll(bool enable){
		gameObject.SetActive(enable);
		
		foreach(Transform t in gameObject.GetComponentInChildren<Transform>()){
			t.gameObject.SetActive(enable);
		}
		
		/*for(int i = 1; i <= 10; i++){
			GameObject.Find ("" + i).SetActive(true);
			GameObject.Find("Score" + i).SetActive(true);
			GameObject.Find("Name" + i).SetActive(true);
		}*/
	}
	
	public HighScoreTable getScoreTable(){
		return hscores;
	}
}
