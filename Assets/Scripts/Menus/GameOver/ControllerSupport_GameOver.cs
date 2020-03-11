using System;
using System.Diagnostics;

using UnityEngine;
using UnityEngine.SceneManagement;

using Debug = UnityEngine.Debug;

public class ControllerSupport_GameOver : MonoBehaviour
{
    /// <summary>The selected.</summary>
    public int selected;

    private int counter;

    /// <summary>The selection.</summary>
    private enum Selection
    {
        TryAgain,
        MainMenu
    };

    private float changeTimer;
    private float moveY;

    private float moveYPad;
    private float moveYStick;

    private SpriteRenderer hand1;

    private SpriteRenderer hand2;


    // Use this for initialization
    void Start()
    {
        this.selected = (int)Selection.TryAgain;
        this.changeTimer = 0;
        this.hand1 = GameObject.Find("hand_1").GetComponent<SpriteRenderer>();
        this.hand2 = GameObject.Find("hand_2").GetComponent<SpriteRenderer>();
        Debug.Log(this.hand1.enabled);
    }

    // Update is called once per frame
    /// <summary>The update.</summary>
    void FixedUpdate()
    {
        this.moveY = Input.GetAxis("Vertical");
        if (this.moveYPad != 0.0f || this.moveYStick != 0.0f)
        {
            this.moveY = Math.Max(this.moveYPad, this.moveYStick);
        }
        this.AdjustSelection(this.moveY);
        this.SelectBasedOnSelection();
        this.changeTimer -= Time.deltaTime;
    }

    /// <summary>The adjust selection.</summary>
    /// <param name="moveY">The move y.</param>
    private void AdjustSelection(float moveY)
    {
        if (moveY > 0.0f && this.changeTimer < 0.0f)
        {
            this.counter--;
            this.changeTimer = .25f;
        }
        else if (moveY < 0.0f && this.changeTimer < 0.0f)
        {
            this.counter++;
            this.changeTimer = .25f;
        }
        this.selected = Math.Abs(this.counter) % 2;
    }

    /// <summary>The function that decides which action to take when 'a' or space is pressed based on selected.</summary>
    private void SelectBasedOnSelection()
    {
        switch (this.selected)
        {
            case (int)Selection.TryAgain:
                this.hand1.enabled = true;
                this.hand2.enabled = false;
                break;
            case (int)Selection.MainMenu:
                this.hand1.enabled = false;
                this.hand2.enabled = true;
                break;
        }
        if (Input.GetKeyDown("joystick button 0") || Input.GetKeyDown("space"))
        {
            switch (this.selected)
            {
                case (int)Selection.TryAgain:
                    SceneManager.LoadScene("DifficultyMenu");
                    break;
                case (int)Selection.MainMenu:
                    SceneManager.LoadScene("MainMenu");
                    break;
                default: break;
            }
        }
    }
}