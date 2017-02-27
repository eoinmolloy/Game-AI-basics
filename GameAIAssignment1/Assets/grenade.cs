using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class grenade : MonoBehaviour {
	float t;
	GameObject enemy;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		t += Time.deltaTime;
		if (t >= 1) {
			explosion (this.transform.position, 10.0f);
			Destroy (gameObject);
		}
	}

	void explosion(Vector3 center, float radius){
		Collider[] hitColliders = Physics.OverlapSphere(center, radius);
			foreach (Collider hitCol in hitColliders) {
			print (hitCol.name);
				if (hitCol.gameObject.name.Contains ("enemy")) {
				enemy = hitCol.gameObject;
				Destroy (enemy);
			}
		}
	}
}
