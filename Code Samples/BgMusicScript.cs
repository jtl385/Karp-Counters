using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BgMusicScript : MonoBehaviour {

	private AudioSource source;
	public AudioClip bgMusic1;
	public AudioClip bossMusic;

	void Start () {
		source = GetComponent<AudioSource> ();
		if (LevelHolder.control.boss) {
			source.clip = bossMusic;
		} else
			source.clip = bgMusic1;
		source.Play ();
	}
		

	// Update is called once per frame
	void Update () {
		
	}
}
