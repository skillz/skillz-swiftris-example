#pragma once

#include "il2cpp-config.h"

#ifndef _MSC_VER
# include <alloca.h>
#else
# include <malloc.h>
#endif

#include <stdint.h>

// System.Text.StringBuilder
struct StringBuilder_t243639308;

#include "mscorlib_System_Object4170816371.h"

#ifdef __clang__
#pragma clang diagnostic push
#pragma clang diagnostic ignored "-Winvalid-offsetof"
#pragma clang diagnostic ignored "-Wunused-variable"
#endif

// SkillzSDK.MiniJSON.Json/Serializer
struct  Serializer_t392897351  : public Il2CppObject
{
public:
	// System.Text.StringBuilder SkillzSDK.MiniJSON.Json/Serializer::builder
	StringBuilder_t243639308 * ___builder_0;

public:
	inline static int32_t get_offset_of_builder_0() { return static_cast<int32_t>(offsetof(Serializer_t392897351, ___builder_0)); }
	inline StringBuilder_t243639308 * get_builder_0() const { return ___builder_0; }
	inline StringBuilder_t243639308 ** get_address_of_builder_0() { return &___builder_0; }
	inline void set_builder_0(StringBuilder_t243639308 * value)
	{
		___builder_0 = value;
		Il2CppCodeGenWriteBarrier(&___builder_0, value);
	}
};

#ifdef __clang__
#pragma clang diagnostic pop
#endif
