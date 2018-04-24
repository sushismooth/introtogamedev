using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnGameRestart : MonoBehaviour {
	//GameObject MainMenuPanel;
	SceneManagement sceneScript;
	PointTrackerScript pointScript;
	TimerScript timerScript;
	// Use this for initialization
	void Start () {
		//MainMenuPanel = GameObject.Find ("MainMenuPanel");
		SceneManagement sceneScript = GameObject.Find ("SceneManagement").GetComponent<SceneManagement> ();
		pointScript = GameObject.Find ("Point Tracker").GetComponent<PointTrackerScript> ();
		timerScript = GameObject.Find ("Timer").GetComponent<TimerScript> ();

	}
	
	public void OnGameRestartClick(){
		sceneScript.level = 0;
		sceneScript.loadLevel ();
		pointScript.points = 0;
		timerScript.totalTime = 0;
	}
}
