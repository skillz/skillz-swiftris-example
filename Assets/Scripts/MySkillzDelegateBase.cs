/// <summary>
/// Handles callbacks for basic Skillz events.
/// </summary>
public class MySkillzDelegateBase : SkillzSDK.SkillzDelegateBase
{
	/// <summary>
	/// Stores the orientation being used for the game so that it can be accessed anywhere.
	/// </summary>
	public static SkillzSDK.Orientation GameOrientation;


	public SkillzSDK.Orientation OrientationToUse = SkillzSDK.Orientation.Landscape;


	public override void OnSkillzWillExit()
	{
		//Don't do anything; we should already be in the main menu scene.
	}
	public override void OnTournamentAborted()
	{
		//Load the main menu in the background.
		UnityEngine.Application.LoadLevel("MainMenu");
	}


	void Start()
	{
		//Keep track of which orientation to use.
		GameOrientation = OrientationToUse;
	}
}