using UnityEngine;
using System.Collections;


/// <summary>
/// Controls the "gameplay" for this project, which is really just a set of buttons
///  that the player clicks to choose the outcome of his match.
/// </summary>
public class GameLogic : MonoBehaviour
{
	public enum TournamentTypes
	{
		//A standard Skillz match where the highest score wins.
		Normal,

		//A turn-based match.
		TurnBased,
		//Reviewing the state of a turn-based match while waiting for the opponent to take their turn.
		ReviewTurnBased,
	}


	public TournamentTypes MatchType = TournamentTypes.Normal;
	
	public GUIStyle Style, LabelStyle;
	

	void FixedUpdate()
	{
		if (SkillzSDK.Api.IsTournamentInProgress)
		{
			// Report in progress score to Skillz.
			// This example project doesn't have any gameplay, so just give the player a constant score.
			SkillzSDK.Api.UpdatePlayerScore(5);
		}

		Screen.orientation = (MySkillzDelegateBase.GameOrientation == SkillzSDK.Orientation.Landscape ?
		                      	ScreenOrientation.LandscapeLeft :
		                        ScreenOrientation.Portrait);
	}

	// GUI callback
	void OnGUI()
	{
		//Some positioning constants for the GUI.
		Vector2 labelPos = new Vector2(20.0f, 20.0f);
		Vector2 buttonSize = new Vector2 (250.0f, 50.0f);
		float buttonXMin = (Screen.width / 2.0f) - (buttonSize.x / 2.0f);
		float highScoreButtonY = 100.0f,
			  lowScoreButtonY = 200.0f,
			  abortButtonY = 300.0f,
			  finishReviewingGameStateButtonY = 150.0f;

		//The "low score" and "high score" buttons change based on the type of tournament.
		//Additionally, turn-basd tournaments need a "finish reviewing game state" button.
		switch (MatchType)
		{
			case TournamentTypes.Normal:
				GUI.Label(new Rect(labelPos.x, labelPos.y, Screen.width - labelPos.x, Screen.height - labelPos.y),
			           	  "Not turn-based", LabelStyle);
				if (GUI.Button (new Rect(buttonXMin, highScoreButtonY, buttonSize.x, buttonSize.y),
			                	"Score HIGH", Style))
				{
					// Report a large final score to Skillz
					SkillzSDK.Api.FinishTournament(99999);
				}
				if (GUI.Button (new Rect(buttonXMin, lowScoreButtonY, buttonSize.x, buttonSize.y),
			                	"Score LOW", Style))
				{
					// Report a random small final score to Skillz
					int score = Random.Range(0, 10000);
					SkillzSDK.Api.FinishTournament(score);
				}
				if (GUI.Button(new Rect(buttonXMin, abortButtonY, buttonSize.x, buttonSize.y),
				               "Abort", Style))
				{
					SkillzSDK.Api.AbortGame();
				}
			break;

			case TournamentTypes.TurnBased:
				GUI.Label(new Rect(labelPos.x, labelPos.y, Screen.width - labelPos.x, Screen.height - labelPos.y),
			           	  "Turn-based", LabelStyle);
				// Report a large turn score to Skillz
				if (GUI.Button (new Rect(buttonXMin, highScoreButtonY, buttonSize.x, buttonSize.y),
			                	"Score HIGH", Style))
				{
					SkillzSDK.Api.FinishTurn("", SkillzSDK.TurnBasedRoundOutcome.NoOutcome,
				                             SkillzSDK.TurnBasedMatchOutcome.NoOutcome,
				                             "99999", 99999, 0);
				}
				// Report a random small turn score to Skillz
				if (GUI.Button (new Rect(buttonXMin, lowScoreButtonY, buttonSize.x, buttonSize.y),
			                	"Score LOW", Style))
				{
					SkillzSDK.Api.FinishTurn("", SkillzSDK.TurnBasedRoundOutcome.NoOutcome,
				                             SkillzSDK.TurnBasedMatchOutcome.NoOutcome,
				                         	 "0", 0, 0);
				}
				if (GUI.Button(new Rect(buttonXMin, abortButtonY, buttonSize.x, buttonSize.y),
				               "Abort", Style))
				{
					SkillzSDK.Api.AbortGame();
				}
			break;

			case TournamentTypes.ReviewTurnBased:
				// Finish viewing the game state.
				if (GUI.Button (new Rect(buttonXMin, finishReviewingGameStateButtonY, buttonSize.x, buttonSize.y),
			                	"Finish reviewing game state", Style))
				{
					SkillzSDK.Api.FinishReviewingTurn();
				}
			break;
		}
	}
}
