using UnityEngine;
using System.Collections;

public class WitchController : MonoBehaviour {

	public float moveSpeed;
	public float spellSpeed;
	public float spellCooldown;
	public GameObject spellOne;

    
	// Use this for initialization
	void Start () { 
	}
	
	// Update is called once per frame
	void Update () {
		float vertInputRaw = Input.GetAxis ("WitchLeft");
		float horizInputRaw = Input.GetAxis ("WitchRight");
		float spellOneInput = Input.GetAxis ("Spell 1");

		float moveVector = 0; 

		if (vertInputRaw != 0) {

			moveVector  = 50;
		} 
		if (horizInputRaw != 0) {

			moveVector = -50;
		}
		if (spellOneInput != 0) {
			GameObject newProjectile = Instantiate( spellOne, transform.position , transform.rotation ) as GameObject;
		}

		transform.parent.GetComponentInParent<Transform> ().Rotate (Vector3.up * Time.deltaTime * moveVector *  moveSpeed);

	}
}
