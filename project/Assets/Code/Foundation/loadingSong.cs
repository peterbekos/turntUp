using UnityEngine;
using System.Collections;
using System.IO;
using UnityEditor;
public class myScript : MonoBehaviour {
	// Use this for initialization
	public AudioClip [] MyAudio = new AudioClip[5];
	public AudioClip toPlay;
	//var showText = true;
	//TextArea textArea = new TextArea("files to select",100);
	string chosenFile;
	int selGridInt;
	void Start () {  // get all the files in the directory,and convert to strings
		StartCoroutine(loadAudio());
	}

	 IEnumerator loadAudio()
	{
		string path = EditorUtility.OpenFilePanel ("Load mp3 files from Directory" ,@Application.dataPath, "");
		Debug.Log (path);
		WWW audioLoader = new WWW ("file://" + path);
		yield return audioLoader;
		toPlay= audioLoader.GetAudioClip(false);
	}
	
	/*
		while (true) {
			AudioSource.PlayClipAtPoint(MyAudio, new Vector3(5,1,2));
			//
			Debug.Log(transoform.position);
			example eventHappened = new example();
		}
		*/
	     
// Update is called once per frame
void Update () {
		Debug.Log("in Update");
				if (Input.GetMouseButtonDown (0)){
						Debug.Log ("Pressed left click.");
						AudioSource source = GetComponent<AudioSource>();
						source.clip = toPlay;
						source.Play();

			//(toPlay, transform.position);
						//example myexample = new example();
				}
  }

}
/*
public class example : MonoBehaviour {
	IEnumerator Example() {
		yield return new WaitForSeconds(5.0F);
	}
}
*/