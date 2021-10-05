using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    
    [SerializeField] private GameObject _levelCylinder;
    [SerializeField] private BallController _ball;

    public BallController CurrentBall; 

    [SerializeField] private Material _unsafeMat;

    public enum GameStates
    {
        IsGameLoaded,
        IsGamePlaying,
        IsGameOver,
        IsGamePaused
    }

    public GameStates GameState;

    private bool _isGameMuted;

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
        Init();
    }

    void Update()
    {
        if (GameState == GameStates.IsGamePlaying)
        {
            InGameCanvasController.Instance.SetLevelText();
        }
        

    }

    #region Level Generate Functions

    private void GenerateLevel() //random level generator
    {
        BallController ball = Instantiate(_ball,_ball.transform.position,_ball.transform.rotation);
        CameraController.Instance.SetTarget(ball);
        CurrentBall = ball;

        GameObject Levelcylinder = Instantiate(_levelCylinder, new Vector3(0, -45, 0), Quaternion.identity);
        Levelcylinder.transform.GetChild(0).GetChild(3).gameObject.SetActive(false);  //baþlangýç halkasý hazýrlama

        //random ring generation after begining ring
        for (int i = 1; i < Levelcylinder.transform.childCount - 1; i++)
        {
            for (int j = 0; j < Random.Range(1, 6); j++)
            {
                if (Levelcylinder.transform.GetChild(i).GetChild(j).gameObject != null)
                {
                    Levelcylinder.transform.GetChild(i).GetChild(Random.Range(1, 6)).gameObject.SetActive(false);
                }

            }

            //assign unsafe platform randomly after begining ring
            for (int k = 1; k < Levelcylinder.transform.childCount - 1; k++)
            {
                for (int l = 0; l < Random.Range(0, 4); l++) // halka içinde max 4 unsafe platform olabilir
                {
                    Levelcylinder.transform.GetChild(k).GetChild(Random.Range(1, 6)).gameObject.GetComponent<Renderer>().material = _unsafeMat;
                }

            }
        }


    }

    private void ResetLevel()
    {
        Destroy(GameObject.FindGameObjectWithTag("Cylinder"));
        Destroy(GameObject.FindGameObjectWithTag("Ball"));
    }
/*
    private void ResetBall()
    {
        _ball.transform.position = BallController.Instance.BallPosition;
        _ball.GetComponent<TrailRenderer>().Clear(); // clear trailrenderer points after reset position of ball
    }
*/
    #endregion


    #region Game Event Functions

    public void RestartGame()
    {
        ResetLevel();
        //ResetBall();
        GenerateLevel();

        GameState = GameStates.IsGameLoaded;

        InGameCanvasController.Instance.ActivateTapToStartButton();
        CanvasController.Instance.InGamePanel.ShowPanel();
        InGameCanvasController.Instance.ResetProgressBar();


        CanvasController.Instance.GameOverPanel.HidePanel();
        CanvasController.Instance.LevelPassedPanel.HidePanel();

        AudioController.Instance.PlaySound("BackgroundSound");
    }

    public void GameOver()
    {
        GameState = GameStates.IsGameOver;

        InGameCanvasController.Instance.ResetProgressBar();
        CanvasController.Instance.InGamePanel.HidePanel();

        CanvasController.Instance.GameOverPanel.ShowPanel();
        GameOverPanelController.Instance.SetHighScoreText();

        AudioController.Instance.StopSound("BackgroundSound");
        AudioController.Instance.PlaySound("GameOverSound");

    }

    public void LevelPassed()
    {
        PlayerPrefs.SetInt("Level", _currentLevel + 1);

        GameState = GameStates.IsGameOver;


        InGameCanvasController.Instance.SetLevelText();
        CanvasController.Instance.InGamePanel.HidePanel();


        CanvasController.Instance.LevelPassedPanel.ShowPanel();

        AudioController.Instance.StopSound("BackgroundSound");
        AudioController.Instance.PlaySound("CheerSound");

    }

    #endregion

    public void TapToStart()  
    {
        GameState = GameStates.IsGamePlaying;

        InGameCanvasController.Instance.DisableTapToStartButton();
    }

    private void Init()
    {
        _currentLevel = PlayerPrefs.GetInt("Level", 1);
        InGameCanvasController.Instance.SetLevelText();

        _isGameMuted = false;

        GameState = GameStates.IsGameLoaded;
        GenerateLevel();
    }

    #region Sound Mute Functions

    public bool GetIsGameMuted()
    {
        return _isGameMuted;
    }

    public void SetIsGameMuted(bool Boolvalue)
    {
        _isGameMuted = Boolvalue;
    }
    #endregion


    #region Current Level Get/Set
    public int GetCurrentLevel()
    {
        return _currentLevel;
    }

    public void SetCurrentLevel(int value)
    {
        _currentLevel = value;
    }


    #endregion



}

