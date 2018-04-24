using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimerScript : MonoBehaviour {

	public float timer;
	public float totalTime;
	public float[] levelTimers = new float[6];
	public GameObject timerText;
	public GameObject resultsTimers;
	public SceneManagement sceneScript;

	// Use this for initialization
	void Start () {
		//DontDestroyOnLoad (this.gameObject);
		timerText = GameObject.Find ("TimerText");
		sceneScript = GameObject.Find ("SceneManagement").GetComponent<SceneManagement> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (Player.alive) {
			timer += Time.deltaTime;
			//timerText.GetComponent<Text> ().text = "Time:" + Mathf.Floor (timer * 10f) / 10f;

			if (timer > 9.9f) {
				timerText.GetComponent<Text> ().text = ":" + timer.ToString ("0");
			} else {
				timerText.GetComponent<Text> ().text = ":" + timer.ToString ("0.0");
			}
		}
	}

	public void resetTimer(){
		totalTime += timer;
		levelTimers [sceneScript.level - 1] = timer;
		timer = 0;
	}
}
