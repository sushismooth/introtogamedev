using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointPickupAudioScript : MonoBehaviour {
	public AudioClip pointPickup;
	AudioSource myAudioSource;

	// Use this for initialization
	void Start () {
		myAudioSource = GetComponent<AudioSource> ();
		myAudioSource.volume = 0.8f * EffectsSliderScript.effectsVolume;
		myAudioSource.clip = pointPickup;
		myAudioSource.Play ();
	}
}
