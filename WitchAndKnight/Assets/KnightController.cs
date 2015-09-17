using UnityEngine;
using System.Collections;

public class KnightController : MonoBehaviour {

	public float moveSpeed;


	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
	
		float vertInputRaw = Input.GetAxisRaw ("Vertical"); // snapped to -1,0,1
		float horizInputRaw = Input.GetAxisRaw ("Horizontal"); // snapped to -1,0,1

		Vector3 moveVector = new Vector3 ();

		if (vertInputRaw != 0) {
			//transform.Translate(Vector3.forward * (vertInputRaw * moveSpeed), 0);
			moveVector += Vector3.forward * (vertInputRaw * moveSpeed);


		} 
		if (horizInputRaw != 0) {
			//transform.Translate(Vector3.right * (horizInputRaw * moveSpeed), 0);
			moveVector += Vector3.right * (horizInputRaw * moveSpeed);
		}

		transform.GetComponent<CharacterController>().Move(moveVector);


	}
}
