using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace SkillzSDK
{
	public enum Orientation
	{
		Landscape,
		Portrait
	};

	/// <summary>
	/// Sandbox allows for testing of both cash and Z games.
	/// Production should only be used for the actual final release into the app store.
	/// </summary>
	public enum Environment
	{
		Sandbox,
		Production
	};

	/// <summary>
	/// The various possible outcomes for a round in a turn-based match.
	/// A "round" is a set of two turns (one for each player).
	/// </summary>
	public enum TurnBasedRoundOutcome
	{
		Loss,
		Win,
		Draw,
		NoOutcome
	};
	/// <summary>
	/// The various possible outcomes for a turn-based match.
	/// </summary>
	public enum TurnBasedMatchOutcome
	{
		Loss,
		Win,
		Draw,
		NoOutcome
	};


	/// <summary>
	/// Wrapper for the Skillz native Objective-C API.
	/// </summary>
	public static class Api
	{
		#region Native iOS API

		[DllImport ("__Internal")]
		private static extern void _addMetadataForMatchInProgress(string metadataJson, bool forMatchInProgress);

		[DllImport ("__Internal")]
		private static extern int _getRandomNumber();

		[DllImport ("__Internal")]
		private static extern int _getRandomNumberWithMinAndMax(int min, int max);

		[DllImport ("__Internal")]
		private static extern void _skillzInitForGameIdAndEnvironment(string gameId, string environment);

		[DllImport ("__Internal")]
		private static extern int _tournamentIsInProgress();

		// Need to use an IntPtr instead of a string here, for more information see
		// http://www.mono-project.com/docs/advanced/pinvoke/#strings-as-return-values
		[DllImport ("__Internal")]
		private static extern IntPtr _player();

		[DllImport ("__Internal")]
		private static extern IntPtr _SDKShortVersion();

		[DllImport ("__Internal")]
		private static extern void _showSDKVersionInfo();

		[DllImport ("__Internal")]
		private static extern void _launchSkillz(string orientation);

		[DllImport ("__Internal")]
		private static extern void _displayTournamentResultsWithScore(int score);

		[DllImport ("__Internal")]
		private static extern void _displayTournamentResultsWithFloatScore(float score);

		[DllImport ("__Internal")]
		private static extern void _completeTurnWithGameData(string gameData, string score, float playerCurrentTotalScore, float opponentCurrentTotalScore, string roundOutcome, string matchOutcome);

		[DllImport ("__Internal")]
		private static extern void _finishReviewingCurrentGameState();

		[DllImport ("__Internal")]
		private static extern void _notifyPlayerAbortWithCompletion();

		[DllImport ("__Internal")]
		private static extern void _updatePlayersCurrentScore(float score);

		[DllImport ("__Internal")]
		private static extern float _getRandomFloat();

		[DllImport ("__Internal")]
		private static extern void _setShouldSkillzLaunchFromURL(bool allowLaunch);

		#endregion //Native iOS API


		/// <summary>
		/// The inclusive range of possible values for the SkillzDifficulty field.
		/// </summary>
		public const uint SkillzDifficultyMin = 1,
						  SkillzDifficultyMax = 10;


		#region Properties

		/// <summary>
		/// Gets whether we are currently in a Skillz tournament.
		/// Use this method to have different logic in single player than in multiplayer.
		/// </summary>
		public static bool IsTournamentInProgress
		{
			get { return (Application.platform == RuntimePlatform.IPhonePlayer) && (_tournamentIsInProgress() != 0); }
		}

		/// <summary>
		/// Gets the Skillz username of the player.
		/// </summary>
		[Obsolete("Use 'Api.Player.DisplayName' instead")]
		public static string CurrentUserDisplayName
		{
			get { return Player.DisplayName; }
		}

		/// <summary>
		/// Gets information for the current player.
		/// </summary>
		public static Player Player
		{
			get
			{
				string playerJson = Marshal.PtrToStringAnsi(_player());
				Dictionary<string, object> playerDict = DeserializeJSONToDictionary(playerJson);
				return new Player(playerDict);
			}
		}

		/// <summary>
		/// Gets the short version of the SDK being used.
		/// </summary>
		public static string SDKVersionShort
		{
			get { return Marshal.PtrToStringAnsi(_SDKShortVersion()); }
		}

		#endregion //Properties


		/// <summary>
		/// Initializes Skillz. Note that this will be called automatically, assuming you follow
		/// the instructions on the Developer Portal and use a script that inherits from SkillzDelegateBase.
		/// </summary>
		/// 
		/// <param name="gameId">The game ID number assigned to your game on the Skillz developer portal.</param>
		/// <param name="environment">Environment.Sandbox for development/testing, or Environment.Production for submitting the final app.</param>
		public static void Initialize(string gameId, Environment environment)
		{
			if (Application.platform == RuntimePlatform.IPhonePlayer)
			{
				string environmentString;
				if (environment == Environment.Sandbox)
				{
					environmentString = "SkillzSandbox";
				}
				else
				{
					environmentString = "SkillzProduction";
				}
				_skillzInitForGameIdAndEnvironment(gameId, environmentString);
			}
			else
			{
				Debug.LogWarning("Trying to initialize Skillz on a platform other than iPhone");
			}
		}

		/// <summary>
		/// Starts up the Skillz UI. Should be used as soon as the player clicks your game's "Multiplayer" button.
		/// </summary>
		/// 
		/// <param name="orientation">
		/// Pass in Orientation.Landscape if your game uses landscape or Orientation.Portrait
		/// if your game uses portrait.
		/// </param>
		public static void LaunchSkillz(Orientation orientation)
		{
			if (Application.platform == RuntimePlatform.IPhonePlayer)
			{
				if (orientation == Orientation.Landscape)
				{
					_launchSkillz ("SkillzLandscape");
				}
				else
				{
					_launchSkillz ("SkillzPortrait");
				}
			}
			else
			{
				Debug.LogError("Tried to launch Skillz on a platform other than iPhone!");
			}
		}


		/// <summary>
		/// Call this method every time the player's score changes during a Skillz match.
		/// This adds important anti-cheating functionality to your game.
		/// </summary>
		/// 
		/// <param name="currentScoreForPlayer">The player's current score.</param>
		public static void UpdatePlayerScore(float currentScoreForPlayer)
		{
			_updatePlayersCurrentScore(currentScoreForPlayer);
		}

		/// <summary>
		/// Call this method to make the player forfeit the game, returning him to the Skillz portal.
		/// </summary>
		public static void AbortGame()
		{
			_notifyPlayerAbortWithCompletion();
		}


		#region Normal tournaments

		/// <summary>
		/// Call this method when a player finishes a multiplayer game. This will report the result of the game
		/// to the Skillz server, and return the player to the Skillz portal.
		/// </summary>
		/// 
		/// <param name="score">An int representing the score a player achieved in the game.</param>
		public static void FinishTournament(int score)
		{
			_displayTournamentResultsWithScore(score);
		}

		/// <summary>
		/// Call this method when a player finishes a multiplayer game. This will report the result of the game
		/// to the Skillz server, and return the player to the Skillz portal.
		/// </summary>
		/// 
		/// <param name="score">A float representing the score a player achieved in the game.</param>
		public static void FinishTournament(float score)
		{
			_displayTournamentResultsWithFloatScore(score);
		}

		#endregion //Normal tournaments


		#region Turn-based tournaments

		/// <summary>
		/// Completes a turn for a turn-based game that doesn't have scores.
		/// </summary>
		/// 
		/// <param name="gameData">
		/// A Base64-encoded String object containing serialized data which can be used
		///   to reconstruct the game state for the next turn, or to review the current game state. This exact string
		///   will be part of the game data passed into the <code>OnTurnBasedTournamentWillBegin()</code> callback in SkillzDelegateTurnBased.
		/// </param>
		/// <param name="roundOutcome">
		/// The outcome of this round. Each round is a set of two turns -- one for each player.
		/// Used in the UI to tell players which rounds they won. Particularly useful to Skillz for objective-based games (e.x. Chess).
		/// </param>
		/// <param name="matchOutcome">The outcome of the whole match. If it's something other than SkillzRoundNoOutcome, the match will end after this turn.</param>
		public static void FinishTurn(string gameData, TurnBasedRoundOutcome roundOutcome, TurnBasedMatchOutcome matchOutcome)
		{
			FinishTurn(gameData, roundOutcome, matchOutcome, null, float.NaN, float.NaN);
		}

		/// <summary>
		/// Completes a turn for a turn-based game that has scores.
		/// </summary>
		/// 
		/// <param name="gameData">
		/// A Base64-encoded String object containing serialized data which can be used
		///   to reconstruct the game state for the next turn, or to review the current game state. This exact string
		///   will be part of the game data passed into the <code>OnTurnBasedTournamentWillBegin()</code> callback in SkillzDelegateTurnBased.
		/// </param>
		/// <param name="roundOutcome">
		/// The outcome of this round. Each round is a set of two turns -- one for each player.
		/// Used in the UI to tell players which rounds they won. Particularly useful to Skillz for objective-based games (e.x. Chess).
		/// </param>
		/// <param name="matchOutcome">The outcome of the whole match. If it's something other than SkillzRoundNoOutcome, the match will end after this turn.</param>
		/// <param name="playerTurnScore">
		/// A nice string representation of the player's score for this turn.
		/// </param>
		/// <param name="playerTotalScore">The total score for the current player.</param>
		/// <param name="opponentTotalScore">The total score for the current player's opponent.</param>
		public static void FinishTurn(string gameData,
									  TurnBasedRoundOutcome roundOutcome, TurnBasedMatchOutcome matchOutcome,
									  string playerTurnScore, float playerTotalScore, float opponentTotalScore)
		{
			_completeTurnWithGameData(gameData, playerTurnScore,
									  playerTotalScore, opponentTotalScore,
									  "SkillzRound" + roundOutcome.ToString(),
									  "SkillzMatch" + matchOutcome.ToString());
		}

		/// <summary>
		/// When your user has finished reviewing the current state of his turn-based match, use this method to return to the Skillz UI.
		/// </summary>
		public static void FinishReviewingTurn()
		{
			_finishReviewingCurrentGameState();
		}

		#endregion //Turn-based tournaments

		/// <summary>
		/// You may use this method to track player actions, level types, or other information pertinent to your Skillz integration.
		/// All keys and values in the metadataJson strign argument should be strings, pass true for forMatchInProgress if this metadata relates to an in progress match. 
		/// </summary>
		public static void AddMetadataForMatchInProgress(string metadataJson, bool forMatchInProgress)
		{
			_addMetadataForMatchInProgress(metadataJson, forMatchInProgress);
		}

		/// <summary>
		/// Gets a random integer. Both players in the tournament start with the exact same seed value,
		/// guaranteeing that this method will return the same sequence of numbers for both players.
		/// </summary>
		public static int GetRandomNumber()
		{
			return _getRandomNumber();
		}

		/// <summary>
		/// Gets a random integer between the given min (inclusive) and max (exclusive).
		/// Both players start with the exact same seed value, guaranteeing that this method
		/// will return the same sequence of numbers for both players, assuming their 'min' and 'max' parameters always match up.
		/// </summary>
		/// 
		/// <param name="min">The minimum possible value, inclusive.</param>
		/// <param name="max">The maximum possible value, exclusive.</param>
		public static int GetRandomNumber(int min, int max)
		{
			return _getRandomNumberWithMinAndMax(min, max);
		}

		
		/// <summary>
		/// Prints the Skillz SDK version to the logs.
		/// </summary>
		public static void PrintSDKVersionInfo()
		{
			_showSDKVersionInfo();
		}

		public static class Random {
		
			/**
			 * Value from Skillz random (if a Skillz game), or Unity random (if not a Skillz game)
			 **/
			public static float Value() {
			
				float randomValue = 0;
				if(IsTournamentInProgress) {
					randomValue = _getRandomFloat();
				} else {
					randomValue = UnityEngine.Random.value;
				}
			
				return randomValue;
			}
		
			/**
			 * Find a point inside the unit sphere using Value()
			 **/
			public static Vector3 InsideUnitSphere() {
				float r = Value();
				float phi = Value() * Mathf.PI;
				float theta = Value() * Mathf.PI * 2;
			
				float x = r * Mathf.Cos(theta) * Mathf.Sin(phi);
				float y = r * Mathf.Sin(theta) * Mathf.Sin(phi);
				float z = r * Mathf.Cos(phi);
			
				return new Vector3(x, y ,z);
			}
		
			/**
			 * Find a point inside the unit circle using Value()
			 **/
			public static Vector2 InsideUnitCircle() {
				float radius = 1.0f;
				float rand = Value() * 2 * Mathf.PI;
				Vector2 val = new Vector2();
			
				val.x = radius * Mathf.Cos(rand);
				val.y = radius * Mathf.Sin(rand);
			
				return val;
			}
		
			/**
			 * Hybrid rejection / trig method to generate points on a sphere using Value()
			 **/
			public static Vector3 OnUnitSphere() {
				Vector3 val = new Vector3();
				float s;
			
				do {
					val.x = 2 * (float) Value() - 1;
					val.y = 2 * (float) Value() - 1;
					s = Mathf.Pow(val.x, 2) + Mathf.Pow(val.y, 2);
				} while (s > 1);
			
				float r = 2 * Mathf.Sqrt(1 - s);
			
				val.x *= r;
				val.y *= r;
				val.z = 2 * s - 1;
			
				return val;
			}
		
			/**
			 * Quaternion random using Value()
			 **/
			public static Quaternion RotationUniform() {
				float u1 = Value();
				float u2 = Value();
				float u3 = Value();
			
				float u1sqrt = Mathf.Sqrt(u1);
				float u1m1sqrt = Mathf.Sqrt(1 - u1);
				float x = u1m1sqrt * Mathf.Sin(2 * Mathf.PI * u2);
				float y = u1m1sqrt * Mathf.Cos(2 * Mathf.PI * u2);
				float z = u1sqrt * Mathf.Sin(2 * Mathf.PI * u3);
				float w = u1sqrt * Mathf.Cos(2 * Mathf.PI * u3);
			
				return new Quaternion(x, y, z, w);
			}
		
			/**
			 * Quaternion random using Value()
			 **/
			public static Quaternion Rotation() {
				return RotationUniform();
			}

			/**
			 * Ranged random float using Value()
			 **/
			public static float Range(float min, float max) {
				float rand = Value();
				return min + (rand * (max-min));
			}

			/**
			 * Ranged random int using Value()
			 **/
			public static int Range(int min, int max) {
				float rand = Value();
				return min + (int) (rand * (max-min));
			}
		}
		/// <summary>
		/// Call this method to allow your game to launch into Skillz from sources external to your application (e.g. from Skillz-run advertisements).
		/// By default, this functionality is disabled (ie, set to false).
		/// 
		/// For example:
		///
		///		- If the user is mid-gameplay, do not call or set to false. This ensures that gameplay is not interrupted.
		///		- If the user is at a splash screen or in an options menu, return true. This is a safe place to launch into Skillz from.
		///
		/// When setting true, be sure that any relevant state is cleaned up in the skillzWillLaunch delegate method.
		/// </summary>
		/// <param name="allowLaunch">signifies that your application is in a state where it is safe for Skillz to launch</param>
		public static void SetShouldSkillzLaunchFromURL(bool allowLaunch)
		{
			_setShouldSkillzLaunchFromURL(allowLaunch);
		}

		private static Dictionary<string, object> DeserializeJSONToDictionary(string jsonString)
		{
			return SkillzSDK.MiniJSON.Json.Deserialize(jsonString) as Dictionary<string,object>;
		}
	}
}
