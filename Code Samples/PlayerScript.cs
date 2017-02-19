using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerScript : MonoBehaviour {

	public static PlayerScript self;
	public GameObject bullet;
	public Transform mouth1;
	public Transform mouth2;
	public GameObject model;
	public RectTransform energyBar;
	public EventSystem eventSystem;
	public string currType = "grass";
	private SpriteRenderer sr;

	public GameObject[] glows;
	public float maxEnergy;
	public float energyRegRate;
	[HideInInspector] public float energy;

	void Awake(){
		self = this;
	}

	void Start () {
		sr = model.GetComponent<SpriteRenderer> ();
		energy = maxEnergy;

	}

	void Update () {
		if (Input.touchCount > 0) {
			
			Touch touch = Input.GetTouch (0); //only care about first touch

			//if you touched, it's not paused, you're not touching a button, and it's not game over...
			if (touch.phase == TouchPhase.Began && !GameControllerScript.control.isPaused
				&& eventSystem.currentSelectedGameObject == null && !GameControllerScript.control.isGameOver) {
				
				Vector3 temp = Camera.main.ScreenToWorldPoint (touch.position); //translate phone screen to world space

				Vector2 dir;
				float angle;

				if (temp.x < 0) { //if shooting to the left
					dir = new Vector2 (temp.x, temp.y) - new Vector2 (mouth1.position.x, mouth1.position.y);
					dir.Normalize (); //now have direction from player mouth to where you touched

					sr.flipY = false;
					angle = Mathf.Atan2 (dir.x, dir.y) * Mathf.Rad2Deg;

					angle += 110; //correction needed because magikarp sprite is tilted
					transform.rotation = Quaternion.AngleAxis (angle, -Vector3.forward); //make sprite face direction of touch
					ShootBullet (dir, mouth1); //shoot bullet in direction of touch

				} else { // if shooting to the right
					dir = new Vector2 (temp.x, temp.y) - new Vector2 (mouth2.position.x, mouth2.position.y);
					dir.Normalize (); //now have direction from player mouth to where you touched
				
					sr.flipY = true;
					angle = Mathf.Atan2 (dir.x, dir.y) * Mathf.Rad2Deg;

					angle += 70; //correction needed because magikarp sprite is tilted
					transform.rotation = Quaternion.AngleAxis (angle, -Vector3.forward); //make sprite face direction of touch
					ShootBullet (dir, mouth2); //shoot bullet in direction of touch
				}
					
			}
		}
		if (energy < maxEnergy)
			energy += energyRegRate * Time.deltaTime;
		else
			energy = maxEnergy;
		energyBar.localScale = new Vector3 (energy / maxEnergy, 1f, 1f);
	}
	void ShootBullet (Vector2 dir, Transform mouth){
		if (energy >= 1) {
			GameObject newBullet = Instantiate (bullet, mouth.position, transform.rotation) as GameObject;
			BulletScript bs = newBullet.GetComponent<BulletScript> ();
			bs.dir = dir;
			bs.type = currType;

			energy -= 1;
		}
	}

	public void changeType(string newType){
		if (!GameControllerScript.control.isPaused) {
			currType = newType;
			foreach (GameObject glow in glows) {
				glow.SetActive (false);
			}
			if (newType == "grass")
				glows [0].SetActive (true);
			else if (newType == "water")
				glows [1].SetActive (true);
			else if (newType == "fire")
				glows [2].SetActive (true);
		}
	}
}
