using UnityEngine;
using System.Collections;

public class KnightController : MonoBehaviour {

	public float moveSpeed;
	public float dodgeRollSpeed;
	public float dodgeRollTime;

	private bool dodgeRolling;
	private bool canDodgeRoll;
	private float dodgeRollTimer;

	// Use this for initialization
	void Start () {
		dodgeRollTimer = 0f;
		dodgeRolling = false;
		canDodgeRoll = true;
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
			}
		}

		Vector3 moveVector = new Vector3 ();

		// get inputs	
		float vertInputRaw = Input.GetAxisRaw ("Vertical"); // snapped to -1,0,1
		float horizInputRaw = Input.GetAxisRaw ("Horizontal"); // snapped to -1,0,1

		float dodgeVertInputRaw = Input.GetAxisRaw ("DodgeVert");
		float dodgeHorizInputRaw = Input.GetAxisRaw ("DodgeHoriz");
		// override regular movement when dodge rolling
		bool overRideMove = (dodgeVertInputRaw != 0 || dodgeHorizInputRaw != 0) && canDodgeRoll;

		// kill dodge roll
		if (dodgeVertInputRaw == 0 && dodgeHorizInputRaw == 0) {
			dodgeRollTimer = 0f;
			canDodgeRoll = true;
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

		// move
		transform.GetComponent<CharacterController>().Move(moveVector);
		
	}
}
