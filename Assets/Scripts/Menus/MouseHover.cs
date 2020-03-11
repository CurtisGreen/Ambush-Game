using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseHover : MonoBehaviour
{
    public Color initialColor;

    public Color hoverColor;

    private Renderer renderer;

    // Use this for initialization
    void Start ()
    {
        this.renderer = this.GetComponent<Renderer>();
        this.renderer.material.color = this.initialColor;
    }
    
    void OnMouseEnter()
    {
        this.renderer.material.color = this.hoverColor;
    }

    void OnMouseExit()
    {
        this.renderer.material.color = this.initialColor;
    }
}
