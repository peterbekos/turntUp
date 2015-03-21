using UnityEngine;
using System.Collections;

public class EnemyObject : PlayObject {

	private bool flashVar = false;
	private int repeatStart = 2;
	private int repeatTemp = 0;
	protected Color defaultColor;
	
	protected SpriteRenderer mSpriteRenderer;

	// Use this for initialization
	public void Start () {
		mSpriteRenderer = gameObject.GetComponent<SpriteRenderer>();
		defaultColor = mSpriteRenderer.color;
	}
	
	// Update is called once per frame
	public new void Update () {
		if(mSpriteRenderer != null) flash ();
	}
	
	public void OnTriggerEnter(Collider2D coll){
		if (coll.tag.Equals("Player"))
		{
			//Debug.Log("" + gameObject.name + " Entered collision");
			
			coll.gameObject.SendMessage("takeDamage", strength);
			takeDamage(strength);
		}
		else if (coll.tag.Equals("Bullet"))
		{
			flashVar = true;
			coll.gameObject.SendMessage("takeDamage", strength);
			takeDamage(strength);
		}
	}
	
	public void flash()
	{
		if(flashVar)
		{
			mSpriteRenderer.color = new Color(1.0f, 0.0f, 0.0f);
			flashVar = false;
			repeatTemp = repeatStart;
		}
		else if(repeatTemp > 0)
		{
			mSpriteRenderer.color = new Color(1.0f, 0.0f, 0.0f);
			repeatTemp--;
			flashVar = false;
		}
		else
		{
			mSpriteRenderer.color = new Color(1.0f, 1.0f, 1.0f);
		}
	}
	
	public new void OnTriggerEnter2D(Collider2D coll){
		base.OnTriggerEnter2D(coll);
		
		if (coll.tag.Equals("Bullet") && coll.GetComponent<ShotObject>().target == ShotObject.hit.enemy)
		{
			flashVar = true;
		}
	}
}
