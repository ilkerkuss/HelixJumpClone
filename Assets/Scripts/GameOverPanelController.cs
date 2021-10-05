using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOverPanelController : CanvasManager
{
    public static GameOverPanelController Instance;

    [SerializeField] private Text _highScoreText;

    private void Awake()
    {
        Instance = this;
    }
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void SetHighScoreText()
    {
        _highScoreText.text = "HighScore" + "\n" + PlayerPrefs.GetInt("HighScore", 0).ToString();
    }
}
