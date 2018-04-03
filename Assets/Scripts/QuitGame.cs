using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuitGame : StateMachineBehaviour {

	 // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
	override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
		Quit ();
	}
		

	public void Quit()
	{
		if (Application.isEditor) {
			UnityEditor.EditorApplication.isPlaying = false;
		} else {
			Application.Quit();
		}
	}
}
