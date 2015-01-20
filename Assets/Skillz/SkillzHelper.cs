using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using MiniJSON;

namespace SkillzHelper
{

    /// <summary>
    /// Convenience methods for Skillz integrations
    /// </summary>
    public static class Helper
    {

        /// <summary>
        /// This is a convenience method for turn based play that will convert the string passed to skillzReviewCurrentGameState and skillzTurnBasedTournamentWillBegin
        /// It will convert this string into a Dictionary<string, object> containing both your match rules and all information contained in SKZTurnBasedMatchInfo.h.
        /// Some values may be null, refer to SKZTurnBasedMatchInfo.h in the Skillz framework for more information.
        /// </summary>
        public static Dictionary<string, object> DeserializeJSONToDictionary (string jsonString)
        {
            var dict = Json.Deserialize (jsonString) as Dictionary<string,object>;
            return dict;
        }

        /// <summary>
        /// This is a convenience method for non-turn based play that takes in a tournament rules string that is passed to skillzTournamentWillBegin().
        /// It converts that in to a Dictionary<string, string> where the keys are the rule names and the values are the rule values.
        /// If there are no rules defined, it returns an empty dictionary.
        /// </summary>
        public static Dictionary<string, string> ParseGameRulesStringInToDictionary (string gameRules)
        {
            Dictionary<string, string> resultDictionary = new Dictionary<string, string> ();	// Define an empty dictionary to hold rules
            string workingString;
            // Trim the ( and ) off the ends of the rules string
            workingString = gameRules.TrimStart ('{').TrimEnd ('}');
            // Now if there's nothing but white space left, no rules
            if (workingString.Trim ().Equals (string.Empty)) {
                // So just return the empty dictionary
                return resultDictionary;
            }
            char[] rulesDelimeter = { ';' };
            // Now parse the remaining string based on semicolons
            string[] rules = workingString.Split (rulesDelimeter);
            // For each rule
            foreach (string rule in rules) {
                if (!rule.Trim ().Equals (string.Empty)) {
                    char[] innerRuleDelimiters = { '=' };
                    // Now parse based on =
                    string[] ruleValues = rule.Split (innerRuleDelimiters);
                    // Put the results in the dictionary
                    string ruleKey = ruleValues [0].Trim ();
                    string ruleValue = ruleValues [1].Trim ();
                    resultDictionary.Add (ruleKey, ruleValue);
                }
            }
            // Return the final dictionary
            return resultDictionary;
        }
    }
}
