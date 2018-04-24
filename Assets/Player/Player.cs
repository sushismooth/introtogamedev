using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

	//unity components
	Rigidbody2D myRigidbody;
	LineRenderer lineRenderer;
	SpriteRenderer mySpriteRenderer;
	Animator myAnimator;
	AudioSource myAudioSource;
	ParticleSystem myParticleSystem;
	SceneManagement sceneScript;
	PointTrackerScript pointScript;

	//player properties
	public float xSpeed = 0.2f;
	public float ySpeed = 10f;
	public float maxSpeed = 20f;
	float gravity = 1f;
	public Vector2 startPos;
	bool isOnGround = true;
	bool isRunning = false;
	public static bool alive = true;

	//hook
	GameObject player;
	public LayerMask raycastLayerMask;
	SpringJoint2D hook;
	public Vector3 mousePosition;
	public Vector3 anchorPosition;
	public Vector3 direction;
	public float slopeDistance;
	public float distance;
	float distanceRatio;
	bool onHook = false;
	float hookSize;
	float hookSizeMax = 10f;
	float lineWidth = 0.2f;

	//UI
	GameObject deathOverlay;
	GameObject deathText;

	//sound effects
	public AudioClip jumpSound;
	public AudioClip hookBreak;
	public AudioClip land;



	void Start () {
		myRigidbody = GetComponent<Rigidbody2D>();
		mySpriteRenderer = GetComponent<SpriteRenderer>();
		myAnimator = GetComponent<Animator>();
		myAudioSource = GetComponent<AudioSource>();
		lineRenderer = GetComponent<LineRenderer> ();
		myParticleSystem = GetComponent<ParticleSystem> ();
		sceneScript = GameObject.Find ("SceneManagement").GetComponent<SceneManagement> ();
		pointScript = GameObject.Find ("Point Tracker").GetComponent<PointTrackerScript> ();

		player = this.gameObject;
		startPos = transform.position;

		raycastLayerMask = LayerMask.GetMask ("Floor") | LayerMask.GetMask ("Unhookable") | LayerMask.GetMask ("Traps");

		deathOverlay = GameObject.Find ("DeathOverlay");
		deathText = GameObject.Find ("DeathText");


	}

	void Update () {
		//movement
		if (alive) {
			//run ();
			jump ();
		}

		//hooks
		if (Input.GetMouseButtonDown (0)) {
			fireHook();
		}
		if (!Input.GetMouseButton (0)) {
			deleteHook();
		}
		if (hookSize < hookSizeMax) {
			hookFly ();
		}
		drawHook ();

		//death
		if (transform.position.y < sceneScript.minHeight) {
			death ();
		}
		if (Input.GetKeyDown (KeyCode.R)) {
			respawn ();
		}

		//animation
		animator ();
	}

	void FixedUpdate(){
		if (alive) {
			run ();
		}
	}

	void run() {
		if (Input.GetKey (KeyCode.A) && !Input.GetKey (KeyCode.D)) {
			mySpriteRenderer.flipX = true;
			if (isOnGround && !onHook) {
				if (myRigidbody.velocity.x > -maxSpeed) {
					myRigidbody.velocity += new Vector2 (-xSpeed, 0);
				}
			} else {
				myRigidbody.velocity = new Vector2 (myRigidbody.velocity.x - xSpeed/10, myRigidbody.velocity.y);
			}
		}
		if (Input.GetKey (KeyCode.D) && !Input.GetKey (KeyCode.A)) {
			mySpriteRenderer.flipX = false;
			if (isOnGround && !onHook) {
				if (myRigidbody.velocity.x < maxSpeed) {
					myRigidbody.velocity += new Vector2 (xSpeed, 0);
				}
			} else {
				myRigidbody.velocity = new Vector2 (myRigidbody.velocity.x + xSpeed/10, myRigidbody.velocity.y);
			}
		}

		if ((Input.GetKey (KeyCode.A) || Input.GetKey (KeyCode.D)) && !(Input.GetKey (KeyCode.A) && Input.GetKey (KeyCode.D))) {
			isRunning = true;
		} else {
			isRunning = false;
		}
	}

	void jump() {
		if ((Input.GetKeyDown (KeyCode.W) || Input.GetKeyDown (KeyCode.Space)) && isOnGround && !onHook) {
			myRigidbody.velocity = new Vector2 (myRigidbody.velocity.x, ySpeed);
			isOnGround = false;
			myAudioSource.volume = 0.8f * EffectsSliderScript.effectsVolume;
			myAudioSource.pitch = 1f;
			myAudioSource.clip = jumpSound;
			myAudioSource.Play ();
			}
			if (Input.GetKey (KeyCode.W)) {
				myRigidbody.gravityScale = 0.8f * gravity;
			} else {
				myRigidbody.gravityScale = gravity;
			}
	}

	void OnTriggerEnter2D(Collider2D collisionInfo){
		if (collisionInfo.gameObject.tag == "Floor" || collisionInfo.gameObject.tag == "Unhookable") {
			isOnGround = true;
			myAudioSource.clip = land;
			myAudioSource.pitch = 0.5f;
			myAudioSource.volume = 0.1f  * EffectsSliderScript.effectsVolume;
			myAudioSource.Play ();
			myParticleSystem.Play ();
		}
		if (collisionInfo.gameObject.tag == "Trap") {
			Debug.Log ("hit trap");
			death ();
		}
		if (collisionInfo.gameObject.tag == "Boost") {
			xSpeed = 0.3f;
			maxSpeed = 30;
		}
	}

	void OnTriggerExit2D(Collider2D collisionInfo){
		if (collisionInfo.gameObject.tag == "Floor" || collisionInfo.gameObject.tag == "Unhookable") {
			isOnGround = false;
		}
		if (collisionInfo.gameObject.tag == "Boost") {
			xSpeed = 0.2f;
			maxSpeed = 20;
		}
	}

	void OnCollisionEnter2D(Collision2D collisionInfo){
		if (collisionInfo.gameObject.tag == "Trap") {
			Debug.Log ("hit trap");
			death ();
		}
	}

	void fireHook() {
		hookSize = 0;
		GameObject.DestroyImmediate (hook);
		mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		distance = 20f;
		
	}

	void hookFly(){
		hookSize += 1;
		direction = mousePosition - player.transform.position;
		slopeDistance = Mathf.Sqrt(Mathf.Pow(direction.x,2) + Mathf.Pow (direction.y,2));
		RaycastHit2D hit = Physics2D.Raycast (player.transform.position, direction, distance * hookSize/hookSizeMax, raycastLayerMask);
		if (hit.collider != null) {
			if (hit.collider.gameObject.tag == "Floor") {
				SpringJoint2D newHook = player.AddComponent<SpringJoint2D> ();
				newHook.enableCollision = true;
				newHook.frequency = 1f;
				newHook.dampingRatio = 0.8f;					
				newHook.connectedAnchor = hit.point;
				newHook.enabled = true;

				GameObject.DestroyImmediate (hook);
				hook = newHook;
				hookSize = 10;
				myAudioSource.clip = jumpSound;
				myAudioSource.pitch = 2f;
				myAudioSource.volume = 0.5f  * EffectsSliderScript.effectsVolume;
				myAudioSource.Play ();
				} else {
				myAudioSource.clip = hookBreak;
				myAudioSource.pitch = 1f;
				myAudioSource.volume = 0.1f  * EffectsSliderScript.effectsVolume;
				myAudioSource.Play ();
				hookSize = hookSizeMax;
				}
		}
	}

	void deleteHook(){
		GameObject.DestroyImmediate (hook);
	}

	void drawHook(){
		//lineRenderer.SetWidth(0.1f, 0.1f);
		lineRenderer.startWidth = lineWidth;
		lineRenderer.endWidth = lineWidth;
		if (hook != null) {
			lineRenderer.enabled = true;
			lineRenderer.SetPosition (0, player.transform.position);
			lineRenderer.SetPosition (1, hook.connectedAnchor);
			onHook = true;
		} else if (hookSize < hookSizeMax) {
			lineRenderer.enabled = true;
			lineRenderer.SetPosition (0, player.transform.position);
			anchorPosition = player.transform.position + (direction * (distance / slopeDistance));
			anchorPosition = new Vector3 (anchorPosition.x, anchorPosition.y, 1);
			lineRenderer.SetPosition (1, Vector3.Lerp(player.transform.position, anchorPosition, hookSize / hookSizeMax));
		} else {
			lineRenderer.enabled = false;
			onHook = false;
		}
	}

	void death(){
		if (alive == true) {
			myRigidbody.velocity = new Vector3 (0, 0, 0);
			myRigidbody.isKinematic = true;
			alive = false;
			mySpriteRenderer.color = new Color (0.5f, 0.5f, 0.5f);
			deathOverlay.GetComponent<Animator>().ResetTrigger("Respawn");
			deathText.GetComponent<Animator> ().ResetTrigger ("Respawn");
			deathOverlay.GetComponent<Animator> ().SetTrigger ("Death");
			deathText.GetComponent<Animator> ().SetTrigger ("Death");
		}
	}

	void respawn(){
		/*
		myRigidbody.velocity = new Vector3 (0, 0, 0);
		transform.position = startPos;
		myRigidbody.isKinematic = false;
		alive = true;
		mySpriteRenderer.color = new Color (1,1,1);
		GameObject.DestroyImmediate (hook);
		*/
		deathOverlay.GetComponent<Animator>().ResetTrigger("Death");
		deathText.GetComponent<Animator> ().ResetTrigger ("Death");
		deathOverlay.GetComponent<Animator> ().SetTrigger ("Respawn");
		deathText.GetComponent<Animator> ().SetTrigger ("Respawn");
		sceneScript.loadLevel ();
		pointScript.resetPoints ();
		alive = true;
	}

	void animator(){
		if (!isOnGround) {
			myAnimator.Play ("player_fly");
		} else 
			if (isRunning){
			myAnimator.Play ("player_walk");
			} else {
				myAnimator.Play ("player_idle");
			}
	}
}