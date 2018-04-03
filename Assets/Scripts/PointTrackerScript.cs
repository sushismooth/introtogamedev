using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PointTrackerScript : MonoBehaviour {
	public static int points;
	public static int pointsThisLevel;
	GameObject pointText;

	// Use this for initialization
	void Start () {
		//DontDestroyOnLoad (this.gameObject);
		pointText = GameObject.Find ("PointText");
	}
	
	// Update is called once per frame
	void Update () {
		pointText.GetComponent<Text> ().text = "Points:" + points + "(" + pointsThisLevel + ")";
	}

	public static void addPoint (){
		points++;
		pointsThisLevel++;
		Debug.Log (points);
	}

	public static void resetPoints (){
		points -= pointsThisLevel;
		pointsThisLevel = 0;
	}
}
