//Code Modified from Alex Johnstone's 3720 Alternative Game Development Course

using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class EnemyHealthNic : MonoBehaviour {
	
	public int startingHP = 10;
	public float damageRate = 0.5f;
	public int enemyNumber;
	private float currentHP = 0;
	public float comboCooldown = .3f;
	private float countdownTimer;


	private bool meleeHit;
	private bool projHit;
	public GameObject EnemyHealthText;
	private Text enemyHealthText;
	private Vector3 respawnLocation;

	// Two public variables that we can set in the editor (on a per-instance basis to change the properties of each platform we drop in the level).
	public Vector3 direction;    // Controls the direction in X/Y/Z coordinates 
	public int movementTime;    // Controls the duration (in seconds) that the platform will move for before returning
	
	// Private variables to track the timing & direction of the platform 
	private float timer = 0;
	private bool outgoing = true;
	
	void Start () {
		
		// Saves the current transform of the character for when we need to respawn later
		respawnLocation = this.gameObject.transform.position;
		
		// Sets the current HP of the character to the starting HP
		currentHP = startingHP;
		
		// Uses the handy "Find" and "Get Component" functions to grab a reference to the HealthText game object and store it in our HealthText variable
		enemyHealthText = EnemyHealthText.GetComponent<Text>();
		
		// Set the HUD to display the current amount of health
		enemyHealthText.text = "Enemy " + enemyNumber +" HP:" + currentHP;
		
	}
	void Update (){
			float timeElapse = (Time.time - countdownTimer);
			if (timeElapse >= 1)
			{
			projHit = false;
			}


		// If the platform is outgoing aka. moving from its origin toward its destination
		if (outgoing) {
			
			// Basic "Translate" function that will move the object in 3D space by the ammounds specified in the Vector3 (X,Y,Z)
			// Mulitplied by delta Time to compensate for any fluctuations in frame rate 
			this.gameObject.transform.Translate (direction * Time.deltaTime);
			
			// Increase the timer by delta time each frame, this effectively counts accurately in seconds
			timer += Time.deltaTime;
			
			// If we've reached the end of our movement time we'll want to trip the "return" switch and set our outgoing boolean to false, also reset timer
			if (timer > movementTime){
				outgoing = false;
				timer = 0;
			}
		}
		
		// If the platform is returning to the origin
		else{
			
			// Translate the platform along the inverted direction (that's what the * -1 is doing)
			this.gameObject.transform.Translate (direction * Time.deltaTime * -1);
			
			timer += Time.deltaTime;
			
			if (timer > movementTime){
				outgoing = true;
				timer = 0;
			}
		}
	
		}
	
	// The OnTriggerStay function is called when the collider attached to this game object (whatever object the script is attached to) continuously another collider set to be a "trigger"
	void OnTriggerStay (Collider collider)
	{
		if (collider.tag == "attacks") {
				
			if(projHit){
				currentHP -= damageRate*30;
			}
				// Reduces the current health by the damage rate (remember this fires every frame!)
				currentHP -= damageRate*5;

				// Set the HUD to display the current amount of health
				enemyHealthText.text = "Enemy " + enemyNumber + " HP:" + Mathf.Round (currentHP);

				// Checks if the currentHP is below or equal to 0, respawns the player and resets health (note that this doesn't "reset" the level, the previously collected items remain collected).
				// If you do want to reset the level you can instead Application.LoadLevel("YourLevelName") or Application.LoadLevel(0) if you want to load the first level.
				if (currentHP <= 0) {
						//this.transform.position = respawnLocation;
						//currentHP = startingHP;
						Destroy (this.gameObject);
				}
		}
		if (collider.tag == "spells") {
			projHit = true;
			countdownTimer = Time.time;
			// Reduces the current health by the damage rate (remember this fires every frame!)
			currentHP -= 5*damageRate;
			
			// Set the HUD to display the current amount of health
			enemyHealthText.text = "Enemy " + enemyNumber + " HP:" + Mathf.Round (currentHP);
			
			// Checks if the currentHP is below or equal to 0, respawns the player and resets health (note that this doesn't "reset" the level, the previously collected items remain collected).
			// If you do want to reset the level you can instead Application.LoadLevel("YourLevelName") or Application.LoadLevel(0) if you want to load the first level.
			if (currentHP <= 0) {
				//this.transform.position = respawnLocation;
				//currentHP = startingHP;
				Destroy (this.gameObject);
			}
		}

	}
}
