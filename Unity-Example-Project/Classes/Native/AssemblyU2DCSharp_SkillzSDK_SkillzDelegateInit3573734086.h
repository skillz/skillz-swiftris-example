#pragma once

#include "il2cpp-config.h"

#ifndef _MSC_VER
# include <alloca.h>
#else
# include <malloc.h>
#endif

#include <stdint.h>


#include "UnityEngine_UnityEngine_MonoBehaviour667441552.h"
#include "AssemblyU2DCSharp_SkillzSDK_Environment2087924769.h"

#ifdef __clang__
#pragma clang diagnostic push
#pragma clang diagnostic ignored "-Winvalid-offsetof"
#pragma clang diagnostic ignored "-Wunused-variable"
#endif

// SkillzSDK.SkillzDelegateInit
struct  SkillzDelegateInit_t3573734086  : public MonoBehaviour_t667441552
{
public:
	// System.Int32 SkillzSDK.SkillzDelegateInit::GameID
	int32_t ___GameID_3;
	// SkillzSDK.Environment SkillzSDK.SkillzDelegateInit::SkillzEnvironment
	int32_t ___SkillzEnvironment_4;

public:
	inline static int32_t get_offset_of_GameID_3() { return static_cast<int32_t>(offsetof(SkillzDelegateInit_t3573734086, ___GameID_3)); }
	inline int32_t get_GameID_3() const { return ___GameID_3; }
	inline int32_t* get_address_of_GameID_3() { return &___GameID_3; }
	inline void set_GameID_3(int32_t value)
	{
		___GameID_3 = value;
	}

	inline static int32_t get_offset_of_SkillzEnvironment_4() { return static_cast<int32_t>(offsetof(SkillzDelegateInit_t3573734086, ___SkillzEnvironment_4)); }
	inline int32_t get_SkillzEnvironment_4() const { return ___SkillzEnvironment_4; }
	inline int32_t* get_address_of_SkillzEnvironment_4() { return &___SkillzEnvironment_4; }
	inline void set_SkillzEnvironment_4(int32_t value)
	{
		___SkillzEnvironment_4 = value;
	}
};

struct SkillzDelegateInit_t3573734086_StaticFields
{
public:
	// System.Boolean SkillzSDK.SkillzDelegateInit::initializedYet
	bool ___initializedYet_2;

public:
	inline static int32_t get_offset_of_initializedYet_2() { return static_cast<int32_t>(offsetof(SkillzDelegateInit_t3573734086_StaticFields, ___initializedYet_2)); }
	inline bool get_initializedYet_2() const { return ___initializedYet_2; }
	inline bool* get_address_of_initializedYet_2() { return &___initializedYet_2; }
	inline void set_initializedYet_2(bool value)
	{
		___initializedYet_2 = value;
	}
};

#ifdef __clang__
#pragma clang diagnostic pop
#endif
