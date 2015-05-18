using UnityEngine;
using System.Collections;


/// <summary>
/// Responds to messages from Skillz about standard (i.e non-turn-based) tournaments.
/// </summary>
public class MySkillzDelegateStandard : SkillzSDK.SkillzDelegateStandard
{
	public override void OnTournamentWillBegin(System.Collections.Generic.Dictionary<string, string> tournamentRules)
	{
		Application.LoadLevel("NormalMatch");
	}
	public override void OnTournamentCompleted()
	{
		Application.LoadLevel("MainMenu");
	}
}