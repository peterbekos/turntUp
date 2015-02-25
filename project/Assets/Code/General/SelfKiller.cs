using UnityEngine;
using System.Collections;

//Kills the component's gameobject when the scene starts
public class SelfKiller : MonoBehaviour {
	void Start () {
		GameObject.Destroy(gameObject);
	}
}
