using UnityEngine;
using System.Collections;

public class WitchController : MonoBehaviour {

	public float moveSpeed;

    
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		bool vertInputRaw = Input.GetKeyDown (KeyCode.E); // snapped to -1,0,1
		bool horizInputRaw = Input.GetKeyDown (KeyCode.Q);

		float speedz = 20; 
		if (vertInputRaw == true) {
			//transform.Translate(Vector3.forward * (vertInputRaw * moveSpeed), 0);
			//moveVector += Vector3.forward * (vertInputRaw * moveSpeed);
			speedz = 50;
			
			
		} 
		if (horizInputRaw == true) {
			//transform.Translate(Vector3.right * (horizInputRaw * moveSpeed), 0);
			//moveVector += Vector3.right * (horizInputRaw * moveSpeed);
			speedz = -50;

		}


		//transform.parent.GetComponentInParent<Transform> ().Rotate (Vector3.up * speedz * moveSpeed*Time.deltaTime);
		transform.parent.GetComponentInParent<Transform> ().Rotate (Vector3.up *Time.deltaTime * 20);

	}
}
