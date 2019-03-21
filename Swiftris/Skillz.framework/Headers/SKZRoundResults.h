//
//  SKZRoundResults.h
//  SkillzSDK-iOS
//
//  Created by teejay on 5/29/14.
//  Copyright (c) 2014 Skillz. All rights reserved.
//

#import "Skillz+TurnBased.h"

/**
 *  This class will manage information pertaining to a given round. (one turn from each player)
 *  This values that are used here are also displayed in the Skillz Post/Pre-turn screen if your tournament
 *  is a score per round type of game. (eg: A Pinball game with each player playing one ball per turn for X rounds.)
 */
@interface SKZRoundResults : NSObject

/**
 *  The round that this information corresponds to. For the first round: roundNumber equals "1"
 */
@property (nonatomic) NSInteger roundNumber;

/**
 *  The score you've submitted for this round for the current player, will be displayed within the Skillz UI.
 */
@property (nonatomic, strong, nonnull) NSString *playerScore;

/**
 *  The score you've submitted for this round for the current player's opponent, will be displayed within the Skillz UI.
 */
@property (nonatomic, strong, nullable) NSString *opponentScore;

/**
 *  This is the outcome you've specified for this round, this will be used to display results within the Skillz UI.
 */
@property (nonatomic) SKZTurnBasedRoundOutcome roundOutcome;

@end
