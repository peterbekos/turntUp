using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class BossHpScript : MonoBehaviour {

    private float bossHP; //1 indicates 100% HP, and 0 = 0%
    private float hitPointsAtFull;
    private EnemyObject infinityBoss = null;

    public GameObject bossHpBar;
    public Text hpBarText;
    private Image hpBarImage;

    public Animator mAnimator;
    private float lastHitPoints;


	// Use this for initialization
	void Start () {
        bossHP = 1;
        hpBarImage = bossHpBar.GetComponent<Image>();
        
	}
	
	// Update is called once per frame
	void Update () {
	    if(infinityBoss != null)
        {
            if(bossHpBar != null && hpBarText != null)
            {
                hpBarImage.fillAmount = infinityBoss.hitpoints / hitPointsAtFull;
                hpBarText.text = infinityBoss.hitpoints + "/" + hitPointsAtFull;
                if(lastHitPoints > infinityBoss.hitpoints)
                {
                    mAnimator.Play("HpBarFlash", -1, 0);
                    lastHitPoints = infinityBoss.hitpoints;
                }
            }
        }
        else
        {
            if (GameManager.infinityBoss != null)
            {
                infinityBoss = GameManager.infinityBoss;
                hitPointsAtFull = infinityBoss.hitpoints;
                lastHitPoints = hitPointsAtFull;
            }
        }
	}
}
