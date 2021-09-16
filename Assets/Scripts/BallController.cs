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

    public bool _isBallTouchGround;

    private bool _isSuperBall;

    public int _perfectRingPass;

    private void Awake()
    {
        if (Instance==null)
        {
            Instance = this;
        }
    }
    void Start()
    {
        _rb = GetComponent<Rigidbody>();
        BallPosition = transform.position;

        _jumpForce = 5f;

        _perfectRingPass = 0;
        _isBallTouchGround = false;
    }


    void Update()
    {
        //Debug.Log("yerde mi : " + _isBallTouchGround);
        Debug.Log("pass sayý : " + _perfectRingPass);
        Debug.Log("süper top : " + _isSuperBall);

        SuperBallCheck();
    }

    private void OnCollisionEnter(Collision other)
    {
        _rb.velocity = Vector3.up * _jumpForce ;

        //instantiate splash efect
        GameObject splash = Instantiate(_splashPrefab, transform.position + new Vector3(0,-0.22f,0),transform.rotation);
        splash.transform.SetParent(other.gameObject.transform);

        AudioController.Instance.PlaySound("SplashSound");
        Destroy(splash,2.5f);



        _isBallTouchGround = true; //collision olduðunda top yere deðdi mi
        _perfectRingPass = 0;   // top yere deðince ring  perfect geçme sayýsý sýfýrla


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
                //Debug.Log("Kaybettiniz.");

                GameManager.Instance.GameOver();
                ScoreController.Instance.ResetScore();
            }

        }
        
    }

    private void OnCollisionExit(Collision collision)
    {
        _isBallTouchGround = false;  //top collision iþleminden çýktý mý yani havalandý mý
    }



    private void SuperBallCheck()  // if the ball pass 3 ring without touching to platform ,ball become superball
    {
        if (_perfectRingPass >= 3)
        {
            _isSuperBall = true;

        }
        else
        {
            _isSuperBall = false;
        }

    }

}
