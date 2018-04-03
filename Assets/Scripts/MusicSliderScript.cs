using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MusicSliderScript : MonoBehaviour {

	Slider musicSlider;
	public static float musicVolume;

	// Use this for initialization
	void Start () {
		musicSlider = this.GetComponent<Slider> ();
		//musicVolume = 0.75f;
	}
	
	public void onValueChanged(){
		musicVolume = musicSlider.value;
	}
}
