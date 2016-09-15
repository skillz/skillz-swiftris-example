using UnityEngine;
using System;
using System.Collections.Generic;
using SkillzSDK.Extensions;

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
    public readonly string DisplayName;
    /// <summary>
    /// An ID unique to this user.
    /// </summary>
    public readonly uint? ID;

    /// <summary>
    /// A link to the user's avatar image.
    /// </summary>
    public readonly string AvatarURL;
    /// <summary>
    /// A link to the user's country's flag image.
    /// </summary>
    public readonly string FlagURL;

    public Player(JSONDict playerJSON)
    {
      ID = playerJSON.SafeGetUintValue("id");
      DisplayName = playerJSON.SafeGetStringValue("displayName");
      AvatarURL = playerJSON.SafeGetStringValue("avatarURL");
      FlagURL = playerJSON.SafeGetStringValue("flagURL");
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
    public readonly string Name;
    /// <summary>
    /// The description of this tournament type.
    /// </summary>
    public readonly string Description;

    /// <summary>
    /// The unique ID for this match.
    /// </summary>
    public readonly int? ID;
    /// <summary>
    /// The unique ID for the tournament template this match is based on.
    /// </summary>
    public readonly int? TemplateID;

    /// <summary>
    /// If this game supports "Automatic Difficulty" (specified in the Developer Portal --
    /// https://www.developers.skillz.com/developer), this value represents the difficulty this game
    /// should have, from 1 to 10 (inclusive).
    /// Note that this value will only exist in Production, not Sandbox.
    public readonly uint? SkillzDifficulty;

    /// <summary>
    /// The custom parameters for this tournament type.
    /// Specified by the developer on the Skillz Developer Portal.
    /// </summary>
    public readonly Dictionary<string, string> GameParams;

    /// <summary>
    /// The user playing this match.
    /// </summary>
    public readonly Player Player;

    /// <summary>
    /// Is this match being played for real cash or for Z?
    /// </summary>
    public readonly bool? IsCash;
    /// <summary>
    /// If this tournament is being played for Z,
    /// this is the amount of Z required to enter.
    /// </summary>
    public readonly int? EntryPoints;
    /// <summary>
    /// If this tournament is being played for real cash,
    /// this is the amount of cash required to enter.
    /// </summary>
    public readonly float? EntryCash;


    public Match(JSONDict jsonData)
    {
      Description = jsonData.SafeGetStringValue("matchDescription");
      EntryCash = (float)jsonData.SafeGetDoubleValue("entryCash");
      EntryPoints = jsonData.SafeGetIntValue("entryPoints");
      ID = jsonData.SafeGetIntValue("id");
      TemplateID = jsonData.SafeGetIntValue("templateId");
      Name = jsonData.SafeGetStringValue("name");
      IsCash = jsonData.SafeGetBoolValue("isCash");

      object player = jsonData.SafeGetValue("player");
      if (player != null && player.GetType() == typeof(JSONDict))
      {
        JSONDict playerData = (JSONDict)player;
        Player = new Player(playerData);
      }
      else
      {
        Player = new Player();
      }

      GameParams = new Dictionary<string, string>();
      object parameters = jsonData.SafeGetValue("gameParameters");
      if (parameters != null && parameters.GetType() == typeof(JSONDict))
      {
        foreach (KeyValuePair<string, object> kvp in (JSONDict)parameters)
        {
          if (kvp.Value == null)
          {
            continue;
          }

          string val = kvp.Value.ToString();
          if (kvp.Key == "skillz_difficulty")
          {
            SkillzDifficulty = Helpers.SafeUintParse(val);
          }
          else
          {
            GameParams.Add(kvp.Key, val);
          }
        }
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
