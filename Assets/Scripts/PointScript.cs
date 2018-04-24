using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointScript : MonoBehaviour {

	public GameObject soundEffect;
	Animator myAnimator;
	PointTrackerScript pointScript;

	// Use this for initialization
	void Start () {
		myAnimator = GetComponent<Animator> ();
		pointScript = GameObject.Find ("Point Tracker").GetComponent<PointTrackerScript> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (Player.alive) {
			myAnimator.speed = 0.5f;
		} else if (!Player.alive) {
			myAnimator.speed = 0f;
		}
	}

	void OnTriggerEnter2D(Collider2D collisionInfo){
		if (collisionInfo.name == "Player" && collisionInfo.GetType() == typeof(CircleCollider2D)) {
			pointScript.addPoint ();
			Instantiate (soundEffect, transform.position,Quaternion.identity);
			Destroy (this.gameObject);
		}
	}
}
