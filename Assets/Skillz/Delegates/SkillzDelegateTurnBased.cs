using System.Collections.Generic;


namespace SkillzSDK
{
	/// <summary>
	/// The base class for a script that responds to messages from the Skillz SDK about events related to turn-based tournaments.
	/// All of the optional methods will by default not do anything when called.
	/// These callbacks can be used to control game flow or aggresively clean up resources you no longer need.
	///
	/// IMPORTANT: In every scene that interacts with the Skillz SDK, a script inheriting from this class (and/or from SkillzDelegateStandard)
	///   must exist.
	/// </summary>
	public abstract class SkillzDelegateTurnBased : UnityEngine.MonoBehaviour
	{
		/// <summary>
		/// Called when a new round for the given turn-based tournament is about to begin.
		/// </summary>
		public abstract void OnTurnBasedTournamentWillBegin(TurnBasedMatch matchInfo);


		/// <summary>
		/// Called when the player is finished his turn and the Skillz UI is about to take over again.
		/// </summary>
		public virtual void OnTurnEnd() { }
		

		/// <summary>
		/// Called when the player wants to review the current state of the given turn-based match.
		/// This functionality is optional -- specified in the Developer Portal
		///  (https://www.developer.skillz.com/developer) -- but recommended.
		/// </summary>
		public virtual void OnTurnBasedReviewWillBegin(TurnBasedMatch matchInfo) { }

		/// <summary>
		/// Called when the player is finished reviewing the current state of a turn-based game,
		///  and the Skillz UI is about to take over again.
		/// </summary>
		public virtual void OnReviewEnd() { }
	}
}
