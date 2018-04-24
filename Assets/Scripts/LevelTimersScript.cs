using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelTimersScript : MonoBehaviour {
	TimerScript timerScript;
	PointTrackerScript pointScript;
	GameObject inGameUI;
	public GameObject ResultsTitle;
	public GameObject ResultsLabel;
	public GameObject ResultsMenuButton;
	Text textComponent;

	float alpha;
	// Use this for initialization
	void Awake() {
		timerScript = GameObject.Find ("Timer").GetComponent<TimerScript> ();
		pointScript = GameObject.Find ("Point Tracker").GetComponent<PointTrackerScript> ();
		inGameUI = GameObject.Find ("InGameUI");
		textComponent = GetComponent<Text> ();
		inGameUI.SetActive (false);
	}

	void Start () {
		updateText ();
		textComponent.color = new Color (1, 1, 1, 0);
		ResultsTitle.GetComponent<Text> ().color = new Color (1, 1, 1, 0);
		ResultsLabel.GetComponent<Text> ().color = new Color (1, 1, 1, 0);
		ResultsMenuButton.GetComponent<Text> ().color = new Color (1, 1, 1, 0);

	}
	
	// Update is called once per frame
	void Update () {
		if (alpha < 1) {
			textComponent.color = new Color (1, 1, 1, alpha);
			ResultsTitle.GetComponent<Text> ().color = new Color (1, 1, 1, alpha);
			ResultsLabel.GetComponent<Text> ().color = new Color (1, 1, 1, alpha);
			ResultsMenuButton.GetComponent<Text> ().color = new Color (1, 1, 1, alpha);
			alpha += Time.deltaTime;
		}
	}

	void updateText(){
		string txt = "";
		float totalTime = 0;
		for (int i = 1; i <= 4; i++) {
			txt += timerScript.levelTimers [i].ToString ("0.0") + "\n";
			totalTime += timerScript.levelTimers [i];
		}
		txt += totalTime.ToString ("0.0") + "\n" + "\n";
		txt += pointScript.points.ToString() + "/7";
		textComponent.text = txt;
	}
}
