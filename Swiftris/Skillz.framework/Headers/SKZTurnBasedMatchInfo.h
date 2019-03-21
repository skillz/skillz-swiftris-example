//
//  SKZTurnInformation.h
//  SkillzSDK-iOS
//
//  Created by teejay on 5/8/14.
//  Copyright (c) 2014 Skillz. All rights reserved.
//

#import <Foundation/Foundation.h>
#import "SKZTurnBasedPlayer.h"

__attribute__ ((visibility("default")))

/**
 *  SKZTurnInformation is used to pass data about a turn based match between the Skillz SDK and a publisher's game.
 */
@interface SKZTurnBasedMatchInfo : NSObject

/**
 * Unique match id
 */
@property (readonly) NSInteger id;

/**
 * Match description as configured in the Skillz Developer Portal
 */
@property (readonly, nullable) NSString *matchDescription;

/**
 * Cash entry fee, nil if there is none
 */
@property (readonly, nullable) NSNumber *entryCash;

/**
 * Z points entry fee, nil if there is none
 */
@property (readonly, nullable) NSNumber *entryPoints;

/**
 * Signifies a cash match
 */
@property (readonly) BOOL isCash;

/**
 *  Match name as configured in the Skillz Developer Portal
 */
@property (readonly, nonnull) NSString *name;

/**
 * Template id for the template that the match is based on. These templates are
 * configured in the Skillz Developer Portal
 */
@property (readonly, nonnull) NSNumber *templateId;

/**
 *  Date that this tournament began
 */
@property (nonatomic, strong, nullable) NSDate *tournamentBeganDate;

/**
 *  Date that the opponent completed their turn, will be nil if player is initiating and has not been matched
 */
@property (nonatomic, strong, nullable) NSDate *lastTurnCompletedDate;

/**
 *  Display name used in Skillz for opponent, will be nil if player is initiating and has not been matched
 *
 *  DEPRECATED: use the display name field on the opponent object instead
 */
@property (nonatomic, strong, nullable) NSString *opponentDisplayName __attribute__ ((deprecated));

/**
 *  URL for opponent's Skillz avatar, will be nil if player is initiating and has not been matched
 *
 *  DEPRECATED: use the avatar URL field on the opponent object instead
 */
@property (nonatomic, strong, nullable) NSString *opponentAvatarURL __attribute__ ((deprecated));

/**
 *  Unique Id for opponent, use this to reference a player's stored data, will be nil if player is initiating and has not been matched
 *
 *  DEPRECATED: use the id field on the opponent object instead
 */
@property (nonatomic, strong, nullable) NSString *opponentUniqueId __attribute__ ((deprecated));


/**
 *  Current total score for the current player's opponent, if relevant to your game. Set when completing a turn, allows you to easily track and display total score without adjusting per round scoring. Used within the Skillz UI to display results.
 *
 *  DEPRECATED: use the current total score field on the opponent object instead
 */
@property (nonatomic, strong, nonnull) NSString *opponentCurrentTotalScore __attribute__ ((deprecated));

/**
 * Current player's opponent
 */
@property (readonly, nullable) SKZTurnBasedPlayer *opponent;

/**
 *  Current total score for the current player, if relevant to your game. Set by you when completing a turn, allows you to easily track and display total score without adjusting per round score. Used within the Skillz UI to display results.
 *
 *  DEPRECATED: use the current total score field on the player object instead
 */
@property (nonatomic, strong, nonnull) NSString *playerCurrentTotalScore __attribute__ ((deprecated));

/**
 *  Display name used in Skillz for current player
 *
 *  DEPRECATED: use the display name field on the player object instead
 */
@property (nonatomic, strong, nonnull) NSString *playerDisplayName __attribute__ ((deprecated));

/**
 *  URL for current player's Skillz avatar
 *
 *  DEPRECATED: use the avatar URL field on the player object instead
 */
@property (nonatomic, strong, nonnull) NSString *playerAvatarURL __attribute__ ((deprecated));

/**
 *  Unique ID for current player, use this to reference stored data for a given player
 *
 *  DEPRECATED: use the id field on the player object instead
 */
@property (nonatomic, strong, nonnull) NSString *playerUniqueId __attribute__ ((deprecated));

/**
 * Current turn based player
 */
@property (readonly, nonnull) SKZTurnBasedPlayer *turnBasedPlayer;

/**
 *  Base64 string encoded from NSData created by publisher used to create the game state for the current turn, will be nil for initiating turn if player has not been matched. 
 */
@property (nonatomic, strong, nullable) NSString *gameData;

/**
 *  Array of SKZRoundResults objects corresponding to each round that has been completed so far. See SKZRoundResults for more information.
 */
@property (nonatomic, strong, nonnull) NSArray *roundInformation;

/**
 *  Index of current turn, for the first turn currentTurnIndex equals 0, and so on.
 */
@property (nonatomic) NSInteger currentTurnIndex;

/**
 *  The first place prize for the winner of this tournament, will be zero if this is a cash tournament
 */
@property (nonatomic) NSInteger prizePoints;

/**
 *  The first place prize for the winner of this tournmanet, will be zero if this is not a cash tournament
 */
@property (nonatomic) CGFloat prizeCash;

/**
 *  Check this variable to determine whether your user is viewing an already complete match, or a match still in Progress
 */
@property (nonatomic) BOOL isGameComplete;

@end
