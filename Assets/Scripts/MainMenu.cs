using UnityEngine;
using System.Collections;


/// <summary>
/// Handles all the behavior for the main menu.
/// </summary>
public class MainMenu : MonoBehaviour
{
	public GUIStyle Style;

	void OnGUI()
	{
		// Launch Skillz on button press
		Vector2 buttonSize = new Vector2(300.0f, 200.0f);
		if (GUI.Button (new Rect ((Screen.width / 2.0f) - (buttonSize.x / 2.0f),
		                          (Screen.height / 2.0f) - (buttonSize.y / 2.0f),
		                          buttonSize.x, buttonSize.y),
		                "Launch Skillz", Style))
		{
			SkillzSDK.Api.LaunchSkillz(MySkillzDelegateBase.GameOrientation);
			Debug.Log(SkillzSDK.Api.SDKVersionShort);
			Debug.Log(SkillzSDK.Api.Player);
		}
	}
}
