using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pause_buttons : MonoBehaviour
{
    public GameObject PauseCanvas;

    void Update() 
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Time.timeScale = 0;
            PauseCanvas.SetActive(true);
        }
    }
}
