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
		private void skillzTournamentWillBegin(string matchInfoJson)
		{
			Dictionary<string, object> matchInfoDict = DeserializeJSONToDictionary(matchInfoJson);
			Match match = new Match(matchInfoDict);
			DelStandard.OnTournamentWillBegin(match);
		}
		private void skillzWithTournamentCompletion(string ignoreMe) { DelStandard.OnTournamentCompleted(); }

		//Turn-based messages:
		private void skillzTurnBasedTournamentWillBegin(string turnBasedMatchInfoJson) 
		{ 
			Dictionary<string, object> turnBasedMatchInfoDict = DeserializeJSONToDictionary(turnBasedMatchInfoJson);
			TurnBasedMatch turnBasedMatch = new TurnBasedMatch(turnBasedMatchInfoDict);
			DelTurnBased.OnTurnBasedTournamentWillBegin(turnBasedMatch);
		}
		private void skillzEndTurnCompletion(string ignoreMe) { DelTurnBased.OnTurnEnd(); }
		private void skillzReviewCurrentGameState(string turnBasedMatchInfoJson) 
		{
			Dictionary<string, object> turnBasedMatchInfoDict = DeserializeJSONToDictionary(turnBasedMatchInfoJson);
			TurnBasedMatch turnBasedMatch = new TurnBasedMatch(turnBasedMatchInfoDict);
			DelTurnBased.OnTurnBasedReviewWillBegin(turnBasedMatch);
		}
		private void skillzFinishReviewingCurrentGameState(string ignoreMe) { DelTurnBased.OnReviewEnd(); }


		//Helper functions for parsing tournament data:
		
		/// <summary>
		/// This is a helper method for turn-based play that will convert the string passed to skillzReviewCurrentGameState and skillzTurnBasedTournamentWillBegin
		/// It will convert this string into a Dictionary<string, object> containing both your match rules and all information contained in SKZTurnBasedMatchInfo.h.
		/// </summary>
		private static Dictionary<string, object> DeserializeJSONToDictionary(string jsonString)
		{
			return SkillzSDK.MiniJSON.Json.Deserialize(jsonString) as Dictionary<string,object>;
		}
	}
}
