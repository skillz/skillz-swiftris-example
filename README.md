# Unity Example Project

This repo contains a basic Unity project with a sample Skillz integration. This particular project is in landscape mode, [a portrait version is also available](https://github.com/skillz/example-ios-unity-portrait).

## Details
There are two scenes in this project, each with their own script.

#### testInitLevel
This scene controls Skillz initialization and has a button to launch the Skillz interface.

#### testLevel
This scene is fired when the user clicks play in the Skillz interface. It has a few buttons for the reporting mock scores to Skillz. Usually these scores would be generated through normal gameplay, but this example sets these scores with fake values.

## Build
Building this project uses the standard Unity build process.

**File -> Build Settings -> Set Platform to iOS (if needed) -> Build**
This will create the XCode project for iOS.

Once the Xcode project is built you will need to follow the [XCode integration steps detailed on the Developer Portal](https://skillz.com/developer/docs/install_framework_ios_unity) to include the needed Skillz files and configure the XCode project for Skillz. It is recommended that after the initial build that additional builds use the append mode to avoid resetting the Skillz integration in the XCode project.

## Run
Running the project through XCode as you would any other project. Please note, you will need to run this projects directly on a device, as the simulator will produce unexpected results.
