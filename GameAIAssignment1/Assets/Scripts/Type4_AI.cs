using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Type4_AI : MonoBehaviour {

	float timer;
	Vector3 pos;
	int closest = 1;
	string pathLetter = "";
	bool inVision;
	int state = 1;
	bool seePlayers;
	float dropTime;
	int crumbNum = 1;
	int playerCrumb;
	bool checkBreadcrumbs = false;
	bool searchForCrumb = false;

	GameObject crumb;
	bool foundCrumb = false;
	bool turnBack = false;

	int bcNum;
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
			transform.Translate (Vector3.forward * timer * 4);

			dropTime += Time.deltaTime;
			//checkWalls ();
			if (dropTime > timeLeft) {
				timeLeft = 2.0f;
				dropBreadcrumb ();
				dropTime = 0.0f;
			}
			if (Vector3.Distance (transform.position, t1.transform.position) < 1.5f) {
				SceneManager.LoadScene ("Level3DeathScene");
			}
			if (Vector3.Distance (transform.position, t1.transform.position) > 10) {
				checkBreadcrumbs = true;
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
		crumb.transform.localScale = new Vector3 (0.01f, 0.01f, 0.01f);
		crumb.name = "breadcrumb" + crumbNum;
		crumb.transform.position = currPos;
		crumbNum++;
	}

	void followCrumbs(){

		GameObject t1 = GameObject.Find ("breadcrumb" + (crumbNum-1));
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
	//Creates radius around NPC, finds all colliding objects, if find name breadcrumb go to next method
	void FindPlayerWithBC(Vector3 center, float radius){
		Collider[] hitColliders = Physics.OverlapSphere(center, radius);
		if(searchForCrumb == false){
			foreach (Collider hitCol in hitColliders) {
				if (hitCol.gameObject.name.Contains ("PlayerBC")) {
					crumb = hitCol.gameObject;
					string name = crumb.name;
					string crumbName = name.Substring (8);
					playerCrumb = int.Parse (crumbName);
					state = 5;
					print ("state : " + state);
					print ("crumb : " + playerCrumb);
					checkBreadcrumbs = false;

				}
			}
		}
	}
	void OnTriggerEnter(Collider other) {
		if (other.tag == "trap") {
			print ("collide");
			turnBack = true;
		}
	}

	//Follows breadcrumb by names and destroys
	void followPlayerCrumbs(){

		GameObject t1 = GameObject.Find ("PlayerBC"+playerCrumb);
		print ("index crumb : " + t1);

		print ("I am here");
		transform.LookAt (t1.transform);

		timer = Time.deltaTime;
		transform.Translate (Vector3.forward * timer * 4);

		if (Vector3.Distance (transform.position, t1.transform.position) < 3.5f) {
			Destroy (t1);
			playerCrumb++;

		}

	}

	// Update is called once per frame
	void Update () {
		switch (state)
		{
		case 5:
			followPlayerCrumbs ();
			print ("Follow Player Crumb");
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
			print ("Idle");
			break;
		default:
			print ("Incorrect intelligence level");
			break;
		}
		if (checkBreadcrumbs == true) {
			FindPlayerWithBC (this.transform.position, 30.0f);
		}
		if (turnBack == true) {
			state = 4;
		}
	}
}
