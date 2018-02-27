using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

	GameObject text;

	//unity components
	Rigidbody2D myRigidbody;
	LineRenderer lineRenderer;
	SpriteRenderer mySpriteRenderer;

	//player properties
	float xSpeed = 0.2f;
	float ySpeed = 10f;
	float gravity = 1f;
	public Vector2 startPos;
	bool isOnGround = true;
	bool alive = true;

	//hook
	GameObject player;
	public LayerMask raycastLayerMask;
	SpringJoint2D hook;
	public Vector3 mousePosition;
	public Vector3 anchorPosition;
	Vector3 playerPosition;
	public Vector3 direction;
	public float slopeDistance;
	public float distance;
	float distanceRatio;
	bool onHook = false;
	float hookSize;
	float hookSizeMax = 10;



	void Start () {
		startPos = transform.position;
		raycastLayerMask = LayerMask.GetMask ("Floor");
		myRigidbody = GetComponent<Rigidbody2D>();
		mySpriteRenderer = GetComponent<SpriteRenderer>();
		player = this.gameObject;
		lineRenderer = GetComponent<LineRenderer> ();
	}

	void Update () {
		//movement
		if (alive) {
			run ();
			jump ();
		}

		//hooks
		if (Input.GetMouseButtonDown (0)) {
			fireHook();
		}
		if (Input.GetMouseButtonUp (0)) {
			deleteHook();
		}
		if (hookSize < hookSizeMax) {
			hookFly ();
		}
		drawHook ();

		//death
		if (transform.position.y < SceneManagement.minHeight) {
			death ();
		}
		if (Input.GetKeyDown (KeyCode.R)) {
			respawn ();
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
		if ((Input.GetKeyDown (KeyCode.W) || Input.GetKeyDown (KeyCode.Space)) && isOnGround && !onHook) {
				myRigidbody.velocity = new Vector2 (myRigidbody.velocity.x, ySpeed);
				isOnGround = false;
			}
			if (Input.GetKey (KeyCode.W)) {
				myRigidbody.gravityScale = 0.8f * gravity;
			} else {
				myRigidbody.gravityScale = gravity;
			}
	}

	void OnTriggerEnter2D(Collider2D collisionInfo){
		if (collisionInfo.gameObject.tag == "Floor") {
			isOnGround = true;
		}
		if (collisionInfo.gameObject.tag == "Trap") {
			Debug.Log ("hit trap");
			death ();
		}
		if (collisionInfo.gameObject.tag == "Boost") {
			xSpeed = 0.3f;
		} else {
			xSpeed = 0.2f;
		}
	}

	void OnTriggerExit2D(Collider2D collisionInfo){
		if (collisionInfo.gameObject.tag == "Floor") {
			isOnGround = false;
		}
	}

	void fireHook() {
		hookSize = 0;
		GameObject.DestroyImmediate (hook);
		mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		playerPosition = player.transform.position;
		distance = 20f;
		
	}

	void hookFly(){
		hookSize += 1;
		direction = mousePosition - player.transform.position;
		slopeDistance = Mathf.Sqrt(Mathf.Pow(direction.x,2) + Mathf.Pow (direction.y,2));
		RaycastHit2D hit = Physics2D.Raycast (player.transform.position, direction, distance * hookSize/hookSizeMax, raycastLayerMask);
			if (hit.collider != null && hit.collider.gameObject.tag == "Floor") {
				SpringJoint2D newHook = player.AddComponent<SpringJoint2D> ();
				newHook.enableCollision = true;
				newHook.frequency = 1f;
				newHook.dampingRatio = 1;
				newHook.connectedAnchor = hit.point;
				newHook.enabled = true;

				GameObject.DestroyImmediate (hook);
				hook = newHook;
			hookSize = 10;
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
			anchorPosition = player.transform.position + (direction * (distance / slopeDistance));
			anchorPosition = new Vector3 (anchorPosition.x, anchorPosition.y, -10);
			lineRenderer.SetPosition (1, Vector3.Lerp(player.transform.position, anchorPosition, hookSize / hookSizeMax));
		} else {
			lineRenderer.enabled = false;
			onHook = false;
		}
	}

	void death(){
		myRigidbody.velocity = new Vector3 (0, 0, 0);
		myRigidbody.isKinematic = true;
		alive = false;
		mySpriteRenderer.color = new Color (1,0,0);
	}

	void respawn(){
		myRigidbody.velocity = new Vector3 (0, 0, 0);
		transform.position = startPos;
		myRigidbody.isKinematic = false;
		alive = true;
		mySpriteRenderer.color = new Color (1,1,1);
		GameObject.DestroyImmediate (hook);
		PointTrackerScript.resetPoints ();
	}
}