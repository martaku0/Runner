using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public Text highScoreValue;
    public Text coinsValue;
    public Text soundBtnText;

    // Start is called before the first frame update
    void Start()
    {

        int highScore = 0;
        if(PlayerPrefs.HasKey("HighScoreValue"))
        {
            highScore = PlayerPrefs.GetInt("HighScoreValue");
        }

        int coins = 0;
        if(PlayerPrefs.HasKey("Coins"))
        {
            coins = PlayerPrefs.GetInt("Coins");
        }

        highScoreValue.text = highScore.ToString();
        coinsValue.text = coins.ToString();

        if(SoundManager.instance.GetMuted())
        {
            soundBtnText.text = "TURN ON SOUND";
        }
        else
        {
            soundBtnText.text = "TURN OFF SOUND";
        }
    }

    public void PlayButton()
    {
        if(!SoundManager.instance.GetMuted())
        {
            SoundManager.instance.PlayOnceClick();
            SoundManager.instance.effectsSource.mute = false;
        }
        SceneManager.LoadScene("Main");
    }

    public void SoundButton()
    {
        if (!SoundManager.instance.GetMuted())
        {
            SoundManager.instance.PlayOnceClick();
        }
        SoundManager.instance.ToggleMuted();
        if (SoundManager.instance.GetMuted())
        {
            soundBtnText.text = "TURN ON SOUND";
        }
        else
        {
            soundBtnText.text = "TURN OFF SOUND";
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
