using UnityEngine;
using System.Collections;

public class SKZTestApp : MonoBehaviour {
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	// GUI callback
	void OnGUI() {
		//		if (GUI.Button (new Rect(Screen.width / 2 - 75,50,250,100), "Score HIGH")) {
		//			Skillz.displayTournamentResultsWithScore(99999);
		//		}
		//
		//		if (GUI.Button (new Rect(Screen.width / 2 - 75,175,250,100), "Score LOW")) {
		//			int score = Random.Range(0, 10000);
		//			Skillz.displayTournamentResultsWithScore(score);
		//		}
		
		if (GUI.Button (new Rect(Screen.width / 2 - 75,300,250,100), "ABORT")) {
			Skillz.notifyPlayerAbortWithCompletion();
		}
		
		if (GUI.Button (new Rect(Screen.width / 2 - 75, 425, 250, 100), "SCORE HIGH TURN BASED")) {
			Skillz.completeTurnWithGameData("GAMEDATASON", "99999", 99999, 0, Skillz.SkillzTurnBasedRoundOutcome.SkillzRoundNoOutcome, Skillz.SkillzTurnBasedMatchOutcome.SkillzMatchNoOutcome);
		}
		
		if (GUI.Button (new Rect(Screen.width / 2 - 75, 550, 250, 100), "SCORE LOW TURN BASED")) {
			Skillz.completeTurnWithGameData("GAMEDATASON", "0", 0, 0, Skillz.SkillzTurnBasedRoundOutcome.SkillzRoundNoOutcome, Skillz.SkillzTurnBasedMatchOutcome.SkillzMatchNoOutcome);
		}
		
		if (GUI.Button (new Rect(Screen.width / 2 - 75, 675, 250, 100), "FINISH REVIEWING GAME STATE")) {
			Skillz.finishReviewingCurrentGameState();
		}
	}
}
