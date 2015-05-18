using UnityEngine;
using UnityEditor;
using UnityEditor.XCodeEditor;
using System.IO;
using System.Diagnostics;


public static class SkillzPostProcessBuild
{
	[UnityEditor.Callbacks.PostProcessBuild]
	public static void OnPostProcessBuild(BuildTarget build, string path)
	{
		if (build != BuildTarget.iPhone || Application.platform != RuntimePlatform.OSXEditor)
		{
			UnityEngine.Debug.LogWarning("Skillz cannot be set up automatically on a platform other than OSX.");
			return;
		}

		SetUpOrientation(path);

		//Set up the XCode project settings.
		//Get the various paths.
		XCProject proj = new XCProject(path);
		proj.AddOtherCFlags("-fmodules");
		proj.AddFolder(Path.Combine(Application.dataPath, "Skillz/Build/IncludeInXcode/"), null, new string[] { "^.*.meta" });
		proj.AddOtherLDFlags(new PBXList() { "-weak-lSystem", "-ObjC", "-lc++", "-lz", "-lsqlite3", "-lxml2" });
		proj.Save();
	}

	private static void SetUpOrientation(string path)
	{
		//Modify the orientation settings.
		string desiredOrientation;
		if (EditorUtility.DisplayDialog("Choose Orientation", "Choose which orientation your game uses for Skillz:",
		                                "Portrait", "Landscape"))
		{
			desiredOrientation = "UIInterfaceOrientationMaskPortrait";
		}
		else
		{
			desiredOrientation = "UIInterfaceOrientationMaskLandscape";
		}
		StreamReader streamIn = null;
		StreamWriter streamOut = null;
		try
		{
			string codePath = Path.Combine(path, "Classes/UnityAppController.mm");
			
			//Read in the code file.
			streamIn = File.OpenText(codePath);
			string codeContent = streamIn.ReadToEnd();
			streamIn.Close();
			streamIn = null;
			
			//Find the start of the function.
			int start = codeContent.IndexOf("supportedInterfaceOrientationsForWindow");
			if (start == -1)
			{
				throw new InvalidDataException("Couldn't find orientation function in the code file!");
			}
			
			//Find the start/end of the function body and replace it with the chosen orientation.
			int openBrace = codeContent.IndexOf('{', start),
			closeBrace = codeContent.IndexOf('}', openBrace);
			if (openBrace == -1 || closeBrace == -1)
			{
				throw new InvalidDataException("Couldn't find a start/end curly brace after the function!");
			}
			codeContent = codeContent.Remove(openBrace + 1, closeBrace - openBrace - 1);
			codeContent = codeContent.Insert(openBrace + 1,
			                                 "return " + desiredOrientation + ";");
			
			//Write out the code.
			streamOut = File.CreateText(codePath);
			streamOut.Write(codeContent);
			streamOut.Close();
		}
		catch (System.Exception e)
		{
			string manualInstructions = "This step failed, so now it must now be done manually by you: open " +
				"'Classes/UnityAppController.mm' in the XCode project, find the " +
					"'supportedInterfaceOrientationsForWindow' function, and make it " +
					"return either 'UIInterfaceOrientationMaskLandscape' or " +
					"'UIInterfaceOrientationMaskPortrait'.";
			string fullError = "Error setting orientation: " + e.Message + "\n\n" + manualInstructions;
			EditorUtility.DisplayDialog("Orientation Set Failed!", fullError, "OK");
			
			if (streamIn != null)
			{
				streamIn.Close();
			}
			if (streamOut != null)
			{
				streamOut.Close();
			}
		}
	}
}