using UnityEngine;
using UnityEditor;
using System.IO;
using System.Diagnostics;


namespace SkillzInternal
{
	//Disable warnings about code blocks that will never be reached.
	#pragma warning disable 162, 429

	public static class SkillzPostProcessBuild
	{
		//The following public static fields can be modified for developers that want to automate their Unity builds.
		//Otherwise, some dialogs will appear at the end of every new/replace build.

		/// <summary>
		/// If this is true, then this class's static fields will be used instead of prompting the developer at build time.
		/// <summary>
		private const bool AutoBuild_On = false;

		/// <summary>
		/// Whether this is a portrait game. Only used if "AutoBuild_Use" is true.
		/// </summary>
		private const bool AutoBuild_IsPortrait = false;
		/// <summary>
		/// The full path of the "SkillzSDK-iOS.embeddedframework" folder that came with the downloaded Skillz SDK.
		/// Only used if "AutoBuild_Use" is true.
		/// </summary>
		private const string AutoBuild_SkillzPath = "/Users/myUsername/Downloads/sdk_ios_10.1.19/SkillzSDK-iOS.embeddedframework";


		/// <summary>
		/// A file with this name is used to track whether a build is appending or replacing.
		/// </summary>
		private const string checkAppendFileName = ".skillzTouch";
		

		[UnityEditor.Callbacks.PostProcessBuild(int.MinValue)]
		public static void OnPostProcessBuild(BuildTarget build, string path)
		{
			//Make sure this build is for iOS.
			//Unity 4 uses 'iPhone' for the enum value; Unity 5 changes it to 'iOS'.
			if (build.ToString() != "iPhone" && build.ToString() != "iOS")
			{
				UnityEngine.Debug.LogWarning("Skillz cannot be set up for a platform other than iOS.");
				return;
			}
			if (Application.platform != RuntimePlatform.OSXEditor)
			{
				UnityEngine.Debug.LogError("Skillz cannot be set up for XCode automatically on a platform other than OSX.");
				return;
			}
			
			//Get whether this is an append build by checking whether a custom file has already been created.
			//If it is, then nothing needs to be modified.
			string checkAppendFilePath = Path.Combine(path, checkAppendFileName);
			FileInfo checkAppend = new FileInfo(checkAppendFilePath);
			if (checkAppend.Exists)
			{
				return;
			}
			
			checkAppend.Create().Close();
			
			bool trySetUp = SetUpOrientation(path) && SetUpSDKFiles(path);
			if (!trySetUp)
			{
				//These failures aren't fatal; the developer can do them manually.
				UnityEngine.Debug.LogWarning("Skillz automated steps failed!");
			}

			//Set up XCode project settings.
			SkillzXCProjEdit editProj = SkillzXCProjEdit.LoadXCProj(path);
			if (editProj == null ||
			    !editProj.AddLinkerFlags() || !editProj.AddSkillzFiles() || !editProj.AddRunScript() ||
			    !editProj.SaveChanges())
			{
				UnityEngine.Debug.LogError("Skillz automated XCode editing failed!");
				return;
			}
		}
		
		private static bool SetUpOrientation(string path)
		{
			//Get the orientation setting to use.
			string desiredOrientation;
			if (AutoBuild_On)
			{
				desiredOrientation = (AutoBuild_IsPortrait ? "UIInterfaceOrientationMaskPortrait" : "UIInterfaceOrientationMaskLandscape");
			}
			else if (EditorUtility.DisplayDialog("Choose Orientation", "Choose which orientation your game uses for Skillz:",
												 "Portrait", "Landscape"))
			{
				desiredOrientation = "UIInterfaceOrientationMaskPortrait";
			}
			else
			{
				desiredOrientation = "UIInterfaceOrientationMaskLandscape";
			}
			
			const string codeFilePath = "Classes/UnityAppController.mm";
			string fullCodePath = Path.Combine(path, codeFilePath);
			
			//Try reading in the relevant code file.
			string codeContent = "";
			StreamReader streamIn = null;
			try
			{
				streamIn = File.OpenText(fullCodePath);
				codeContent = streamIn.ReadToEnd();
			}
			catch (System.Exception e)
			{
				PrintOrientationSetError(e);
				return false;
			}
			finally
			{
				if (streamIn != null)
				{
					streamIn.Close();
				}
			}
			
			//Find the start of the function.
			int start = codeContent.IndexOf("supportedInterfaceOrientationsForWindow");
			if (start == -1)
			{
				PrintOrientationSetError(new InvalidDataException("Couldn't find orientation function in the code file!"));
				return false;
			}
			
			//Find the start/end of the function body and replace it with the chosen orientation.
			int openBrace = codeContent.IndexOf('{', start),
			closeBrace = codeContent.IndexOf('}', openBrace);
			if (openBrace == -1 || closeBrace == -1)
			{
				PrintOrientationSetError(new InvalidDataException("Couldn't find a start/end curly brace after the function!"));
				return false;
			}
			codeContent = codeContent.Remove(openBrace + 1, closeBrace - openBrace - 1);
			codeContent = codeContent.Insert(openBrace + 1,
			                                 "\n\treturn " + desiredOrientation + ";\n");
			
			//Save the modified code back out to the original file.
			StreamWriter streamOut = null;
			try
			{
				streamOut = File.CreateText(fullCodePath);
				streamOut.Write(codeContent);
			}
			catch (System.Exception e)
			{
				PrintOrientationSetError(e);
				return false;
			}
			finally
			{
				if (streamOut != null)
				{
					streamOut.Close();
				}
			}

			return true;
		}
		private static void PrintOrientationSetError(System.Exception e)
		{
			string manualInstructions = "This step failed, so now it must now be done manually by you: open " +
										"'Classes/UnityAppController.mm' in the XCode project, find the " +
										"'supportedInterfaceOrientationsForWindow' function, and make it " +
										"return either 'UIInterfaceOrientationMaskLandscape' or " +
										"'UIInterfaceOrientationMaskPortrait'.";
			string fullError = "Error setting orientation: " + e.Message + "\n\n" + manualInstructions;
			EditorUtility.DisplayDialog("Orientation Set Failed!", fullError, "OK");
		}

		private static bool SetUpSDKFiles(string projPath)
		{
			//Ask the user for the embeddedframework path if necessary.
			bool askForPath = true;
			string sdkPath = ".dummy";
			if (AutoBuild_On)
			{
				if (new DirectoryInfo(AutoBuild_SkillzPath).Exists)
				{
					askForPath = false;
					sdkPath = AutoBuild_SkillzPath;
				}
				else
				{
					EditorUtility.DisplayDialog("Skillz auto-build failed!",
					                            "Couldn't find the directory '" + AutoBuild_SkillzPath +
					                            	"'; please locate it manually in the following dialog.",
					                            "OK");
				}
			}
			while (askForPath && Path.GetFileName(sdkPath) != "SkillzSDK-iOS.embeddedframework")
			{
				//If the user hit "cancel" on the dialog, quit out.
				if (sdkPath == "")
				{
					UnityEngine.Debug.Log("You canceled the auto-copying of the 'SkillzSDK-iOS.embeddedframework' folder. " +
										  "You must copy it yourself into '" + projPath + "' before building the XCode project.");
					return true;
				}

				sdkPath = EditorUtility.OpenFolderPanel("Select the SkillzSDK-iOS.embeddedframework folder",
				                                        "", "");
			}

			//Copy the SDK files into the XCode project.
			try
			{
				DirectoryInfo newDir = new DirectoryInfo(Path.Combine(projPath, "SkillzSDK-iOS.embeddedframework"));
				if (newDir.Exists)
				{
					newDir.Delete();
					newDir.Create();
				}
				if (!CopyFolder(new DirectoryInfo(sdkPath), newDir))
				{
					newDir.Delete();
					throw new IOException("Couldn't copy the .embeddedframework contents");
				}
			}
			catch (System.Exception e)
			{
				PrintSDKFileError(e, sdkPath, projPath);
				return false;
			}

			return true;
		}
		private static bool CopyFolder(DirectoryInfo oldPath, DirectoryInfo newPath)
		{
			if (!oldPath.Exists)
			{
				return false;
			}
			if (!newPath.Exists)
			{
				newPath.Create();
			}

			//Copy each file.
			foreach (FileInfo oldFile in oldPath.GetFiles())
			{
				oldFile.CopyTo(Path.Combine(newPath.FullName, oldFile.Name), true);
			}
			//Copy each subdirectory.
			foreach (DirectoryInfo oldSubDir in oldPath.GetDirectories())
			{
				DirectoryInfo newSubDir = newPath.CreateSubdirectory(oldSubDir.Name);
				if (!CopyFolder(oldSubDir, newSubDir))
				{
					return false;
				}
			}

			return true;
		}
		private static void PrintSDKFileError(System.Exception e, string sdkPath, string projPath)
		{
			string manualInstructions = "Failed to copy the Skillz SDK files. Please manually copy '" + sdkPath +
										"' to '" + projPath + "/'. If this error persists, please contact " +
										"integrations@skillz.com.\n\nError: " + e.Message;
			EditorUtility.DisplayDialog("Skillz SDK setup failed!", manualInstructions, "OK");
		}
	}

	//Restore the warnings that were disabled.
	#pragma warning restore 162, 429
}