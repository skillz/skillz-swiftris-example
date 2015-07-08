using UnityEngine;
using System;
using System.Collections.Generic;


using JSONDict = System.Collections.Generic.Dictionary<string, object>;

namespace SkillzSDK
{
	/// <summary>
	/// A Skillz user.
	/// </summary>
	public struct Player
	{
		/// <summary>
		/// The user's display name.
		/// </summary>
		public string DisplayName;
		/// <summary>
		/// An ID unique to this user.
		/// </summary>
		public uint ID;

		/// <summary>
		/// A link to the user's avatar image.
		/// </summary>
		public string AvatarURL;
		/// <summary>
		/// A link to the user's country's flag image.
		/// </summary>
		public string FlagURL;


		public Player(string displayName, uint id, string avatarURl, string flagURL)
		{
			DisplayName = displayName;
			ID = id;
			AvatarURL = avatarURl;
			FlagURL = flagURL;
		}
		public Player(ref string tryKey, JSONDict playerJSON)
		{
			tryKey = "id";
			ID = uint.Parse(playerJSON[tryKey].ToString());

			tryKey = "displayName";
			if (playerJSON.ContainsKey(tryKey))
			{
				DisplayName = playerJSON[tryKey].ToString();
			}
			else
			{
				DisplayName = null;
			}

			tryKey = "avatarURL";
			if (playerJSON.ContainsKey(tryKey))
			{
				AvatarURL = playerJSON[tryKey].ToString();
			}
			else
			{
				AvatarURL = null;
			}

			tryKey = "flagURL";
			if (playerJSON.ContainsKey(tryKey))
			{
				FlagURL = playerJSON[tryKey].ToString();
			}
			else
			{
				FlagURL = null;
			}
		}

		public override string ToString()
		{
			return "Player: " +
				" ID: [" + ID + "]" +
				" DisplayName: [" + DisplayName + "]" +
				" AvatarURL: [" + AvatarURL + "]" +
				" FlagURL: [" + FlagURL + "]";
		}
	}


	/// <summary>
	/// A Skillz match.
	/// </summary>
	public class Match
	{
		/// <summary>
		/// The name of this tournament type.
		/// </summary>
		public string Name;
		/// <summmary>
		/// The description of this tournament type.
		/// </summary>
		public string Description;

		/// <summary>
		/// The unique ID for this match.
		/// </summary>
		public int ID;
		/// <summary>
		/// The unique ID for the tournament template this match is based on.
		/// </summary>
		public int TemplateID;
		
		/// <summary>
		/// If this game supports "Automatic Difficulty" (specified in the Developer Portal --
		/// https://www.developers.skillz.com/developer), this value represents the difficulty this game
		/// should have, from 1 to 10 (inclusive).
		/// Note that this value will only exist in Production, not Sandbox.
		public uint? SkillzDifficulty;

		/// <summary>
		/// The custom parameters for this tournament type.
		/// Specified by the developer on the Skillz Developer Portal.
		/// </summary>
		public Dictionary<string, string> GameParams;

		/// <summary>
		/// The user playing this match.
		/// </summary>
		public Player Player;

		/// <summary>
		/// Is this match being played for real cash or for Z?
		/// </summary>
		public bool IsCash;
		/// <summary>
		/// If this tournament is being played for Z,
		/// this is the amount of Z required to enter.
		/// </summary>
		public int EntryPoints;
		/// <summary>
		/// If this tournament is being played for real cash,
		/// this is the amount of cash required to enter.
		/// </summary>
		public float EntryCash;


		public Match(JSONDict jsonData)
		{
			Name = "";
			Description = "";
			ID = -1;
			TemplateID = -1;
			GameParams = new Dictionary<string, string>();
			Player = new Player("", 0, "", "");
			IsCash = false;
			EntryPoints = -1;
			EntryCash = -1.0f;
			SkillzDifficulty = null;

			//Keep track of what key we're currently trying in case an exception is thrown.
			string tryKey = "[unknown]";
			try
			{
				tryKey = "matchDescription";
				if (jsonData.ContainsKey(tryKey))
				{
					Description = jsonData[tryKey].ToString();
				}

				tryKey = "entryCash";
				EntryCash = float.Parse(jsonData[tryKey].ToString());

				tryKey = "entryPoints";
				EntryPoints = int.Parse(jsonData[tryKey].ToString());

				tryKey = "id";
				ID = int.Parse(jsonData[tryKey].ToString());

				tryKey = "templateId";
				TemplateID = int.Parse(jsonData[tryKey].ToString());

				tryKey = "name";
				if (jsonData.ContainsKey(tryKey))
				{
					Name = jsonData[tryKey].ToString();
				}

				tryKey = "isCash";
				IsCash = !(jsonData[tryKey].ToString() == "0");


				tryKey = "player";
				JSONDict playerData = (JSONDict)jsonData[tryKey];
				Player = new Player(ref tryKey, playerData);

				tryKey = "gameParameters";
				foreach (KeyValuePair<string, object> kvp in (JSONDict)jsonData[tryKey])
				{
					if (kvp.Key == "skillz_difficulty")
					{
						SkillzDifficulty = uint.Parse(kvp.Value.ToString());
					}
					else
					{
						GameParams.Add(kvp.Key, kvp.Value.ToString());
					}
				}
			}
			catch (Exception e)
			{
				Debug.LogError("Error parsing Skillz data into Match at key '" + tryKey + "'!\n" +
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

			return "Match: " +
				" ID: [" + ID + "]" +
				" Name: [" + Name + "]" +
				" Description: [" + Description + "]" +
				" TemplateID: [" + TemplateID + "]" +
				" SkillzDifficulty: [" + SkillzDifficulty + "]" +
				" IsCash: [" + IsCash + "]" +
				" EntryPoints: [" + EntryPoints + "]" +
				" EntryCash: [" + EntryCash + "]" +
				" GameParams: [" + paramStr + "]" +
				" Player: [" + Player + "]";
		}
	}
}
