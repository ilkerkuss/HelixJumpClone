using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreController : MonoBehaviour
{
    public static ScoreController Instance;

    private int _score;
    [SerializeField] private Text _scoreText;

    

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);

        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }


    void Start()
    {
        addScore(0);
    }

    void Update()
    {
        if (_scoreText == null)
        {
            _scoreText = GameObject.Find("ScoreText").GetComponent<Text>();
        }


    }

    public void addScore(int value)
    {
        _score += value;
        if (_score > PlayerPrefs.GetInt("HighScore", 0))
        {
            PlayerPrefs.SetInt("HighScore", _score);
        }

        _scoreText.text = _score.ToString();
    }

    public void ResetScore()
    {
        _score = 0;
        _scoreText.text = "0";
    }




}
