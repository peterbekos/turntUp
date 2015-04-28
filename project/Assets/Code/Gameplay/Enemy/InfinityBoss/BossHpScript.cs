using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class BossHpScript : MonoBehaviour {

    private float bossHP; //1 indicates 100% HP, and 0 = 0%
    private float hitPointsAtFull;
    private EnemyObject infinityBoss = null;

    //public GameObject bossHpBar;
    public Text hpBarText;
    private Image hpBarImage;

    private Animator mAnimator;
    private float lastHitPoints;


	// Use this for initialization
	void Start () {
        bossHP = 1;
        hpBarImage = this.GetComponent<Image>();
        mAnimator = this.GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
	    if(infinityBoss != null)
        {
            if( hpBarText != null)
            {
                hpBarImage.fillAmount = infinityBoss.hitpoints / hitPointsAtFull;
                hpBarText.text = infinityBoss.hitpoints + "/" + hitPointsAtFull;
                if(lastHitPoints > infinityBoss.hitpoints)
                {
                    mAnimator.Play("HpBarFlash", -1, 0);
                    lastHitPoints = infinityBoss.hitpoints;
                }
            }
            lastHitPoints = infinityBoss.hitpoints;
        }
        else
        {
            if (GameManager.infinityBoss != null)
            {
                infinityBoss = GameManager.infinityBoss;
                hitPointsAtFull = infinityBoss.hitpoints;
                lastHitPoints = hitPointsAtFull;
            }
            else if(lastHitPoints != 0)
            {
                lastHitPoints = 0;
                hpBarImage.fillAmount = infinityBoss.hitpoints / hitPointsAtFull;
                hpBarText.text = "REKT";

            }
        }
	}
}
