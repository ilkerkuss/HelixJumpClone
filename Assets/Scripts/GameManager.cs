using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    
    [SerializeField] private GameObject _levelCylinder;
    [SerializeField] private GameObject _ball;

    [SerializeField] private Material _unsafeMat;

    private bool _isGameStarted;
    private bool _isGamePaused;
    private bool _isGameOver;
    private bool _isGameMuted;

    [SerializeField] private GameObject _gameOverPanel;
    [SerializeField] private Text _scoreText;
    [SerializeField] private GameObject _buttonParent;
    [SerializeField] private GameObject _tapToStartButton;
    [SerializeField] private GameObject _levelPassedPanel;

    [SerializeField] private Slider _gameProgressBar;

    private int _currentLevel;

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
        Time.timeScale = 0;

        _currentLevel = PlayerPrefs.GetInt("Level", 1); ;
        _isGameStarted = false;
        _isGamePaused = false;
        _isGameOver = false;
        _isGameMuted=false;

        GenerateLevel();
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log("Pause"+_isGamePaused);
        //Debug.Log("Isgamestarted"+_isGameStarted);
        //Debug.Log("Isgamemuted"+_isGameMuted);
        Debug.Log(PlayerPrefs.GetInt("Level", 0));
        SetLevelText();
    }

    private void GenerateLevel() //random level generator
    {      

        GameObject Levelcylinder = Instantiate(_levelCylinder,new Vector3(0,-45,0),Quaternion.identity);
        Levelcylinder.transform.GetChild(0).GetChild(3).gameObject.SetActive(false);  //baþlangýç halkasý hazýrlama

        //random ring generation after begining ring
        for (int i = 1; i < Levelcylinder.transform.childCount-1; i++)
        {
            for (int j = 0; j < Random.Range(1,6); j++)
            {
                if (Levelcylinder.transform.GetChild(i).GetChild(j).gameObject != null)
                {
                    Levelcylinder.transform.GetChild(i).GetChild(Random.Range(1,6)).gameObject.SetActive(false);
                }
                
            }

            //assign unsafe platform randomly after begining ring
            for (int k = 1; k < Levelcylinder.transform.childCount-1; k++)
            {
                for (int l = 0; l < Random.Range(0,4); l++) // halka içinde max 4 unsafe platform olabilir
                {
                    Levelcylinder.transform.GetChild(k).GetChild(Random.Range(1,6)).gameObject.GetComponent<Renderer>().material = _unsafeMat;
                }
                
            }
        }

        
    }




    private void ResetLevel()
    {
        Destroy(GameObject.FindGameObjectWithTag("Cylinder")); 
    }

    private void ResetBall()
    {
        _ball.transform.position = BallController.Instance.BallPosition;
        _ball.GetComponent<TrailRenderer>().Clear(); // clear trailrenderer points after reset position of ball
    }

    public void TapToStart()
    {
        _isGameStarted = true;
        Time.timeScale = 1;

        _tapToStartButton.SetActive(false);
    }

    public void RestartGame() 
    {
        ResetLevel();
        ResetBall();
        GenerateLevel();

        _tapToStartButton.SetActive(true);
        _buttonParent.SetActive(true);
        _scoreText.gameObject.SetActive(true);
        _gameOverPanel.SetActive(false);
        _levelPassedPanel.SetActive(false);

        _gameProgressBar.value = 0;

        AudioController.Instance.PlaySound("BackgroundSound");
    }

    public void GameOver()
    {
        Time.timeScale = 0;


        _isGameOver = true;
        _isGameStarted = false;
        _buttonParent.SetActive(false);
        _scoreText.gameObject.SetActive(false);
        _gameOverPanel.SetActive(true);

        _gameProgressBar.value = 0;

        AudioController.Instance.StopSound("BackgroundSound");
        AudioController.Instance.PlaySound("GameOverSound");

    }
    public void LevelPassed()
    {
        Time.timeScale = 0;

        PlayerPrefs.SetInt("Level",_currentLevel+1);
        SetLevelText();

        _isGameStarted = false;
        
        _buttonParent.SetActive(false);
        _scoreText.gameObject.SetActive(false);
        _levelPassedPanel.SetActive(true);

        AudioController.Instance.StopSound("BackgroundSound");
        AudioController.Instance.PlaySound("CheerSound");

    }

    public bool GetIsGamePaused()
    {
        return _isGamePaused;
    }

    public void SetIsGamePaused(bool Boolvalue)
    {
         _isGamePaused = Boolvalue;
    }

    public bool GetIsGameMuted()
    {
        return _isGameMuted;
    }

    public void SetIsGameMuted(bool Boolvalue)
    {
        _isGameMuted = Boolvalue;
    }

    public int GetCurrentLevel()
    {
        return _currentLevel;
    }

    public void SetCurrentLevel(int value)
    {
        _currentLevel = value;
    }

    public void SetGameProgressBar(float value)
    {
        _gameProgressBar.value += value;
    }

    public void SetLevelText()
    {
        _gameProgressBar.transform.GetChild(2).GetChild(0).GetComponent<Text>().text = PlayerPrefs.GetInt("Level",0).ToString();  //current level text
        _gameProgressBar.transform.GetChild(3).GetChild(0).GetComponent<Text>().text = (PlayerPrefs.GetInt("Level", 0) + 1).ToString();  //next level text
    }
}

