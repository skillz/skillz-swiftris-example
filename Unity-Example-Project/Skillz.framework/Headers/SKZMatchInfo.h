//
//  SKZMatchInfo.h
//  SkillzSDK-iOS
//
//  Created by James McMahon on 6/10/15.
//  Copyright (c) 2015 Skillz. All rights reserved.
//

#import <Foundation/Foundation.h>
#import "SKZPlayer.h"

__attribute__ ((visibility("default")))

/**
 *  This object contains various Skillz specific pieces of data that will allow you to customize your UI further.
 */
@interface SKZMatchInfo : NSObject

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
 * Current player in match
 */
@property (readonly, nonnull) SKZPlayer *player;

/**
 * Template id for the template that the match is based on. These templates are
 * configured in the Skillz Developer Portal
 */
@property (readonly, nonnull) NSNumber *templateId;

@end
