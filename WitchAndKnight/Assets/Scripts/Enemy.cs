//Code Modified from Alex Johnstone's 3720 Alternative Game Development Course

using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Enemy : MonoBehaviour {
	
	public int startingHP = 10;
	public float damageRate = 0.5f;
	public int enemyNumber;
	public float comboCooldown = .3f;
	public float knockbackAmt;
	public float turnRedTime;

	public GameObject EnemyHealthText;
	public GameObject comboEffectOne;


	// Two public variables that we can set in the editor (on a per-instance basis to change the properties of each platform we drop in the level).
	public Vector3 direction;    // Controls the direction in X/Y/Z coordinates 
	public int movementTime;    // Controls the duration (in seconds) that the platform will move for before returning
	
	// Private variables
	private Text enemyHealthText;
	
	private float spellcountdownTimer;
	private float meleecountdownTimer;

	private float timer = 0;
	private bool outgoing = true;
	private float currentHP = 0;

	private float redTimer = 0f;
	private bool turnRed = false;

	private bool meleeHit;
	private bool projHit;

	/// <summary>
	/// Knockback from the specified obj by knockbackAmt.
	/// </summary>
	/// <param name="obj">Object.</param>
	/// <param name="amt">Amt.</param>
	void Knockback (GameObject obj){

		CharacterController cc = transform.GetComponent<CharacterController> ();
		Vector3 moveDirection = (this.transform.position - obj.rigidbody.position);
		moveDirection.y = 0;
		moveDirection *= knockbackAmt;		
		cc.Move (moveDirection);
	}

	void TurnRed()
	{
		turnRed = true;
		redTimer = 0f;
		gameObject.renderer.material.color = Color.red;
	}


	void Start () {
		// Sets the current HP of the character to the starting HP
		currentHP = startingHP;
		
		// Uses the handy "Find" and "Get Component" functions to grab a reference to the HealthText game object and store it in our HealthText variable
		enemyHealthText = EnemyHealthText.GetComponent<Text>();
		
		// Set the HUD to display the current amount of health
		enemyHealthText.text = "Enemy " + enemyNumber +" HP:" + currentHP;
		
	}
	void Update (){
			float timeElapse = (Time.time - meleecountdownTimer);
			if (timeElapse >= comboCooldown)
			{
				projHit = false;
			}
			timeElapse = (Time.time - spellcountdownTimer);
			if (timeElapse >= comboCooldown)
			{
				meleeHit = false;
			}

		if (turnRed) {
			redTimer += Time.deltaTime;
			if (redTimer >= turnRedTime) {
				turnRed = false;
				gameObject.renderer.material.color = Color.white;
			}
		}


		// If the platform is outgoing aka. moving from its origin toward its destination
		if (outgoing) {
			
			// Basic "Translate" function that will move the object in 3D space by the ammounds specified in the Vector3 (X,Y,Z)
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


	void OnTriggerEnter (Collider collider)
	{
		if (collider.tag == "attacks") 
		{
			if(projHit)
			{
				currentHP -= damageRate*30;
				GameObject lcoe = Instantiate(comboEffectOne,this.transform.position,Quaternion.identity) as GameObject;
				projHit = false;
				meleeHit = false;
			}
			meleeHit = true;
			meleecountdownTimer = Time.time;

			Knockback(collider.gameObject);
			TurnRed();

			currentHP -= damageRate*5;
			enemyHealthText.text = "Enemy " + enemyNumber + " HP:" + Mathf.Round (currentHP);

			if (currentHP <= 0)
			{
					Destroy (this.gameObject);
			}
		}
		if (collider.tag == "spells") {
			if(meleeHit)
			{
				currentHP -= damageRate*30;
				GameObject lcoe = Instantiate(comboEffectOne,this.transform.position,Quaternion.identity) as GameObject;
				meleeHit = false;
				projHit = false;
			}

			projHit = true;
			spellcountdownTimer = Time.time;
			currentHP -= 5*damageRate;

			Knockback(collider.gameObject);
			TurnRed();

			//collider.transform.parent.gameObject.SetActive(false);
			collider.transform.gameObject.SetActive(false);

			enemyHealthText.text = "Enemy " + enemyNumber + " HP:" + Mathf.Round (currentHP);
			
			if (currentHP <= 0) 
			{
				Destroy (this.gameObject);
			}
		}

	}
}
