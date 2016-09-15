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

#include "mscorlib_System_ValueType1744280289.h"
#include "AssemblyU2DCSharp_SkillzSDK_Player1109057033.h"
#include "mscorlib_System_Nullable_1_gen3952353088.h"

#ifdef __clang__
#pragma clang diagnostic push
#pragma clang diagnostic ignored "-Winvalid-offsetof"
#pragma clang diagnostic ignored "-Wunused-variable"
#endif

// SkillzSDK.ContinuedTurnBasedMatch
struct  ContinuedTurnBasedMatch_t2912208314 
{
public:
	// System.String SkillzSDK.ContinuedTurnBasedMatch::GameData
	String_t* ___GameData_0;
	// SkillzSDK.Player SkillzSDK.ContinuedTurnBasedMatch::Opponent
	Player_t1109057033  ___Opponent_1;
	// System.Nullable`1<System.Double> SkillzSDK.ContinuedTurnBasedMatch::MyCurrentTotalScore
	Nullable_1_t3952353088  ___MyCurrentTotalScore_2;
	// System.Nullable`1<System.Double> SkillzSDK.ContinuedTurnBasedMatch::OpponentCurrentTotalScore
	Nullable_1_t3952353088  ___OpponentCurrentTotalScore_3;

public:
	inline static int32_t get_offset_of_GameData_0() { return static_cast<int32_t>(offsetof(ContinuedTurnBasedMatch_t2912208314, ___GameData_0)); }
	inline String_t* get_GameData_0() const { return ___GameData_0; }
	inline String_t** get_address_of_GameData_0() { return &___GameData_0; }
	inline void set_GameData_0(String_t* value)
	{
		___GameData_0 = value;
		Il2CppCodeGenWriteBarrier(&___GameData_0, value);
	}

	inline static int32_t get_offset_of_Opponent_1() { return static_cast<int32_t>(offsetof(ContinuedTurnBasedMatch_t2912208314, ___Opponent_1)); }
	inline Player_t1109057033  get_Opponent_1() const { return ___Opponent_1; }
	inline Player_t1109057033 * get_address_of_Opponent_1() { return &___Opponent_1; }
	inline void set_Opponent_1(Player_t1109057033  value)
	{
		___Opponent_1 = value;
	}

	inline static int32_t get_offset_of_MyCurrentTotalScore_2() { return static_cast<int32_t>(offsetof(ContinuedTurnBasedMatch_t2912208314, ___MyCurrentTotalScore_2)); }
	inline Nullable_1_t3952353088  get_MyCurrentTotalScore_2() const { return ___MyCurrentTotalScore_2; }
	inline Nullable_1_t3952353088 * get_address_of_MyCurrentTotalScore_2() { return &___MyCurrentTotalScore_2; }
	inline void set_MyCurrentTotalScore_2(Nullable_1_t3952353088  value)
	{
		___MyCurrentTotalScore_2 = value;
	}

	inline static int32_t get_offset_of_OpponentCurrentTotalScore_3() { return static_cast<int32_t>(offsetof(ContinuedTurnBasedMatch_t2912208314, ___OpponentCurrentTotalScore_3)); }
	inline Nullable_1_t3952353088  get_OpponentCurrentTotalScore_3() const { return ___OpponentCurrentTotalScore_3; }
	inline Nullable_1_t3952353088 * get_address_of_OpponentCurrentTotalScore_3() { return &___OpponentCurrentTotalScore_3; }
	inline void set_OpponentCurrentTotalScore_3(Nullable_1_t3952353088  value)
	{
		___OpponentCurrentTotalScore_3 = value;
	}
};

#ifdef __clang__
#pragma clang diagnostic pop
#endif
// Native definition for marshalling of: SkillzSDK.ContinuedTurnBasedMatch
struct ContinuedTurnBasedMatch_t2912208314_marshaled_pinvoke
{
	char* ___GameData_0;
	Player_t1109057033_marshaled_pinvoke ___Opponent_1;
	Nullable_1_t3952353088  ___MyCurrentTotalScore_2;
	Nullable_1_t3952353088  ___OpponentCurrentTotalScore_3;
};
// Native definition for marshalling of: SkillzSDK.ContinuedTurnBasedMatch
struct ContinuedTurnBasedMatch_t2912208314_marshaled_com
{
	Il2CppChar* ___GameData_0;
	Player_t1109057033_marshaled_com ___Opponent_1;
	Nullable_1_t3952353088  ___MyCurrentTotalScore_2;
	Nullable_1_t3952353088  ___OpponentCurrentTotalScore_3;
};
