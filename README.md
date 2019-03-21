# Swiftris Example Skillz + Swift Project

This repository contains an example iOS game written in Swift that integrates the Skillz SDK. It is intended to supplement the [documentation](https://cdn.skillz.com/doc/developer/ios_native/integrate_skillz_sdk/install_skillz_for_swift/) that walks users through the process of integrating an iOS game.

Credit for the original Swiftris belongs to Bloc. ([Original Swiftris Repository](https://github.com/Bloc/swiftris))

Credit for the Swift 3 upgrade to Swiftris belongs to ccezar ([Swiftris Swift 3.0 Commit](https://github.com/ccezar/swiftris/tree/e8c2c4cd01105cb820c04688da2e17f90537674b
))

Skillz integration of the project done by Rich Hung

Use governed by the MIT License.


## Build Environment

This project integrated the Skillz SDK version 21.0.25. Check the [Downloads](https://developers.skillz.com/downloads) page for the latest version.

The project was built on Xcode 10.1. Also, make sure you have the appropriate developer certificate and provisioning profiles so that it can be run on an iOS device. The provisioning profiles need to set the bundle name to `br.com.swiftris`.

If you are experiencing trouble, please email integrations@skillz.com with a detailed description of the issue you are encountering.

## Commit Details

The commits in this repository have been organized to make the SDK integration easier to follow.

### Commit 01: Swiftris

The [first commit](https://github.com/skillz/example-ios-unity/commit/a919e70470efadfdbc830ed866dc5d5969c84a61) contains Swiftris without Skillz integration. This can be used to compare to the next commits.

### Commit 02: Install the Skillz SDK

The [second commit](https://github.com/skillz/example-ios-unity/commit/c849e3eaf6167179a8abbec8980779c4a318455d) simply installs the Skillz SDK that was downloaded from the [Skillz Developer Portal](https://developers.skillz.com/downloads). 
This is done by: 
* Dragging the `Skillz.framework` file into your XCode project 
* Moving the `SkillzBridgingHeader.h` file into your project
* Adding the `Skillz.framework` to both your `Linked Libraries` and `Embedded Binaries` build phases. 
* Setting your deployment target to 9.0 
* Adding the Skillz run script 

(NOTE: Need to fix the commits to reflect the last 3 bullets. Embedded Binaries, deployment target, and run script were missed on the first pass and do not appear until the final commit)

Refer to the [full instructions](https://cdn.skillz.com/doc/developer/ios_native/integrate_skillz_sdk/install_skillz_for_swift/) for more information.

At this point, the game is not integrated with the SDK and still compiles and runs normally.

### Commit 03: Getting In and Out of the Skillz SDK

Finally, the Skillz SDK is [integrated](https://github.com/skillz/example-ios-unity/commit/ee1943a212cdadf3368ced1e1f4131815b10b6ae)

* Implementing the `SkillzDelegate` protocol in the `AppDelegate` class, so that the game is notified when a match will begin or end.
* Modifying `Main.storyboard` and adding `MainMenuViewController.swift` to give the game a main menu screen
* Reporting the user's final score when the match ends.
* Making sure that gameplay relevant randomness (which blocks spawn) is tied to Skillz's random function
