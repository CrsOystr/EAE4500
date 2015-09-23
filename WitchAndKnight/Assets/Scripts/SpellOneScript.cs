using UnityEngine;
using System.Collections;

public class SpellOneScript : MonoBehaviour {

	public float timeToLive;
	public float spellSpeed;

	// Use this for initialization
	void Start () {
		this.rigidbody.velocity = Vector3.zero;
		this.rigidbody.angularVelocity = Vector3.zero;
		Object.Destroy(gameObject, timeToLive);
	}
	
	// Update is called once per frame
	void Update () {
		transform.Translate (Vector3.forward * spellSpeed * Time.deltaTime);
	}
}
