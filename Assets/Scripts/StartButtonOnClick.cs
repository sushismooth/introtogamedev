using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartButtonOnClick : MonoBehaviour {

	public GameObject mainMenuPanel;
	public GameObject inGameUIPanel;

	public void StartGame() {
		SceneManagement.level = 1;
		SceneManagement.loadLevel ();
		mainMenuPanel.SetActive (false);
		inGameUIPanel.SetActive (true);
	}
}
