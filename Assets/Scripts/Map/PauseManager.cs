using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseManager : MonoBehaviour
{
    private GameObject pauseText;

    // Use this for initialization
    void Start ()
    {
        Time.timeScale = 1;
        this.pauseText = GameObject.Find("Pause");
        this.pauseText.SetActive(false);
    }
    
    // Update is called once per frame
    void Update () {
        if (Input.GetKeyDown("joystick button 6") && Time.timeScale == 0)
        {
            Time.timeScale = 1;
            SceneManager.LoadScene("MainMenu");
        }
        if (Input.GetKeyDown("joystick button 7") || Input.GetKeyDown("escape"))
        {
            if (Time.timeScale == 0)
            {
                this.pauseText.SetActive(false);
                Time.timeScale = 1;
            }
            else if (Time.timeScale == 1)
            {
                this.pauseText.SetActive(true);
                Time.timeScale = 0;
            }
        }
    }
}
