#pragma once

#include "il2cpp-config.h"

#ifndef _MSC_VER
# include <alloca.h>
#else
# include <malloc.h>
#endif

#include <stdint.h>

// SkillzSDK.SkillzDelegateBase
struct SkillzDelegateBase_t3573513351;
// SkillzSDK.SkillzDelegateStandard
struct SkillzDelegateStandard_t855934707;
// SkillzSDK.SkillzDelegateTurnBased
struct SkillzDelegateTurnBased_t374563190;

#include "UnityEngine_UnityEngine_MonoBehaviour667441552.h"

#ifdef __clang__
#pragma clang diagnostic push
#pragma clang diagnostic ignored "-Winvalid-offsetof"
#pragma clang diagnostic ignored "-Wunused-variable"
#endif

// SkillzSDK.SkillzMessageReceiver
struct  SkillzMessageReceiver_t3704573403  : public MonoBehaviour_t667441552
{
public:
	// SkillzSDK.SkillzDelegateBase SkillzSDK.SkillzMessageReceiver::DelBase
	SkillzDelegateBase_t3573513351 * ___DelBase_2;
	// SkillzSDK.SkillzDelegateStandard SkillzSDK.SkillzMessageReceiver::DelStandard
	SkillzDelegateStandard_t855934707 * ___DelStandard_3;
	// SkillzSDK.SkillzDelegateTurnBased SkillzSDK.SkillzMessageReceiver::DelTurnBased
	SkillzDelegateTurnBased_t374563190 * ___DelTurnBased_4;

public:
	inline static int32_t get_offset_of_DelBase_2() { return static_cast<int32_t>(offsetof(SkillzMessageReceiver_t3704573403, ___DelBase_2)); }
	inline SkillzDelegateBase_t3573513351 * get_DelBase_2() const { return ___DelBase_2; }
	inline SkillzDelegateBase_t3573513351 ** get_address_of_DelBase_2() { return &___DelBase_2; }
	inline void set_DelBase_2(SkillzDelegateBase_t3573513351 * value)
	{
		___DelBase_2 = value;
		Il2CppCodeGenWriteBarrier(&___DelBase_2, value);
	}

	inline static int32_t get_offset_of_DelStandard_3() { return static_cast<int32_t>(offsetof(SkillzMessageReceiver_t3704573403, ___DelStandard_3)); }
	inline SkillzDelegateStandard_t855934707 * get_DelStandard_3() const { return ___DelStandard_3; }
	inline SkillzDelegateStandard_t855934707 ** get_address_of_DelStandard_3() { return &___DelStandard_3; }
	inline void set_DelStandard_3(SkillzDelegateStandard_t855934707 * value)
	{
		___DelStandard_3 = value;
		Il2CppCodeGenWriteBarrier(&___DelStandard_3, value);
	}

	inline static int32_t get_offset_of_DelTurnBased_4() { return static_cast<int32_t>(offsetof(SkillzMessageReceiver_t3704573403, ___DelTurnBased_4)); }
	inline SkillzDelegateTurnBased_t374563190 * get_DelTurnBased_4() const { return ___DelTurnBased_4; }
	inline SkillzDelegateTurnBased_t374563190 ** get_address_of_DelTurnBased_4() { return &___DelTurnBased_4; }
	inline void set_DelTurnBased_4(SkillzDelegateTurnBased_t374563190 * value)
	{
		___DelTurnBased_4 = value;
		Il2CppCodeGenWriteBarrier(&___DelTurnBased_4, value);
	}
};

#ifdef __clang__
#pragma clang diagnostic pop
#endif
