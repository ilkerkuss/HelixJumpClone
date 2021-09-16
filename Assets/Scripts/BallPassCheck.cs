using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallPassCheck : MonoBehaviour
{
    private GameObject _ball;

    private GameObject _final;



    void Start()
    {
        _ball = GameObject.FindGameObjectWithTag("Ball");
        _final = GameObject.FindGameObjectWithTag("Final");


    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.y >= _ball.transform.position.y)
        {
            Destroy(gameObject);
            ScoreController.Instance.addScore(PlayerPrefs.GetInt("Level",0));  //addscore according to level value
            GameManager.Instance.SetGameProgressBar(1/26f);  //ring amount

            BallController.Instance._perfectRingPass += 1;

        }
    }


}
