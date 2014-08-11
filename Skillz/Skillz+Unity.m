//
//  Skillz+Unity.m
//  SkillzSDK-iOS
//
//  Created by dzog on 12/2/13.
//  Copyright (c) 2013 Skillz. All rights reserved.
//

#import <SkillzSDK-iOS/Skillz.h>

@interface Skillz (Unity)
@end

@implementation Skillz (Unity)

//################################################################################
//#####
//##### C-style wrapper methods that will be accessed
//##### by the Unity C# wrapper for the iOS Skillz SDK
//#####
//################################################################################

NSString *unitySkillzDelegateName = @"SkillzDelegate";

// C-style wrapper for getRandomNumber so that it can be accessed by Unity in C#
extern int _getRandomNumber() {
    return (int)[Skillz getRandomNumber];
}

// C-style wrapper for getRandNumberWithMin:andMax so that it can be accessed by Unity in C#
extern int _getRandomNumberWithMinAndMax(int min, int max) {
    return (int)[Skillz getRandomNumberWithMin:min andMax:max];
}

// C-style wrapper for skillzInitForGameId:AndEnvironment: so that it can be accessed by Unity in C#
extern void _skillzInitForGameIdAndEnvironment(const char* gameId, const char* environment) {
    NSString * gameIdString = [[NSString alloc] initWithUTF8String:gameId];
    NSString * environmentString = [[NSString alloc] initWithUTF8String:environment];
    
    // Initialize the game in either sandbox or production based on the input
    if ([environmentString isEqualToString:@"SkillzSandbox"]) {
        [[Skillz skillzInstance] performSelector:@selector(setDev) withObject:nil];
        [[Skillz skillzInstance] skillzInitForGameId:gameIdString environment:SkillzSandbox];
    } else if ([environmentString isEqualToString:@"SkillzProduction"]) {
        [[Skillz skillzInstance] skillzInitForGameId:gameIdString environment:SkillzProduction];
    } else {
        // If the input environment is not SkillzSandbox or SkillzProduction, throw an error
        NSString *exceptionReason = [@"Invalid value for environment: " stringByAppendingString:environmentString];
        NSException* badEnvironmentException = [NSException exceptionWithName:@"InvalidArgumentException"
                                                                       reason:exceptionReason
                                                                     userInfo:nil];
        [badEnvironmentException raise];
    }
}

// C-style wrapper for checking whether a tournament is in progress so that it can be accessed by Unity in C#
extern int _tournamentIsInProgress() {
    if ([[Skillz skillzInstance] tournamentIsInProgress]) {
        return 1;
    } else {
        return 0;
    }
}

// C-style wrapper for SDKShortVersion so that it can be accessed by Unity in C#
extern const char* _SDKShortVersion() {
    return [[Skillz SDKShortVersion] UTF8String];
}

// C-style wrapper for showSDKVersionInfo so that it can be accessed by Unity in C#
extern void _showSDKVersionInfo() {
    [Skillz showSDKVersionInfo];
}

// C-style wrapper for launchSkillz so that it can be accessed by Unity in C#
extern void _launchSkillz(const char* orientation) {
    // Parse the input string in to a SkillzOrientation
    NSString *orientationString = [[NSString alloc] initWithUTF8String:orientation];
    SkillzOrientation skillzOrientation;
    if ([orientationString isEqualToString:@"SkillzLandscape"]) {
        skillzOrientation = SkillzLandscape;
    } else if ([orientationString isEqualToString:@"SkillzPortrait"]) {
        skillzOrientation = SkillzPortrait;
    } else {
        // If the orientation is not SkillzLandscape or SkillzPortrait, throw an error
        NSString *exceptionReason = [@"Invalid value for orientation: " stringByAppendingString:orientationString];
        NSException* badEnvironmentException = [NSException exceptionWithName:@"InvalidArgumentException"
                                                                       reason:exceptionReason
                                                                     userInfo:nil];
        [badEnvironmentException raise];
    }
    
    [[Skillz skillzInstance] launchSkillzForOrientation:skillzOrientation
                                     launchHasCompleted:^{
                                         // Send message to Unity object to call C# method
                                         // SkillzDelegate.skillzLaunchHasCompleted, implemented by publisher
                                         [Skillz sendMessageToUnityObject:unitySkillzDelegateName callingMethod:@"skillzLaunchHasCompleted" withParamMessage:@""];
                                     }
                                    tournamentWillBegin:^(NSDictionary *gameRules) {
                                        NSString *gameRulesString = [gameRules description];
                                        // Send message to Unity object to call C# method
                                        // SkillzDelegate.skillzTournamentWillBegin, implemented by publisher
                                        [Skillz sendMessageToUnityObject:unitySkillzDelegateName callingMethod:@"skillzTournamentWillBegin" withParamMessage:gameRulesString];
                                    }
                                         skillzWillExit:^{
                                             // Send message to Unity object to call C# method
                                             // SkillzDelegate.skillzWillExit, implemented by publisher
                                             [Skillz sendMessageToUnityObject:unitySkillzDelegateName callingMethod:@"skillzWillExit" withParamMessage:@""];
                                         }];
    
}

// C-style wrapper for displayTournamentResultsWithScore so that it can be accessed by Unity in C#
extern void _displayTournamentResultsWithScore(int score) {
    [[Skillz skillzInstance] displayTournamentResultsWithScore: @(score)
                                                andScoreExtras:nil
                                                withCompletion:^{
                                                    // Send message to Unity object to call C# method
                                                    // SkillzDelegate.skillzWithTournamentCompletion, implemented by publisher
                                                    [Skillz sendMessageToUnityObject:unitySkillzDelegateName
                                                                       callingMethod:@"skillzWithTournamentCompletion"
                                                                    withParamMessage:@""];
                                                }];
}

// C-style wrapper for displayTournamentResultsWithFloatScore so that it can be accessed by Unity in C#
extern void _displayTournamentResultsWithFloatScore(float score) {
    [[Skillz skillzInstance] displayTournamentResultsWithScore: @(score)
                                                andScoreExtras:nil
                                                withCompletion:^{
                                                    // Send message to Unity object to call C# method
                                                    // SkillzDelegate.skillzWithTournamentCompletion, implemented by publisher
                                                    [Skillz sendMessageToUnityObject:unitySkillzDelegateName
                                                                       callingMethod:@"skillzWithTournamentCompletion"
                                                                    withParamMessage:@""];
                                                }];
}

// C-style wrapper for notifyPlayerAbortWithCompletion so that it can be accessed by Unity in C#
extern void _notifyPlayerAbortWithCompletion() {
    [[Skillz skillzInstance] notifyPlayerAbortWithCompletion:^() {
        // Send message to Unity object to call C# method
        // SkillzDelegate.skillzWithPlayerAbort, implemented by publisher
        [Skillz sendMessageToUnityObject:unitySkillzDelegateName
                           callingMethod:@"skillzWithPlayerAbort"
                        withParamMessage:@""];
    }];
}

void UnitySendMessage(const char* obj, const char* method, const char* msg);
    
+ (void) sendMessageToUnityObject: (NSString *) objName
                callingMethod: (NSString *) methodName
             withParamMessage: (NSString *) msg {
    UnitySendMessage([objName cStringUsingEncoding:NSUTF8StringEncoding],
                     [methodName cStringUsingEncoding:NSUTF8StringEncoding],
                     [msg cStringUsingEncoding:NSUTF8StringEncoding]);
}

//################################################################################
//#####
//##### End of C wrappers
//#####
//################################################################################

@end
