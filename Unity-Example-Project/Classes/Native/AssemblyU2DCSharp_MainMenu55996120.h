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

#ifdef __clang__
#pragma clang diagnostic push
#pragma clang diagnostic ignored "-Winvalid-offsetof"
#pragma clang diagnostic ignored "-Wunused-variable"
#endif

// MainMenu
struct  MainMenu_t55996120  : public MonoBehaviour_t667441552
{
public:
	// UnityEngine.GUIStyle MainMenu::Style
	GUIStyle_t2990928826 * ___Style_2;

public:
	inline static int32_t get_offset_of_Style_2() { return static_cast<int32_t>(offsetof(MainMenu_t55996120, ___Style_2)); }
	inline GUIStyle_t2990928826 * get_Style_2() const { return ___Style_2; }
	inline GUIStyle_t2990928826 ** get_address_of_Style_2() { return &___Style_2; }
	inline void set_Style_2(GUIStyle_t2990928826 * value)
	{
		___Style_2 = value;
		Il2CppCodeGenWriteBarrier(&___Style_2, value);
	}
};

#ifdef __clang__
#pragma clang diagnostic pop
#endif
