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

// SkillzSDK.SkillzMessageReceiver
struct SkillzMessageReceiver_t3704573403;
// System.String
struct String_t;
// System.Collections.Generic.Dictionary`2<System.String,System.Object>
struct Dictionary_2_t696267445;

#include "codegen/il2cpp-codegen.h"
#include "mscorlib_System_String7231557.h"

// System.Void SkillzSDK.SkillzMessageReceiver::.ctor()
extern "C"  void SkillzMessageReceiver__ctor_m1800644891 (SkillzMessageReceiver_t3704573403 * __this, const MethodInfo* method) IL2CPP_METHOD_ATTR;
// System.Void SkillzSDK.SkillzMessageReceiver::Start()
extern "C"  void SkillzMessageReceiver_Start_m747782683 (SkillzMessageReceiver_t3704573403 * __this, const MethodInfo* method) IL2CPP_METHOD_ATTR;
// System.Void SkillzSDK.SkillzMessageReceiver::skillzWillExit(System.String)
extern "C"  void SkillzMessageReceiver_skillzWillExit_m2947854944 (SkillzMessageReceiver_t3704573403 * __this, String_t* ___ignoreMe0, const MethodInfo* method) IL2CPP_METHOD_ATTR;
// System.Void SkillzSDK.SkillzMessageReceiver::skillzLaunchHasCompleted(System.String)
extern "C"  void SkillzMessageReceiver_skillzLaunchHasCompleted_m3183403276 (SkillzMessageReceiver_t3704573403 * __this, String_t* ___ignoreMe0, const MethodInfo* method) IL2CPP_METHOD_ATTR;
// System.Void SkillzSDK.SkillzMessageReceiver::skillzWillLaunch(System.String)
extern "C"  void SkillzMessageReceiver_skillzWillLaunch_m2303506411 (SkillzMessageReceiver_t3704573403 * __this, String_t* ___ignoreMe0, const MethodInfo* method) IL2CPP_METHOD_ATTR;
// System.Void SkillzSDK.SkillzMessageReceiver::skillzWithPlayerAbort(System.String)
extern "C"  void SkillzMessageReceiver_skillzWithPlayerAbort_m529319081 (SkillzMessageReceiver_t3704573403 * __this, String_t* ___ignoreMe0, const MethodInfo* method) IL2CPP_METHOD_ATTR;
// System.Void SkillzSDK.SkillzMessageReceiver::skillzTournamentWillBegin(System.String)
extern "C"  void SkillzMessageReceiver_skillzTournamentWillBegin_m3635650980 (SkillzMessageReceiver_t3704573403 * __this, String_t* ___matchInfoJson0, const MethodInfo* method) IL2CPP_METHOD_ATTR;
// System.Void SkillzSDK.SkillzMessageReceiver::skillzWithTournamentCompletion(System.String)
extern "C"  void SkillzMessageReceiver_skillzWithTournamentCompletion_m1154982533 (SkillzMessageReceiver_t3704573403 * __this, String_t* ___ignoreMe0, const MethodInfo* method) IL2CPP_METHOD_ATTR;
// System.Void SkillzSDK.SkillzMessageReceiver::skillzTurnBasedTournamentWillBegin(System.String)
extern "C"  void SkillzMessageReceiver_skillzTurnBasedTournamentWillBegin_m3601968376 (SkillzMessageReceiver_t3704573403 * __this, String_t* ___turnBasedMatchInfoJson0, const MethodInfo* method) IL2CPP_METHOD_ATTR;
// System.Void SkillzSDK.SkillzMessageReceiver::skillzEndTurnCompletion(System.String)
extern "C"  void SkillzMessageReceiver_skillzEndTurnCompletion_m2181406526 (SkillzMessageReceiver_t3704573403 * __this, String_t* ___ignoreMe0, const MethodInfo* method) IL2CPP_METHOD_ATTR;
// System.Void SkillzSDK.SkillzMessageReceiver::skillzReviewCurrentGameState(System.String)
extern "C"  void SkillzMessageReceiver_skillzReviewCurrentGameState_m2422192306 (SkillzMessageReceiver_t3704573403 * __this, String_t* ___turnBasedMatchInfoJson0, const MethodInfo* method) IL2CPP_METHOD_ATTR;
// System.Void SkillzSDK.SkillzMessageReceiver::skillzFinishReviewingCurrentGameState(System.String)
extern "C"  void SkillzMessageReceiver_skillzFinishReviewingCurrentGameState_m4176346549 (SkillzMessageReceiver_t3704573403 * __this, String_t* ___ignoreMe0, const MethodInfo* method) IL2CPP_METHOD_ATTR;
// System.Collections.Generic.Dictionary`2<System.String,System.Object> SkillzSDK.SkillzMessageReceiver::DeserializeJSONToDictionary(System.String)
extern "C"  Dictionary_2_t696267445 * SkillzMessageReceiver_DeserializeJSONToDictionary_m1977053525 (Il2CppObject * __this /* static, unused */, String_t* ___jsonString0, const MethodInfo* method) IL2CPP_METHOD_ATTR;
