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
        if (GameManager.Instance.GameState == GameManager.GameStates.IsGamePaused)
        {
            GameManager.Instance.GameState = GameManager.GameStates.IsGamePlaying;
            AudioController.Instance.UnMuteSounds();
        }
        else
        {
            GameManager.Instance.GameState = GameManager.GameStates.IsGamePaused;
            AudioController.Instance.MuteSounds();
        }
        AudioController.Instance.PlaySound("ButtonSound");
    }

    public void OnClickMute()
    {
        if (!GameManager.Instance.GetIsGameMuted())
        {
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
        GameManager.Instance.RestartGame();
    }
}
