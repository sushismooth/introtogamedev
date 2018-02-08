using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

	GameObject text;

	//unity components
	Rigidbody2D myRigidbody;
	LineRenderer lineRenderer;

	//player properties
	float xSpeed = 0.2f;
	float ySpeed = 10f;
	public Vector2 startPos;
	bool isOnGround = true;
	bool alive = true;

	//hook
	GameObject player;
	SpringJoint2D hook;
	Vector3 mousePosition;
	Vector3 playerPosition;
	Vector3 direction;
	float distance;
	float distanceRatio;
	bool onHook = false;
	float hookSize;
	float hookSizeMax = 10;



	void Start () {
		startPos = transform.position;

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
			fireHook();
		}
		if (Input.GetMouseButtonDown (1)) {
			deleteHook();
		}
		if (hookSize < hookSizeMax) {
			hookFly ();
		}
		drawHook ();

		death ();

		if (!alive) {
			if (Input.GetKeyDown (KeyCode.Space)) {
				transform.position = startPos;
				myRigidbody.isKinematic = false;
				alive = true;
			}
		}

	}

	void run() {
		if (Input.GetKey (KeyCode.A) && !Input.GetKey (KeyCode.D)) {
			if (isOnGround && !onHook) {
				if (myRigidbody.velocity.x > -20) {
					myRigidbody.velocity += new Vector2 (-xSpeed, 0);
				}
			} else {
				myRigidbody.velocity = new Vector2 (myRigidbody.velocity.x - xSpeed/10, myRigidbody.velocity.y);
			}
		}
		if (Input.GetKey (KeyCode.D) && !Input.GetKey (KeyCode.A)) {
			if (isOnGround && !onHook) {
				if (myRigidbody.velocity.x < 20) {
					myRigidbody.velocity += new Vector2 (xSpeed, 0);
				}
			} else {
				myRigidbody.velocity = new Vector2 (myRigidbody.velocity.x + xSpeed/10, myRigidbody.velocity.y);
			}
		}
	}

	void jump() {
			if (Input.GetKeyDown (KeyCode.W) && isOnGround && !onHook) {
				myRigidbody.velocity = new Vector2 (myRigidbody.velocity.x, ySpeed);
				isOnGround = false;
			}
			if (Input.GetKey (KeyCode.W)) {
				myRigidbody.gravityScale = 0.8f;
			} else {
				myRigidbody.gravityScale = 1f;
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

	void fireHook() {
		hookSize = 0;
		GameObject.DestroyImmediate (hook);
		mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		playerPosition = player.transform.position;
		direction = mousePosition - playerPosition;
		distance = Vector3.Distance (mousePosition + new Vector3 (0,0,10), playerPosition) + 2;
		if (distance > 20) {
			distanceRatio = 20 / distance;
			distance = 20;
		} else {
			distanceRatio = 1f;
		}
	}

	void hookFly(){
	hookSize += 1;
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
		lineRenderer.SetWidth(0.1f, 0.1f);
		if (hook != null) {
			lineRenderer.enabled = true;
			lineRenderer.SetPosition (0, player.transform.position);
			lineRenderer.SetPosition (1, hook.connectedAnchor);
			onHook = true;
		} else if (hookSize < hookSizeMax) {
				lineRenderer.enabled = true;
				lineRenderer.SetPosition (0, player.transform.position);
				lineRenderer.SetPosition (1, Vector3.Lerp(playerPosition, mousePosition, distanceRatio * hookSize / hookSizeMax));
		} else {
			lineRenderer.enabled = false;
			onHook = false;
		}
	}

	void death(){
		if (transform.position.y < SceneManager.levelMinHeight) {
			myRigidbody.velocity = new Vector3 (0, 0, 0);
			myRigidbody.isKinematic = true;
			alive = false;
		}
	}
}