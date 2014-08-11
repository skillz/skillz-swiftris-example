using UnityEngine;
using System.Collections;
using System.Collections.Generic;

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
	}
	
	// This block of code will be run when a player is entering a game from Skillz. It should load the level and take any
	// other necessary initialization actions in order to start the game.
	// The parameter passed in to this method is a string that contains all of the game rules that have been defined for the tournament the player
	// is about to enter. These rules can be used to intialize different game types.
	public void skillzTournamentWillBegin(string param) {
		//TODO: Game Developers implement here
		Debug.Log("Game Rules: " + param);
		Dictionary<string, string> gameRulesDictionary = parseGameRulesStringInToDictionary(param);

		Debug.Log("Calling skillzTournamentWillBegin()");

		Application.LoadLevel ("testLevel");
	}
	
	// This block of code is run when exiting Skillz back to the main application.
	// Code here can be used to make sure the player lands back in the correct place in the main game application.
	// The string param here will always just be an empty string.
	public void skillzWillExit(string param) {
		//TODO: Game Developers implement here
		Debug.Log("Calling skillzWillExit()");
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
	
	// This is a convenience method that takes in a tournament rules string that is passed to skillzTournamentWillBegin().
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
