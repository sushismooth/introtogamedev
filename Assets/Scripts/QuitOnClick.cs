using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuitOnClick : MonoBehaviour {
	bool quitting;
	float quitTimer;

	void Update(){
		if (quitting) {
			quitTimer -= Time.deltaTime;
			if (quitTimer < 0) {
				Application.Quit ();
			}
		}
	}

	public void Quit()
	{
		quitting = true;
		quitTimer = 1.5f;
		Debug.Log ("Quit");
/*		if (Application.isEditor) {
			UnityEditor.EditorApplication.isPlaying = false;
		} else {
			Application.Quit();
		}*/
	}
}
