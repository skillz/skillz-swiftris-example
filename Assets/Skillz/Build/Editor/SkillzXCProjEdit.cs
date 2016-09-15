using System;
using System.Collections.Generic;
using System.IO;
using UnityEditor;


namespace SkillzInternal
{
	/// <summary>
	/// Edits parts of an XCode .pbxproj file for Skillz integration.
	/// All methods return whether they were successful.
	/// </summary>
	public class SkillzXCProjEdit
	{
		/// <summary>
		/// Tries to create an instance for modifying the project at the given build path.
		/// Returns "null" if something went wrong.
		/// </summary>
		public static SkillzXCProjEdit LoadXCProj(string projPath)
		{
			TextReader reader = null;
			try
			{
				const string projFilePath = "Unity-iPhone.xcodeproj/project.pbxproj";

				SkillzXCProjEdit edit = new SkillzXCProjEdit();
				edit.ProjectPath = projPath;
				edit.FilePath = Path.Combine(projPath, projFilePath);

				reader = new StreamReader(edit.FilePath);

				string line = reader.ReadLine();
				while (line != null)
				{
					edit.LinesOfFile.Add(line);
					line = reader.ReadLine();
				}

				return edit;
			}
			catch (Exception e)
			{
				AlertError("Error opening pbxproj file at '" + projPath + "' for Skillz: " + e.GetType().ToString() + ": " + e.Message);
				return null;
			}
			finally
			{
				if (reader != null)
				{
					reader.Close();
				}
			}
		}

		private static string GenerateGUID()
		{
			return Guid.NewGuid().ToString("N").Substring(8).ToUpper();
		}

		private static void AlertError(string message)
		{
			EditorUtility.DisplayDialog("Skillz Error", message, "OK");
		}

		/// <summary>
		/// If the given test value is false, prints a comprehensive error message.
		/// Returns the test value.
		/// </summary>
		private static bool Assert(bool test, string error)
		{
			if (!test)
			{
				AlertError("Error: " + error + ".\n\nTry building again. If this problem persists, please email us at integrations@skillz.com");
			}

			return test;
		}


		/// <summary>
		/// The path to the project root.
		/// </summary>
		public string ProjectPath = "";
		/// <summary>
		/// The full path to the XCode project's .pbxproj file.
		/// </summary>
		public string FilePath = "";

		/// <summary>
		/// Each line of the XCode project's .pbxproj file, in order.
		/// </summary>
		public List<string> LinesOfFile = new List<string>();


		/// <summary>
		/// Saves the changes out to the project file.
		/// Returns whether the file was successfully modified.
		/// </summary>
		public bool SaveChanges()
		{
			TextWriter writer = null;
			try
			{
				writer = new StreamWriter(FilePath, false);
				foreach (string line in LinesOfFile)
				{
					writer.WriteLine(line);
				}

				return true;
			}
			catch (Exception e)
			{
				AlertError("Couldn't save out .pbxproj file for project. " + e.GetType().ToString() + ": " + e.Message);
				return false;
			}
			finally
			{
				if (writer != null)
				{
					writer.Close();
				}
			}
		}


		public bool AddRunScript()
		{
			string guid = GenerateGUID();

			//Search the file for the list of build phases, and insert the run phase.
			string searchFor = "buildConfigurationList = ";
			List<int> lines = FindLines(searchFor);
			int lineToUse = -1;
			foreach (int line in lines)
			{
				//Find the run configuration for the game project, not the tests project.
				if (LinesOfFile[FindLineWith("name = ", 1, line)].Contains("Unity-iPhone"))
				{
					//Find the end of the build phase list so that we can insert our run script there.
					int counter = FindLineWith("buildPhases = (", 1, line);
					if (!Assert(counter < LinesOfFile.Count, "Couldn't find build phase list for a PBXNativeTarget"))
					{
						return false;
					}
					counter = FindLineWith(");", 1, counter);
					if (!Assert(counter < LinesOfFile.Count, "Couldn't find end of build phase list for PBXNativeTarget"))
					{
						return false;
					}

					lineToUse = counter;
					break;
				}
			}
			if (!Assert(lineToUse != -1, "No build configuration list for 'Unity-iPhone' was found! Please manually add postproccess.sh." + 
			            " For more information see: https://cdn.skillz.com/doc/developer/integrating_ios/install_framework/"))
			{
				return false;
			}
			LinesOfFile.Insert(lineToUse, "\t\t\t\t" + guid + " /* ShellScript */,");


			//Now add the actual run script definition.

			string[] newLines = {
				"\t\t" + guid + " /* ShellScript */ = {",
				"\t\t\tisa = PBXShellScriptBuildPhase;",
				"\t\t\tbuildActionMask = 2147483647;",
				"\t\t\tfiles = ();",
				"\t\t\tinputPaths = ();",
				"\t\t\toutputPaths = ();",
				"\t\t\trunOnlyForDeploymentProcessing = 0;",
				"\t\t\tshellPath = /bin/sh;",
				"\t\t\tshellScript = \"if [ -e \\\"${BUILT_PRODUCTS_DIR}/${FRAMEWORKS_FOLDER_PATH}/Skillz.framework/postprocess.sh\\\" ]; then\\n    /bin/sh \\\"${BUILT_PRODUCTS_DIR}/${FRAMEWORKS_FOLDER_PATH}/Skillz.framework/postprocess.sh\\\"\\nfi\";",
				"\t\t};"
			};

			//Find the list of build phase definitions and insert our build phase into it.
			int firstBuildPhaseElement = FindLineWith("isa = PBXShellScriptBuildPhase");
			if (!Assert(firstBuildPhaseElement != LinesOfFile.Count,
			            "Couldn't find first element of PBXShellScriptBuildPhase") ||
			    !Assert(LinesOfFile[firstBuildPhaseElement - 1].Contains(" = {"),
			        "Couldn't find start of PBXShellScriptBuildPhase list"))
			{
				return false;
			}
			const int buildPhaseElementOffset = -1;
			LinesOfFile.InsertRange(firstBuildPhaseElement + buildPhaseElementOffset, newLines);

			return true;
		}


		/// <summary>
		/// Finds all lines that have the given text in them.
		/// </summary>
		private List<int> FindLines(string text)
		{
			List<int> vals = new List<int>();
			for (int i = 0; i < LinesOfFile.Count; ++i)
				if (LinesOfFile[i].Contains(text))
					vals.Add(i);
			return vals;
		}

		/// <summary>
		/// Finds the first line that has the given text in it.
		/// Returns LinesOfFile.Count if the given text isn't found.
		/// </summary>
		/// Optionally takes in which line to find, instead of the first one.
		/// Also optionally takes in which line to start the search at.
		/// <param name="whichLine">Which line with the given text to return (first, second, third, etc.).</param>
		/// <param name="firstLineToSearch">The starting line of the search.</param>
		/// <param name="stopSearchingAt">
		/// If not 'null', a line containing this string will cause the search to exit without finding anything.
		/// </param>
		private int FindLineWith(string text, int whichLine = 1, int firstLineToSearch = 0, string stopSearchingAt = null)
		{
			int nMatches = 0;
			int i = firstLineToSearch;

			while (i < LinesOfFile.Count)
			{
				if (LinesOfFile[i].Contains(text))
				{
					nMatches += 1;
					if (nMatches == whichLine)
						break;
				}
				else if (stopSearchingAt != null && LinesOfFile[i].Contains(stopSearchingAt))
				{
					i = LinesOfFile.Count;
					break;
				}
				i += 1;
			}

			return i;
		}
	}
}
