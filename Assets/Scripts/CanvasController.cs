using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasController : MonoBehaviour
{
    public static CanvasController Instance;

    public GameOverPanelController GameOverPanel;
    public LevelPassedPanelController LevelPassedPanel;
    public InGameCanvasController InGamePanel;

    private void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        
    }


    void Update()
    {
        
    }
}
