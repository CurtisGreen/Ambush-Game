using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerMoveControl : MonoBehaviour {

    public int playerSpeed = 50;
    public float knockback = 5;

    public float knockbackCount;
    public Vector3 knockdirection;

    private float moveX;
    private float moveY;

    public string direction;

    private float moveXPad;
    private float moveYPad;
    private float moveXStick;
    private float moveYStick;

    public Animator anim;
    private NavMeshAgent navMeshAgent;

    // Use this for initialization
    void Start () {
        anim = this.gameObject.GetComponentInChildren<Animator>();
    }
    
    // Update is called once per frame
    void Update () {
        this.moveX = Input.GetAxis("Horizontal");
        this.moveY = Input.GetAxis("Vertical");

        // Check if a contoller is being used
        this.CheckControls();

        // Check direction of movement and send movement to animator
        this.CheckDirection();

        // Create movement vector and normalize it so movement in any direction is the same velocity and rate of change is based on time
        var movement = this.NormalizeSpeed();

        if (!this.CheckKnockback())
        {
            //this.navMeshAgent.Move(movement);
            this.transform.Translate(movement);
        }
        this.knockbackCount -= Time.deltaTime;
    }

    void FixedUpdate()
    {
        // For IDLE ANIMATION, this updates at a fixed interval to store if the player is moving and what the last direction moved in was.
        float lastInputX = Input.GetAxis("Horizontal");
        float lastInputY = Input.GetAxis("Vertical");

        if (lastInputX != 0f || lastInputY != 0f)
        {
            this.anim.SetBool("walking", true);
            if (lastInputX > 0)
            {
                this.anim.SetFloat("LastMoveX", 1f);
            }
            else if (lastInputX < 0)
            {
                this.anim.SetFloat("LastMoveX", -1f);
            }
            else
            {
                this.anim.SetFloat("LastMoveX", 0.0f);
            }

            if (lastInputY > 0)
            {
                this.anim.SetFloat("LastMoveY", 1f);
            }
            else if (lastInputY < 0)
            {
                this.anim.SetFloat("LastMoveY", -1f);
            }
            else
            {
                this.anim.SetFloat("LastMoveY", 0);
            }
        }
        else
        {
            this.anim.SetBool("walking", false);
        }
    }

    void OnTriggerEnter(Collider collider)
    {
        Enemy enemy1 = collider.gameObject.GetComponent<Enemy>();
        Dragon dragon1 = collider.gameObject.GetComponent<Dragon>();
        if ((collider.gameObject.tag == "Enemy" && enemy1.isAlive) || (collider.gameObject.tag == "Dragon" && dragon1.isAlive))
        {
            this.Knockback(collider);
        }
    }

    bool CheckKnockback()
    {
        // Move the character
        if (this.knockbackCount > 0)
        {
            this.transform.Translate(this.knockback * this.knockdirection);
            return true;
        }
        return false;
    }

    /// <summary>The check controls function sets movement to controller if a controller is being used.</summary>
    void CheckControls()
    {
        if (this.moveXPad != 0.0f || this.moveXStick != 0.0f)
        {
            this.moveX = Math.Max(this.moveXPad, this.moveXStick);
        }
        if (this.moveYPad != 0.0f || this.moveYStick != 0.0f)
        {
            this.moveY = Math.Max(this.moveYPad, this.moveYStick);
        }
    }

    /// <summary>Check Direction, sets the direction string and adjusts movement to -1, 0, or 1. Then sends to animator.</summary>
    void CheckDirection()
    {
        this.direction = "";
        if (this.moveX > 0.0f)
        {
            this.direction = "right";
            this.moveX = 1f;
        }
        else if (this.moveX < 0.0f)
        {
            this.direction = "left";
            this.moveX = -1f;
        }
        if (this.moveY > 0.0f)
        {
            this.direction = " up";
            this.moveY = 1f;
        }
        else if (this.moveY < 0.0f)
        {
            this.direction = " down";
            this.moveY = -1f;
        }
        //Send movement to animator
        this.anim.SetFloat("SpeedX", moveX);
        this.anim.SetFloat("SpeedY", moveY);
    }

    Vector3 NormalizeSpeed()
    {
        var movement = new Vector3(this.moveX, 0.0f, this.moveY).normalized;

        movement *= this.playerSpeed;
        movement *= Time.deltaTime;

        return movement;
    }
    /// <summary>
    /// Pushback animation
    /// </summary>
    protected void Knockback(Collider collider)
    {
        this.knockbackCount = .1f;
        this.knockdirection = (this.gameObject.GetComponent<Rigidbody>().position
                               - collider.gameObject.GetComponent<Rigidbody>().position).normalized;
    }
		
}
