using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Finish : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter2D(Collider2D collisionInfo){
		Debug.Log ("next level");	
		if (collisionInfo.name == "Player") {
			SceneManagement.nextLevel ();
			Destroy (this);
		}
	}
}
