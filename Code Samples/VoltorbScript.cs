using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VoltorbScript : MonoBehaviour {

	private EnemyScript enemyScript;

	void Start(){
		enemyScript = GetComponent<EnemyScript> ();
	}

	void Update () {
		if (enemyScript.hp == 0) {
			PlayerScript.self.energy = PlayerScript.self.maxEnergy;
			GameControllerScript.control.source.PlayOneShot(GameControllerScript.control.gainEnergySound, 1);
		}
	}
}
