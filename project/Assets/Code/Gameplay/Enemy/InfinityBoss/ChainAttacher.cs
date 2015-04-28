using UnityEngine;
using System.Collections;

[RequireComponent (typeof(HingeJoint2D))]
public class ChainAttacher : MonoBehaviour{

	public GameObject target;
	public HingeJoint2D targetHinge;
	
	void Start(){
		if(targetHinge == null) targetHinge = gameObject.GetComponent<HingeJoint2D>();
	}
	
	void Update(){
		if(target != null && targetHinge.connectedBody == null){
			targetHinge.connectedBody = target.rigidbody2D;
			Debug.Log ("WTF");
		}
	}
	
	public void setTarget(GameObject target){
		this.target = target;
		targetHinge.connectedBody = target.rigidbody2D;
		Debug.Log ("Target Set");
	}
}
