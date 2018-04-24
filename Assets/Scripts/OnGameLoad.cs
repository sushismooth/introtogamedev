using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnGameLoad : MonoBehaviour {
	public GameObject controlsPanel;
	public GameObject settingsPanel;
	public GameObject inGameUI;

	// Use this for initialization
	void Start () {
		MusicSliderScript.musicVolume = 0.75f;
		EffectsSliderScript.effectsVolume = 0.75f;
		controlsPanel.SetActive (false);
		settingsPanel.SetActive (false);
		inGameUI.SetActive (false);
	}
}
