using UnityEngine;
using System.Collections;
using System.Runtime.InteropServices;

/// <summary>
/// Wrapper for Skillz native methods
/// </summary>
public class Skillz : MonoBehaviour
{

    /// Interface to the native Objective-C methods that call the iOS SDK.
    /// These methods should not be called directly; use the C# methods defined lower down in this file instead.
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

    [DllImport ("__Internal")]
    private static extern void _setShouldSkillzLaunchFromURL (bool allowLaunch);

    // Enumerations to be used with launchSkillz() and skillzInitForGameIdAndEnvironment
    public enum SkillzOrientation
    {
        SkillzLandscape,
        SkillzPortrait
    };

    public enum SkillzEnvironment
    {
        SkillzSandbox,
        SkillzProduction
    };

    // Enumerations to be used for completeTurnWithGameData
    public enum SkillzTurnBasedRoundOutcome
    {
        SkillzRoundLoss,
        SkillzRoundWin,
        SkillzRoundDraw,
        SkillzRoundNoOutcome
    };

    public enum SkillzTurnBasedMatchOutcome
    {
        SkillzMatchLoss,
        SkillzMatchWin,
        SkillzMatchDraw,
        SkillzMatchNoOutcome
    };

    /// C# methods accesible from the game that utilize the native interface

    /// <summary>
    /// This method will return a random integer. Use this anywhere in your game where randomness impacts the game player.
    /// </summary>
    /// <returns>
    /// Returns the same set of random numbers to all players, ensuring they get the same gameplay
    /// experience and fairness.
    /// </returns>
    public static int getRandomNumber ()
    {
        return _getRandomNumber ();
    }

    /// <summary>
    /// This method will return a random integer. Use this anywhere in your game where randomness impacts the game player.
    /// </summary>
    /// <param name="min">The minimum possible value returned, inclusive.</params>
    /// <param name="max">The maximum possible value returned, exclusive.</params>
    /// <returns>
    /// Returns the same set of random numbers to all players, ensuring they get the same gameplay
    /// experience and fairness.
    /// </returns>
    public static int getRandomNumberWithMinAndMax (int min, int max)
    {
        return _getRandomNumberWithMinAndMax (min, max);
    }

    /// <summary>
    /// Initialize Skillz -- call this somewhere it will be called quickly after opening the app, before the player
    /// can hit the multiplayer button. This does Skillz initialization that must occur before the Skillz portal can be launched.
    /// </summary>
    /// <param name="gameId">The game ID number assigned to your game by the Skillz developer portal.</param>
    /// <param name="environment">one of SkillzEnvironment.SkillzSandbox for development/testing, or SkillzEnvironment.SkillzProduction for production.</param>
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

    /// <summary>
    /// This method returns true if the player is currently engaged in a Skillz tournament. 
    /// Otherwise, it returns false. Use this method to have different logic in single player than in multiplayer.
    /// </summary>
    public static bool tournamentIsInProgress ()
    {
        int isInProgress = _tournamentIsInProgress ();
        if (isInProgress == 1) {
            return true;
        } else {
            return false;
        }
    }

    /// <returns>
    /// Returns a string containing the version of the Skillz SDK being used.
    /// </returns>
    public static string SDKShortVersion ()
    {
        return _SDKShortVersion ();
    }

    /// <summary>
    /// Prints the Skillz SDK version to the logs.
    /// </summary>
    public static void showSDKVersionInfo ()
    {
        _showSDKVersionInfo ();
    }

    /// <summary>
    /// Call this method when the player chooses the multiplayer option if your game does not implement turn based play. This call will launch the Skillz portal.
    /// </summary>
    /// <param name="orientation">One of either SkillzOrientation.SkillzLandscape or SkillzOrientation.SkillzPortrait, based on how the game is configured.</param>
    public static void launchSkillz (SkillzOrientation orientation)
    {
        if (orientation == SkillzOrientation.SkillzLandscape) {
            _launchSkillz ("SkillzLandscape");
        } else {
            _launchSkillz ("SkillzPortrait");
        }
    }

    /// <summary>
    /// Call this method when the player chooses the multiplayer option if your game implements turn based play. This call will launch the Skillz portal.
    /// </summary>
    /// <param name="orientation">One of either SkillzOrientation.SkillzLandscape or SkillzOrientation.SkillzPortrait, based on how the game is configured.</param>
    /// <param name="gameImplementsReviewGameState">Pass true here if you have implemented skillzReviewCurrentGameState in SkillzDelegate.cs, otherwise pass false.</param>
    public static void launchTurnBasedSkillz (SkillzOrientation orientation, bool gameImplementsReviewGameState)
    {
        string skillzOrientationString = (orientation == SkillzOrientation.SkillzLandscape) ? "SkillzLandscape" : "SkillzPortrait";
        _launchSkillzTurnBased (skillzOrientationString, gameImplementsReviewGameState);
    }

    /// <summary>
    /// Call this method when a player finishes a multiplayer game. This will report the result of the game
    /// to the Skillz server, and return the player to the Skillz portal.
    /// </summary>
    /// <param name="score">An int representing the score a player achieved in the game.</param>
    public static void displayTournamentResultsWithScore (int score)
    {
        _displayTournamentResultsWithScore (score);
    }

    /// <summary>
    /// (Use this version if your game has scores with decimal places, e.g. 42.44)
    ///
    /// Call this method when a player finishes a multiplayer game. This will report the result of the game
    /// to the Skillz server, and return the player to the Skillz portal.
    /// </summary>
    /// <param name="score">A float representing the score a player achieved in the game</param>
    public static void displayTournamentResultsWithFloatScore (float score)
    {
        _displayTournamentResultsWithFloatScore (score);
    }

    /// <summary>
    /// If the player has the option to prematurely quit the game, call this method when the player quits.
    /// This will report a forfeiture to the Skillz server, and return the player to the Skillz portal.
    /// </summary>
    public static void notifyPlayerAbortWithCompletion ()
    {
        _notifyPlayerAbortWithCompletion ();
    }

    /// <summary>
    /// Completes a turn for a game, giving you access to a full range of options to convey to Skillz the final outcome of the match
    /// </summary>
    /// <param name="gameData">A Base64 encoded String object containing serialized data which can be used to reconstruct the game state for the next turn, or to review the current game state.</param>
    /// <param name="playerTotalScore">The final score for the current player, passing nil will not modify the current player's score. Also used in Skillz UI, not match outcomes.</param>
    /// <param name="turnOutcome"> Passed to determine the outcome of a match and round, especially important for objective based games such as Chess. 
    /// Round outcomes are used in the Skillz UI. Match outcomes determine the actual winner. See documentation above for more info.
    /// (e.g.: Pass SkillzMatchDraw if the turn has resulted in a stalemate)
    /// (e.g.: Pass (SkillzMatchWin | SkillzRoundLoss) if the turn has resulted in the current player winning the match, but losing the round.)
    /// </param>
    public static void completeTurnWithGameData (string gameData, string score, float playerTotalScore, float opponentTotalScore, SkillzTurnBasedRoundOutcome roundOutcome, SkillzTurnBasedMatchOutcome matchOutcome)
    {
        _completeTurnWithGameData (gameData, score, playerTotalScore, opponentTotalScore, roundOutcome.ToString (), matchOutcome.ToString ());
    }

    /// <summary>
    /// When your user has finished reviewing the current game state, use this method to return to the Skillz UI.
    /// </summary>
    public static void finishReviewingCurrentGameState ()
    {
        _finishReviewingCurrentGameState ();
    }

    /// <summary>
    /// To provide better analytics and tracking for your game, we ask that you call this method to notify the Skillz SDK each time the current player's score changes during a Skillz match.
    /// </summary>
    /// <param name="currentScoreForPlayer">Current score value for the player</param>
    public static void updatePlayersCurrentScore (float currentScoreForPlayer)
    {
        _updatePlayersCurrentScore (currentScoreForPlayer);
    }

    /// <summary>
    /// Call this method to allow your game to launch into Skillz from sources external to your application (e.g. from Skillz-run advertisements). By default, this functionality is disabled (ie, set to false).
    /// 
    /// For example:
    ///
    ///     - If the user is mid-gameplay, do not call or set to false. This ensures that gameplay is not interrupted.
    ///     - If the user is at a splash screen or in an options menu, return true. This is a safe place to launch into Skillz from.
    ///
    /// When setting true, be sure that any relevant state is cleaned up in the skillzWillLaunch delegate method.
    /// </summary>
    /// <param name="allowLaunch">signifies that your application is in a state where it is safe for Skillz to launch</param>
    public static void setShouldSkillzLaunchFromURL (bool allowLaunch)
    {
        _setShouldSkillzLaunchFromURL (allowLaunch);
    }
}
