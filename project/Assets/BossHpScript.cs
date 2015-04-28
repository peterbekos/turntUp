using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class BossHpScript : MonoBehaviour {

    private float bossHP; //1 indicates 100% HP, and 0 = 0%
    private float HitPointsAtFull;
    private EnemyObject infinityBoss = null;

    public GameObject bossHpBar;
    public Text HpBarText;
    private Image hpBarImage;


	// Use this for initialization
	void Start () {
        bossHP = 1;
        hpBarImage = bossHpBar.GetComponent<Image>();
	}
	
	// Update is called once per frame
	void Update () {
	    if(infinityBoss != null)
        {
            if(bossHpBar != null && HpBarText != null)
            {
                hpBarImage.fillAmount = infinityBoss.hitpoints / HitPointsAtFull;
                HpBarText.text = infinityBoss.hitpoints + "/" + HitPointsAtFull;
            }
        }
        else
        {
            if (GameManager.infinityBoss != null)
            {
                infinityBoss = GameManager.infinityBoss;
                HitPointsAtFull = infinityBoss.hitpoints;
            }
        }
	}
}
