using UnityEngine;
using System.Collections;

/**
 * This scene is fired when the user clicks play in the Skillz interface. 
 * It has a few buttons for the reporting mock scores to Skillz.
 * 
 * Usually these scores would be generated through normal gameplay, 
 * but this example sets these scores with fake values.
 */
public class SKZTestApp : MonoBehaviour {

	public enum TournamentTypes
	{
		Normal,
		TurnBased,
	}
	public TournamentTypes MatchType = TournamentTypes.Normal;


	void FixedUpdate() {
		if (Skillz.tournamentIsInProgress()) {
			// report in progress score to Skillz
			// this example project doesn't have any gameplay, so just give the player a constant score.
			Debug.Log("Skillz Heartbeat");
			Skillz.updatePlayersCurrentScore(5);
		}
	}

	// GUI callback
	void OnGUI() {

		//Some positioning constants for the GUI.
		Vector2 labelPos = new Vector2(20.0f, 20.0f);
		Vector2 buttonSize = new Vector2 (250.0f, 50.0f);
		float buttonXMin = (Screen.width / 2.0f) - (buttonSize.x / 2.0f);
		float highScoreButtonY = 100.0f,
			  lowScoreButtonY = 200.0f,
			  abortButtonY = 300.0f,
			  finishReviewingGameStateButtonY = 400.0f;

		//The "low score" and "high score" buttons change based on the type of tournament.
		//Additionally, turn-basd tournaments need a "finish reviewing game state" button.
		switch (MatchType) {
			case TournamentTypes.Normal:
				GUI.Label(new Rect(labelPos.x, labelPos.y, Screen.width - labelPos.x, Screen.height - labelPos.y),
			           	  "Not turn-based");
				if (GUI.Button (new Rect(buttonXMin, highScoreButtonY, buttonSize.x, buttonSize.y),
			                	"Score HIGH")) {
					// Report a large final score to Skillz
					Skillz.displayTournamentResultsWithScore(99999);
				}
				if (GUI.Button (new Rect(buttonXMin, lowScoreButtonY, buttonSize.x, buttonSize.y),
			                	"Score LOW")) {
					// Report a random small final score to Skillz
					int score = Random.Range(0, 10000);
					Skillz.displayTournamentResultsWithScore(score);
				}
			break;
			case TournamentTypes.TurnBased:
				GUI.Label(new Rect(labelPos.x, labelPos.y, Screen.width - labelPos.x, Screen.height - labelPos.y),
			           	  "Turn-based");
				// Report a large turn score to Skillz
				if (GUI.Button (new Rect(buttonXMin, highScoreButtonY, buttonSize.x, buttonSize.y),
			                	"Score HIGH")) {
					Skillz.completeTurnWithGameData("GAMEDATASON", "99999", 99999, 0,
				                                Skillz.SkillzTurnBasedRoundOutcome.SkillzRoundNoOutcome,
				                                Skillz.SkillzTurnBasedMatchOutcome.SkillzMatchNoOutcome);
				}
				// Report a random small turn score to Skillz
				if (GUI.Button (new Rect(buttonXMin, lowScoreButtonY, buttonSize.x, buttonSize.y),
			                	"Score LOW")) {
					Skillz.completeTurnWithGameData("GAMEDATASON", "0", 0, 0,
				                                	Skillz.SkillzTurnBasedRoundOutcome.SkillzRoundNoOutcome,
				                                	Skillz.SkillzTurnBasedMatchOutcome.SkillzMatchNoOutcome);
				}
				// Finish viewing the game state.
				if (GUI.Button (new Rect(buttonXMin, finishReviewingGameStateButtonY, buttonSize.x, buttonSize.y),
			                	"Finish reviewing game state")) {
					Skillz.finishReviewingCurrentGameState();
				}
			break;
		}

		//Abort button is the same regardless of game-type.
		if (GUI.Button(new Rect(buttonXMin, abortButtonY, buttonSize.x, buttonSize.y),
		               "Abort")) {
			Skillz.notifyPlayerAbortWithCompletion();
		}
	}
}
