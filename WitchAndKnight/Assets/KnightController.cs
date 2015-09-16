using UnityEngine;
using System.Collections;

public class KnightController : MonoBehaviour {

	public float moveSpeed;

	private Transform trans;

	// Use this for initialization
	void Start () {
		trans = GetComponent<Transform> ();
	}
	
	// Update is called once per frame
	void Update () {
	
		float vertInputRaw = Input.GetAxisRaw ("Vertical"); // snapped to -1,0,1
		float horizInputRaw = Input.GetAxisRaw ("Horizontal"); // snapped to -1,0,1


		if (vertInputRaw != 0) {
			trans.Translate(Vector3.forward * (vertInputRaw * moveSpeed), 0);
		} 
		if (horizInputRaw != 0) {
				trans.Translate(Vector3.right * (horizInputRaw * moveSpeed), 0);
		}

	}
}
