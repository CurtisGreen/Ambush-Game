using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ControllerInstructions_Control : MonoBehaviour
{
	// Use this for initialization
	void Start ()
	{
	}
	
	// Update is called once per frame
	void Update ()
	{
	    if (Input.GetKeyDown("joystick button 0") || Input.GetKeyDown("space"))
        {
	        SceneManager.LoadScene("MainMenu");
	    }
	}
}
