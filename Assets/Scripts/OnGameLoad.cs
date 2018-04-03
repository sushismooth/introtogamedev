using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnGameLoad : MonoBehaviour {

	// Use this for initialization
	void Start () {
		MusicSliderScript.musicVolume = 0.75f;
		EffectsSliderScript.effectsVolume = 0.75f;
	}
}
