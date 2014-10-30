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

	void FixedUpdate() {
		if (Skillz.tournamentIsInProgress()) {
			// report in progress score to Skillz
			// for this example this is just a made up score
			Debug.Log("Skillz Heartbeat");
			Skillz.updatePlayersCurrentScore(5);
		}
	}

	// GUI callback
	void OnGUI() {
		// Basic implementation
		if (GUI.Button (new Rect(0, Screen.height / 4, 250, 100), "Score HIGH")) {
			// Report a score to Skillz
			Skillz.displayTournamentResultsWithScore(99999);
		}

		if (GUI.Button (new Rect(275, Screen.height / 4, 250, 100), "Score LOW")) {
			int score = Random.Range(0, 10000);
			Skillz.displayTournamentResultsWithScore(score);
		}
		
		if (GUI.Button (new Rect(550, Screen.height / 4, 250, 100), "ABORT")) {
			// Aborting is a way for a user to back out of a game
			Skillz.notifyPlayerAbortWithCompletion();
		}

		// Sample turn based implementation (most games do not need to worry about this) 
		if (GUI.Button (new Rect(825, Screen.height / 4, 250, 100), "SCORE HIGH TURN BASED")) {
			Skillz.completeTurnWithGameData("GAMEDATASON", "99999", 99999, 0, Skillz.SkillzTurnBasedRoundOutcome.SkillzRoundNoOutcome, Skillz.SkillzTurnBasedMatchOutcome.SkillzMatchNoOutcome);
		}
		
		if (GUI.Button (new Rect(0, Screen.height / 2, 250, 100), "SCORE LOW TURN BASED")) {
			Skillz.completeTurnWithGameData("GAMEDATASON", "0", 0, 0, Skillz.SkillzTurnBasedRoundOutcome.SkillzRoundNoOutcome, Skillz.SkillzTurnBasedMatchOutcome.SkillzMatchNoOutcome);
		}
		
		if (GUI.Button (new Rect(275, Screen.height / 2, 250, 100), "FINISH REVIEWING GAME STATE")) {
			Skillz.finishReviewingCurrentGameState();
		}
	}
}
