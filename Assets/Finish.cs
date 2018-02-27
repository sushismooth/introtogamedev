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
		Debug.Log (collisionInfo.GetType());
		if (collisionInfo.name == "Player" && collisionInfo.GetType() == typeof(CircleCollider2D)) {
			SceneManagement.level++;
			SceneManagement.loadLevel ();
			Destroy (this.gameObject);
		}
	}
}
