using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagement : MonoBehaviour {
	public static int minHeight = -100;
	public static int level;
	static List<string> levelNames = new List<string> ();
	static List<int> levelMinHeights = new List<int> ();

	void Start () {
		DontDestroyOnLoad (this.gameObject);

		levelNames.Add ("Menu");
		levelMinHeights.Add (0);

		levelNames.Add ("Level 1");
		levelMinHeights.Add (-35);

		levelNames.Add ("Level 2");
		levelMinHeights.Add (-35);

		levelNames.Add ("Level 3");
		levelMinHeights.Add (-35);

		levelNames.Add ("Level 4");
		levelMinHeights.Add (-100);

		level = 0;
		nextLevel ();

	}

	void Update () {
		
	}

	public static void nextLevel (){
		level++;
		Debug.Log (levelNames [level]);
		Debug.Log(levelMinHeights[level]);
		SceneManager.LoadScene (levelNames [level]);
		minHeight = levelMinHeights[level];

	}
}
