using UnityEngine;
using System.Collections;
using System.Runtime.InteropServices;

public class Skillz : MonoBehaviour
{

	/**
	 *  Interface to the native Objective-C methods that call the iOS SDK.
	 *  These methods should not be called directly; use the C# methods defined lower down in this file instead.
	 */
	[DllImport ("__Internal")]
	private static extern int _getRandomNumber ();

	[DllImport ("__Internal")]
	private static extern int _getRandomNumberWithMinAndMax (int min, int max);

	[DllImport ("__Internal")]
	private static extern void _skillzInitForGameIdAndEnvironment (string gameId, string environment);

	[DllImport ("__Internal")]
	private static extern int _tournamentIsInProgress ();

	[DllImport ("__Internal")]
	private static extern string _SDKShortVersion ();

	[DllImport ("__Internal")]
	private static extern void _showSDKVersionInfo ();

	[DllImport ("__Internal")]
	private static extern void _launchSkillz (string orientation);

	[DllImport ("__Internal")]
	private static extern void _launchSkillzTurnBased (string orientation, bool gameImplementsReviewGameState);

	[DllImport ("__Internal")]
	private static extern void _displayTournamentResultsWithScore (int score);

	[DllImport ("__Internal")]
	private static extern void _displayTournamentResultsWithFloatScore (float score);

	[DllImport ("__Internal")]
	private static extern void _completeTurnWithGameData (string gameData, string score, float playerCurrentTotalScore, float opponentCurrentTotalScore, string roundOutcome, string matchOutcome);

	[DllImport ("__Internal")]
	private static extern void _finishReviewingCurrentGameState ();

	[DllImport ("__Internal")]
	private static extern void _notifyPlayerAbortWithCompletion ();

	[DllImport ("__Internal")]
	private static extern void _updatePlayersCurrentScore (float score);

	// Enumerations to be used with launchSkillz() and skillzInitForGameIdAndEnvironment
	public enum SkillzOrientation
	{
		SkillzLandscape,
		SkillzPortrait }
	;

	public enum SkillzEnvironment
	{
		SkillzSandbox,
		SkillzProduction }
	;

	// Enumerations to be used for completeTurnWithGameData
	public enum SkillzTurnBasedRoundOutcome
	{
		SkillzRoundLoss,
		SkillzRoundWin,
		SkillzRoundDraw,
		SkillzRoundNoOutcome }
	;

	public enum SkillzTurnBasedMatchOutcome
	{
		SkillzMatchLoss,
		SkillzMatchWin,
		SkillzMatchDraw,
		SkillzMatchNoOutcome }
	;

	/**
	 *  C# methods accesible from the game that utilize the native interface
	 */

	/**
	 *  This method will return a random integer. Use this anywhere in your game where randomness impacts the game player.
	 *  Skillz.getRandomNumber() will return the same set of random numbers to all players, ensuring they get the same gameplay
	 *  experience and fairness.
	 */
	public static int getRandomNumber ()
	{
		return _getRandomNumber ();
	}

	public static int getRandomNumberWithMinAndMax (int min, int max)
	{
		return _getRandomNumberWithMinAndMax (min, max);
	}

	/**
 		Initialize Skillz -- call this somewhere it will be called quickly after opening the app, before the player
 		can hit the multiplayer button. This does Skillz initialization that must occur before the Skillz portal can be launched.

 		gameId- The game ID number assigned to your game by the Skillz developer portal.
 		environment- one of SkillzEnvironment.SkillzSandbox for development/testing, or SkillzEnvironment.SkillzProduction for production.
 	*/
	public static void skillzInitForGameIdAndEnvironment (string gameId, SkillzEnvironment environment)
	{
		string environmentString;
		if (environment == SkillzEnvironment.SkillzSandbox) {
			environmentString = "SkillzSandbox";
		} else {
			environmentString = "SkillzProduction";
		}
		_skillzInitForGameIdAndEnvironment (gameId, environmentString);
	}

	/**
	 *  This method returns true if the player is currently engaged in a Skillz tournament.
	 *  Otherwise, it returns false. Use this method to have different logic in single player than in multiplayer.
	 */
	public static bool tournamentIsInProgress ()
	{
		int isInProgress = _tournamentIsInProgress ();
		if (isInProgress == 1) {
			return true;
		} else {
			return false;
		}
	}

	/**
	 *  Returns a string containing the version of the Skillz SDK being used.
	 */
	public static string SDKShortVersion ()
	{
		return _SDKShortVersion ();
	}

	/**
	 *  Prints the Skillz SDK version to the logs.
	 */
	public static void showSDKVersionInfo ()
	{
		_showSDKVersionInfo ();
	}

	/**
	 *  Call this method when the player chooses the multiplayer option if your game does not implement turn based play. This call will launch the Skillz portal.
	 *
	 *  orientation- One of either SkillzOrientation.SkillzLandscape or SkillzOrientation.SkillzPortrait, based on how the
	 *  game is configured.
	 *
	 */
	public static void launchSkillz (SkillzOrientation orientation)
	{
		if (orientation == SkillzOrientation.SkillzLandscape) {
			_launchSkillz ("SkillzLandscape");
		} else {
			_launchSkillz ("SkillzPortrait");
		}
	}

	/**
	 *  Call this method when the player chooses the multiplayer option if your game implements turn based play. This call will launch the Skillz portal.
	 *
	 *  orientation- One of either SkillzOrientation.SkillzLandscape or SkillzOrientation.SkillzPortrait, based on how the
	 *  game is configured.
	 * 
	 * 	gameImplementsReviewGameState- Return true here if you have implemented skillzReviewCurrentGameState in SkillzDelegate.cs, otherwise pass false.
	 *
	 */
	public static void launchTurnBasedSkillz (SkillzOrientation orientation, bool gameImplementsReviewGameState)
	{
		string skillzOrientationString = (orientation == SkillzOrientation.SkillzLandscape) ? "SkillzLandscape" : "SkillzPortrait";
		_launchSkillzTurnBased (skillzOrientationString, gameImplementsReviewGameState);
	}

	/**
	 *  Call this method when a player finishes a multiplayer game. This will report the result of the game
	 *  to the Skillz server, and return the player to the Skillz portal.
	 *
	 *  score - An int representing the score a player achieved in the game
	 *
	 */
	public static void displayTournamentResultsWithScore (int score)
	{
		_displayTournamentResultsWithScore (score);
	}

	/**
     *  (Use this version if your game has scores with decimal places, e.g. 42.44)
     *
	 *  Call this method when a player finishes a multiplayer game. This will report the result of the game
	 *  to the Skillz server, and return the player to the Skillz portal.
	 *
	 *  score - A float representing the score a player achieved in the game
	 *
	 */
	public static void displayTournamentResultsWithFloatScore (float score)
	{
		_displayTournamentResultsWithFloatScore (score);
	}

	/**
	 *  If the player has the option to prematurely quit the game, call this method when the player quits.
	 *  This will report a forfeiture to the Skillz server, and return the player to the Skillz portal.
	 *
	 */
	public static void notifyPlayerAbortWithCompletion ()
	{
		_notifyPlayerAbortWithCompletion ();
	}

	/**
	 *  Completes a turn for a game, giving you access to a full range of options to convey to Skillz the final outcome of the match
	 *
	 *  @param gameData             A Base64 encoded String object containing serialized data which can be used to reconstruct the game state for the next turn, or to review the current game state.
	 *  @param score                The score the current user has obtained for *this* turn, this will be displayed in the Skillz UI, not used to calculate match outcomes.
	 *  @param playerTotalScore     The final score for the current player, passing nil will not modify the current player's score. Also used in Skillz UI, not match outcomes
	 *  @param opponentTotalScore   The final score for the opponent player, passing nil will not modify the opponent's score. Also used in Skillz UI, not match outcomes
	 *  @param turnOutcome          Passed to determine the outcome of a match and round, especially important for objective based games such as Chess.
	 Round outcomes are used in the Skillz UI. Match outcomes determine the actual winner. See documentation above for mor3e info.

	 (e.g.: Pass SkillzMatchDraw if the turn has resulted in a stalemate)
	 (e.g.: Pass (SkillzMatchWin | SkillzRoundLoss) if the turn has resulted in the current player winning the match, but losing the round.)
	 */

	public static void completeTurnWithGameData (string gameData, string score, float playerTotalScore, float opponentTotalScore, SkillzTurnBasedRoundOutcome roundOutcome, SkillzTurnBasedMatchOutcome matchOutcome)
	{
		_completeTurnWithGameData (gameData, score, playerTotalScore, opponentTotalScore, roundOutcome.ToString (), matchOutcome.ToString ());
	}

	/**
	 *  When your user has finished reviewing the current game state, use this method to return to the Skillz UI.
	 */
	public static void finishReviewingCurrentGameState ()
	{
		_finishReviewingCurrentGameState ();
	}

	/**
	 * 
     *  To provide better analytics and tracking for your game, we ask that you call this method to notify the Skillz SDK each time the current player's score changes during a Skillz match.
     *
     *  @param currentScoreForPlayer	Current score value for the player
	 */
	public static void updatePlayersCurrentScore (float currentScoreForPlayer)
	{
		_updatePlayersCurrentScore (currentScoreForPlayer);
	}
}
