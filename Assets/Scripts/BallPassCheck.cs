using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallPassCheck : MonoBehaviour
{
    private BallController _ball;

    private GameObject _final;



    void Start()
    {
        _final = GameObject.FindGameObjectWithTag("Final");
        

    }

    void Update()
    {
        if (_ball == null)
        {
            GetBallReference();
        }

        if (transform.position.y >= _ball.transform.position.y)
        {
            Destroy(gameObject);
            ScoreController.Instance.addScore(PlayerPrefs.GetInt("Level",0));  //addscore according to level value
            InGameCanvasController.Instance.SetGameProgressBar(1/26f); //ring amount

            BallController.Instance.PerfectRingPass += 1;
            InGameCanvasController.Instance.SetComboText(BallController.Instance.PerfectRingPass);

        }
    }

    public void GetBallReference()
    {
        _ball = GameManager.Instance.CurrentBall;
    }


}
