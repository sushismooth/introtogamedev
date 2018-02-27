using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter2D(Collider2D collisionInfo){
		if (collisionInfo.name == "Player" && collisionInfo.GetType() == typeof(CircleCollider2D)) {
			PointTrackerScript.addPoint ();
			Destroy (this.gameObject);
		}
	}
}
