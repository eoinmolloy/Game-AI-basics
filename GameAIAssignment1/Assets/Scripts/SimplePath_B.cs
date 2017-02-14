using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimplePath_B : MonoBehaviour {

	float timer;
	Vector3 pos;
	int counter = 2;

	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {
		GameObject t1 = GameObject.Find ("WP_B_"+counter);
		if (Vector3.Distance (transform.position, t1.transform.position) > 2) {
			transform.LookAt (t1.transform);
			timer = Time.deltaTime;
			transform.Translate (Vector3.forward * timer*5);

			if (Vector3.Distance (transform.position, t1.transform.position) < 2) {
				counter++;
				if (counter == 5) {
					counter = 1;
				}
			}

		}
	}
}
