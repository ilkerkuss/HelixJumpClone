using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CylinderController : MonoBehaviour
{
    [SerializeField]private float _rotateSpeed;
    private float _moveX;


    void Start()
    {
        _rotateSpeed = 4000f;
    }


    void Update()
    {
        _moveX = Input.GetAxis("Mouse X");

        if (Input.GetMouseButton(0) && GameManager.Instance.GameState == GameManager.GameStates.IsGamePlaying)
        {
            transform.Rotate(0f, _moveX * _rotateSpeed * Time.deltaTime, 0f);

        }
    }
}
