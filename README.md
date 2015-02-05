# Unity Example Project

This repo contains an example of a very simple Unity "game" with Skillz integration. The purpose of this example is to show the bare minimum of code needed to use Skillz correctly. This project supports being built into either landscape mode or portrait mode without requiring much work to switch between the two.

## Details
The game starts at a "main menu" screen and moves to a "gameplay" screen when a tournament starts (though the "gameplay" is really just a couple of buttons allowing the player to choose what his score will be).

There are two different "main menu" scenes; they are identical with the exception of a single field in the "SKZInitTestApp" component of the "Main Camera" object that determines the orientation:
* "testInitLevelLandscape" -- the start screen for the landscape version of the game
* "testInitLevelPortrait" -- the start screen for the portrait version of the game

There are three different "gameplay" scenes. Each one is loaded in a particular part of the SkillzDelegate component's behavior; they are identical with the exception of a single field in the "SKZTestApp" component of the "Main Camera" object that determines the type of gameplay:
* "testLevelNormal" -- the gameplay for a normal Skillz Head-to-Head tournament.
* "testLevelTurnBased" -- the gameplay for a turn-based Skillz tournament.
* "testLevelReviewTurnBased" -- a scene for turn-based tournaments where the player watches his opponent's last turn. Not really gameplay per se, but a way to view an opponent's gameplay.

## Build
Building this project uses the standard Unity build process. Make sure to choose either "testInitLevelLandscape" or "testInitLevelPortrait" as the starting scene, and uncheck the other one to remove it from the build.

**File -> Build Settings -> Set Platform to iOS (if needed) -> Build**
This will create the XCode project for iOS.

Once the Xcode project is built you will need to follow the [XCode integration steps detailed on the Developer Portal](https://skillz.com/developer/docs/install_framework_ios_unity) to include the needed Skillz files and configure the XCode project for Skillz. It is recommended that after the initial build that additional builds use the append mode to avoid resetting the Skillz integration in the XCode project.

## Run
Running the project through XCode as you would any other project. **Please note**: you should run this project directly on a physical device, as XCode's simulator will produce unexpected results with the SDK.
