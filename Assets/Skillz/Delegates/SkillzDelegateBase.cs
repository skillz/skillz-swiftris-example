using System.Collections.Generic;


namespace SkillzSDK
{
	/// <summary>
	/// The base class for a script that responds to messages from the Skillz SDK about events relating to the Skillz UI.
	/// All of the optional methods will by default not do anything when called.
	/// These callbacks can be useful if you want to control game flow or aggresively clean up resources you no longer need.
	///
	/// IMPORTANT: In every scene that interacts with the Skillz SDK, a script inheriting from this class must exist.
	/// </summary>
	public abstract class SkillzDelegateBase : UnityEngine.MonoBehaviour
	{
		/// <summary>
		/// Called when the Skillz UI will exit back to the Unity game.
		/// The player should generally be taken back to whatever screen Skillz was launched from (presumably the main menu).
		/// </summary>
		public virtual void OnSkillzWillExit() { }
		
		/// <summary>
		/// Called when the Skillz UI is about to launch.
		/// </summary>
		public virtual void OnSkillzWillLaunch() { }
		
		/// <summary>
		/// Called when the Skillz UI has just finished successfully launching.
		/// </summary>
		public virtual void OnSkillzLaunchCompleted() { }
		
		/// <summary>
		/// Called when the player aborted a match.
		/// </summary>
		public virtual void OnTournamentAborted() { }
	}
}
