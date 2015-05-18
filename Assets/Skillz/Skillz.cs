using UnityEngine;
using System.Collections;
using System.Runtime.InteropServices;

/// <summary>
/// The *deprecated* wrapper for the Skillz native Objective-C API. Use the Api class instead.
/// </summary>
[System.Obsolete("Use the SkillzSDK.Api class instead")]
public class Skillz : MonoBehaviour
{
    public enum SkillzOrientation
    {
        SkillzLandscape,
        SkillzPortrait
    };

    /// <summary>
    /// Sandbox allows for testing of both cash and Z games.
    /// Production should only be used for the actual final release into the app store.
    /// </summary>
    public enum SkillzEnvironment
    {
        SkillzSandbox,
        SkillzProduction
    };

    /// <summary>
    /// The various possible outcomes for a round.
    /// A "round" is a set of two turns (one for each player)
    /// </summary>
    public enum SkillzTurnBasedRoundOutcome
    {
        SkillzRoundLoss,
        SkillzRoundWin,
        SkillzRoundDraw,
        SkillzRoundNoOutcome
    };

    /// <summary>
    /// The various possible outcomes for a match.
    /// </summary>
    public enum SkillzTurnBasedMatchOutcome
    {
        SkillzMatchLoss,
        SkillzMatchWin,
        SkillzMatchDraw,
        SkillzMatchNoOutcome
    };


    #region Getters

    /// <summary>
    /// This method will return a random integer. Use this anywhere in your game where randomness impacts the game player.
    /// </summary>
    /// 
    /// <returns>
    /// Returns the same set of random numbers to all players, ensuring they get the same gameplay
    /// experience and fairness.
    /// </returns>
    public static int getRandomNumber ()
    {
        return SkillzSDK.Api.GetRandomNumber();
    }

    /// <summary>
    /// This method will return a random integer. Use this anywhere in your game where randomness impacts the game player.
    /// </summary>
    /// 
    /// <param name="min">The minimum possible value returned, inclusive.</param>
    /// <param name="max">The maximum possible value returned, exclusive.</param>
    /// 
    /// <returns>
    /// Returns the same set of random numbers to all players, ensuring they get the same gameplay
    /// experience and fairness.
    /// </returns>
    public static int getRandomNumberWithMinAndMax (int min, int max)
    {
        return SkillzSDK.Api.GetRandomNumber(min, max);
    }

    /// <summary>
    /// Gets whether we are currently in a Skillz tournament.
    /// Use this method to have different logic in single player than in multiplayer.
    /// </summary>
    /// 
    /// <returns>
    /// Returns true if the player is currently engaged in a Skillz tournament. 
    /// Otherwise, it returns false.
    /// </returns>
    public static bool tournamentIsInProgress ()
    {
        return SkillzSDK.Api.IsTournamentInProgress;
    }

    /// <summary>
    /// Gets the Skillz username of the person currently playing.
    /// </summary>
    /// 
    /// <returns>
    /// Returns a string containing the display name of the currently logged in Skillz User, or null.
    /// </returns>
    public static string currentUserDisplayName ()
    {
        return SkillzSDK.Api.CurrentUserDisplayName;
    }

    /// <summary>
    /// Gets the version of the SDK being used.
    /// </summary>
    /// 
    /// <returns>
    /// Returns a string containing the version of the Skillz SDK being used.
    /// </returns>
    public static string SDKShortVersion ()
    {
        return SkillzSDK.Api.SDKVersionShort;
    }

    /// <summary>
    /// Prints the Skillz SDK version to the logs.
    /// </summary>
    public static void showSDKVersionInfo ()
    {
        SkillzSDK.Api.PrintSDKVersionInfo();
    }

    #endregion //Getters


    /// <summary>
    /// Initialize Skillz -- call this somewhere it will be called quickly after opening the app, before the player
    /// can hit the multiplayer button. This does Skillz initialization that must occur before the Skillz portal can be launched.
    /// </summary>
    /// 
    /// <param name="gameId">The game ID number assigned to your game by the Skillz developer portal.</param>
    /// <param name="environment">one of SkillzEnvironment.SkillzSandbox for development/testing, or SkillzEnvironment.SkillzProduction for production.</param>
    public static void skillzInitForGameIdAndEnvironment (string gameId, SkillzEnvironment environment)
    {
        SkillzSDK.Api.Initialize(gameId,
                                 (environment == SkillzEnvironment.SkillzProduction ?
                                      SkillzSDK.Environment.Production :
                                      SkillzSDK.Environment.Sandbox));
    }


    /// <summary>
    /// Starts up the Skillz UI.
    /// Call this method when the player chooses the "multiplayer" option <em>if your game does not implement turn-based play</em>.
    /// If your game implements turn-based play, use <code>launchTurnBasedSkillz()</code> instead.
    /// </summary>
    /// 
    /// <param name="orientation">
    /// Pass in SkillzOrientation.SkillzLandscape if your game uses landscape or SkillzOrientation.SkillzPortrait
    /// if your game uses portrait.
    /// </param>
    public static void launchSkillz (SkillzOrientation orientation)
    {
        SkillzSDK.Api.LaunchSkillz(orientation == SkillzOrientation.SkillzLandscape ?
                                       SkillzSDK.Orientation.Landscape :
                                       SkillzSDK.Orientation.Portrait);
    }


    /// <summary>
    /// Call this method to make the player forfeit the game, returning him to the Skillz portal.
    /// </summary>
    public static void notifyPlayerAbortWithCompletion ()
    {
        SkillzSDK.Api.AbortGame();
    }

    /// <summary>
    /// Call this method every time the current player's score changes during a Skillz match.
    /// This provides important analytics and tracking for your game.
    /// </summary>
    /// 
    /// <param name="currentScoreForPlayer">The player's current score.</param>
    public static void updatePlayersCurrentScore (float currentScoreForPlayer)
    {
        SkillzSDK.Api.UpdatePlayerScore(currentScoreForPlayer);
    }


    #region Normal tournaments

    /// <summary>
    /// Starts up the Skillz UI.
    /// Call this method when the player chooses the "multiplayer" option <em>if your game implements turn-based play</em>.
    /// If your game doesn't implement turn-based play, use <code>launchSkillz()</code> instead.
    /// </summary>
    /// 
    /// <param name="orientation">
    /// Pass in SkillzOrientation.SkillzLandscape if your game uses landscape or SkillzOrientation.SkillzPortrait
    /// if your game uses portrait.
    /// </param>
    /// <param name="gameImplementsReviewGameState">
    /// Pass true here if your code supports reviewing the current game state in a turn-based match.
    /// </param>
    public static void launchTurnBasedSkillz (SkillzOrientation orientation, bool gameImplementsReviewGameState)
    {
        //Turns out that under the hood, launching turn-based Skillz is literally no different from launching normal Skillz.
        launchSkillz(orientation);
    }


    /// <summary>
    /// Call this method when a player finishes a multiplayer game. This will report the result of the game
    /// to the Skillz server, and return the player to the Skillz portal.
    /// If your game uses scores with decimal places, use <code>displayTournamentResultsWithFloatScore()</code> instead.
    /// </summary>
    /// 
    /// <param name="score">An int representing the score a player achieved in the game.</param>
    public static void displayTournamentResultsWithScore (int score)
    {
        SkillzSDK.Api.FinishTournament(score);
    }

    /// <summary>
    /// Call this method when a player finishes a multiplayer game. This will report the result of the game
    /// to the Skillz server, and return the player to the Skillz portal.
    /// Use this instead of <code>displayTournamentResultsWithScore()</code> if your game has scores with decimal places, e.g. 42.44.
    /// </summary>
    /// 
    /// <param name="score">A float representing the score a player achieved in the game.</param>
    public static void displayTournamentResultsWithFloatScore (float score)
    {
        SkillzSDK.Api.FinishTournament(score);
    }

    #endregion //Normal tournaments

    #region Turn-based tournaments

    /// <summary>
    /// Completes a turn for a turn-based game.
    /// Gives you access to a full range of options to convey to Skillz the final outcome of the match.
    /// </summary>
    /// 
    /// <param name="gameData">
    /// A Base64-encoded String object containing serialized data which can be used
    ///   to reconstruct the game state for the next turn, or to review the current game state. This exact string
    ///   will be part of the game data passed into the <code>skillzTurnBasedTournamentWillBegin()</code> message in SkillzDelegate.
    /// </param>
    /// <param name="score">
    /// A nice string representation of the player's score. Pass "null" if your game does not use scores.
    /// </param>
    /// <param name="playerTotalScore">The total score for the current player. Pass NaN if your game does not use scores.</param>
    /// <param name="opponentTotalScore">The total score for the current player's opponent. Pass NaN if your game does not use scores.</param>
    /// <param name="roundOutcome">
    /// The outcome of this round. Each round is a set of two turns -- one for each player.
    /// Used in the UI to tell players which rounds they won. Particularly useful to Skillz for objective-based games (e.x. Chess).
    /// </param>
    /// <param name="matchOutcome">The outcome of the whole match. If it's anything other than SkillzRoundNoOutcome, the match will end after this turn.</param>
    public static void completeTurnWithGameData (string gameData, string score, float playerTotalScore, float opponentTotalScore,
                                                 SkillzTurnBasedRoundOutcome roundOutcome, SkillzTurnBasedMatchOutcome matchOutcome)
    {
        SkillzSDK.Api.FinishTurn(gameData,
                                 (roundOutcome == SkillzTurnBasedRoundOutcome.SkillzRoundLoss ?
                                       SkillzSDK.TurnBasedRoundOutcome.Loss :
                                       (roundOutcome == SkillzTurnBasedRoundOutcome.SkillzRoundWin ?
                                           SkillzSDK.TurnBasedRoundOutcome.Win :
                                           (roundOutcome == SkillzTurnBasedRoundOutcome.SkillzRoundDraw ?
                                               SkillzSDK.TurnBasedRoundOutcome.Draw :
                                               SkillzSDK.TurnBasedRoundOutcome.NoOutcome))),
                                 (matchOutcome == SkillzTurnBasedMatchOutcome.SkillzMatchLoss ?
                                       SkillzSDK.TurnBasedMatchOutcome.Loss :
                                       (matchOutcome == SkillzTurnBasedMatchOutcome.SkillzMatchWin ?
                                           SkillzSDK.TurnBasedMatchOutcome.Win :
                                           (matchOutcome == SkillzTurnBasedMatchOutcome.SkillzMatchDraw ?
                                               SkillzSDK.TurnBasedMatchOutcome.Draw :
                                               SkillzSDK.TurnBasedMatchOutcome.NoOutcome))),
                                 score, playerTotalScore, opponentTotalScore);
    }

    /// <summary>
    /// When your user has finished reviewing the current state of his turn-based match, use this method to return to the Skillz UI.
    /// </summary>
    public static void finishReviewingCurrentGameState()
    {
        SkillzSDK.Api.FinishReviewingTurn();
    }

    #endregion //Turn-based tournaments


    /// <summary>
    /// Call this method to allow your game to launch into Skillz from sources external to your application (e.g. from Skillz-run advertisements).
    /// By default, this functionality is disabled (ie, set to false).
    /// 
    /// For example:
    ///
    ///     - If the user is mid-gameplay, do not call or set to false. This ensures that gameplay is not interrupted.
    ///     - If the user is at a splash screen or in an options menu, return true. This is a safe place to launch into Skillz from.
    ///
    /// When setting true, be sure that any relevant state is cleaned up in the skillzWillLaunch delegate method.
    /// </summary>
    /// <param name="allowLaunch">signifies that your application is in a state where it is safe for Skillz to launch</param>
    public static void SetShouldSkillzLaunchFromURL (bool allowLaunch)
    {
        SkillzSDK.Api.SetShouldSkillzLaunchFromURL(allowLaunch);
    }
}
