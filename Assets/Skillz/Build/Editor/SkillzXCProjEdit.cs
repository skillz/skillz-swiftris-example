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


		public bool AddLinkerFlags()
		{
			//Search the file for all places that linker flags are set.
			List<int> lines = FindLines("OTHER_LDFLAGS");
			int nPlaces = 0;
			string[] linkerLines = new string[] {
				"                    \"-ObjC\",",
				"                    \"-lc++\",",
				"                    \"-lz\",",
				"                    \"-lsqlite3\",",
				"                    \"-lxml2\",",
			};
			//Start from the end so that inserting lines won't move other found lines.
			for (int i = lines.Count - 1; i >= 0; --i)
			{
				//Only insert it in places where some linker flags already exist.
				if (!LinesOfFile[lines[i]].Contains(" = \"\";"))
				{
					LinesOfFile.InsertRange(lines[i] + 3, linkerLines);
					nPlaces += 1;
				}
			}
			Assert(nPlaces > 0, "No 'OTHER_LDFLAGS' found!");

			return true;
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
				if (LinesOfFile[FindLineWith("name = ", 1, line)].Contains("\"Unity-iPhone\";"))
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
			if (!Assert(lineToUse != -1, "No build configuration list for 'Unity-iPhone' was found!"))
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
				"\t\t\tshellScript = \"if [ -e \\\"${BUILT_PRODUCTS_DIR}/${FULL_PRODUCT_NAME}/SkillzSDK.bundle/postprocess.sh\\\" ]; then\\n    /bin/sh \\\"${BUILT_PRODUCTS_DIR}/${FULL_PRODUCT_NAME}/SkillzSDK.bundle/postprocess.sh\\\"\\nfi\";",
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
		/// Adds all Skillz files to the XCode project (wrapper files and the framework itself).
		/// Assumes the "SkillzSDK-iOS.embeddedframework" folder is stored in the XCode project's root.
		/// </summary>
		public bool AddSkillzFiles()
		{
			const string wrapperFile = "Skillz+Unity.mm";
			const string embeddedFrameworkFolder = "SkillzSDK-iOS.embeddedframework";
			const string frameworkFile = "SkillzSDK-iOS.framework";
			const string bundleFile = "SkillzSDK.bundle";

			string wrapperBuildFileGuid = GenerateGUID(),
				   wrapperFileRefGuid = GenerateGUID();
			string frameworkBuildFileGuid = GenerateGUID(),
				   frameworkFileRefGuid = GenerateGUID();
			string bundleBuildFileGuid = GenerateGUID(),
				   bundleFileRefGuid = GenerateGUID();
			string embeddedFrameworkGuid = GenerateGUID();

			string wrapperFolderPath = Path.Combine(UnityEngine.Application.dataPath,
													"Skillz/Build/IncludeInXcode/");
			
			//Annoyingly, Unity changed how its auto-integration support works in Unity 5.
			bool usingUnity4 = (UnityEngine.Application.unityVersion[0] == '4');
			
			//Add the "build file" entries.
			int buildFileBlockStart = FindLineWith("PBXBuildFile");
			if (!Assert(buildFileBlockStart < LinesOfFile.Count, "Can't find 'PBXBuildFile' anywhere"))
			{
				return false;
			}
			if (usingUnity4)
			{
				AddBuildFile(buildFileBlockStart, wrapperBuildFileGuid, wrapperFileRefGuid, wrapperFile, "Sources");
			}
			AddBuildFile(buildFileBlockStart, frameworkBuildFileGuid, frameworkFileRefGuid, frameworkFile, "Frameworks");
			AddBuildFile(buildFileBlockStart, bundleBuildFileGuid, bundleFileRefGuid, bundleFile, "Resources");
			
			
			//Add the "file reference" entries.
			int fileRefBlockStart = FindLineWith("PBXFileReference");
			if (!Assert(fileRefBlockStart < LinesOfFile.Count, "Can't find 'PBXFileReference' anywhere"))
			{
				return false;
			}
			if (usingUnity4)
			{
				AddFileRef(fileRefBlockStart, wrapperFileRefGuid, wrapperFile, "sourcecode.cpp.objcpp", wrapperFile, wrapperFolderPath);
			}
			AddFileRef(fileRefBlockStart, frameworkFileRefGuid, frameworkFile, "wrapper.framework");
			AddFileRef(fileRefBlockStart, bundleFileRefGuid, bundleFile, "wrapper.plug-in");


			//Add the framework build phase entry.
			int frameworkBuildPhase = FindLineWith("isa = PBXFrameworksBuildPhase"),
				frameworkBuildPhaseFiles = FindLineWith("files = (", 1, frameworkBuildPhase);
			if (!Assert(frameworkBuildPhase < LinesOfFile.Count, "Can't find PBXFrameworksBuildPhase item anywhere") ||
				!Assert(frameworkBuildPhaseFiles < LinesOfFile.Count, "Can't find 'files' block of PBXFrameworksBuildPhase") ||
				!Assert(!LinesOfFile[frameworkBuildPhaseFiles].Contains(")"), "PBXFrameworksBuildPhase has no files"))
			{
				return false;
			}
			const int frameworkBuildPhaseFilesOffset = 1;
			LinesOfFile.Insert(frameworkBuildPhaseFiles + frameworkBuildPhaseFilesOffset,
			                   "\t\t\t\t" + frameworkBuildFileGuid + " /* " + frameworkFile + " in Frameworks */,");


			//Add the bundle's build phase entry.
			int bundleBuildPhase = FindLineWith("isa = PBXResourcesBuildPhase"),
				bundleBuildPhaseFiles = FindLineWith("files = (", 1, bundleBuildPhase);
			if (!Assert(bundleBuildPhase < LinesOfFile.Count, "Can't find PBXResourcesBuildPhase item anywhere") ||
			    !Assert(bundleBuildPhaseFiles < LinesOfFile.Count, "Can't find 'files' block of PBXResourcesBuildPhase") ||
			    !Assert(!LinesOfFile[bundleBuildPhaseFiles].Contains(")"), "PBXResourcesBuildPhase has no files"))
			{
				return false;
			}
			const int bundleBuildPhaseFilesOffset = 1;
			LinesOfFile.Insert(bundleBuildPhaseFiles + bundleBuildPhaseFilesOffset,
			                   "\t\t\t\t" + bundleBuildFileGuid + " /* " + bundleFile + " in Resources */,");

			
			//Add the "file group" entries for the project.
			int fileGroupBlockStart = FindLineWith("name = CustomTemplate;");
			if (!Assert(fileGroupBlockStart < LinesOfFile.Count, "Can't find 'name = CustomTemplate' anywhere"))
			{
				return false;
			}
			const int fileGroupOffset = -3;
			string fileGroupEntry = "";
			if (usingUnity4)
			{
				fileGroupEntry = "\t\t\t\t" + wrapperFileRefGuid +
								 " /* " + wrapperFile + " */,";
				LinesOfFile.Insert(fileGroupBlockStart + fileGroupOffset, fileGroupEntry);
			}
			fileGroupEntry = "\t\t\t\t" + embeddedFrameworkGuid + " /* " + embeddedFrameworkFolder + " */,";
			LinesOfFile.Insert(fileGroupBlockStart + fileGroupOffset, fileGroupEntry);


			//Add the "embeddedframework" group entry.
			int fileGroupElementStart = FindLineWith("isa = PBXGroup;") - 1;
			if (!Assert(fileGroupBlockStart < LinesOfFile.Count - 1, "Can't find 'isa = PBXGroup' anywhere"))
			{
				return false;
			}
			string[] embeddedFrameworkGroupEntry = new string[] {
				"\t\t" + embeddedFrameworkGuid + " /* " + embeddedFrameworkFolder + " */ = {",
				"\t\t\tisa = PBXGroup;",
				"\t\t\tchildren = (",
				"\t\t\t\t" + frameworkFileRefGuid + " /* " + frameworkFile + " */,",
				"\t\t\t\t" + bundleFileRefGuid + " /* " + bundleFile + " */,",
				"\t\t\t);",
				"\t\t\tpath = \"" + embeddedFrameworkFolder + "\";",
				"\t\t\tsourceTree = SOURCE_ROOT;",
				"\t\t};",
			};
			LinesOfFile.InsertRange(fileGroupElementStart, embeddedFrameworkGroupEntry);
			
			
			//Add the unity wrapper file to the "source build" phase for "Unity-iPhone".
			if (usingUnity4)
			{
				int sourceBuildPhaseDecl = FindLineWith("isa = PBXSourcesBuildPhase");
				if (!Assert(sourceBuildPhaseDecl < LinesOfFile.Count, "Can't find the first PBXSourcesBuildPhase"))
				{
					return false;
				}
				int sourceBuildPhaseFiles = FindLineWith("files = (", 1, sourceBuildPhaseDecl);
				if (!Assert(sourceBuildPhaseFiles < LinesOfFile.Count, "Can't find the files for the first PBXSourcesBuildPhase"))
				{
					return false;
				}
				string buildPhaseEntry = "                " + wrapperBuildFileGuid + " /* " + wrapperFile + " in Sources */,";
				const int sourceBuildPhaseOffset = 1;
				LinesOfFile.Insert(sourceBuildPhaseFiles + sourceBuildPhaseOffset, buildPhaseEntry);
			}


			//Tell the build settings to look for SkillzSDK-iOS.embeddedframework.

			string[] frameworkSearchPaths = new string[] {
				"\t\t\t\tFRAMEWORK_SEARCH_PATHS = (",
				"\t\t\t\t\t\"$(inherited)\",",
				"\t\t\t\t\t\"$(PROJECT_DIR)/" + embeddedFrameworkFolder + "\",",
				"\t\t\t\t);",
			};

			List<int> buildConfigurations = FindLines("isa = XCBuildConfiguration;");

			//The first two configurations are for the Unity game.
			if (!Assert(buildConfigurations.Count >= 2, "Less than two XCBuildConfigurations"))
			{
				return false;
			}
			for (int i = 0; i < 2; ++i)
			{
				int line = buildConfigurations[i];

				if (usingUnity4)
				{
					int lineToInsert = FindLineWith("COPY_PHASE_STRIP = ", 1, line) + 1;
					if (!Assert(lineToInsert < LinesOfFile.Count, "Couldn't find 'COPY_PHASE_STRIP' in build configuration"))
					{
						return false;
					}
					LinesOfFile.InsertRange(lineToInsert, frameworkSearchPaths);
				}
				else // Using Unity 5
				{
					int lineToInsert = FindLineWith("FRAMEWORK_SEARCH_PATHS = \"$(inherited)\";", 1, line, "\t\t\t};");
					if (!Assert(lineToInsert < LinesOfFile.Count,
					            "Couldn't find 'FRAMEWORK_SEARCH_PATHS = \"$(inherited)\";' in build configuration " + i.ToString()))
					{
						return false;
					}
					LinesOfFile.RemoveAt(lineToInsert);
					LinesOfFile.InsertRange(lineToInsert, frameworkSearchPaths);
				}
			}

			
			return true;
		}

		//Helper functions for "AddSkillzFiles":

		private void AddBuildFile(int buildFileBlockStart, string buildFileGuid, string fileRefGuid,
								  string fileName, string fileGroupName)
		{
			string entry = "\t\t" + buildFileGuid + " /* " + fileName + " in " + fileGroupName +
						   " */ = { isa = PBXBuildFile; fileRef = " + fileRefGuid + " /* " + fileName + " */; };";

			const int buildFileBlockOffset = 1;
			LinesOfFile.Insert(buildFileBlockStart + buildFileBlockOffset, entry);
		}

		private void AddFileRef(int fileRefBlockStart, string fileRefGuid, string fileName,
								string fileType, string name = null, string filePath = "")
		{
			string entry = "\t\t" + fileRefGuid + " /* " + fileName +
						   " */ = {isa = PBXFileReference; lastKnownFileType = " + fileType + "; ";
			if (name != null)
			{
				entry += "name = \"" + name + "\"; ";
			}
			entry += "path = \"" + Path.Combine(filePath, fileName) + "\"; sourceTree = \"<group>\"; };";

			const int fileRefBlockOffset = 1;
			LinesOfFile.Insert(fileRefBlockStart + fileRefBlockOffset, entry);
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
