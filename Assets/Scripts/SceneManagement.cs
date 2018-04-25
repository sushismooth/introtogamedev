using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagement : MonoBehaviour {
	Player playerScript;
	TimerScript timerScript;
	public AudioSource backgroundMusic;
	public int minHeight = -100;
	public int level;
	public List<string> levelNames = new List<string> ();
	public List<int> levelMinHeights = new List<int> ();

	public AudioClip MenuBGM;
	public AudioClip BGM1;
	public AudioClip BGM2;
	public AudioClip BGM3;

	void Awake () {
		DontDestroyOnLoad (this.gameObject);
		timerScript = GameObject.Find ("Timer").GetComponent<TimerScript> ();
		backgroundMusic = GetComponentInChildren<AudioSource>();

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

		levelNames.Add ("End");
		levelMinHeights.Add (0);

		level = 0;
	}

	void Update () {
		if (Player.alive == true) {
			backgroundMusic.pitch = 1;
		} else if (level >= 1 && level <= 4 && backgroundMusic.pitch > 0){
			backgroundMusic.pitch -= Time.deltaTime / 2;
		}
	}

	void checkPlayerAlive(){
	}

	public void loadLevel (){
		SceneManager.LoadScene (levelNames [level]);
		timerScript.resetTimer ();
		minHeight = levelMinHeights[level];

		Debug.Log (level);
		//ChangeMusic ();
		switch (level) {
		case 0:
			backgroundMusic.clip = MenuBGM;
			break;
		case 1:
			backgroundMusic.clip = BGM1;
			break;
		case 2:
			backgroundMusic.clip = BGM2;
			break;
		case 3:
			backgroundMusic.clip = BGM2;
			break;
		case 4:
			backgroundMusic.clip = BGM3;
			break;
		case 5:
			backgroundMusic.clip = MenuBGM;
			break;
		default:
			break;
		}
		backgroundMusic.Play ();
	}

	public void UpdateMusicVolume(){
		AudioSource backgroundMusic = GameObject.Find("BackgroundMusic").GetComponent<AudioSource> ();
		backgroundMusic.volume = MusicSliderScript.musicVolume * 0.67f;
	}
}
