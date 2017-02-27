using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDeath : MonoBehaviour {
	void OnTriggerEnter(Collider other){
		
		if (other.gameObject.tag == "enemy") {
			print ("Lose");
		}
	}
}
