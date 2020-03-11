using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectedHighlight : MonoBehaviour
{
    public int selectionNumber;

    public int sceneSelection;

    public Color initialColor;

    public Color hoverColor;

    private enum Scene
    {
        MainMenu,
        Difficulty,
        MainScene,
        GameOver
    };

    private Renderer renderer;

    private ControllerSupport_Menu controlNav_MainMenu;

    private ControllerSupport_DifficultyMenu controlNav_Difficulty;

    private ControllerSupport_GameOver controlNav_GameOver;

    //private ControllerSupport_GameOver controlNav_GameOver;

    // Use this for initialization
    void Start()
    {
        this.renderer = this.GetComponent<Renderer>();
        this.renderer.material.color = this.initialColor;
        switch (this.sceneSelection)
        {
            case (int)Scene.MainMenu:
                this.controlNav_MainMenu = this.GetComponentInParent<ControllerSupport_Menu>();
                break;
            case (int)Scene.Difficulty:
                this.controlNav_Difficulty = this.GetComponentInParent<ControllerSupport_DifficultyMenu>();
                break;
            case (int)Scene.GameOver:
                this.controlNav_GameOver = this.GetComponentInParent<ControllerSupport_GameOver>();
                break;
        }
}

    void Update()
    {
        switch (sceneSelection)
        {
            case (int)Scene.MainMenu:
                if (this.controlNav_MainMenu.selected == this.selectionNumber)
                {
                    this.renderer.material.color = this.hoverColor;
                    if (Input.GetKeyDown("joystick button 0") || Input.GetKeyDown("space") && this.controlNav_MainMenu.selected == this.selectionNumber)
                    {
                        this.renderer.material.color = Color.white;
                    }
                }
                else
                {
                    this.renderer.material.color = this.initialColor;
                }
                break;
            case (int)Scene.Difficulty:
                if (this.controlNav_Difficulty.selected == this.selectionNumber)
                {
                    this.renderer.material.color = this.hoverColor;
                    if (Input.GetKeyDown("joystick button 0") || Input.GetKeyDown("space") && this.controlNav_Difficulty.selected == this.selectionNumber)
                    {
                        this.renderer.material.color = Color.white;
                    }
                }
                else
                {
                    this.renderer.material.color = this.initialColor;
                }
                break;
            case (int)Scene.GameOver:
                if (this.controlNav_GameOver.selected == this.selectionNumber)
                {
                    this.renderer.material.color = this.hoverColor;
                    if (Input.GetKeyDown("joystick button 0") || Input.GetKeyDown("space") && this.controlNav_Difficulty.selected == this.selectionNumber)
                    {
                        this.renderer.material.color = Color.white;
                    }
                }
                else
                {
                    this.renderer.material.color = this.initialColor;
                }
                break;
        }
    }
}
