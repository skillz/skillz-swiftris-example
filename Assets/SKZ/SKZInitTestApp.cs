using UnityEngine;
using System.Collections;

public class SKZInitTestApp : MonoBehaviour {

	void Awake() {
		// 213 is the Game ID
		// SkillzSandbox is the Skillz server to connect to
		// (SkillzSandbox for development/testing).
		Skillz.skillzInitForGameIdAndEnvironment("110", Skillz.SkillzEnvironment.SkillzSandbox);
		Skillz.launchSkillz(Skillz.SkillzOrientation.SkillzLandscape);
	}

	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {

	}
}
