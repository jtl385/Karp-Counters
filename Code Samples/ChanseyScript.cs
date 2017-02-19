using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChanseyScript : MonoBehaviour {

	private EnemyScript enemyScript;

	void Start(){
		enemyScript = GetComponent<EnemyScript> ();
	}

	void Update () {
		if (enemyScript.hp == 0) {
			GameControllerScript.control.UpdateLives (-5); //negative means adding lives
			GameControllerScript.control.source.PlayOneShot(GameControllerScript.control.gainLivesSound, 1);
		}

	}
}
