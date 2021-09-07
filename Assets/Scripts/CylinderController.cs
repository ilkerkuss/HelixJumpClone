using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CylinderController : MonoBehaviour
{

    [SerializeField]private float _rotateSpeed;
    private float _moveX;

    // Start is called before the first frame update
    void Start()
    {
        _rotateSpeed = 4000f;
    }

    // Update is called once per frame
    void Update()
    {
        _moveX = Input.GetAxis("Mouse X");

        if (Input.GetMouseButton(0))
        {
            transform.Rotate(0f, _moveX * _rotateSpeed * Time.deltaTime, 0f);

        }
    }
}
