using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Launcher : MonoBehaviour {
	public Rigidbody grenade;
	float throwPower = 10;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.F)) {
			Rigidbody clone;
			clone = Instantiate (grenade, transform.position, transform.rotation);
			clone.velocity = transform.TransformDirection (Vector3.forward*throwPower);
		}
	}
}
