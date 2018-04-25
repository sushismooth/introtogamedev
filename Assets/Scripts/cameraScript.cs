using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraScript : MonoBehaviour {
	GameObject player;
	// Use this for initialization
	void Start () {
		player = GameObject.Find ("Player");
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		Vector3 position = new Vector3 (player.transform.position.x, player.transform.position.y + 5, -10);
		Vector2 pVelocity = player.GetComponent<Rigidbody2D> ().velocity;
		position += new Vector3 (pVelocity.x, pVelocity.y, 0)/5;
		transform.position = Vector3.Lerp(transform.position,position,0.2f);
	}
}
