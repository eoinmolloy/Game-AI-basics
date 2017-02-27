using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerCollect : MonoBehaviour {
	int score;
	public string level;
	void OnTriggerEnter(Collider other){
		if (other.gameObject.tag == "pickup") {
			other.gameObject.SetActive (false);
			score++;
		}
		if (other.gameObject.tag == "goal" && score == 3) {
			SceneManager.LoadScene (level);
		}
	}
}
