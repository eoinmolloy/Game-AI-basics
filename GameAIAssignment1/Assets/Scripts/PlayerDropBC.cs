using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDropBC : MonoBehaviour {
	int crumbNum = 1;
	float dropTime;
	float timeLeft = 1.0f;

	// Use this for initialization
	void Start () {
		
	}
	void dropBreadcrumb(){
		Vector3 currPos = new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z - 0.4f);
		var crumb = GameObject.CreatePrimitive (PrimitiveType.Sphere);
		crumb.transform.localScale = new Vector3 (0.1f, 0.1f, 0.1f);
		crumb.name = "PlayerBC" + crumbNum;
		crumb.transform.position = currPos;
		crumbNum++;
	}
	// Update is called once per frame
	void Update () {
		dropTime += Time.deltaTime;
		if (dropTime > timeLeft) {
			timeLeft = 1.5f;
			dropBreadcrumb ();
			dropTime = 0.0f;
			print ("drop crumb");
		}
	}
}
