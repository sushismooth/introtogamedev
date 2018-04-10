using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuitOnClick : MonoBehaviour {

	public void Quit()
	{
		Application.Quit ();
/*		if (Application.isEditor) {
			UnityEditor.EditorApplication.isPlaying = false;
		} else {
			Application.Quit();
		}*/
	}
}
