using UnityEngine;
using System.Collections;

public class PlayerBounds : MonoBehaviour {

	public enum direction { top, left, right, bottom };
	public direction location = direction.left;

	void OnTriggerEnter2D(Collider2D coll){
		GameObject g = coll.gameObject;
	
		if(g.tag.Equals("Player")){
			float px = g.transform.position.x;
			float py = g.transform.position.y;
			float pz = g.transform.position.z;
		
			switch (location){
			case direction.left:
				g.transform.position = new Vector3(transform.position.x + .5f, py, pz);
				break;
			case direction.right:
				g.transform.position = new Vector3(transform.position.x - .5f, py, pz);
				break;
			case direction.top:
				g.transform.position = new Vector3(px, transform.position.y - .5f, pz);
				break;
			case direction.bottom:
				g.transform.position = new Vector3(px, transform.position.y + .5f, pz);
				break;
			}
		}
	}
}
