using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : MonoBehaviour
{
    public BoxCollider shieldUp;
    public BoxCollider shieldDown;
    public BoxCollider shieldLeft;
    public BoxCollider shieldRight;
    public PlayerMoveControl playerMC;

    private Vector3 agentVelocity;
    private Animator anim;
    public string direction;

    // Use this for initialization
    void Start()
    {
        anim = this.gameObject.GetComponentInChildren<Animator>();
        TurnOffHitboxes();
    }

    // Update is called once per frame
    void Update()
    {
        SetPlayerFacing();
    }

    private void TurnOffHitboxes()
    {
        shieldUp.enabled = false;
        shieldDown.enabled = false;
        shieldLeft.enabled = false;
        shieldRight.enabled = false;
    }

    public void SetPlayerFacing()
    {
        //Debug.Log ("test");
        if (this.playerMC.direction.Contains("left"))
        {
            this.TurnOffHitboxes();
            shieldLeft.enabled = true;
            direction = "left";
        }
        else if (this.playerMC.direction.Contains("right"))
        {
            this.TurnOffHitboxes();
            shieldRight.enabled = true;
            direction = "right";
        }
        else if (this.playerMC.direction.Contains("up"))
        {
            this.TurnOffHitboxes();
            shieldUp.enabled = true;
            direction = "up";
        }
        else if (this.playerMC.direction.Contains("down"))
        {
            this.TurnOffHitboxes();
            shieldDown.enabled = true;
            direction = "down";
        }
    }

}