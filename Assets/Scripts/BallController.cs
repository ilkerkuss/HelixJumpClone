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
    }


    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision other)
    {
        //_rb.AddForce(Vector3.up * _jumpForce);
        _rb.velocity = Vector3.up * _jumpForce ;

        //instantiate splash efect
        GameObject splash = Instantiate(_splashPrefab, transform.position + new Vector3(0,-0.22f,0),transform.rotation);
        splash.transform.SetParent(other.gameObject.transform);

        AudioController.Instance.PlaySound("SplashSound");
        Destroy(splash,2.5f);

        //Debug.Log(other.gameObject.GetComponent<Renderer>().material.name);


        if (other.gameObject.GetComponent<Renderer>().material.name== "Final_Plat_Mat (Instance)")
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
