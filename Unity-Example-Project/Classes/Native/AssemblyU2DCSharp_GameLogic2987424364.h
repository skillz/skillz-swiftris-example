#pragma once

#include "il2cpp-config.h"

#ifndef _MSC_VER
# include <alloca.h>
#else
# include <malloc.h>
#endif

#include <stdint.h>

// UnityEngine.GUIStyle
struct GUIStyle_t2990928826;

#include "UnityEngine_UnityEngine_MonoBehaviour667441552.h"
#include "AssemblyU2DCSharp_GameLogic_TournamentTypes4265569869.h"

#ifdef __clang__
#pragma clang diagnostic push
#pragma clang diagnostic ignored "-Winvalid-offsetof"
#pragma clang diagnostic ignored "-Wunused-variable"
#endif

// GameLogic
struct  GameLogic_t2987424364  : public MonoBehaviour_t667441552
{
public:
	// GameLogic/TournamentTypes GameLogic::MatchType
	int32_t ___MatchType_2;
	// UnityEngine.GUIStyle GameLogic::Style
	GUIStyle_t2990928826 * ___Style_3;
	// UnityEngine.GUIStyle GameLogic::LabelStyle
	GUIStyle_t2990928826 * ___LabelStyle_4;

public:
	inline static int32_t get_offset_of_MatchType_2() { return static_cast<int32_t>(offsetof(GameLogic_t2987424364, ___MatchType_2)); }
	inline int32_t get_MatchType_2() const { return ___MatchType_2; }
	inline int32_t* get_address_of_MatchType_2() { return &___MatchType_2; }
	inline void set_MatchType_2(int32_t value)
	{
		___MatchType_2 = value;
	}

	inline static int32_t get_offset_of_Style_3() { return static_cast<int32_t>(offsetof(GameLogic_t2987424364, ___Style_3)); }
	inline GUIStyle_t2990928826 * get_Style_3() const { return ___Style_3; }
	inline GUIStyle_t2990928826 ** get_address_of_Style_3() { return &___Style_3; }
	inline void set_Style_3(GUIStyle_t2990928826 * value)
	{
		___Style_3 = value;
		Il2CppCodeGenWriteBarrier(&___Style_3, value);
	}

	inline static int32_t get_offset_of_LabelStyle_4() { return static_cast<int32_t>(offsetof(GameLogic_t2987424364, ___LabelStyle_4)); }
	inline GUIStyle_t2990928826 * get_LabelStyle_4() const { return ___LabelStyle_4; }
	inline GUIStyle_t2990928826 ** get_address_of_LabelStyle_4() { return &___LabelStyle_4; }
	inline void set_LabelStyle_4(GUIStyle_t2990928826 * value)
	{
		___LabelStyle_4 = value;
		Il2CppCodeGenWriteBarrier(&___LabelStyle_4, value);
	}
};

#ifdef __clang__
#pragma clang diagnostic pop
#endif
