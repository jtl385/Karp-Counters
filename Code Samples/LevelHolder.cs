using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelHolder : MonoBehaviour {

	public static LevelHolder control;
	public int level;
	public bool boss;
	[HideInInspector] public List<string> introMessages;
	[HideInInspector] public float[] spawnIntervals;
	[HideInInspector] public float[] timeLimits;

	void Awake(){
		if (control == null) {
			control = this;
			DontDestroyOnLoad (gameObject);
		} else if (control != this)
			Destroy (gameObject);
		
		introMessages.Add ("placeholder");
		introMessages.Add ("Level 1\n\nRemember, enemies only take damage from the type they are weak against!");
		introMessages.Add ("Level 2\n\nGot the hang of it? Let's make sure!");
		introMessages.Add ("Level 3\n\nWatch out for Tauros. It's faster and tougher than the rest, but it can be damaged by all types!");

		spawnIntervals = new float[] {0f, 4f, 3f, 3f};
		timeLimits = new float[] { 0f, 20f, 30f, 60f };
	}

	void Start(){

	}
}
