using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour {
	public GameObject spawn;
	float t;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		t += Time.deltaTime;
		if(t >= 5.0f){
			Instantiate (spawn, transform.position, Quaternion.identity);
			t = 0.0f;
		}
	}
}
