using UnityEngine;
using System.Collections;

public class KnightController : MonoBehaviour {

	public float moveSpeed;
	public float dodgeRollSpeed;
	public float dodgeRollTime;

	private bool dodgeRolling;
	private bool canDodgeRoll;
	private float dodgeRollTimer;
	private float dodgeRollCooldown;
	// Use this for initialization
	void Start () {
		dodgeRollTimer = 0f;
		dodgeRolling = false;
		canDodgeRoll = true;
		dodgeRollCooldown = 0f;
	}
	
	// Update is called once per frame
	void Update () {

		// dodge roll timekeeping
		if (dodgeRolling) {
			dodgeRollTimer += Time.deltaTime;
			if (dodgeRollTimer > dodgeRollTime)			
			{
				canDodgeRoll = false;
				dodgeRolling = false;
					// trying to force a cooldown period for analog input
				/*
				dodgeRollCooldown +=Time.deltaTime;
				if (dodgeRollCooldown > 50f)
				{
					dodgeRollCooldown = 0f;
				}
			} */
		}

		Vector3 moveVector = new Vector3 ();

		// get inputs	
		float vertInputRaw = Input.GetAxisRaw ("Vertical"); // snapped to -1,0,1
		float horizInputRaw = Input.GetAxisRaw ("Horizontal"); // snapped to -1,0,1

		float vertInputPad = Input.GetAxis ("padVertical");
		float horizInputPad = Input.GetAxis ("padHorizontal");

		float dodgeVertInputRaw = Input.GetAxisRaw ("DodgeVert");
		float dodgeHorizInputRaw = Input.GetAxisRaw ("DodgeHoriz");

		float dodgeVertInputPad = Input.GetAxis ("padDodgeVert");
		float dodgeHorizInputPad = Input.GetAxis ("padDodgeHoriz");
		// override regular movement when dodge rolling
		bool overRideMove = (dodgeVertInputRaw != 0 || dodgeHorizInputRaw != 0) && canDodgeRoll;

		// kill dodge roll
		if ((dodgeVertInputRaw == 0 && dodgeHorizInputRaw == 0) || (dodgeVertInputPad == 0 && dodgeHorizInputPad == 0)) {
			dodgeRollTimer = 0f;
			canDodgeRoll = true;
		}
		/*
		if (dodgeRollCooldown > 0f) {
			canDodgeRoll = false;
			}
		*/


		// regular movement
		if (vertInputRaw != 0 && !overRideMove) {
			moveVector += Vector3.forward * (vertInputRaw * moveSpeed);
		} 
		if (horizInputRaw != 0 && !overRideMove) {
			moveVector += Vector3.right * (horizInputRaw * moveSpeed);
		}

		// gamepad movement
		if (vertInputPad != 0 && !overRideMove) {
			moveVector += Vector3.forward * (vertInputPad * moveSpeed);
		}
		if (horizInputPad != 0 && !overRideMove) {
			moveVector += Vector3.right * (horizInputPad * moveSpeed);
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

		if (dodgeVertInputPad != 0 && canDodgeRoll) {
			moveVector += Vector3.forward * (dodgeVertInputPad * dodgeRollSpeed);
			dodgeRolling = true;
		}
		if (dodgeHorizInputPad != 0 && canDodgeRoll) {
			moveVector += Vector3.right * (dodgeHorizInputPad * dodgeRollSpeed);
			dodgeRolling = true;
		}

		// move
		transform.GetComponent<CharacterController>().Move(moveVector);
		
	}
}
