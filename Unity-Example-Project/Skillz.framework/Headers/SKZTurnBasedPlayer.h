//
//  SKZTurnBasedPlayer.h
//  SkillzSDK-iOS
//
//  Created by James McMahon on 6/10/15.
//  Copyright (c) 2015 Skillz. All rights reserved.
//

#import "SKZPlayer.h"

__attribute__ ((visibility("default")))

/**
 *  SKZTurnBasedPlayer will allow you to further customize your UI, and/or can be used to help manage the state of your game.
 */
@interface SKZTurnBasedPlayer : SKZPlayer

/**
 * Current total score for the player
 */
@property NSString *currentTotalScore;

@end
