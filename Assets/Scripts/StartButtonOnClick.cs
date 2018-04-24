using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartButtonOnClick : MonoBehaviour {

	public GameObject mainMenuPanel;
	public GameObject inGameUIPanel;
	SceneManagement sceneScript;

	void Start(){
		sceneScript = GameObject.Find ("SceneManagement").GetComponent<SceneManagement> ();
	}

	public void StartGame() {
		mainMenuPanel.SetActive (false);
		inGameUIPanel.SetActive (true);
		sceneScript.level = 1;
		sceneScript.loadLevel ();
	}
}
