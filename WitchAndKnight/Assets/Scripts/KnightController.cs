﻿using UnityEngine;
using System.Collections;

public class KnightController : MonoBehaviour {

	public float moveSpeed;
	public float dodgeRollSpeed;
	public float dodgeRollTime;
	public float dodgeRollDelay; // time after a dodge roll before the next one is allowed
	public float slashSpeed;
	public float slashTime;

	// dodge roll variables
	private bool dodgeRolling;
	private bool dodgeDelayed;
	private bool canDodgeRoll;
	private float dodgeRollTimer;
	private float dodgeRollDelayTimer;

	// slash variables
	private GameObject sword;
	private int slashState; // 0: not slashing, 1: extending sword, 2: drawing sword in
	private float slashTimer;

	public GameObject rotateCyl;

	// Use this for initialization
	void Start () {
		dodgeRollTimer = 0f;
		dodgeRollDelayTimer = 0f;
		dodgeRolling = false;
		dodgeDelayed = false;
		canDodgeRoll = true;
		sword = transform.Find ("Sword").gameObject;
		slashState = 0;
		slashTimer = 0f;
	}

	/// <summary>
	/// Kills the dodge roll.
	/// </summary>
	private void KillDodgeRoll()
	{
		dodgeRollTimer = 0f;
		dodgeRollDelayTimer = 0f;
		dodgeDelayed = true;
		dodgeRolling = false;
		canDodgeRoll = false;
	}

	// Update is called once per frame
	void Update () {

		// dodge roll timekeeping
		if (dodgeRolling) {
			dodgeRollTimer += Time.deltaTime;
			if (dodgeRollTimer > dodgeRollTime) {
				canDodgeRoll = false;  // don't fully kill the dodge roll, we have to wait for stick to be reset
			} 
		}
		if (dodgeDelayed) {
			dodgeRollDelayTimer += Time.deltaTime;
			if (dodgeRollDelayTimer > dodgeRollDelay) {
				canDodgeRoll = true;
				dodgeDelayed = false;
			}
		}

		// slash timekeeping
		if (slashState > 0) {
			slashTimer += Time.deltaTime;
		}
		switch (slashState) {
			case 0: // not slashing
				break;
			case 1: // extending sword
				sword.transform.Translate(Vector3.forward * Time.deltaTime * slashSpeed, this.transform);
				if (slashTimer > slashTime/2)
					slashState = 2;
				break;

			case 2: // withdrawing sword
				sword.transform.Translate(Vector3.back * Time.deltaTime * slashSpeed, this.transform);
				if (slashTimer > slashTime){
					slashState = 0;
					slashTimer = 0f;
				}
				break;
		}

		Vector3 moveVector = new Vector3 ();

		// get inputs	
		float vertInputRaw = Input.GetAxisRaw ("Vertical"); // snapped to -1,0,1
		float horizInputRaw = Input.GetAxisRaw ("Horizontal"); // snapped to -1,0,1

		float dodgeVertInputRaw = Input.GetAxisRaw ("DodgeVert");
		float dodgeHorizInputRaw = Input.GetAxisRaw ("DodgeHoriz");

		// override regular movement when dodge rolling
		bool overRideMove = (dodgeVertInputRaw != 0 || dodgeHorizInputRaw != 0) && canDodgeRoll;

		// kill dodge roll upon resetting joystick to center
		if (dodgeRolling && dodgeVertInputRaw == 0 && dodgeHorizInputRaw == 0) {
			KillDodgeRoll();
		}

		// regular movement
		if (vertInputRaw != 0 && !overRideMove) {
			moveVector += Vector3.forward * (vertInputRaw * moveSpeed);
		} 
		if (horizInputRaw != 0 && !overRideMove) {
			moveVector += Vector3.right * (horizInputRaw * moveSpeed);
		}
	
		// dodge roll
		if (dodgeVertInputRaw != 0 && canDodgeRoll) {
			moveVector += Vector3.forward * (dodgeVertInputRaw * dodgeRollSpeed);
			dodgeRolling = true;
		}
		if (dodgeHorizInputRaw != 0 && canDodgeRoll) {
			moveVector += Vector3.right * (dodgeHorizInputRaw * dodgeRollSpeed);
			dodgeRolling = true;
		}

		// slash
		if (Input.GetButtonDown ("Slash")) {
			if (slashState==0)
				slashState = 1;			
		}

		// move
		transform.GetComponent<CharacterController>().Move(moveVector);
		// rotate into moving direction
		if (moveVector != Vector3.zero) {
			Quaternion oldWitchRot = rotateCyl.transform.rotation;
			transform.rotation = Quaternion.LookRotation (moveVector);
			rotateCyl.transform.rotation = oldWitchRot;
		}
	}
}
