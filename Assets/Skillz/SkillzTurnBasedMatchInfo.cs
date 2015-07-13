using UnityEngine;
using System;
using System.Collections.Generic;
using SkillzSDK.Extensions;

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
		public readonly SkillzSDK.TurnBasedRoundOutcome Outcome;
		
		/// <summary>
		/// Each player's score by the end of the round.
		/// If this is the first turn/round, this value will be set to NaN.
		/// Stored as a double because some games submit floating-point scores.
		/// </summary>
		public readonly double? OpponentRoundScore, MyRoundScore;

		public TurnBasedRound(JSONDict dict)
		{
			OpponentRoundScore = dict.SafeGetDoubleValue("opponentScore");
			MyRoundScore = dict.SafeGetDoubleValue("playerScore");

			uint? roundOutcome = dict.SafeGetUintValue("roundOutcome");
			if (roundOutcome == null)
			{
				Outcome = SkillzSDK.TurnBasedRoundOutcome.NoOutcome;
			}
			else
			{
				switch (roundOutcome)
				{
					case 0:
						Outcome = SkillzSDK.TurnBasedRoundOutcome.Loss;
						break;
					case 1:
						Outcome = SkillzSDK.TurnBasedRoundOutcome.Win;
						break;
					case 2:
						Outcome = SkillzSDK.TurnBasedRoundOutcome.Draw;
						break;
					case 4:
					default:
						Outcome = SkillzSDK.TurnBasedRoundOutcome.NoOutcome;
						break;
				}
			}
		}

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
		public readonly uint? PrizeZ;
		/// <summary>
		/// The prize for winning this match, assuming it's for real cash.
		/// </summary>
		public readonly double? PrizeCash;

		/// <summary>
		/// The time the tournament began, in UTC.
		/// </summary>
		public readonly DateTime? TimeTournamentBegan;

		/// <summary>
		/// The time the last turn completed, in UTC.
		/// </summary>
		public readonly DateTime? TimeLastTurnCompleted;

		/// <summary>
		/// Whether the match has already ended.
		/// This should be false unless the player is reviewing the game state of a finished match.
		/// </summary>
		public readonly bool? IsMatchOver;
		
		/// <summary>
		/// All the rounds so far in this game, in order.
		/// The current round is the last element in the list.
		/// </summary>
		public readonly List<TurnBasedRound> Rounds;

		/// <summary>
		/// The 1-based index of the current round.
		/// Each round is two turns.
		/// </summary>
		public readonly int? CurrentTurnIndex;
		
		/// <summary>
		/// Information that is only available if this isn't the first turn.
		/// </summary>
		public readonly ContinuedTurnBasedMatch? ContinueMatchData;
		
		/// <summary>
		/// Creates a new instance based on the given information coming in from the server.
		/// </summary>
		public TurnBasedMatch(JSONDict matchInfo)
			: base(matchInfo)
		{
			IsMatchOver = matchInfo.SafeGetBoolValue("isGameComplete");
			TimeTournamentBegan = matchInfo.SafeGetUnixDateTimeValue("tournamentBeganDate");
			TimeLastTurnCompleted = matchInfo.SafeGetUnixDateTimeValue("lastTurnCompletedDate");
			PrizeZ = matchInfo.SafeGetUintValue("prizePoints");
			PrizeCash = matchInfo.SafeGetDoubleValue("prizeCash");

			CurrentTurnIndex = matchInfo.SafeGetIntValue("currentTurnIndex");

			Rounds = new List<TurnBasedRound>();
			object roundsObj = matchInfo.SafeGetValue("roundInformation");
			if (roundsObj != null && roundsObj.GetType() == typeof(List<object>))
			{
				List<object> rounds = (List<object>)roundsObj;
				foreach (object round in rounds)
				{
					if (round != null && round.GetType() == typeof(JSONDict))
					{
						JSONDict roundDict = (JSONDict)round;
						TurnBasedRound roundData = new TurnBasedRound(roundDict);
						Rounds.Add(roundData);
					}
				}
			}

			ContinueMatchData = new ContinuedTurnBasedMatch(matchInfo);
		}

		public override string ToString()
		{
			string paramStr = "";
			foreach(KeyValuePair<string, string> entry in GameParams)
			{
				paramStr += " " + entry.Key + ": " + entry.Value;
			}

			string roundsStr = "";
			foreach(TurnBasedRound round in Rounds)
			{
				roundsStr += "{" + round + "} ";
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
				" TimeLastTurnCompleted: [" + TimeLastTurnCompleted + "]" +
				" IsMatchOver: [" + IsMatchOver + "]" +
				" Rounds: [" + roundsStr + "]" +
				" CurrentTurnIndex: [" + CurrentTurnIndex + "]" +
				" ContinueMatchData: [" + ContinueMatchData + "]";
		}

		#region Deprecated fields

		[Obsolete("Use 'Player.ID' instead")]
		public uint PlayerID { get { return Player.ID ?? 0; } }
		[Obsolete("Use 'Player.DisplayName' instead")]
		public string PlayerDisplayName { get { return Player.DisplayName ?? ""; } }
		[Obsolete("Use 'Player.AvatarURL' instead")]
		public string PlayerAvatarURL { get { return Player.AvatarURL ?? ""; } }
		[Obsolete("Use 'GameParams' instead")]
		public Dictionary<string, string> TournamentParams { get { return GameParams; } }

		#endregion
	}

	/// <summary>
	/// Information about a turn-based match that has already finished at least one turn.
	/// </summary>
	public struct ContinuedTurnBasedMatch
	{
		/// <summary>
		/// The game data from the end of the last turn, passed in by "SkillzSDK.Api.FinishTurn()".
		/// </summary>
		public readonly string GameData;

		/// <summary>
		/// The competing player in this match.
		/// </summary>
		public readonly Player Opponent;

		/// <summary>
		/// Each player's current total score.
		/// Stored as a double because some games submit large or floating-point scores.
		/// </summary>
		public readonly double? MyCurrentTotalScore, OpponentCurrentTotalScore;
		
		public ContinuedTurnBasedMatch(JSONDict matchInfo)
		{
			GameData = matchInfo.SafeGetStringValue("gameData");

			object player = matchInfo.SafeGetValue("player");
			if (player != null && player.GetType() == typeof(JSONDict))
			{
				JSONDict playerData = (JSONDict)player;
				MyCurrentTotalScore = playerData.SafeGetDoubleValue("currentTotalScore");
			}
			else
			{
				MyCurrentTotalScore = null;
			}

			object opponent = matchInfo.SafeGetValue("opponent");
			if (opponent != null && opponent.GetType() == typeof(JSONDict))
			{
				JSONDict opponentData = (JSONDict)opponent;
				Opponent = new Player(opponentData);
				OpponentCurrentTotalScore = opponentData.SafeGetDoubleValue("currentTotalScore");
			} 
			else 
			{
				Opponent = new Player();
				OpponentCurrentTotalScore = null;
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
		public string OpponentAvatarURL { get { return Opponent.AvatarURL ?? ""; } }
		[Obsolete("Use 'Opponent.DisplayName' instead")]
		public string OpponentDisplayName { get { return Opponent.DisplayName ?? ""; } }
		[Obsolete("Use 'Opponent.ID' instead")]
		public uint OpponentUniqueID { get { return Opponent.ID ?? 0; } }

		#endregion
	}
}
