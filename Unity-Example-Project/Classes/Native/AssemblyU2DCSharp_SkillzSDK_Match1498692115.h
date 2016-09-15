#pragma once

#include "il2cpp-config.h"

#ifndef _MSC_VER
# include <alloca.h>
#else
# include <malloc.h>
#endif

#include <stdint.h>

// System.String
struct String_t;
// System.Collections.Generic.Dictionary`2<System.String,System.String>
struct Dictionary_2_t827649927;

#include "mscorlib_System_Object4170816371.h"
#include "mscorlib_System_Nullable_1_gen1237965023.h"
#include "mscorlib_System_Nullable_1_gen108794504.h"
#include "AssemblyU2DCSharp_SkillzSDK_Player1109057033.h"
#include "mscorlib_System_Nullable_1_gen560925241.h"
#include "mscorlib_System_Nullable_1_gen81078199.h"

#ifdef __clang__
#pragma clang diagnostic push
#pragma clang diagnostic ignored "-Winvalid-offsetof"
#pragma clang diagnostic ignored "-Wunused-variable"
#endif

// SkillzSDK.Match
struct  Match_t1498692115  : public Il2CppObject
{
public:
	// System.String SkillzSDK.Match::Name
	String_t* ___Name_0;
	// System.String SkillzSDK.Match::Description
	String_t* ___Description_1;
	// System.Nullable`1<System.Int32> SkillzSDK.Match::ID
	Nullable_1_t1237965023  ___ID_2;
	// System.Nullable`1<System.Int32> SkillzSDK.Match::TemplateID
	Nullable_1_t1237965023  ___TemplateID_3;
	// System.Nullable`1<System.UInt32> SkillzSDK.Match::SkillzDifficulty
	Nullable_1_t108794504  ___SkillzDifficulty_4;
	// System.Collections.Generic.Dictionary`2<System.String,System.String> SkillzSDK.Match::GameParams
	Dictionary_2_t827649927 * ___GameParams_5;
	// SkillzSDK.Player SkillzSDK.Match::Player
	Player_t1109057033  ___Player_6;
	// System.Nullable`1<System.Boolean> SkillzSDK.Match::IsCash
	Nullable_1_t560925241  ___IsCash_7;
	// System.Nullable`1<System.Int32> SkillzSDK.Match::EntryPoints
	Nullable_1_t1237965023  ___EntryPoints_8;
	// System.Nullable`1<System.Single> SkillzSDK.Match::EntryCash
	Nullable_1_t81078199  ___EntryCash_9;

public:
	inline static int32_t get_offset_of_Name_0() { return static_cast<int32_t>(offsetof(Match_t1498692115, ___Name_0)); }
	inline String_t* get_Name_0() const { return ___Name_0; }
	inline String_t** get_address_of_Name_0() { return &___Name_0; }
	inline void set_Name_0(String_t* value)
	{
		___Name_0 = value;
		Il2CppCodeGenWriteBarrier(&___Name_0, value);
	}

	inline static int32_t get_offset_of_Description_1() { return static_cast<int32_t>(offsetof(Match_t1498692115, ___Description_1)); }
	inline String_t* get_Description_1() const { return ___Description_1; }
	inline String_t** get_address_of_Description_1() { return &___Description_1; }
	inline void set_Description_1(String_t* value)
	{
		___Description_1 = value;
		Il2CppCodeGenWriteBarrier(&___Description_1, value);
	}

	inline static int32_t get_offset_of_ID_2() { return static_cast<int32_t>(offsetof(Match_t1498692115, ___ID_2)); }
	inline Nullable_1_t1237965023  get_ID_2() const { return ___ID_2; }
	inline Nullable_1_t1237965023 * get_address_of_ID_2() { return &___ID_2; }
	inline void set_ID_2(Nullable_1_t1237965023  value)
	{
		___ID_2 = value;
	}

	inline static int32_t get_offset_of_TemplateID_3() { return static_cast<int32_t>(offsetof(Match_t1498692115, ___TemplateID_3)); }
	inline Nullable_1_t1237965023  get_TemplateID_3() const { return ___TemplateID_3; }
	inline Nullable_1_t1237965023 * get_address_of_TemplateID_3() { return &___TemplateID_3; }
	inline void set_TemplateID_3(Nullable_1_t1237965023  value)
	{
		___TemplateID_3 = value;
	}

	inline static int32_t get_offset_of_SkillzDifficulty_4() { return static_cast<int32_t>(offsetof(Match_t1498692115, ___SkillzDifficulty_4)); }
	inline Nullable_1_t108794504  get_SkillzDifficulty_4() const { return ___SkillzDifficulty_4; }
	inline Nullable_1_t108794504 * get_address_of_SkillzDifficulty_4() { return &___SkillzDifficulty_4; }
	inline void set_SkillzDifficulty_4(Nullable_1_t108794504  value)
	{
		___SkillzDifficulty_4 = value;
	}

	inline static int32_t get_offset_of_GameParams_5() { return static_cast<int32_t>(offsetof(Match_t1498692115, ___GameParams_5)); }
	inline Dictionary_2_t827649927 * get_GameParams_5() const { return ___GameParams_5; }
	inline Dictionary_2_t827649927 ** get_address_of_GameParams_5() { return &___GameParams_5; }
	inline void set_GameParams_5(Dictionary_2_t827649927 * value)
	{
		___GameParams_5 = value;
		Il2CppCodeGenWriteBarrier(&___GameParams_5, value);
	}

	inline static int32_t get_offset_of_Player_6() { return static_cast<int32_t>(offsetof(Match_t1498692115, ___Player_6)); }
	inline Player_t1109057033  get_Player_6() const { return ___Player_6; }
	inline Player_t1109057033 * get_address_of_Player_6() { return &___Player_6; }
	inline void set_Player_6(Player_t1109057033  value)
	{
		___Player_6 = value;
	}

	inline static int32_t get_offset_of_IsCash_7() { return static_cast<int32_t>(offsetof(Match_t1498692115, ___IsCash_7)); }
	inline Nullable_1_t560925241  get_IsCash_7() const { return ___IsCash_7; }
	inline Nullable_1_t560925241 * get_address_of_IsCash_7() { return &___IsCash_7; }
	inline void set_IsCash_7(Nullable_1_t560925241  value)
	{
		___IsCash_7 = value;
	}

	inline static int32_t get_offset_of_EntryPoints_8() { return static_cast<int32_t>(offsetof(Match_t1498692115, ___EntryPoints_8)); }
	inline Nullable_1_t1237965023  get_EntryPoints_8() const { return ___EntryPoints_8; }
	inline Nullable_1_t1237965023 * get_address_of_EntryPoints_8() { return &___EntryPoints_8; }
	inline void set_EntryPoints_8(Nullable_1_t1237965023  value)
	{
		___EntryPoints_8 = value;
	}

	inline static int32_t get_offset_of_EntryCash_9() { return static_cast<int32_t>(offsetof(Match_t1498692115, ___EntryCash_9)); }
	inline Nullable_1_t81078199  get_EntryCash_9() const { return ___EntryCash_9; }
	inline Nullable_1_t81078199 * get_address_of_EntryCash_9() { return &___EntryCash_9; }
	inline void set_EntryCash_9(Nullable_1_t81078199  value)
	{
		___EntryCash_9 = value;
	}
};

#ifdef __clang__
#pragma clang diagnostic pop
#endif
