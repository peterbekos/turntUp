using UnityEngine;
using System.Collections;

public class NoteObject : BeatObject {

    public float speed;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	public void Update () {
	
	}

    public void interpolate(float interp)
    {
        transform.position += Vector3.left * interp * speed;
    }
}
