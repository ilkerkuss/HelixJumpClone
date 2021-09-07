using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonController : MonoBehaviour
{

    void Start()
    {
        
    }


    void Update()
    {
        
    }

    public void OnClickPauseGame()
    {
        if (GameManager.Instance.GetIsGamePaused())
        {
            Time.timeScale = 1;
            GameManager.Instance.SetIsGamePaused(false);
            AudioController.Instance.PlaySound("ButtonSound");
            AudioController.Instance.UnMuteSounds();
        }
        else
        {
            Time.timeScale = 0;
            GameManager.Instance.SetIsGamePaused(true);
            AudioController.Instance.PlaySound("ButtonSound");
            AudioController.Instance.MuteSounds();

        }
        
    }

    public void OnClickMute()
    {
        if (!GameManager.Instance.GetIsGameMuted())
        {
            //AudioController.Instance.StopSound("BackgroundSound");
            GameManager.Instance.SetIsGameMuted(true);
            AudioController.Instance.MuteSounds();

        }
        else
        {
            GameManager.Instance.SetIsGameMuted(false);
            AudioController.Instance.UnMuteSounds();
        }
        AudioController.Instance.PlaySound("ButtonSound");
        

        
    }

    public void OnClickReplayGame()
    {
        GameManager.Instance.RestartGame();
        AudioController.Instance.PlaySound("ButtonSound");

    }

    public void OnClickStartGame()
    {
        GameManager.Instance.TapToStart();

    }

    public void OnClickPlayGame()
    {
        GameManager.Instance.CloseHomeScreen();
        GameManager.Instance.RestartGame();
    }
}
