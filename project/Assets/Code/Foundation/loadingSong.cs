using UnityEngine;
using System.Collections;
using System.IO;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class myScript : MonoBehaviour {
	public AudioClip [] MyAudio = new AudioClip[5];
	public AudioClip toPlay;
	
	string chosenFile;
	int selGridInt;
	
	//Use this for initialization
	void Start () {  // get all the files in the directory, and convert to strings
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
	     
	// Update is called once per frame
	void Update () {
		Debug.Log("in Update");
		if (Input.GetMouseButtonDown (0)){
			Debug.Log ("Pressed left click.");
			AudioSource source = GetComponent<AudioSource>();
			source.clip = toPlay;
			source.Play();
		}
 	}
}