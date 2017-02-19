using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour {

	public string weakAgainst;
	[HideInInspector] public float maxHp;
	public int hp;
	public float speed;
	public int power;
	public int worth; //points worth

	[HideInInspector] public Vector2 dir;
	[HideInInspector] public Rigidbody2D rb;
	private AudioSource source;

	public RectTransform hpBar;

	private bool isDead;

	void Start () {
		rb = GetComponent<Rigidbody2D> ();
		source = GetComponent<AudioSource> ();
		maxHp = hp;
		isDead = false;
	}

	void Update(){
		if (isDead) {
			Destroy (gameObject, .01f);
		}
	}
	void FixedUpdate () {
		if (rb.velocity.magnitude < speed) {
			rb.AddForce (dir * speed);
		}
	}
		
	void OnTriggerEnter2D(Collider2D other){

		//if it hits the player, lose lives 
		if (other.CompareTag ("Player")) {
			GameControllerScript.control.UpdateLives (power);
			isDead = true;
		}

		//if it hits a bullet, and it's weak against against it
		else if (other.GetComponent<BulletScript>() != null) {
			BulletScript otherBullet = other.GetComponent<BulletScript> ();
			if (weakAgainst == "none" || otherBullet.type == weakAgainst)
				LoseHealth (1);
			
			rb.velocity = Vector2.zero;
			Destroy (otherBullet.gameObject);
		}

		if (hp <= 0){
//			transform.position = new Vector3 (0, 10, 0);
//			source.Play ();
			isDead = true;
			GameControllerScript.control.UpdateScore (worth);
		}
	}

	void LoseHealth(int n){
		hp -= n;
		hpBar.localScale = new Vector3 (hp / maxHp, 1f, 1f);

	}

/* for debugging and testing

	void OnMouseDown(){
		hp = 0;
		isDead = true;
	}

*/
}
