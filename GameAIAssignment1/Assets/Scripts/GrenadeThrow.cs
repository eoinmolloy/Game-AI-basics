using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrenadeThrow : MonoBehaviour {
	float radius = 5.0f;
	float power = 10.0f;
	float explosionDelay = 1.0f;
	float lift = 5.0f;
	// Use this for initialization
	void Start () {
		StartCoroutine ("waitSeconds");
		Vector3 grenadePos = transform.position;
		Collider[] hitColliders = Physics.OverlapSphere(grenadePos, radius);

		foreach(Collider hitCol in hitColliders){
			Rigidbody rb = hitCol.GetComponent<Rigidbody>();

			if (rb != null) {
				rb.AddExplosionForce (power, grenadePos, radius, 3.0F);
				Destroy (gameObject);
			}
		}
	}

	IEnumerator waitSeconds(){
		yield return new WaitForSeconds (1.5f);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
