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

    void Setup()
    {
        musicVolumePercentText.text = "100%";
        soundVolumePercentText.text = "100%";
    }

    void Update()
    {
        if(!(levelSelection.CompareTo("") == 0) && playButtonVisible == false)
        {
            fadeInPlayButton();
        }
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
