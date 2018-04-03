using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EffectsSliderScript : MonoBehaviour {

	Slider effectsSlider;
	public static float effectsVolume;

	// Use this for initialization
	void Start () {
		effectsSlider = this.GetComponent<Slider> ();
		//effectsVolume = 0.75f;
	}
	
	// Update is called once per frame
	public void onValueChanged(){
		effectsVolume = effectsSlider.value;
	}
}
