using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public static CameraController Instance;

    private Vector3 _offset = new Vector3(0, 3, -8.75f);
    private float _smoothnessSpeed;

    public BallController Target;

    private void Awake()
    {
        Instance = this;
    }
    void Start()
    {
        _smoothnessSpeed = 0.1f;
        
    }


    void Update()
    {
        if(Target != null)
        {
            Vector3 newPos = Vector3.Lerp(transform.position, _offset + Target.transform.position, _smoothnessSpeed);
            transform.position = newPos;
        }
       
    }

    public void SetTarget(BallController Ball)
    {
        Target = Ball; 
    }
}
