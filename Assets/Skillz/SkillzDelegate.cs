using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using MiniJSON;

/**
 *  The methods defined below in SkillzDelegate.cs must be implemented by the game developer.
 *  This methods will be called by the Skillz API at certain points, as indicated in the comments below.
 *  This script must be attached to a copy of the SkillzDelegate prefab, because these methods are called
 *  by passing messages to the SkillzDelegate game object. Put a copy of that prefab in all the scenes
 *  in your game to ensure these methods are always accesible when they may be needed.
 */ 
public class SkillzDelegate : MonoBehaviour {
	
	// This block of code will be run immediately after launching Skillz from the multiplayer button
	// The string param here will always just be an empty string.
	public void skillzLaunchHasCompleted(string param) {
		//TODO: Game Developers implement here
		Debug.Log("Calling skillzLaunchHasCompleted()");
		Debug.Log ("skillzLaunchHasCompleted " + param);

		Application.LoadLevel("testLevel");
	}
	
	// This block of code will be run when a player is entering a game from Skillz. It should load the level and take any
	// other necessary initialization actions in order to start the game.
	// The parameter passed in to this method is a string that contains all of the game rules that have been defined for the tournament the player
	// is about to enter. These rules can be used to intialize different game types.
	public void skillzTournamentWillBegin(string param) {
		//TODO: Game Developers implement here
		Debug.Log("Game Rules: " + param);
		Dictionary<string, string> gameRulesDictionary = parseGameRulesStringInToDictionary(param);

		Debug.Log("Calling skillzTournamentWillBegin()" + gameRulesDictionary);
	}
	
	// This block of code is run when exiting Skillz back to the main application.
	// Code here can be used to make sure the player lands back in the correct place in the main game application.
	// The string param here will always just be an empty string.
	public void skillzWillExit(string param) {
		//TODO: Game Developers implement here
		Debug.Log("Calling skillzWillExit()");

		Application.LoadLevel("testInitLevel");
	}
	
	// This block of code is run when a player finishes a Skillz game and is reporting their score and exiting back to the Skillz portal.
	// Code here can be used to reset/clean up the game that was just played.
	// The string param here will always just be an empty string.
	public void skillzWithTournamentCompletion(string param) {
		//TODO: Game Developers implement here
		Debug.Log("Calling skillzWithTournamentCompletion()");
	}
	
	// This code is called when a player quits a game early via Skillz.notifyPlayerAbortWithCompletion().
	// Code here can be used to reset/clean up the game that was just played.
	public void skillzWithPlayerAbort(string param) {
		//TODO: Game Developers implement here
		Debug.Log("Calling skillzWithPlayerAbort()");
	}
	
	
#region Turn-Based
	// This code is called when a player is beginning a turn-based tournament. It should load the level and do any necessary init
	public void skillzTurnBasedTournamentWillBegin(string param) {
		// TODO: Game Developers implement here
		Dictionary<string, object> turnBasedMatchInfo = deserializeJSONToDictionary (param);

		Debug.Log("Calling skillzTurnBasedTournamentWillBegin");
		Debug.Log ("skillzTurnBasedTournamentWillBegin " + turnBasedMatchInfo);
	}

	// This code is called when a player is completing their turn within a turn-based tournament
	// It will take them back into the Skillz UI
	public void skillzEndTurnCompletion(string param) {
		// TODO: Game Developers implement here
		Debug.Log("Calling skillzEndTurnCompletion");
		Debug.Log ("skillzEndTurnCompletion " + param);
	}
	
	// This code is called in order to load an uninteractive game state, allowing the player to review the game state, but not allowing them to complete a turn or manipulate a game state.
	public void skillzReviewCurrentGameState(string param) {
		// TODO: Game Developers implement here
		Dictionary<string, object> turnBasedMatchInfo = deserializeJSONToDictionary (param);

		Debug.Log("Calling skillzReviewCurrentGameState");
		Debug.Log ("skillzReviewCurrentGameState " + turnBasedMatchInfo);
	}
	
	// This code is called when your user has finished reviewing the current game state, use this method to return to the Skillz UI.
	public void skillzFinishReviewingCurrentGameState (string param) {
		// TODO: Game Developers implement here
		Debug.Log("Calling skillzFinishReviewingCurrentGameState");
		Debug.Log ("skillzFinishReviewingCurrentGameState " + param);
	}
#endregion

	// This is a convenience method for turn based play that will convert the string passed to skillzReviewCurrentGameState and skillzTurnBasedTournamentWillBegin
	// It will convert this string into a Dictionary<string, object> containing both your match rules and all information contained in SKZTurnBasedMatchInfo.h.
	// Some values may be null, refer to SKZTurnBasedMatchInfo.h in the Skillz framework for more information.
	private Dictionary<string, object> deserializeJSONToDictionary(string jsonString) {
		var dict = Json.Deserialize(jsonString) as Dictionary<string,object>;
		return dict;
	}
	
	// This is a convenience method for non-turn based play that takes in a tournament rules string that is passed to skillzTournamentWillBegin().
	// It converts that in to a Dictionary<string, string> where the keys are the rule names and the values are the rule values.
	// If there are no rules defined, it returns an empty dictionary.
	private Dictionary<string, string> parseGameRulesStringInToDictionary(string gameRules) {
		Dictionary<string, string> resultDictionary = new Dictionary<string, string>();	// Define an empty dictionary to hold rules
		string workingString;
		workingString = gameRules.TrimStart('{').TrimEnd('}');							// Trim the ( and ) off the ends of the rules string
		if (workingString.Trim().Equals(string.Empty)) {								// Now if there's nothing but white space left, no rules
			return resultDictionary;													// So just return the empty dictionary
		}
		char[] rulesDelimeter = { ';' };	
		string[] rules = workingString.Split(rulesDelimeter);							// Now parse the remaining string based on semicolons
		foreach (string rule in rules) {												// For each rule
			if (!rule.Trim().Equals(string.Empty)) {
				char[] innerRuleDelimiters = { '=' };									
				string[] ruleValues = rule.Split(innerRuleDelimiters);			// Now parse based on =
				string ruleKey = ruleValues[0].Trim();										// Put the results in the dictionary
				string ruleValue = ruleValues[1].Trim();
				resultDictionary.Add(ruleKey, ruleValue);
			}
		}
		return resultDictionary;														// Return the final dictionary
	}
}
