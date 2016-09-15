using System;
using UnityEngine;
using SkillzSDK;

namespace SkillzSDK
{
	/// <summary>
	/// Handles initialization of the SkillzDelegate GameObject (created by the menu script "Skillz>GenerateDelegate").
	/// </summary>
	public class SkillzDelegateInit : MonoBehaviour
	{
		private static bool initializedYet = false;


		public int GameID = 0;
		public SkillzSDK.Environment SkillzEnvironment = SkillzSDK.Environment.Sandbox;


		/// <summary>
		/// Once the scene starts up, this script initializes Skillz
		/// and makes the owner GameObject persistent through scene changes.
		/// </summary>
		void Awake()
		{
			//If Skillz has already been initialized, then an instance of this delegate object already exists.
			if (initializedYet)
			{
				Destroy(gameObject);
				return;
			}

			initializedYet = true;
			DontDestroyOnLoad(gameObject);
			Api.Initialize(GameID.ToString(), SkillzEnvironment);

			//Destroy this script now that initialization is finished.
			Destroy(this);
		}
	}
}