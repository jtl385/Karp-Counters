using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour {

	public string type = "grass";
	public int speed;
	private float aliveFor = 5f;
	private float deathTime;
	[HideInInspector] public Vector2 dir;

	[HideInInspector] public Rigidbody2D rb;
	public SpriteRenderer sr;
	private AudioSource source;

	public Sprite grassSprite;
	public Sprite waterSprite;
	public Sprite fireSprite;
	public AudioClip grassSound;
	public AudioClip waterSound;
	public AudioClip fireSound;

	void Start () {
		rb = GetComponent<Rigidbody2D> ();
		source = GetComponent<AudioSource> ();

		deathTime = Time.time + aliveFor;

		if (type == "grass") {
			sr.sprite = grassSprite;
			rb.AddTorque (500f);
			source.PlayOneShot (grassSound, 1f);
		} else if (type == "water") {
			sr.sprite = waterSprite;
			rb.AddTorque (500f);
			source.PlayOneShot (waterSound, .5f);
		} else if (type == "fire") {
			sr.sprite = fireSprite;
			rb.AddTorque (500f);
			source.PlayOneShot (fireSound, 1f);
		}
		else
			print ("something went wrong with bullet");


	}

	void Update () {
		if (Time.time > deathTime) {
			Destroy (gameObject);
		}
	}

	void FixedUpdate(){
		rb.AddForce (dir * speed); //dir is determined by PlayerScript upon instantiaion
	}
}
