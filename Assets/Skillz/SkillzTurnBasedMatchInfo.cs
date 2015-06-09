using UnityEngine;
using System;
using System.Collections.Generic;


using SkillzInfoDict = System.Collections.Generic.Dictionary<string, object>;

namespace SkillzSDK
{
	/// <summary>
	/// A single round in a turn-based match.
	/// A "round" is defined as two turns (one for each player).
	/// </summary>
	public struct TurnBasedRound
	{
		/// <summary>
		/// The outcome of the round. If this is the current round, it will be set to "SkillzRoundNoOutcome".
		/// </summary>
		public SkillzSDK.TurnBasedRoundOutcome Outcome;
		
		/// <summary>
		/// Each player's score by the end of the round.
		/// If this is the first turn/round, this value will be set to NaN.
		/// Stored as a double because some games submit floating-point scores.
		/// </summary>
		public double OpponentRoundScore, MyRoundScore;
	}


	/// <summary>
	/// Information about any kind of turn-based match.
	/// </summary>
	public struct TurnBasedMatch
	{
		/// <summary>
		/// The player's unique Skillz ID number.
		/// </summary>
		public uint PlayerID;
		
		/// <summary>
		/// The amount of virtual currency ("Z") and real cash this match is worth.
		/// </summary>
		public uint PrizeZ, PrizeCash;
		
		/// <summary>
		/// The player's Skillz username.
		/// </summary>
		public string PlayerDisplayName;
		
		/// <summary>
		/// A URL linking to the player's avatar image.
		/// </summary>
		public string PlayerAvatarURL;
		
		/// <summary>
		/// If this game supports "Automatic Difficulty" (specified in the Developer Portal --
		/// https://www.developers.skillz.com/developer), this value represents the difficulty this game
		/// should have, from 1 to 10 (inclusive).
		/// Note that this value will only exist in Production, not Sandbox.
		public uint? SkillzDifficulty;

		/// <summary>
		/// Custom parameters for the tournament type being played. These parameters are defined by
		/// the game developer in the Developer Portal (https://www.developers.skillz.com/developer).
		/// </summary>
		public Dictionary<string, string> TournamentParams;

		/// <summary>
		/// The time the tournament began, in UTC.
		/// </summary>
		public DateTime TimeTournamentBegan;

		/// <summary>
		/// Whether the match has already ended.
		/// This should be false unless the player is reviewing the game state of a finished match.
		/// </summary>
		public bool IsMatchOver;
		
		/// <summary>
		/// All the rounds so far in this game, in order.
		/// The current round is the last element in the list.
		/// </summary>
		public List<TurnBasedRound> Rounds;

		/// <summary>
		/// The 1-based index of the current round.
		/// Each round is two turns.
		/// </summary>
		public int CurrentTurnIndex;
		
		/// <summary>
		/// Information that is only available if this isn't the first turn.
		/// </summary>
		public ContinuedTurnBasedMatch? ContinueMatchData;
		
		
		/// <summary>
		/// Creates a new instance based on the given information coming in from the server.
		/// </summary>
		public TurnBasedMatch(SkillzInfoDict matchInfo)
		{
			Rounds = new List<TurnBasedRound>();
			PlayerID = 0;
			PrizeZ = 0;
			PrizeCash = 0;
			PlayerDisplayName = "";
			PlayerAvatarURL = "";
			TimeTournamentBegan = DateTime.Now;
			TournamentParams = new Dictionary<string, string>();
			SkillzDifficulty = null;
			ContinueMatchData = null;
			IsMatchOver = false;
			CurrentTurnIndex = 1;

			string tryKey = "[null]";
			try
			{
				//Keep track of all the built-in parameter keys that we use.
				//Anything left over is a custom Game Parameter.
				List<string> keysSoFar = new List<string>();

				tryKey = "playerUniqueId";
				PlayerID = uint.Parse(matchInfo[tryKey].ToString());
				keysSoFar.Add(tryKey);
				
				tryKey = "prizePoints";
				PrizeZ = uint.Parse(matchInfo[tryKey].ToString());
				keysSoFar.Add(tryKey);
				tryKey = "prizeCash";
				PrizeCash = uint.Parse(matchInfo[tryKey].ToString());
				keysSoFar.Add(tryKey);
				
				tryKey = "playerDisplayName";
				PlayerDisplayName = matchInfo[tryKey].ToString();
				keysSoFar.Add(tryKey);
				tryKey = "playerAvatarURL";
				PlayerAvatarURL = matchInfo[tryKey].ToString();
				keysSoFar.Add(tryKey);
				
				tryKey = "isGameComplete";
				IsMatchOver = !(matchInfo[tryKey].ToString() == "False");
				keysSoFar.Add("isGameComplete");
				
				//The date is in the form "yyyy-mm-dd hh:mm:ss +0000".
				tryKey = "tournamentBeganDate";
				string date = matchInfo[tryKey].ToString();
				int year = int.Parse(date.Substring(0, 4)),
				month = int.Parse(date.Substring(5, 2)),
				day = int.Parse(date.Substring(8, 2)),
				hour = int.Parse(date.Substring(11, 2)),
				minute = int.Parse(date.Substring(14, 2)),
				second = int.Parse(date.Substring(17, 2));
				TimeTournamentBegan = new DateTime(year, month, day, hour, minute, second);
				keysSoFar.Add(tryKey);

				tryKey = "currentTurnIndex";
				CurrentTurnIndex = int.Parse(matchInfo[tryKey].ToString());
				keysSoFar.Add(tryKey);
				
				
				tryKey = "roundInformation";
				List<object> rounds = (List<object>)matchInfo[tryKey];
				keysSoFar.Add(tryKey);
				foreach (object roundO in rounds)
				{
					SkillzInfoDict roundD = (SkillzInfoDict)roundO;
					TurnBasedRound roundData = new TurnBasedRound();
					
					tryKey = "opponentScore";
					if (!matchInfo.ContainsKey(tryKey) ||
					    !double.TryParse(matchInfo[tryKey].ToString(), out roundData.OpponentRoundScore))
					{
						roundData.OpponentRoundScore = double.NaN;
					}
					
					tryKey = "playerScore";
					if (!matchInfo.ContainsKey(tryKey) ||
					    !double.TryParse(matchInfo[tryKey].ToString(), out roundData.MyRoundScore))
					{
						roundData.MyRoundScore = double.NaN;
					}
					
					
					tryKey = "roundOutcome";
					uint roundOutcome = uint.Parse(roundD[tryKey].ToString());
					switch (roundOutcome)
					{
						case 0:
							roundData.Outcome = SkillzSDK.TurnBasedRoundOutcome.Loss;
							break;
						case 1:
							roundData.Outcome = SkillzSDK.TurnBasedRoundOutcome.Win;
							break;
						case 2:
							roundData.Outcome = SkillzSDK.TurnBasedRoundOutcome.Draw;
							break;
						case 3:
						case 4:
							roundData.Outcome = SkillzSDK.TurnBasedRoundOutcome.NoOutcome;
							break;
							
						default:
							throw new ArgumentException("Unknown round outcome " + roundOutcome.ToString());
					}

					Rounds.Add(roundData);
				}
				
				tryKey = "[ContinuedTurnBasedMatch]";
				ContinueMatchData = ContinuedTurnBasedMatch.TryGetInfo(matchInfo);
				keysSoFar.Add("gameData");
				keysSoFar.Add("opponentAvatarURL");
				keysSoFar.Add("opponentDisplayName");
				keysSoFar.Add("opponentUniqueId");
				keysSoFar.Add("opponentCurrentTotalScore");
				keysSoFar.Add("playerCurrentTotalScore");

				tryKey = "skillz_difficulty";
				if (matchInfo.ContainsKey(tryKey))
				{
					SkillzDifficulty = uint.Parse(matchInfo[tryKey] as string);
				}
				keysSoFar.Add(tryKey);

				//Now that we have all the keys used for built-in params, find the custom params.
				foreach (string key in matchInfo.Keys)
				{
					tryKey = key;
					string val = matchInfo[key] as string;
					if (val != null && !keysSoFar.Contains(key))
					{
						TournamentParams.Add(key, val);
					}
				}
			}
			catch (Exception e)
			{
				Debug.LogError("Error parsing Skillz data into TurnBasedMatch at key '" + tryKey + "'!\n" +
				               "Please contact Skillz support at integrations@skillz.com.\n" +
				               "Error message: " + e.GetType() + "; " + e.Message);
			}
		}
	}

	/// <summary>
	/// Information about a turn-based match that has already finished at least one turn.
	/// </summary>
	public struct ContinuedTurnBasedMatch
	{
		/// <summary>
		/// Tries to create the continued match info with the given dictionary.
		/// Returns "null" if the given info describes a match that's starting the first turn.
		/// </summary>
		public static ContinuedTurnBasedMatch? TryGetInfo(SkillzInfoDict matchInfo)
		{
			if (!matchInfo.ContainsKey("gameData"))
			{
				return null;
			}
			return new ContinuedTurnBasedMatch(matchInfo);
		}
		
		
		/// <summary>
		/// The game data from the end of the last turn, passed in by "Skillz.completeTurnWithGameData()".
		/// </summary>
		public string GameData;
		
		/// <summary>
		/// A URL linking to the other player's avatar image.
		/// </summary>
		public string OpponentAvatarURL;
		
		/// <summary>
		/// The Skillz username of the other player.
		/// </summary>
		public string OpponentDisplayName;
		
		/// <summary>
		/// The unique Skillz ID of the other player.
		/// </summary>
		public uint OpponentUniqueID;
		
		/// <summary>
		/// Each player's current total score.
		/// Stored as a double because some games submit floating-point scores.
		/// </summary>
		public double MyCurrentTotalScore, OpponentCurrentTotalScore;
		
		
		private ContinuedTurnBasedMatch(SkillzInfoDict matchInfo)
		{
			GameData = "";
			OpponentAvatarURL = "";
			OpponentDisplayName = "";
			OpponentUniqueID = 0;
			MyCurrentTotalScore = 0.0;
			OpponentCurrentTotalScore = 0.0;
			
			string tryKey = "[null]";
			try
			{
				tryKey = "gameData";
				GameData = matchInfo[tryKey].ToString();
				
				tryKey = "opponentAvatarURL";
				if (matchInfo.ContainsKey(tryKey))
				{
					OpponentAvatarURL = matchInfo[tryKey].ToString();
				}
				
				tryKey = "opponentDisplayName";
				if (matchInfo.ContainsKey(tryKey))
				{
					OpponentDisplayName = matchInfo[tryKey].ToString();
				}
				
				tryKey = "opponentUniqueId";
				if (matchInfo.ContainsKey(tryKey))
				{
					OpponentUniqueID = uint.Parse(matchInfo[tryKey].ToString());
				}
				
				tryKey = "playerCurrentTotalScore";
				MyCurrentTotalScore = double.Parse(matchInfo[tryKey].ToString());

				tryKey = "opponentCurrentTotalScore";
				if (matchInfo.ContainsKey(tryKey))
				{
					OpponentCurrentTotalScore = double.Parse(matchInfo[tryKey].ToString());
				}
			}
			catch (Exception e)
			{
				Debug.LogError("Error parsing Skillz data into ContinuedTurnBasedMatch at key '" + tryKey + "'!\n" +
				               "Please contact Skillz support at integrations@skillz.com.\n" +
				               "Error message: " + e.GetType() + "; " + e.Message);
			}
		}
	}
}
