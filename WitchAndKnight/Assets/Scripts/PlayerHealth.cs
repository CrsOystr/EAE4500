//Code Modified from Alex Johnstone's 3720 Alternative Game Development Course
using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour {

	public int startingHP = 25;
	public float damageRate = 0.5f;
	private float currentHP = 25;
	public GameObject HealthText;
	private Text healthText;

	public float knockbackAmt;
	private KnightController kc;
	
	void Start () {
		// Sets the current HP of the character to the starting HP
		currentHP = startingHP;
		
		// Uses the handy "Find" and "Get Component" functions to grab a reference to the HealthText game object and store it in our HealthText variable
		//HealthText = GameObject.Find("HealthText").GetComponent<Text>();
		healthText = HealthText.GetComponent<Text>();
		// Set the HUD to display the current amount of health
		healthText.text = "" + currentHP + " Player HP";

		kc = FindObjectOfType<KnightController> ();
	}
	
	// The OnTriggerStay function is called when the collider attached to this game object (whatever object the script is attached to) continuously another collider set to be a "trigger"
	void OnTriggerEnter (Collider collider)
	{	
		// We want to check if the thing we're colliding with is a damaging, this will differentiate it from other trigger objects which we might add in the future
		if (collider.tag == "damaging")
		{
			// Reduces the current health by the damage rate (remember this fires every frame!)
			currentHP -= damageRate;

			//kc.Knockback(knockbackAmt, collider.gameObject);

			// Checks if the currentHP is below or equal to 0, respawns the player and resets health (note that this doesn't "reset" the level, the previously collected items remain collected).
			// If you do want to reset the level you can instead Application.LoadLevel("YourLevelName") or Application.LoadLevel(0) if you want to load the first level.
			if (currentHP <= 0){
				Destroy(this.gameObject);
				Application.LoadLevel ("GameOver");
			}

			healthText.text = "" + Mathf.Round(currentHP) + " Player HP";
		}
	}
}
