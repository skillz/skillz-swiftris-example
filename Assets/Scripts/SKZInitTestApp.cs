using UnityEngine;
using System.Collections;

/**
 * This scene controls Skillz initialization and has a button to launch the Skillz interface.
 * The orientation of the program is set based on the orientation when Skillz is launched.
 */
public class SKZInitTestApp : MonoBehaviour {

	/// <summary>
	/// The orientation of the device when starting Skillz.
	/// Gets set at the start of the component's life.
	/// It is static so that it can be accessed anywhere, at any time,
	/// without an instance of this component persisting.
	/// </summary>
	public static Skillz.SkillzOrientation Orientation { get; private set; }


	[SerializeField]
	private Skillz.SkillzOrientation programOrientation;
	

	void Awake() {
		Orientation = programOrientation;
	}
	void Start () {
		// General Skillz initialization
		// Id is given to us by the devportal
		Skillz.skillzInitForGameIdAndEnvironment("240", Skillz.SkillzEnvironment.SkillzSandbox);
	}

	void OnGUI() {
		// Launch Skillz on button press
		Vector2 buttonSize = new Vector2(300.0f, 200.0f);
		if (GUI.Button (new Rect ((Screen.width / 2.0f) - (buttonSize.x / 2.0f),
		                          (Screen.height / 2.0f) - (buttonSize.y / 2.0f),
		                          buttonSize.x, buttonSize.y),
		                "Launch Skillz")) {

			Skillz.launchSkillz (Orientation);
		}
	}
}
