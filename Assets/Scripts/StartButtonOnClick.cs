using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartButtonOnClick : MonoBehaviour {

	public GameObject mainMenuPanel;
	public GameObject timerText;
	public GameObject pointText;

	public void StartGame() {
		SceneManagement.level = 1;
		SceneManagement.loadLevel ();
		mainMenuPanel.SetActive (false);
		timerText.SetActive (true);
		pointText.SetActive (true);
	}
}
