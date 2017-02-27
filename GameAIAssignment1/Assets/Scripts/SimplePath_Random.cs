using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SimplePath_Random : MonoBehaviour {
	float timer;
	Vector3 pos;
	int closest = 1;
	string pathLetter = "";
	bool inVision;
	int state = 1;
	bool seePlayers;
	float dropTime;
	int crumbNum = 1;

	float timeLeft = 0.0f;

	// Use this for initialization
	void Start () {
		float path = Random.Range (0, 2);
		Debug.Log (path);

		if (path == 1) {
			pathLetter = "A";
		} else {
			pathLetter = "B";
		}
		float temp = 1000.0f;
		for (int i = 1; i < 5; i++) {
			GameObject obj = GameObject.Find ("WP_"+pathLetter+"_"+i);
			float num = Vector3.Distance (transform.position, obj.transform.position);
			if (num < temp) {
				temp = num;
				closest = i;
			}
		}

	}

	//checks to see if wall in vision
	void checkWalls(){
		Ray ray = new Ray(transform.position, transform.forward);
		RaycastHit hit;
		if(Physics.Raycast(ray, out hit, 100)){
			if (hit.collider.gameObject.tag == "wall") {
				state = 4;
			}
		}
	}

	//Checks if player is around 
	void seePlayer(){

		Ray ray = new Ray(transform.position, transform.forward);
		RaycastHit hit;
		if(Physics.Raycast(ray, out hit, 500)){
			if (hit.collider.gameObject.tag == "Player") {
				state = 2;
			} 
		}
	}
	//Method to follow player
	void followPlayer(){
		seePlayers = true;
		GameObject t1 = GameObject.Find ("Player");
		if (seePlayers == true) {
			transform.LookAt (t1.transform);
			timer = Time.deltaTime;
			transform.Translate (Vector3.forward * timer * 2);

			dropTime += Time.deltaTime;
			print (dropTime);
			checkWalls ();
			if (dropTime > timeLeft) {
				timeLeft = 2.0f;
				dropBreadcrumb ();
				dropTime = 0.0f;
				print ("drop crumb");
			}
			if (Vector3.Distance (transform.position, t1.transform.position) < 1) {
				SceneManager.LoadScene ("Level1DeathScene");
			}
			if (Vector3.Distance (transform.position, t1.transform.position) > 10) {
				state = 4;
			}
		} 
	}

	void patrol(){
		
		GameObject t1 = GameObject.Find ("WP_"+pathLetter+"_"+closest);
		if (Vector3.Distance (transform.position, t1.transform.position) > 2) {
			transform.LookAt (t1.transform);
			timer = Time.deltaTime;
			transform.Translate (Vector3.forward * timer*4);

			if (Vector3.Distance (transform.position, t1.transform.position) < 2.5f) {
				closest++;
				if (closest == 5) {
					closest = 1;
				}
			}
			seePlayer ();
			hearPlayer ();
		}
	}

		void hearPlayer(){
			GameObject t1 = GameObject.Find ("Player");
			if (Vector3.Distance (transform.position, t1.transform.position) < 5) {
				state = 2;
			}
		}

	void dropBreadcrumb(){
		Vector3 currPos = new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z - 0.4f);
		var crumb = GameObject.CreatePrimitive (PrimitiveType.Sphere);
		crumb.transform.localScale = new Vector3 (0.1f, 0.1f, 0.1f);
		crumb.name = "breadcrumb" + crumbNum;
		crumb.transform.position = currPos;
		crumbNum++;
	}

	void followCrumbs(){

		GameObject t1 = GameObject.Find ("breadcrumb" + (crumbNum-1));
		print ("crumb"+crumbNum);
		transform.LookAt (t1.transform);

		timer = Time.deltaTime;
		transform.Translate (Vector3.forward * timer * 5);

		if (Vector3.Distance (transform.position, t1.transform.position) < 1) {
			Destroy (t1);
			crumbNum--;
			if (crumbNum == 1) {
				state = 3;
			}

		}

	}

	void idle(){
		hearPlayer ();
		seePlayer ();
	}


	// Update is called once per frame
	void Update () {
		switch (state)
		{
		case 5:
			print ("Why hello there good sir! Let me teach you about Trigonometry!");
			break;
		case 4:
			followCrumbs ();
			print ("Crumbs");
			break;
		case 3:
			patrol ();
			print ("Patrol");
			break;
		case 2:
			followPlayer ();
			print ("Follow");
			break;
		case 1:
			idle ();
			print ("Patrol");
			break;
		default:
			print ("Incorrect intelligence level.");
			break;
		}
	}
}
