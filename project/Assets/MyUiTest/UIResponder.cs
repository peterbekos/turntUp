using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UIResponder : MonoBehaviour
{
    public Animator mAnimator;
    public Text musicVolumePercentText;
    public Text soundVolumePercentText;
    public Text healthShakePercentText;
    public Text beatShakePercentText;
    public Button playLevelButton;
    private string levelSelection = "";
    private bool playButtonVisible = false;
    
    private enum states { MAIN, LEVELS, UPGRADES, SETTINGS, HIGHSCORES };
    private states state = states.MAIN;

    void Setup()
    {
        musicVolumePercentText.text = "100%";
        soundVolumePercentText.text = "100%";
        Debug.Log("Should play the infinitybosspass animation!");
        mAnimator.Play("infinityBossPass", -1, 0);
    }

    void Update()
    {
        if(!(levelSelection.CompareTo("") == 0) && playButtonVisible == false)
        {
            fadeInPlayButton();
            
        }
        
        if(state == states.MAIN){
        	if(Input.GetAxis("Horizontal") < 0){ //if left, open Upgrades panel
        		state = states.UPGRADES;
        		upgradesRollIn();
        	}
        	else if( Input.GetAxis("Horizontal") > 0){ //if right, open settings
        		state = states.SETTINGS;
        		settingsRollIn();
        	}
        	else if( Input.GetAxis("Vertical") > 0){ //if up, open level select
        		state = states.LEVELS;
        		levelListRollIn();
        	}
        	else if (Input.GetAxis("Vertical") < 0 ){ //if down, do nothing *cough*
        		//state = states.HIGHSCORES;
        		//highscoresRollIn();
        	}
        }
        else if (Input.GetAxis("Cancel") > 0){
        	state = states.MAIN;
        	switch( state){
        	case states.HIGHSCORES:
        		//highScoresRollOut();
        		break;
        	case states.LEVELS:
        		levelListRollOut();
        		break;
        	case states.UPGRADES:
        		upgradesRollOut();
        		break;
        	case states.SETTINGS:
        		settingsRollOut();
        		break;
        	}	
        }
    }
    public void quitGame(){
    	Application.Quit();
    }
    public void colorsLevelClicked()
    {
        levelSelection = "Colors";
    }

    public void ffboss2LevelClicked()
    {
        levelSelection = "FFBoss2";
    }

    public void sandstormLevelClicked()
    {
        levelSelection = "Sandstorm";
    }

    public void infinityLevelClicked()
    {
        levelSelection = "JustThis";
    }

    private void fadeInPlayButton()
    {
        playLevelButton.enabled = true;
        playLevelButton.image.fillAmount += .01f;
        if(playLevelButton.image.fillAmount >= 1.0f)
        {
            playButtonVisible = true;
        }
    }

    public void playLevelButtonClicked()
    {
        Application.LoadLevel(levelSelection);
    }

    public void musicVolumeChange(float value)
    {
        int percent = (int)(value * 100.0f);
        musicVolumePercentText.text = percent + "%";
    }

    public void soundVolumeChange(float value)
    {
        int percent = (int)(value * 100.0f);
        soundVolumePercentText.text = percent + "%";
    }

    public void healthShakeChange(float value)
    {
        int percent = (int)(value * 100.0f);
        healthShakePercentText.text = percent + "%";
    }

    public void beatShakeChange(float value)
    {
        int percent = (int)(value * 100.0f);
        beatShakePercentText.text = percent + "%";
    }

    public void levelListRollIn()
    {
        mAnimator.Play("LevelListRollIn");
        Debug.Log("Rolling Level List In");
    }
    public void levelListRollOut()
    {
        mAnimator.Play("LevelListRollOut");
        Debug.Log("Rolling Level List Out");
    }
    public void settingsRollIn()
    {
        mAnimator.Play("SettingsRollIn");
        Debug.Log("Rolling Settings In");
    }
    public void settingsRollOut()
    {
        mAnimator.Play("SettingsRollOut");
        Debug.Log("Rolling Settings Out");
    }
    public void upgradesRollIn()
    {
        mAnimator.Play("UpgradesRollIn");
        Debug.Log("Rolling Upgrades In");
    }
    public void upgradesRollOut()
    {
        mAnimator.Play("UpgradesRollOut");
        Debug.Log("Rolling Upgrades Out");
    }
}
