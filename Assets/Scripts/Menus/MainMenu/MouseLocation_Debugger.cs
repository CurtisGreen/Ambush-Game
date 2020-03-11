using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLocation_Debugger : MonoBehaviour
{

    public Vector2 mouseLocation;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
	{
	    this.mouseLocation = Input.mousePosition;
	}
}
