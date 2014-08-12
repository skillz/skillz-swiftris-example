using UnityEngine;
using System.Collections;

public class SKZInitTestApp : MonoBehaviour {

	void OnGUI() {
		if (GUI.Button (new Rect (Screen.width / 2 - 100, Screen.height / 6, 200, Screen.height / 4), "Launch Skillz")) {
			Skillz.launchSkillz (Skillz.SkillzOrientation.SkillzLandscape);
		}
	}

	// Use this for initialization
	void Start () {
		// 213 is the Game ID
		// SkillzSandbox is the Skillz server to connect to
		// (SkillzSandbox for development/testing).
		Skillz.skillzInitForGameIdAndEnvironment("110", Skillz.SkillzEnvironment.SkillzSandbox);
	}

	// Update is called once per frame
	void Update () {

	}
}
