using UnityEngine;
using System.Collections;

public class WitchController : MonoBehaviour {

	public float moveSpeed;

    
	// Use this for initialization
	void Start () { 
	
	}
	
	// Update is called once per frame
	void Update () {
		float vertInputRaw = Input.GetAxis ("WitchLeft");
		float horizInputRaw = Input.GetAxis ("WitchRight");
		float vertInputPad = Input.GetAxis ("padWitchLeft");
		float horizInputPad = Input.GetAxis ("padWitchRight");

		float moveVector = 0; 

		if (vertInputRaw != 0) {
			//transform.Translate(Vector3.forward * (vertInputRaw * moveSpeed), 0);
			//moveVector += Vector3.forward * (vertInputRaw * moveSpeed);
			moveVector  = 50;
			
			
		} 
		if (horizInputRaw != 0) {
			//transform.Translate(Vector3.right * (horizInputRaw * moveSpeed), 0);
			//moveVector += Vector3.right * (horizInputRaw * moveSpeed);
			moveVector = -50;

		}

		if (vertInputPad != 0) {
			moveVector = 50;
		}

		if (horizInputPad != 0) {
			moveVector = -50;
		}
		//transform.parent.GetComponentInParent<Transform> ().Rotate (Vector3.up * speedz * moveSpeed*Time.deltaTime);
		transform.parent.GetComponentInParent<Transform> ().Rotate (Vector3.up * Time.deltaTime * moveVector *  moveSpeed);

	}
}
