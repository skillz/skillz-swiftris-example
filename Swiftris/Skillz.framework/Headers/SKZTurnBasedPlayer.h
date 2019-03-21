//
//  SKZTurnBasedPlayer.h
//  SkillzSDK-iOS
//
//  Created by James McMahon on 6/10/15.
//  Copyright (c) 2015 Skillz. All rights reserved.
//

#import <Foundation/Foundation.h>

__attribute__ ((visibility("default")))

/**
 *  SKZTurnBasedPlayer will allow you to further customize your UI, and/or can be used to help manage the state of your game.
 */
@interface SKZTurnBasedPlayer : NSObject

/**
 * Player's unique id
 */
@property (readonly, nonnull) NSString *id;

/**
 * Player's profile picture (or avatar) URL
 */
@property (readonly, nullable) NSString *avatarURL;

/**
 *  Player's display name
 */
@property (readonly, nonnull) NSString *displayName;

/**
 *  URL for the flag for the player
 */
@property (readonly, nullable) NSString *flagURL;

/**
 * Current total score for the player
 */
@property (nullable, assign) NSString *currentTotalScore;

@end
