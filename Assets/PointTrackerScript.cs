using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointTrackerScript : MonoBehaviour {
	public static int points;
	public static int pointsThisLevel;
	// Use this for initialization
	void Start () {
		DontDestroyOnLoad (this.gameObject);
	}
	
	// Update is called once per frame
	void Update () {
		
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
