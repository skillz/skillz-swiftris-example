using UnityEngine;
using System.Collections.Generic;
using SkillzSDK;

namespace SkillzSDK
{
	/// <summary>
	/// Receives messages from the Skillz SDK and forwards them to the correct delegate.
	/// </summary>
	public class SkillzMessageReceiver : MonoBehaviour
	{
		//The scripts that will handle callbacks from Skillz:
		public SkillzDelegateBase DelBase = null;
		public SkillzDelegateStandard DelStandard = null;
		public SkillzDelegateTurnBased DelTurnBased = null;
		

		void Start()
		{
			//Sanity checks.
			if (DelBase == null)
			{
				UnityEngine.Debug.LogError("There is no script inheriting from 'SkillzDelegateBase' on the SkillzDelegate object!");
			}
			if (DelStandard == null && DelTurnBased == null)
			{
				UnityEngine.Debug.LogError("A script inheriting from 'SkillzDelegateStandard' or 'SkillzDelegateTurnBased' must be added to the SkillzDelegate object!");
			}
		}


		//Base messages:
		private void skillzWillExit(string ignoreMe) { DelBase.OnSkillzWillExit(); }
		private void skillzLaunchHasCompleted(string ignoreMe) { DelBase.OnSkillzLaunchCompleted(); }
		private void skillzWillLaunch(string ignoreMe) { DelBase.OnSkillzWillLaunch(); }
		private void skillzWithPlayerAbort(string ignoreMe) { DelBase.OnTournamentAborted(); }

		//Standard messages:
		private void skillzTournamentWillBegin(string gameRules) { DelStandard.OnTournamentWillBegin(ParseGameRulesStringInToDictionary(gameRules)); }
		private void skillzWithTournamentCompletion(string ignoreMe) { DelStandard.OnTournamentCompleted(); }

		//Turn-based messages:
		private void skillzTurnBasedTournamentWillBegin(string turnBasedMatchInfo) { DelTurnBased.OnTurnBasedTournamentWillBegin(new TurnBasedMatch(DeserializeJSONToDictionary(turnBasedMatchInfo))); }
		private void skillzEndTurnCompletion(string ignoreMe) { DelTurnBased.OnTurnEnd(); }
		private void skillzReviewCurrentGameState(string turnBasedMatchInfo) { DelTurnBased.OnTurnBasedReviewWillBegin(new TurnBasedMatch(DeserializeJSONToDictionary(turnBasedMatchInfo))); }
		private void skillzFinishReviewingCurrentGameState(string ignoreMe) { DelTurnBased.OnReviewEnd(); }


		//Helper functions for parsing tournament data:
		
		/// <summary>
		/// This is a convenience method for non-turn-based play that takes in a tournament rules string that is passed to skillzTournamentWillBegin().
		/// It converts that in to a Dictionary<string, string> where the keys are the rule names and the values are the rule values.
		/// If there are no rules defined, it returns an empty dictionary.
		/// </summary>
		private static Dictionary<string, string> ParseGameRulesStringInToDictionary(string gameRules)
		{
			Dictionary<string, string> resultDictionary = new Dictionary<string, string>();    // Define an empty dictionary to hold rules
			string workingString;
			// Trim the ( and ) off the ends of the rules string
			workingString = gameRules.TrimStart('{').TrimEnd('}');
			// Now if there's nothing but white space left, no rules
			if (workingString.Trim().Equals(string.Empty))
			{
				// So just return the empty dictionary
				return resultDictionary;
			}
			char[] rulesDelimeter = { ';' };
			// Now parse the remaining string based on semicolons
			string[] rules = workingString.Split(rulesDelimeter);
			// For each rule
			foreach (string rule in rules)
			{
				if (!rule.Trim().Equals(string.Empty))
				{
					char[] innerRuleDelimiters = { '=' };
					// Now parse based on =
					string[] ruleValues = rule.Split(innerRuleDelimiters);
					// Put the results in the dictionary
					string ruleKey = ruleValues[0].Trim().TrimStart('"').TrimEnd('"');
					string ruleValue = ruleValues[1].Trim();
					resultDictionary.Add(ruleKey, ruleValue);
				}
			}
			// Return the final dictionary
			return resultDictionary;
		}
		/// <summary>
		/// This is a helper method for turn-based play that will convert the string passed to skillzReviewCurrentGameState and skillzTurnBasedTournamentWillBegin
		/// It will convert this string into a Dictionary<string, object> containing both your match rules and all information contained in SKZTurnBasedMatchInfo.h.
		/// </summary>
		private static Dictionary<string, object> DeserializeJSONToDictionary(string jsonString)
		{
			return MiniJSON.Json.Deserialize(jsonString) as Dictionary<string,object>;
		}
	}
}