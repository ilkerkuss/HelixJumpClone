using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InGameCanvasController : CanvasManager
{
    public static InGameCanvasController Instance;

    [SerializeField] private Slider _gameProgressBar;
    [SerializeField] private Text _scoreText;

    [SerializeField] private GameObject _buttonParent;
    [SerializeField] private GameObject _tapToStartButton;

    [SerializeField] private Text _comboText;
    [SerializeField] private int _comboTextFontSize;


    private void Awake()
    {
        Instance = this;
    }
    void Start()
    {
        _comboTextFontSize = _comboText.fontSize;
    }

    void Update()
    {
        
    }


    public void ResetProgressBar()
    {
        _gameProgressBar.value = 0;
    }

    public void SetGameProgressBar(float value)
    {
        _gameProgressBar.value += value;
    }

    public void SetLevelText()
    {
        _gameProgressBar.transform.GetChild(2).GetChild(0).GetComponent<Text>().text = PlayerPrefs.GetInt("Level", 0).ToString();  //current level text
        _gameProgressBar.transform.GetChild(3).GetChild(0).GetComponent<Text>().text = (PlayerPrefs.GetInt("Level", 0) + 1).ToString();  //next level text
    }

    public void DisableTapToStartButton()
    {
        _tapToStartButton.SetActive(false);
    }

    public void ActivateTapToStartButton()
    {
        _tapToStartButton.SetActive(true);
    }

    public void SetComboText(int value)
    {
        _comboText.text = value.ToString();

        _comboText.fontSize += 10;
    }

    public void ResetComboText()
    {
        _comboText.text = "";
        _comboText.fontSize = _comboTextFontSize;
    }
    
}
