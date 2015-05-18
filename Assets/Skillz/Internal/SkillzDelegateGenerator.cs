using System;
using System.Reflection;
using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using SkillzSDK;

#if UNITY_EDITOR

namespace SkillzEditor
{
	/// <summary>
	/// Generates a SkillzDelegate object for the developer.
	/// </summary>
	public static class SkillzDelegateGenerator
	{
		[UnityEditor.MenuItem("Skillz/Generate Delegate")]
		public static void GenerateDelegate()
		{
			//Find the child classes that inherit from our delegates.
			Type baseDel = typeof(SkillzDelegateBase),
				 standardDel = typeof(SkillzDelegateStandard),
				 turnBasedDel = typeof(SkillzDelegateTurnBased);
			Type[] baseChildren = GetChildClasses(baseDel),
				   standardChildren = GetChildClasses(standardDel),
				   turnBasedChildren = GetChildClasses(turnBasedDel);

			//Make sure there is only at most 1 of each delegate type.
			if (baseChildren.Length > 1)
			{
				PrintChildClassesError(baseDel,  baseChildren);
				return;
			}
			if (standardChildren.Length > 1)
			{
				PrintChildClassesError(standardDel,  standardChildren);
				return;
			}
			if (turnBasedChildren.Length > 1)
			{
				PrintChildClassesError(turnBasedDel,  turnBasedChildren);
				return;
			}

			//Make sure the developer implemented all necessary delegates.
			bool usesThisSystem = (baseChildren.Length == 1),
				 usesStandard = (standardChildren.Length == 1),
				 usesTurnBased = (turnBasedChildren.Length == 1);
			if (!usesStandard && !usesTurnBased)
			{
				UnityEngine.Debug.LogError("The developer didn't create a child class of SkillzDelegateStandard or SkillzDelegateTurnBased!");
				return;
			}
			if (!usesThisSystem)
			{
				UnityEngine.Debug.LogError("You didn't create a child class of SkillzDelegateBase!");
				return;
			}

			//Create the object and add scripts to it.
			GameObject delegateObj = new GameObject("SkillzDelegate");
			delegateObj.AddComponent<SkillzDelegateInit>();
			SkillzMessageReceiver msg = delegateObj.AddComponent<SkillzMessageReceiver>();
			msg.DelBase = (SkillzDelegateBase)delegateObj.AddComponent(baseChildren[0]);
			if (standardChildren.Length == 1)
			{
				msg.DelStandard = (SkillzDelegateStandard)delegateObj.AddComponent(standardChildren[0]);
			}
			if (turnBasedChildren.Length == 1)
			{
				msg.DelTurnBased = (SkillzDelegateTurnBased)delegateObj.AddComponent(turnBasedChildren[0]);
			}
		}

		private static Type[] GetChildClasses(Type baseClass)
		{
			return Assembly.GetExecutingAssembly().GetTypes().Where(t => t.IsClass && t != baseClass && baseClass.IsAssignableFrom(t)).ToArray();
		}
		private static void PrintChildClassesError(Type baseClass, Type[] childClasses)
		{
			string listOfChildren = "{";
			foreach (Type t in childClasses)
			{
				listOfChildren += t.ToString() + ", ";
			}
			listOfChildren = listOfChildren.Substring(0, listOfChildren.Length - 2) + "}";

			UnityEngine.Debug.LogError("There should only be one class inheriting from '" + baseClass.ToString() +
			                           ", but there are " + childClasses.Length.ToString() + ": " + listOfChildren);
		}
	}
}

#endif