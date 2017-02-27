using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class Lvl1GameOver : MonoBehaviour {

	public void retry(){
		Application.LoadLevel ("Level1_PathFinding");
	}
}
