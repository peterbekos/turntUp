using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class LoadScreen : MonoBehaviour {

	Text text;
	
	void Start () {
		text = gameObject.GetComponent<Text>();
		//StartCoroutine("AnimateText");
		Application.LoadLevel(GameManager.nextLevel);
	}
	
	IEnumerator AnimateText(){
		int i =0;
		string dots = "";
		while(true){
			i = i%3;
			dots = "";
			for(int j = 0; j <= i; j++){
				dots+= ".";
			}
			text.text = "Loading[" + dots;
		}
	}
}
