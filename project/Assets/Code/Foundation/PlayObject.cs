using UnityEngine;
using System.Collections;

public abstract class PlayObject : BeatObject {

	//Variables
	public bool invincible = false;
	public bool flashVar = false;
	protected int repeatStart = 2;
	protected int repeatTemp = 0;
	protected Color defaultColor;
	
	protected SpriteRenderer mSpriteRenderer;
	
	//note: they have to be public so we can tweak them in the editor, 
	// or at least protected so we can see them at all in sub-classes
	public int hitpoints;
	public int strength;
	public float speed;
	public int scoreOnKill = 0;
	
	private int damageOverTime = 0;
	
	//plays a sound and animation when the unit dies
	public GameObject deathAnimation; 

	// Use this for initialization
	public void Start () {
		if(gameObject.GetComponent<SpriteRenderer>() != null){
			mSpriteRenderer = gameObject.GetComponent<SpriteRenderer>();
			defaultColor = mSpriteRenderer.color;
		}
	}

	protected void Update(){
		if(damageOverTime != 0 && !invincible) takeDamage(damageOverTime);
		
		if(mSpriteRenderer != null) flash ();
	}
	
	//reduce health, and if <= 0 die
	public void takeDamage(int dmg){
		flashVar = true;
	
		hitpoints -= dmg;
		
		if(gameObject.tag.Equals("Enemy"))
			GameManager.score += dmg;
		
		if(hitpoints <= 0)
		{
			//play the death sound and kill the object
			if(deathAnimation != null) Instantiate(deathAnimation, transform.position, Quaternion.identity);
			GameManager.score += scoreOnKill;
			Destroy(gameObject);
			
			if(gameObject.tag == "Player") 
			{
				Camera.main.GetComponent<GameTimer>().startRespawnTimer();
			}
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
	
	public void changeDoT (int dmg){
		damageOverTime += dmg;
	}
	
	public void rotateToward(GameObject target){
		//Rotate to face object
		float zRot = Mathf.Atan2(target.transform.position.y - transform.position.y, target.transform.position.x - transform.position.x) * Mathf.Rad2Deg;
		transform.rotation = Quaternion.Euler(0f, 0f, zRot - 90);
	}
	
	public void rotateToward(Vector3 target){
		//Rotate to face point
		float zRot = Mathf.Atan2(target.y - transform.position.y, target.x - transform.position.x) * Mathf.Rad2Deg;
		transform.rotation = Quaternion.Euler(0f, 0f, zRot - 90);
	}
	
	//Use this for collisions, built-in to Unity
	//Triggers don't physically collide (as in, they don't send things flying when you bump into them)
	//they just pass through each other and trigger this method
	public void OnTriggerEnter2D(Collider2D coll){
		
	}
	
	public void OnTriggerExit2D(Collider2D coll){
	
		if (coll.Equals(Camera.main.collider2D)){
			Destroy(gameObject);
		}
	}
}
