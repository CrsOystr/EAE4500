using UnityEngine;
using System.Collections;

public class SpellOneScript : MonoBehaviour {

	float timeToLive;

	// Use this for initialization
	void Start () {
		Object.Destroy(gameObject, 0.4f);
	}
	
	// Update is called once per frame
	void Update () {
		transform.Translate (Vector3.forward * 10 *Time.deltaTime);
	}
}
