using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

	GameObject text;

	//unity components
	Rigidbody2D myRigidbody;
	LineRenderer lineRenderer;

	//player properties
	float xSpeed = 5f;
	float ySpeed = 10f;
	bool isOnGround = true;

	//hook
	GameObject player;
	SpringJoint2D hook;
	Vector3 mousePosition;
	Vector3 playerPosition;
	Vector3 direction;
	float distance;
	bool onHook = false;
	float hookSize;
	float hookSizeMax = 10;



	void Start () {
		text = GameObject.Find ("Text");

		myRigidbody = GetComponent<Rigidbody2D>();
		player = this.gameObject;
		lineRenderer = GetComponent<LineRenderer> ();
	}

	void Update () {
		//movement
		run();
		jump();

		//hooks
		if (Input.GetMouseButtonDown (0)) {
			fire();
		}
		if (Input.GetMouseButtonDown (1)) {
			deleteHook();
		}
		if (hookSize < hookSizeMax) {
			shootHook ();
		}
		drawHook ();

	}

	void run() {
		if (Input.GetKey (KeyCode.A) && !Input.GetKey (KeyCode.D)) {
			if (isOnGround && !onHook) {
				myRigidbody.velocity = new Vector2 (-xSpeed, myRigidbody.velocity.y);
			} else {
				myRigidbody.velocity = new Vector2 (myRigidbody.velocity.x - xSpeed/100, myRigidbody.velocity.y);
			}
		}
		if (Input.GetKey (KeyCode.D) && !Input.GetKey (KeyCode.A)) {
			if (isOnGround && !onHook) {
				myRigidbody.velocity = new Vector2 (xSpeed, myRigidbody.velocity.y);
			} else {
				myRigidbody.velocity = new Vector2 (myRigidbody.velocity.x + xSpeed/100, myRigidbody.velocity.y);
			}
		}
	}

	void jump() {
			if (Input.GetKeyDown (KeyCode.W) && isOnGround && !onHook) {
				myRigidbody.velocity = new Vector2 (myRigidbody.velocity.x, ySpeed);
				isOnGround = false;
			}
			if (Input.GetKey (KeyCode.W)) {
				myRigidbody.gravityScale = 0.4f;
			} else {
				myRigidbody.gravityScale = 0.5f;
			}
	}

	void OnTriggerEnter2D(Collider2D collisionInfo){
		if (collisionInfo.gameObject.tag == "Floor") {
			isOnGround = true;
			Debug.Log (collisionInfo);
		}
	}

	void onTriggerExit2D(Collider2D collisionInfo){
		if (collisionInfo.gameObject.tag == "Floor") {
			isOnGround = false;
			Debug.Log (collisionInfo);
		}
	}

	void fire() {
		hookSize = 0;
	}

	void shootHook() {
		GameObject.DestroyImmediate (hook);
		hookSize += 1;
		mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		playerPosition = player.transform.position;
		direction = mousePosition - playerPosition;
		distance = Vector3.Distance (mousePosition + new Vector3 (0,0,10), playerPosition) + 2;
		RaycastHit2D hit = Physics2D.Raycast (player.transform.position, direction, distance * hookSize/hookSizeMax);
		if (hit.collider != null) {
			SpringJoint2D newHook = player.AddComponent<SpringJoint2D> ();
			newHook.enableCollision = true;
			newHook.frequency = 1f;
			newHook.dampingRatio = 1;
			newHook.connectedAnchor = hit.point;
			newHook.enabled = true;

			GameObject.DestroyImmediate (hook);
			hook = newHook;
		}
	}

	void deleteHook(){
		GameObject.DestroyImmediate (hook);
	}

	void drawHook(){
		if (hook != null) {
			lineRenderer.enabled = true;
			lineRenderer.SetVertexCount (2);
			lineRenderer.SetPosition (0, player.transform.position);
			lineRenderer.SetPosition (1, hook.connectedAnchor);
			onHook = true;
		} else if (hookSize < hookSizeMax) {
				lineRenderer.enabled = true;
				lineRenderer.SetVertexCount (2);
				lineRenderer.SetPosition (0, player.transform.position);
				lineRenderer.SetPosition (1, Vector3.Lerp(playerPosition,mousePosition,hookSize/hookSizeMax));
		} else {
			lineRenderer.enabled = false;
			onHook = false;
		}
	}
}