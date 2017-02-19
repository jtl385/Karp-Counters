using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spin2Dscript : MonoBehaviour {

	public Transform rotatePoint;

	void Start () {
		
	}

	// Update is called once per frame
	void Update () {
		transform.Rotate(Vector3.forward * 10);
		transform.RotateAround (rotatePoint.position, Vector3.forward, 5f);
	}
}
