using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>The main menu_ control.</summary>
public class MainMenu_Control : MonoBehaviour
{
    /// <summary>The is new game.</summary>
    public bool isNewGame;

    /// <summary>The is credits.</summary>
    public bool isCredits;

    /// <summary>The is exit.</summary>
    public bool isExit;

    /// <summary>The renderer.</summary>
    private Renderer renderer;

    // Use this for initialization
    void Start()
    {
        this.renderer = this.gameObject.GetComponent<Renderer>();
    }

    /// <summary>The on mouse up.</summary>
    void OnMouseUp()
    {
        if (this.isNewGame)
        {
            SceneManager.LoadScene("DifficultyMenu");
            this.renderer.material.color  = Color.black;
        }
        else if (this.isCredits)
        {
            SceneManager.LoadScene("Credits");
            this.renderer.material.color = Color.black;
        }
        else if (this.isExit)
        {
            Application.Quit();
            this.renderer.material.color = Color.black;
        }
    }

    // Update is called once per frame
    void Update () {
        
    }
}
