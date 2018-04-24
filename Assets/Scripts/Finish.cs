using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Finish : MonoBehaviour {

	GameObject fade;
	SceneManagement sceneScript;
	PointTrackerScript pointScript;

	// Use this for initialization
	void Start () {
		fade = GameObject.Find ("Fade");
		sceneScript = GameObject.Find ("SceneManagement").GetComponent<SceneManagement> ();
		pointScript = GameObject.Find ("Point Tracker").GetComponent<PointTrackerScript> ();
	}
	
	// Update is called once per frame
	void Update () {
	}

	void OnTriggerEnter2D(Collider2D collisionInfo){
		if (collisionInfo.name == "Player" && collisionInfo.GetType() == typeof(CircleCollider2D)) {
			sceneScript.level++;
			sceneScript.loadLevel ();
			//Destroy (this.gameObject);
			pointScript.pointsThisLevel = 0;
			fade.GetComponent<Animator> ().SetTrigger("Start");
		}
	}
}
