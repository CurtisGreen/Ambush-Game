using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>The game over_ control.</summary>
public class GameOverControl : MonoBehaviour {

    /// <summary>The is try again.</summary>
    public bool isTryAgain;

    /// <summary>The is main menu.</summary>
    public bool isMainMenu;

    // Use this for initialization
    void Start()
    {
    }

    void OnMouseUp()
    {
        if (this.isTryAgain)
        {
            SceneManager.LoadScene("DifficultyMenu");
        }
        else if (this.isMainMenu)
        {
            SceneManager.LoadScene("MainMenu");
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
