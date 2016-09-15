#pragma once

#include "il2cpp-config.h"

#ifndef _MSC_VER
# include <alloca.h>
#else
# include <malloc.h>
#endif

#include <stdint.h>
#include <assert.h>
#include <exception>

// System.String
struct String_t;
// System.Collections.Generic.Dictionary`2<System.String,System.Object>
struct Dictionary_2_t696267445;

#include "codegen/il2cpp-codegen.h"
#include "mscorlib_System_String7231557.h"
#include "mscorlib_System_IntPtr4010401971.h"
#include "AssemblyU2DCSharp_SkillzSDK_Player1109057033.h"
#include "AssemblyU2DCSharp_SkillzSDK_Environment2087924769.h"
#include "AssemblyU2DCSharp_SkillzSDK_Orientation734328798.h"
#include "AssemblyU2DCSharp_SkillzSDK_TurnBasedRoundOutcome1849591944.h"
#include "AssemblyU2DCSharp_SkillzSDK_TurnBasedMatchOutcome1871643761.h"

// System.Void SkillzSDK.Api::_addMetadataForMatchInProgress(System.String,System.Boolean)
extern "C"  void Api__addMetadataForMatchInProgress_m3250407120 (Il2CppObject * __this /* static, unused */, String_t* ___metadataJson0, bool ___forMatchInProgress1, const MethodInfo* method) IL2CPP_METHOD_ATTR;
// System.Int32 SkillzSDK.Api::_getRandomNumber()
extern "C"  int32_t Api__getRandomNumber_m1685941387 (Il2CppObject * __this /* static, unused */, const MethodInfo* method) IL2CPP_METHOD_ATTR;
// System.Int32 SkillzSDK.Api::_getRandomNumberWithMinAndMax(System.Int32,System.Int32)
extern "C"  int32_t Api__getRandomNumberWithMinAndMax_m4127705846 (Il2CppObject * __this /* static, unused */, int32_t ___min0, int32_t ___max1, const MethodInfo* method) IL2CPP_METHOD_ATTR;
// System.Void SkillzSDK.Api::_skillzInitForGameIdAndEnvironment(System.String,System.String)
extern "C"  void Api__skillzInitForGameIdAndEnvironment_m735445390 (Il2CppObject * __this /* static, unused */, String_t* ___gameId0, String_t* ___environment1, const MethodInfo* method) IL2CPP_METHOD_ATTR;
// System.Int32 SkillzSDK.Api::_tournamentIsInProgress()
extern "C"  int32_t Api__tournamentIsInProgress_m2786948830 (Il2CppObject * __this /* static, unused */, const MethodInfo* method) IL2CPP_METHOD_ATTR;
// System.IntPtr SkillzSDK.Api::_player()
extern "C"  IntPtr_t Api__player_m192294753 (Il2CppObject * __this /* static, unused */, const MethodInfo* method) IL2CPP_METHOD_ATTR;
// System.IntPtr SkillzSDK.Api::_SDKShortVersion()
extern "C"  IntPtr_t Api__SDKShortVersion_m1455033496 (Il2CppObject * __this /* static, unused */, const MethodInfo* method) IL2CPP_METHOD_ATTR;
// System.Void SkillzSDK.Api::_showSDKVersionInfo()
extern "C"  void Api__showSDKVersionInfo_m3930938484 (Il2CppObject * __this /* static, unused */, const MethodInfo* method) IL2CPP_METHOD_ATTR;
// System.Void SkillzSDK.Api::_launchSkillz(System.String)
extern "C"  void Api__launchSkillz_m4055537339 (Il2CppObject * __this /* static, unused */, String_t* ___orientation0, const MethodInfo* method) IL2CPP_METHOD_ATTR;
// System.Void SkillzSDK.Api::_displayTournamentResultsWithScore(System.Int32)
extern "C"  void Api__displayTournamentResultsWithScore_m312667401 (Il2CppObject * __this /* static, unused */, int32_t ___score0, const MethodInfo* method) IL2CPP_METHOD_ATTR;
// System.Void SkillzSDK.Api::_displayTournamentResultsWithFloatScore(System.Single)
extern "C"  void Api__displayTournamentResultsWithFloatScore_m3903203161 (Il2CppObject * __this /* static, unused */, float ___score0, const MethodInfo* method) IL2CPP_METHOD_ATTR;
// System.Void SkillzSDK.Api::_completeTurnWithGameData(System.String,System.String,System.Single,System.Single,System.String,System.String)
extern "C"  void Api__completeTurnWithGameData_m1340534909 (Il2CppObject * __this /* static, unused */, String_t* ___gameData0, String_t* ___score1, float ___playerCurrentTotalScore2, float ___opponentCurrentTotalScore3, String_t* ___roundOutcome4, String_t* ___matchOutcome5, const MethodInfo* method) IL2CPP_METHOD_ATTR;
// System.Void SkillzSDK.Api::_finishReviewingCurrentGameState()
extern "C"  void Api__finishReviewingCurrentGameState_m4007448660 (Il2CppObject * __this /* static, unused */, const MethodInfo* method) IL2CPP_METHOD_ATTR;
// System.Void SkillzSDK.Api::_notifyPlayerAbortWithCompletion()
extern "C"  void Api__notifyPlayerAbortWithCompletion_m4016213919 (Il2CppObject * __this /* static, unused */, const MethodInfo* method) IL2CPP_METHOD_ATTR;
// System.Void SkillzSDK.Api::_updatePlayersCurrentScore(System.Single)
extern "C"  void Api__updatePlayersCurrentScore_m1293975922 (Il2CppObject * __this /* static, unused */, float ___score0, const MethodInfo* method) IL2CPP_METHOD_ATTR;
// System.Single SkillzSDK.Api::_getRandomFloat()
extern "C"  float Api__getRandomFloat_m1709864058 (Il2CppObject * __this /* static, unused */, const MethodInfo* method) IL2CPP_METHOD_ATTR;
// System.Void SkillzSDK.Api::_setShouldSkillzLaunchFromURL(System.Boolean)
extern "C"  void Api__setShouldSkillzLaunchFromURL_m1022303350 (Il2CppObject * __this /* static, unused */, bool ___allowLaunch0, const MethodInfo* method) IL2CPP_METHOD_ATTR;
// System.Boolean SkillzSDK.Api::get_IsTournamentInProgress()
extern "C"  bool Api_get_IsTournamentInProgress_m465898288 (Il2CppObject * __this /* static, unused */, const MethodInfo* method) IL2CPP_METHOD_ATTR;
// System.String SkillzSDK.Api::get_CurrentUserDisplayName()
extern "C"  String_t* Api_get_CurrentUserDisplayName_m2769774789 (Il2CppObject * __this /* static, unused */, const MethodInfo* method) IL2CPP_METHOD_ATTR;
// SkillzSDK.Player SkillzSDK.Api::get_Player()
extern "C"  Player_t1109057033  Api_get_Player_m4094283361 (Il2CppObject * __this /* static, unused */, const MethodInfo* method) IL2CPP_METHOD_ATTR;
// System.String SkillzSDK.Api::get_SDKVersionShort()
extern "C"  String_t* Api_get_SDKVersionShort_m4266186404 (Il2CppObject * __this /* static, unused */, const MethodInfo* method) IL2CPP_METHOD_ATTR;
// System.Void SkillzSDK.Api::Initialize(System.String,SkillzSDK.Environment)
extern "C"  void Api_Initialize_m3620631794 (Il2CppObject * __this /* static, unused */, String_t* ___gameId0, int32_t ___environment1, const MethodInfo* method) IL2CPP_METHOD_ATTR;
// System.Void SkillzSDK.Api::LaunchSkillz(SkillzSDK.Orientation)
extern "C"  void Api_LaunchSkillz_m2902368013 (Il2CppObject * __this /* static, unused */, int32_t ___orientation0, const MethodInfo* method) IL2CPP_METHOD_ATTR;
// System.Void SkillzSDK.Api::UpdatePlayerScore(System.Single)
extern "C"  void Api_UpdatePlayerScore_m427289463 (Il2CppObject * __this /* static, unused */, float ___currentScoreForPlayer0, const MethodInfo* method) IL2CPP_METHOD_ATTR;
// System.Void SkillzSDK.Api::AbortGame()
extern "C"  void Api_AbortGame_m2986410702 (Il2CppObject * __this /* static, unused */, const MethodInfo* method) IL2CPP_METHOD_ATTR;
// System.Void SkillzSDK.Api::FinishTournament(System.Int32)
extern "C"  void Api_FinishTournament_m3003568579 (Il2CppObject * __this /* static, unused */, int32_t ___score0, const MethodInfo* method) IL2CPP_METHOD_ATTR;
// System.Void SkillzSDK.Api::FinishTournament(System.Single)
extern "C"  void Api_FinishTournament_m3054334841 (Il2CppObject * __this /* static, unused */, float ___score0, const MethodInfo* method) IL2CPP_METHOD_ATTR;
// System.Void SkillzSDK.Api::FinishTurn(System.String,SkillzSDK.TurnBasedRoundOutcome,SkillzSDK.TurnBasedMatchOutcome)
extern "C"  void Api_FinishTurn_m3052404881 (Il2CppObject * __this /* static, unused */, String_t* ___gameData0, int32_t ___roundOutcome1, int32_t ___matchOutcome2, const MethodInfo* method) IL2CPP_METHOD_ATTR;
// System.Void SkillzSDK.Api::FinishTurn(System.String,SkillzSDK.TurnBasedRoundOutcome,SkillzSDK.TurnBasedMatchOutcome,System.String,System.Single,System.Single)
extern "C"  void Api_FinishTurn_m3848802967 (Il2CppObject * __this /* static, unused */, String_t* ___gameData0, int32_t ___roundOutcome1, int32_t ___matchOutcome2, String_t* ___playerTurnScore3, float ___playerTotalScore4, float ___opponentTotalScore5, const MethodInfo* method) IL2CPP_METHOD_ATTR;
// System.Void SkillzSDK.Api::FinishReviewingTurn()
extern "C"  void Api_FinishReviewingTurn_m1545909408 (Il2CppObject * __this /* static, unused */, const MethodInfo* method) IL2CPP_METHOD_ATTR;
// System.Void SkillzSDK.Api::AddMetadataForMatchInProgress(System.String,System.Boolean)
extern "C"  void Api_AddMetadataForMatchInProgress_m983659365 (Il2CppObject * __this /* static, unused */, String_t* ___metadataJson0, bool ___forMatchInProgress1, const MethodInfo* method) IL2CPP_METHOD_ATTR;
// System.Int32 SkillzSDK.Api::GetRandomNumber()
extern "C"  int32_t Api_GetRandomNumber_m2535032252 (Il2CppObject * __this /* static, unused */, const MethodInfo* method) IL2CPP_METHOD_ATTR;
// System.Int32 SkillzSDK.Api::GetRandomNumber(System.Int32,System.Int32)
extern "C"  int32_t Api_GetRandomNumber_m2537519338 (Il2CppObject * __this /* static, unused */, int32_t ___min0, int32_t ___max1, const MethodInfo* method) IL2CPP_METHOD_ATTR;
// System.Void SkillzSDK.Api::PrintSDKVersionInfo()
extern "C"  void Api_PrintSDKVersionInfo_m1557137701 (Il2CppObject * __this /* static, unused */, const MethodInfo* method) IL2CPP_METHOD_ATTR;
// System.Void SkillzSDK.Api::SetShouldSkillzLaunchFromURL(System.Boolean)
extern "C"  void Api_SetShouldSkillzLaunchFromURL_m108787841 (Il2CppObject * __this /* static, unused */, bool ___allowLaunch0, const MethodInfo* method) IL2CPP_METHOD_ATTR;
// System.Collections.Generic.Dictionary`2<System.String,System.Object> SkillzSDK.Api::DeserializeJSONToDictionary(System.String)
extern "C"  Dictionary_2_t696267445 * Api_DeserializeJSONToDictionary_m2709150242 (Il2CppObject * __this /* static, unused */, String_t* ___jsonString0, const MethodInfo* method) IL2CPP_METHOD_ATTR;
