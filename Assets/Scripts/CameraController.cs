using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{

    private Transform _ball;

    private Vector3 _offset;

    private float _smoothnessSpeed;

    // Start is called before the first frame update
    void Start()
    {
        _ball = GameObject.FindGameObjectWithTag("Ball").transform;

        _smoothnessSpeed = 0.1f;
        _offset = transform.position - _ball.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 newPos = Vector3.Lerp(transform.position,_offset + _ball.transform.position,_smoothnessSpeed);
        transform.position = newPos;
    }
}
