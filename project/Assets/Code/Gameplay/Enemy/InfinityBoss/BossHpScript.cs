using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class BossHpScript : MonoBehaviour {

    //private float bossHP; //1 indicates 100% HP, and 0 = 0%
    private float hitPointsAtFull;
    public EnemyObject boss = null;

    //public GameObject bossHpBar;
    public Text hpBarText;
    private Image hpBarImage;

    private Animator mAnimator;
    private float lastHitPoints;


	// Use this for initialization
	void Start () {
        //bossHP = 1;
        hpBarImage = this.GetComponent<Image>();
        mAnimator = this.GetComponent<Animator>();
        hitPointsAtFull = boss.GetComponent<EnemyObject>().hitpoints;
	}
	
	// Update is called once per frame
	void Update () {
	    if(boss != null)
        {
            if( hpBarText != null)
            {
                hpBarImage.fillAmount = boss.hitpoints / hitPointsAtFull;
                hpBarText.text = boss.hitpoints + "/" + hitPointsAtFull;
                if(lastHitPoints > boss.hitpoints)
                {
                    mAnimator.Play("HpBarFlash", -1, 0);
                }
            }
            lastHitPoints = boss.hitpoints;
            if(boss.hitpoints == 0){
            	hpBarText.text = "REKT";
            }
        }
        else
        {
            if (GameManager.infinityBoss != null)
            {
                boss = GameManager.infinityBoss;
                hitPointsAtFull = boss.hitpoints;
                lastHitPoints = hitPointsAtFull;
            }
            else if(lastHitPoints != 0)
            {
                lastHitPoints = 0;
                hpBarImage.fillAmount = 0;
                hpBarText.text = "REKT";

            }
        }
	}
}
