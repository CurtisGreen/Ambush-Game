using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Pause_Controller : MonoBehaviour {

    // Use this for initialization
    void Start()
    {
    }

    void Update()
    {
        if (Input.GetKeyDown("joystick button 6"))
        {
            SceneManager.LoadScene("MainMenu");
        }
    }
}
