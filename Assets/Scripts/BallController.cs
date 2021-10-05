using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour
{
    public static BallController Instance;

    public Vector3 BallPosition;
    private Rigidbody _rb;
    [SerializeField]private float _jumpForce;

    [SerializeField] private GameObject _splashPrefab;

    private bool _isSuperBall;

    public int PerfectRingPass;


    private void Awake()
    {
        if (Instance==null)
        {
            Instance = this;
        }
    }
    void Start()
    {
        Init();
    }


    void Update()
    {
        SuperBallCheck();
        SetBallGravityValue();
    }

    private void OnCollisionEnter(Collision other)
    {
        if (GameManager.Instance.GameState == GameManager.GameStates.IsGamePlaying)
        {
            _rb.velocity = Vector3.up * _jumpForce;

            //instantiate splash efect
            GameObject splash = Instantiate(_splashPrefab, transform.position + new Vector3(0, -0.22f, 0), transform.rotation);
            splash.transform.SetParent(other.gameObject.transform);

            AudioController.Instance.PlaySound("SplashSound");
            Destroy(splash, 2.5f);

            PerfectRingPass = 0;   // top yere deðince ring  perfect geçme sayýsý sýfýrla
            InGameCanvasController.Instance.ResetComboText();


            if (_isSuperBall)
            {
                if (!(other.gameObject.GetComponent<Renderer>().material.name == "Final_Plat_Mat (Instance)"))
                {
                    Destroy(other.transform.parent.gameObject);
                    ScoreController.Instance.addScore(PlayerPrefs.GetInt("Level", 0));  //addscore according to level value

                }

            }
            else
            {
                if (other.gameObject.GetComponent<Renderer>().material.name == "Final_Plat_Mat (Instance)")
                {
                    Debug.Log("Tebrikler,bölümü geçtiniz.");

                    GameManager.Instance.LevelPassed();
                }
                else if (other.gameObject.GetComponent<Renderer>().material.name == "Unsafe_Platform (Instance)")
                {
                    GameManager.Instance.GameOver();
                    ScoreController.Instance.ResetScore();
                }

            }
        }
       
        
    }

    private void OnCollisionStay(Collision other)
    {
        if (GameManager.Instance.GameState == GameManager.GameStates.IsGamePlaying)
        {
            _rb.velocity = Vector3.up * _jumpForce;

            //instantiate splash efect
            GameObject splash = Instantiate(_splashPrefab, transform.position + new Vector3(0, -0.22f, 0), transform.rotation);
            splash.transform.SetParent(other.gameObject.transform);

            AudioController.Instance.PlaySound("SplashSound");
            Destroy(splash, 2.5f);

            PerfectRingPass = 0;   // top yere deðince ring  perfect geçme sayýsý sýfýrla


            if (_isSuperBall)
            {
                if (!(other.gameObject.GetComponent<Renderer>().material.name == "Final_Plat_Mat (Instance)"))
                {
                    Destroy(other.transform.parent.gameObject);
                    ScoreController.Instance.addScore(PlayerPrefs.GetInt("Level", 0));  //addscore according to level value

                }

            }
            else
            {
                if (other.gameObject.GetComponent<Renderer>().material.name == "Final_Plat_Mat (Instance)")
                {
                    Debug.Log("Tebrikler,bölümü geçtiniz.");

                    GameManager.Instance.LevelPassed();
                    //ScoreController.Instance.ResetScore();
                }
                else if (other.gameObject.GetComponent<Renderer>().material.name == "Unsafe_Platform (Instance)")
                {
                    GameManager.Instance.GameOver();
                    ScoreController.Instance.ResetScore();
                }

            }
        }

    }

    private void Init()
    {
        _rb = GetComponent<Rigidbody>();
        BallPosition = transform.position;

        _jumpForce = 5f;

        PerfectRingPass = 0;
    }

    private void SuperBallCheck()  // if the ball pass 3 ring without touching to platform ,ball become superball
    {
        if (PerfectRingPass >= 3)
        {
            _isSuperBall = true;

        }
        else
        {
            _isSuperBall = false;
        }

    }


    private void SetBallGravityValue()
    {
        if (GameManager.Instance.GameState == GameManager.GameStates.IsGamePlaying)
        {
            _rb.constraints = RigidbodyConstraints.FreezeRotation | RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionZ;
        }
        else
        {
            _rb.constraints = RigidbodyConstraints.FreezeAll;
        }
    }

}
