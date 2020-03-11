using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour {
	public BoxCollider hitboxUp;
	public BoxCollider hitboxDown;
	public BoxCollider hitboxLeft;
	public BoxCollider hitboxRight;
	public PlayerMoveControl playerMC;

	private Vector3 agentVelocity;
	private Animator anim;
	public string direction;

	// Use this for initialization
    void Start () {
		anim = this.gameObject.GetComponentInChildren<Animator> ();
		TurnOffHitboxes ();
	}
	
	// Update is called once per frame
	void Update () {
		SetPlayerFacing ();
	}

	private void TurnOffHitboxes() {
		hitboxUp.enabled = false;
		hitboxDown.enabled = false;
		hitboxLeft.enabled = false;
		hitboxRight.enabled = false;
	}

	public void SetPlayerFacing()
	{
		//Debug.Log ("test");
		if (this.playerMC.direction.Contains("left")) {
			this.TurnOffHitboxes ();
			hitboxLeft.enabled = true;
			direction = "left";
		}
		else if (this.playerMC.direction.Contains ("right")) {
			this.TurnOffHitboxes ();
			hitboxRight.enabled = true;
			direction = "right";
		}
		else if (this.playerMC.direction.Contains ("up")) {
			this.TurnOffHitboxes ();
			hitboxUp.enabled = true;
			direction = "up";
		}
		else if (this.playerMC.direction.Contains ("down")) {
			this.TurnOffHitboxes ();
			hitboxDown.enabled = true;
			direction = "down";
		}
	}

}