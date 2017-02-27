using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NavMeshHitPlayer : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		GameObject t1 = GameObject.Find ("Player");
		if (Vector3.Distance (transform.position, t1.transform.position) < 1) {
			SceneManager.LoadScene ("Level4DeathScene");
		}
	}
}
