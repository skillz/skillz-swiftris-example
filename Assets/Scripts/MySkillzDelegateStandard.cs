using UnityEngine;
using System.Collections;


/// <summary>
/// Responds to messages from Skillz about standard (i.e non-turn-based) tournaments.
/// </summary>
public class MySkillzDelegateStandard : SkillzSDK.SkillzDelegateStandard
{
	public override void OnTournamentWillBegin(SkillzSDK.Match matchInfo)
	{
		Debug.Log(matchInfo);
		Application.LoadLevel("NormalMatch");
	}
	public override void OnTournamentCompleted()
	{
		Application.LoadLevel("MainMenu");
	}
}