using UnityEngine;
using System;
using System.Collections.Generic;


using JSONDict = System.Collections.Generic.Dictionary<string, object>;

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

		public override string ToString()
		{
			return "TurnBasedRound: " +
				" MyRoundScore: [" + MyRoundScore + "]" +
				" OpponentRoundScore: [" + OpponentRoundScore + "]" +
				" Outcome: [" + Outcome + "]";
		}
	}


	/// <summary>
	/// Information about any kind of turn-based match.
	/// </summary>
	public class TurnBasedMatch : Match
	{
		/// <summary>
		/// The prize for winning this match, assuming it's for Z.
		/// </summary>
		public uint PrizeZ;
		/// <summary>
		/// The prize for winning this match, assuming it's for real cash.
		/// </summary>
		public float PrizeCash;

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
		public TurnBasedMatch(JSONDict matchInfo)
			: base(matchInfo)
		{
			//Note that the JSON is completely different for normal vs. turn-based matches.
			//For that reason, this constructor doesn't call the base "Match" constructor
			//    that takes in a similar dictionary; instead, it parses that data itself.

			Rounds = new List<TurnBasedRound>();
			PrizeZ = 0;
			PrizeCash = 0.0f;
			Player.ID = 0;
			Player.DisplayName = "";
			Player.AvatarURL = "";
			Player.FlagURL = "";
			TimeTournamentBegan = DateTime.Now;
			ContinueMatchData = null;
			IsMatchOver = false;
			CurrentTurnIndex = 1;

			string tryKey = "[unknown]";
			try
			{
				tryKey = "isGameComplete";
				IsMatchOver = !(matchInfo[tryKey].ToString() == "False");
				
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

				tryKey = "currentTurnIndex";
				CurrentTurnIndex = int.Parse(matchInfo[tryKey].ToString());
				
				tryKey = "player";
				Dictionary<string, object> playerInfo = null;
				if (matchInfo.ContainsKey(tryKey))
				{
					playerInfo = (Dictionary<string, object>)matchInfo[tryKey];
					Player = new Player(ref tryKey, playerInfo);
				}
				
				tryKey = "roundInformation";
				List<object> rounds = (List<object>)matchInfo[tryKey];
				foreach (object roundO in rounds)
				{
					JSONDict roundD = (JSONDict)roundO;
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
			}
			catch (Exception e)
			{
				Debug.LogError("Error parsing Skillz data into TurnBasedMatch at key '" + tryKey + "'!\n" +
				               "Please contact Skillz support at integrations@skillz.com.\n" +
				               "Error message: " + e.GetType() + "; " + e.Message);
			}
		}

		public override string ToString()
		{
			string paramStr = "";
			foreach(KeyValuePair<string, string> entry in GameParams)
			{
				paramStr += " " + entry.Key + ": " + entry.Value;
			}

			return "TurnBasedMatch: " +
				" ID: [" + ID + "]" +
				" Name: [" + Name + "]" +
				" Description: [" + Description + "]" +
				" TemplateID: [" + TemplateID + "]" +
				" SkillzDifficulty: [" + SkillzDifficulty + "]" +
				" IsCash: [" + IsCash + "]" +
				" EntryPoints: [" + EntryPoints + "]" +
				" EntryCash: [" + EntryCash + "]" +
				" GameParams: [" + paramStr + "]" +
				" Player: [" + Player + "]" +
				" PrizeZ: [" + PrizeZ + "]" +
				" PrizeCash: [" + PrizeCash + "]" +
				" TimeTournamentBegan: [" + TimeTournamentBegan + "]" +
				" IsMatchOver: [" + IsMatchOver + "]" +
				" Rounds: [" + Rounds + "]" +
				" CurrentTurnIndex: [" + CurrentTurnIndex + "]" +
				" ContinueMatchData: [" + ContinueMatchData + "]";
		}

		#region Deprecated fields

		[Obsolete("Use 'Player.ID' instead")]
		public uint PlayerID { get { return Player.ID; } set { Player.ID = value; } }
		[Obsolete("Use 'Player.DisplayName' instead")]
		public string PlayerDisplayName { get { return Player.DisplayName; } set { Player.DisplayName = value; } }
		[Obsolete("Use 'Player.AvatarURL' instead")]
		public string PlayerAvatarURL { get { return Player.AvatarURL; } set { Player.AvatarURL = value; } }
		[Obsolete("Use 'GameParams' instead")]
		public Dictionary<string, string> TournamentParams { get { return GameParams; } set { GameParams = value; } }

		#endregion
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
		public static ContinuedTurnBasedMatch? TryGetInfo(JSONDict matchInfo)
		{
			if (!matchInfo.ContainsKey("gameData"))
			{
				return null;
			}
			return new ContinuedTurnBasedMatch(matchInfo);
		}
		
		
		/// <summary>
		/// The game data from the end of the last turn, passed in by "SkillzSDK.Api.FinishTurn()".
		/// </summary>
		public string GameData;

		/// <summary>
		/// The competing player in this match.
		/// </summary>
		public Player Opponent;

		/// <summary>
		/// Each player's current total score.
		/// Stored as a double because some games submit large or floating-point scores.
		/// </summary>
		public double MyCurrentTotalScore, OpponentCurrentTotalScore;
		
		
		private ContinuedTurnBasedMatch(JSONDict matchInfo)
		{
			GameData = "";
			Opponent = new Player();
			Opponent.AvatarURL = "";
			Opponent.DisplayName = "";
			Opponent.ID = 0;
			MyCurrentTotalScore = 0.0;
			OpponentCurrentTotalScore = 0.0;
			
			string tryKey = "[null]";
			try
			{
				tryKey = "gameData";
				GameData = matchInfo[tryKey].ToString();
				
				tryKey = "opponent";
				Dictionary<string, object> opponentInfo = null;
				if (matchInfo.ContainsKey(tryKey))
				{
					opponentInfo = (Dictionary<string, object>)matchInfo[tryKey];
					Opponent = new Player(ref tryKey, opponentInfo);
				}
				
				tryKey = "currentTotalScore";
				Dictionary<string, object> playerDat = (Dictionary<string, object>)matchInfo["player"];
				if (playerDat.ContainsKey(tryKey))
				{
					MyCurrentTotalScore = double.Parse(playerDat[tryKey].ToString());
				}
				if (opponentInfo != null && opponentInfo.ContainsKey(tryKey))
				{
					OpponentCurrentTotalScore = double.Parse(opponentInfo[tryKey].ToString());
				}
			}
			catch (Exception e)
			{
				Debug.LogError("Error parsing Skillz data into ContinuedTurnBasedMatch at key '" + tryKey + "'!\n" +
				               "Please contact Skillz support at integrations@skillz.com.\n" +
				               "Error message: " + e.GetType() + "; " + e.Message);
			}
		}

		public override string ToString()
		{
			return "ContinuedTurnBasedMatch: " +
				" GameData: [" + GameData + "]" +
				" MyCurrentTotalScore: [" + MyCurrentTotalScore + "]" +
				" Opponent: [" + Opponent + "]" +
				" OpponentCurrentTotalScore: [" + OpponentCurrentTotalScore + "]";
		}

		#region Deprecated fields
		
		[Obsolete("Use 'Opponent.AvatarURL' instead")]
		public string OpponentAvatarURL { get { return Opponent.AvatarURL; } set { Opponent.AvatarURL = value; } }
		[Obsolete("Use 'Opponent.DisplayName' instead")]
		public string OpponentDisplayName { get { return Opponent.DisplayName; } set { Opponent.DisplayName = value; } }
		[Obsolete("Use 'Opponent.ID' instead")]
		public uint OpponentUniqueID { get { return Opponent.ID; } set { Opponent.ID = value; } }

		#endregion
	}
}
