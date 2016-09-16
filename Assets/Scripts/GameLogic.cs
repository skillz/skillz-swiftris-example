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

	// Instantiating variables to be used to show an example on how to integrate Skillz Random Number Generation
	public int szkRandomNumber = 0;
	public int timer = 0;


	void FixedUpdate()
	{
		Screen.orientation = (MySkillzDelegateBase.GameOrientation == SkillzSDK.Orientation.Landscape ?
			ScreenOrientation.LandscapeRight :
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
			GUI.Label (new Rect (labelPos.x, labelPos.y, Screen.width - labelPos.x, Screen.height - labelPos.y),
				"Not turn-based", LabelStyle);
			
			// Example of how to Implement Skillz Random Generation for fairness. Random Number is added to score at the end just for this example.
			if (SkillzSDK.Api.IsTournamentInProgress) 
			{
				timer += 1;
				if (timer > 60) {
					timer = 0;
					szkRandomNumber += SkillzSDK.Api.GetRandomNumber (1, 10);
				}
				GUI.Label (new Rect (labelPos.x, 50.0f, Screen.width - labelPos.x, Screen.height - labelPos.y),
					"Skillz Random Number: " + szkRandomNumber.ToString (), LabelStyle);
				GUI.Label (new Rect (labelPos.x, 100.0f, Screen.width - labelPos.x, Screen.height - labelPos.y),
					"Timer: " + timer.ToString (), LabelStyle);	
			}

			if (GUI.Button (new Rect(buttonXMin, highScoreButtonY, buttonSize.x, buttonSize.y),
				"Score HIGH", Style))
			{
				// Report a large final score to Skillz
				SkillzSDK.Api.FinishTournament(9999 + szkRandomNumber);
			}
			if (GUI.Button (new Rect(buttonXMin, lowScoreButtonY, buttonSize.x, buttonSize.y),
				"Score LOW", Style))
			{
				// Report a random small final score to Skillz
				int score = Random.Range(0, 50);
				SkillzSDK.Api.FinishTournament(score + szkRandomNumber);
			}
			if (GUI.Button(new Rect(buttonXMin, abortButtonY, buttonSize.x, buttonSize.y),
				"Abort", Style))
			{
				SkillzSDK.Api.AbortGame();
			}
			break;
		}
	}
}
