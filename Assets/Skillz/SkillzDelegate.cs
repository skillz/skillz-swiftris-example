using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using SkillzHelper;

/// <summary>
/// The methods defined below in SkillzDelegate.cs must be implemented by the game developer.
///
/// These methods will be called by the Skillz API at certain points, as indicated in the comments below.
/// This script must be attached to a copy of the SkillzDelegate prefab, because these methods are called
/// by passing messages to the SkillzDelegate game object. Put a copy of that prefab in all the scenes
/// in your game to ensure these methods are always accesible when they may be needed.
/// </summary>
public class SkillzDelegate : MonoBehaviour
{
    /// <summary>
    /// This block of code will be run immediately after launching Skillz from the multiplayer button.
    /// </summary>
    /// <param name="param">
    /// This parameter will always be an empty string and is here to meet the Unity method signature requirements.
    /// </param>
    public void skillzLaunchHasCompleted (string param)
    {
        //TODO: Game Developers implement here
		Debug.Log ("Calling skillzLaunchHasCompleted()");
    }

    /// <summary>
    /// This block of code will be run when a player is entering a NON-TURN-BASED game from Skillz. It should load the level and take any
    /// other necessary initialization actions in order to start the game.
    /// </summary>
    /// <param name="gameRules">
    /// A string that contains all of the game rules that have been defined for the tournament the player
    /// is about to enter. These rules can be used to intialize different game types.
	/// The rules are specified in teh developer portal.
    /// </param>
    public void skillzTournamentWillBegin (string gameRules)
    {
        Debug.Log ("Game Rules: " + gameRules);
        Dictionary<string, string> gameRulesDictionary = Helper.ParseGameRulesStringInToDictionary (gameRules);

        Debug.Log ("Calling skillzTournamentWillBegin()" + gameRulesDictionary);

		Application.LoadLevel ("testLevelNormal");
    }

    /// <summary>
    /// This block of code is run when exiting Skillz back to the main application.
    /// Code here can be used to make sure the player lands back in the correct place in the main game application.
    /// </summary>
    /// <param name="param">
    /// This parameter will always be an empty string and is here to meet the Unity method signature requirements.
    /// </param>
    public void skillzWillExit (string param)
    {
        Debug.Log ("Calling skillzWillExit()");
    }

    /// <summary>
    /// This block of code will be run immediately before launching Skillz.
    /// </summary>
    /// <param name="param">
    /// This parameter will always be an empty string and is here to meet the Unity method signature requirements.
    /// </param>
    public void skillzWillLaunch (string param)
    {
        //TODO: Game Developers implement here
        Debug.Log ("Calling skillzWillLaunch()");
    }

    /// <summary>
    /// This block of code is run when a player finishes a Skillz game and is reporting their score and exiting back to the Skillz portal.
    /// Code here can be used to reset/clean up the game that was just played.
    /// </summary>
    /// <param name="param">
    /// This parameter will always be an empty string and is here to meet the Unity method signature requirements.
    /// </param>
    public void skillzWithTournamentCompletion (string param)
    {
        //TODO: Game Developers implement here
        Debug.Log ("Calling skillzWithTournamentCompletion()");
    }

    /// <summary>
    /// This code is called when a player quits a game early via Skillz.notifyPlayerAbortWithCompletion().
    /// Code here can be used to reset/clean up the game that was just played.
    /// </summary>
    /// <param name="param">
    /// This parameter will always be an empty string and is here to meet the Unity method signature requirements.
    /// </param>
    public void skillzWithPlayerAbort (string param)
    {
        //TODO: Game Developers implement here
        Debug.Log ("Calling skillzWithPlayerAbort()");
    }


#region Turn-Based
    /// <summary>
    /// This code is called when a player is beginning a turn-based tournament. It should load the level and do any necessary init
    /// </summary>
    /// <param name="turnBasedMatchInfo">
    /// The current turn based game state as a JSON string.
    /// </param>
    public void skillzTurnBasedTournamentWillBegin (string turnBasedMatchInfo)
    {
        // TODO: Game Developers implement here
        Dictionary<string, object> turnBasedMatchInfoDictionary = Helper.DeserializeJSONToDictionary (turnBasedMatchInfo);

        Debug.Log ("Calling skillzTurnBasedTournamentWillBegin");
        Debug.Log ("skillzTurnBasedTournamentWillBegin " + turnBasedMatchInfoDictionary);

		Application.LoadLevel ("testLevelTurnBased");
    }

    /// <summary>
    /// This code is called when a player is completing their turn within a turn-based tournament
    /// It will take them back into the Skillz UI
    /// </summary>
    /// <param name="param">
    /// This parameter will always be an empty string and is here to meet the Unity method signature requirements.
    /// </param>
    public void skillzEndTurnCompletion (string param)
    {
        // TODO: Game Developers implement here
        Debug.Log ("Calling skillzEndTurnCompletion");
    }

    /// <summary>
    /// This code is called in order to load an uninteractive game state, 
    /// allowing the player to review the game state, but not allowing them to
    /// complete a turn or manipulate a game state.
    /// </summary>
    /// <param name="turnBasedMatchInfo">
    /// The current turn based game state as a JSON string.
    /// </param>
    public void skillzReviewCurrentGameState (string turnBasedMatchInfo)
    {
        // TODO: Game Developers implement here
        Dictionary<string, object> turnBasedMatchInfoDictionary = Helper.DeserializeJSONToDictionary (turnBasedMatchInfo);

        Debug.Log ("Calling skillzReviewCurrentGameState");
        Debug.Log ("skillzReviewCurrentGameState " + turnBasedMatchInfoDictionary);
    }

    /// <summary>
    /// This code is called when your user has finished reviewing the current game state, use this method to return to the Skillz UI.
    /// </summary>
    /// <param name="param">
    /// This parameter will always be an empty string and is here to meet the Unity method signature requirements.
    /// </param>
    public void skillzFinishReviewingCurrentGameState (string param)
    {
        // TODO: Game Developers implement here
        Debug.Log ("Calling skillzFinishReviewingCurrentGameState");
    }
#endregion

    [System.Obsolete("use SkillzHelper.Helper.DeserializeJSONToDictionary")]
    private Dictionary<string, object> deserializeJSONToDictionary (string jsonString)
    {
        return Helper.DeserializeJSONToDictionary (jsonString);
    }

    [System.Obsolete("use SkillzHelper.Helper.parseGameRulesStringInToDictionary")]
    private Dictionary<string, string> parseGameRulesStringInToDictionary (string gameRules)
    {
        return Helper.ParseGameRulesStringInToDictionary (gameRules);
    }
}
