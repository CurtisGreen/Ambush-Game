using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroyer : MonoBehaviour
{
    public float destructionTime;

    public float destructionTimer;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
	{
	    this.destructionTimer += Time.deltaTime;
	    if (this.destructionTimer >= this.destructionTime)
	    {
	        DestroyObject(this.gameObject);
	    }
	}
}
