using UnityEngine;
using System.Collections;

public class explodeScript : MonoBehaviour {

	public float timeToLive;
	private float startTime;

	// Use this for initialization
	void Start () {

			ParticleSystem exp = GetComponent<ParticleSystem>();
			exp.Play();
			startTime = Time.time;	
	
	}
	
	// Update is called once per frame
	void Update () {
		if (Time.time - startTime > timeToLive) {
			Destroy(this.gameObject);
				}
	
	}
}
