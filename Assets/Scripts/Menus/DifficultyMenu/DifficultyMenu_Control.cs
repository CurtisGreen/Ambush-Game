using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DifficultyMenu_Control : MonoBehaviour
{

    public bool isEasy;

    public bool isMedium;

    public bool isHard;

    // Use this for initialization
    void Start()
    {
    }

    void OnMouseUp()
    {
        if (this.isEasy)
        {
            Debug.Log("stuff0");
            PlayerPrefs.SetInt("Difficulty", 1);
            Debug.Log("stuff");
            SceneManager.LoadScene("MapTest");
            
        }
        else if (this.isMedium)
        {
            PlayerPrefs.SetInt("Difficulty", 2);
            SceneManager.LoadScene("MapTest");
        }
        else if (this.isHard)
        {
            PlayerPrefs.SetInt("Difficulty", 3);
            SceneManager.LoadScene("MapTest");
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}