﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimerScript : MonoBehaviour {

	public static float timer;
	public static float totalTime;
	GameObject timerText;

	// Use this for initialization
	void Start () {
		//DontDestroyOnLoad (this.gameObject);
		timerText = GameObject.Find ("TimerText");
	}
	
	// Update is called once per frame
	void Update () {
		if (Player.alive) {
			timer += Time.deltaTime;
			//timerText.GetComponent<Text> ().text = "Time:" + Mathf.Floor (timer * 10f) / 10f;

			if (timer > 9.9f) {
				timerText.GetComponent<Text> ().text = "Time:" + timer.ToString ("0");
			} else {
				timerText.GetComponent<Text> ().text = "Time:" + timer.ToString ("0.0");
			}
		}
	}

	public static void resetTimer(){
		timer = 0;
	}
}
