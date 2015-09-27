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
