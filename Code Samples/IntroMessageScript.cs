using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IntroMessageScript : MonoBehaviour {

	private Text introText;

	void Start () {
		introText = GetComponent<Text> ();
		introText.text = LevelHolder.control.introMessages [LevelHolder.control.level];
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
