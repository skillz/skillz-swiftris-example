using UnityEngine;
using System.Collections;

/**
 * This scene controls Skillz initialization and has a button to launch the Skillz interface.
 */
public class SKZInitTestApp : MonoBehaviour {
		
	// Use this for initialization
	void Start () {
		// General Skillz initialization
		// Id is given to us by the devportal
		Skillz.skillzInitForGameIdAndEnvironment("110", Skillz.SkillzEnvironment.SkillzSandbox);
	}

	void OnGUI() {
		// Launch Skillz on button press
		if (GUI.Button (new Rect (Screen.width / 2 - 100, Screen.height / 6, 200, Screen.height / 4), "Launch Skillz")) {
			Skillz.launchSkillz (Skillz.SkillzOrientation.SkillzLandscape);
		}
	}
}
