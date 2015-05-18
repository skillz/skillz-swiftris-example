using UnityEngine;
using System.Collections;


/// <summary>
/// Responds to messages from Skillz about turn-based tournaments.
/// </summary>
public class MySkillzDelegateTurnBased : SkillzSDK.SkillzDelegateTurnBased
{
	public override void OnTurnBasedTournamentWillBegin(SkillzSDK.TurnBasedMatch matchInfo)
	{
		Application.LoadLevel("TurnBasedMatch");
	}
	public override void OnTurnEnd()
	{
		Application.LoadLevel("MainMenu");
	}

	public override void OnTurnBasedReviewWillBegin(SkillzSDK.TurnBasedMatch matchInfo)
	{
		Application.LoadLevel("TurnBasedReview");
	}
	public override void OnReviewEnd()
	{
		Application.LoadLevel("MainMenu");
	}
}