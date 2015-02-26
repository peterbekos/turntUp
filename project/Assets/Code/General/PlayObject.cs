using UnityEngine;
using System.Collections;

public abstract class PlayObject : BeatObject {

	//Variables
	private int hitpoints;
	private int strength;
	private float speed;

	void onColideSelf() {

	}

	void onColideTarget(PlayObject target) {

	}
}
