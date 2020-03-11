using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire : MonoBehaviour {

    public float speed;
    public Dragon dragon;

    void Start()
    {
        dragon = FindObjectOfType<Dragon>();
        if (dragon.transform.localScale.x < 0)
        {
            speed = -speed;
            gameObject.GetComponent<SpriteRenderer>().flipX = true;
        }
    }

    void Update()
    {
        GetComponent<Rigidbody>().velocity = new Vector3(10, 0, 0);
        //Destroy(gameObject, 3f);
    }
}
