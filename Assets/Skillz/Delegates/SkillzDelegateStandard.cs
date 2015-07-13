using System.Collections.Generic;

namespace SkillzSDK
{
	/// <summary>
	/// The base class for a script that responds to messages from the Skillz SDK about events related to normal tournaments.
	/// All of the optional methods will by default not do anything when called.
	/// These callbacks can be used to control game flow or aggresively clean up resources you no longer need.
	///
	/// IMPORTANT: In every scene that interacts with the Skillz SDK, a script inheriting from this class (and/or from SkillzDelegateTurnBased)
	///   must exist.
	/// </summary>
	public abstract class SkillzDelegateStandard : UnityEngine.MonoBehaviour
	{
		/// <summary>
		/// Called when a match is about to start from the Skillz UI. You should load the level
		///  and take any other necessary initialization actions in order to start the game.
		/// </summary>
		///
		/// <param name="matchInfo">
		/// The custom tournament parameters you set up on the developer's portal (https://developers.skillz.com/developer)
		///  for the type of tournament that is about to begin.
		/// </param>
		/// <remarks>
		/// Replaces the previous `OnTournamentWillBegin(Dictionary<string, string> tournamentRules)` method.
		/// `tournamentRules` (aka Game Parameters) are avaiable by using `matchInfo.GameParams`.
		/// </remarks>
		public abstract void OnTournamentWillBegin(Match matchInfo);


		/// <summary>
		/// Called when a match was just finished and the Skillz UI is about to take over again.
		/// </summary>
		public virtual void OnTournamentCompleted() { }
	}
}
