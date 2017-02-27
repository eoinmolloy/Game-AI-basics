using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrenadeThrow : MonoBehaviour {
	public GameObject prefab;

	// Use this for initialization
	void Start () {
		
	}
		
	// Update is called once per frame
	void Update () {
		
		if (Input.GetKeyDown (KeyCode.G)) {
			GameObject grenade = Instantiate (prefab) as GameObject;
			grenade.transform.position = transform.position + Camera.main.transform.forward;
			Rigidbody rb = grenade.GetComponent<Rigidbody> ();
			rb.velocity = Camera.main.transform.forward * 20;
		}
	}
}
