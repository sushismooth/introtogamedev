using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimerScript : MonoBehaviour {

	public static float timer;
	public static float totalTime;

	// Use this for initialization
	void Start () {
		DontDestroyOnLoad (this.gameObject);
	}
	
	// Update is called once per frame
	void Update () {
		timer += Time.deltaTime;
		GameObject.Find ("TimerText").GetComponent<Text> ().text = "Time: " + Mathf.Floor(timer*10f)/10f;
	}

	public static void resetTimer(){
		timer = 0;
	}
}
