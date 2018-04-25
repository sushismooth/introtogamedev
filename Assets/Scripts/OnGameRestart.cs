using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnGameRestart : MonoBehaviour {
	//GameObject MainMenuPanel;
	SceneManagement sceneScript;
	PointTrackerScript pointScript;
	TimerScript timerScript;
	GameObject canvas;
	// Use this for initialization
	void Start () {
		//MainMenuPanel = GameObject.Find ("MainMenuPanel");
		sceneScript = GameObject.Find ("SceneManagement").GetComponent<SceneManagement> ();
		canvas = GameObject.Find ("Canvas");
		pointScript = canvas.GetComponentInChildren<PointTrackerScript> ();
		timerScript = canvas.GetComponentInChildren<TimerScript> ();


	}
	
	public void OnGameRestartClick(){
		Destroy (sceneScript.gameObject);
		Destroy (canvas.gameObject);
		sceneScript.level = 0;
		sceneScript.loadLevel ();
	}
}
