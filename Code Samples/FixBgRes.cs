using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FixBgRes : MonoBehaviour {

	//BG picture is 1280 x 800, but for some reason you need  to scale the picture
	//as x = 1.25 and y = 1.25 to make it fit a 1280 x 800 phone screen.

	private float xScale;

	void Start () {
		print (Screen.height);
		print (Screen.width);
		float h = Screen.height;
		float w = Screen.width;
		xScale = (800f / 1280f) / (h / w) * 1.25f;
		transform.localScale = new Vector3 (xScale, 1.25f, 1f);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
