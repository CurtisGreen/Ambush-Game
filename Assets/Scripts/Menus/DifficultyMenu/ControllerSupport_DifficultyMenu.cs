using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ControllerSupport_DifficultyMenu : MonoBehaviour
{
    /// <summary>The selected.</summary>
    public int selected;

    private int counter;

    /// <summary>The selection.</summary>
    private enum Selection
    {
        Easy,
        Medium,
        Hard
    };

    private float changeTimer;
    private float moveY;

    private float moveYPad;
    private float moveYStick;

    private SpriteRenderer hand1Rend, hand2Rend, hand3Rend;


    // Use this for initialization
    void Start()
    {
        this.selected = (int)Selection.Easy;
        this.changeTimer = 0;
        this.hand1Rend = GameObject.Find("hand_1").GetComponent<SpriteRenderer>();
        this.hand2Rend = GameObject.Find("hand_2").GetComponent<SpriteRenderer>();
        this.hand3Rend = GameObject.Find("hand_3").GetComponent<SpriteRenderer>();
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
        this.selected = Math.Abs(this.counter) % 3;
    }

    /// <summary>The function that decides which action to take when 'a' or space is pressed based on selected.</summary>
    private void SelectBasedOnSelection()
    {
        switch (this.selected)
        {
            case (int)Selection.Easy:
                this.hand1Rend.enabled = true;
                this.hand2Rend.enabled = false;
                this.hand3Rend.enabled = false;
                break;
            case (int)Selection.Medium:
                this.hand1Rend.enabled = false;
                this.hand2Rend.enabled = true;
                this.hand3Rend.enabled = false;
                break;
            case (int)Selection.Hard:
                this.hand1Rend.enabled = false;
                this.hand2Rend.enabled = false;
                this.hand3Rend.enabled = true;
                break;
        }
        if (Input.GetKeyDown("joystick button 0") || Input.GetKeyDown("space") || Input.GetKeyDown("enter"))
        {
            switch (this.selected)
            {
                case (int)Selection.Easy:
                    PlayerPrefs.SetInt("Difficulty", 1);
                    SceneManager.LoadScene("MapTest");
                    break;
                case (int)Selection.Medium:
                    PlayerPrefs.SetInt("Difficulty", 2);
                    SceneManager.LoadScene("MapTest");
                    break;
                case (int)Selection.Hard:
                    PlayerPrefs.SetInt("Difficulty", 3);
                    SceneManager.LoadScene("MapTest");
                    break;
                default: break;
            }
        }
    }
}
