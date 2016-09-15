#pragma once

#include "il2cpp-config.h"

#ifndef _MSC_VER
# include <alloca.h>
#else
# include <malloc.h>
#endif

#include <stdint.h>


#include "mscorlib_System_ValueType1744280289.h"
#include "AssemblyU2DCSharp_SkillzSDK_TurnBasedRoundOutcome1849591944.h"
#include "mscorlib_System_Nullable_1_gen3952353088.h"

#ifdef __clang__
#pragma clang diagnostic push
#pragma clang diagnostic ignored "-Winvalid-offsetof"
#pragma clang diagnostic ignored "-Wunused-variable"
#endif

// SkillzSDK.TurnBasedRound
struct  TurnBasedRound_t1071595552 
{
public:
	// SkillzSDK.TurnBasedRoundOutcome SkillzSDK.TurnBasedRound::Outcome
	int32_t ___Outcome_0;
	// System.Nullable`1<System.Double> SkillzSDK.TurnBasedRound::OpponentRoundScore
	Nullable_1_t3952353088  ___OpponentRoundScore_1;
	// System.Nullable`1<System.Double> SkillzSDK.TurnBasedRound::MyRoundScore
	Nullable_1_t3952353088  ___MyRoundScore_2;

public:
	inline static int32_t get_offset_of_Outcome_0() { return static_cast<int32_t>(offsetof(TurnBasedRound_t1071595552, ___Outcome_0)); }
	inline int32_t get_Outcome_0() const { return ___Outcome_0; }
	inline int32_t* get_address_of_Outcome_0() { return &___Outcome_0; }
	inline void set_Outcome_0(int32_t value)
	{
		___Outcome_0 = value;
	}

	inline static int32_t get_offset_of_OpponentRoundScore_1() { return static_cast<int32_t>(offsetof(TurnBasedRound_t1071595552, ___OpponentRoundScore_1)); }
	inline Nullable_1_t3952353088  get_OpponentRoundScore_1() const { return ___OpponentRoundScore_1; }
	inline Nullable_1_t3952353088 * get_address_of_OpponentRoundScore_1() { return &___OpponentRoundScore_1; }
	inline void set_OpponentRoundScore_1(Nullable_1_t3952353088  value)
	{
		___OpponentRoundScore_1 = value;
	}

	inline static int32_t get_offset_of_MyRoundScore_2() { return static_cast<int32_t>(offsetof(TurnBasedRound_t1071595552, ___MyRoundScore_2)); }
	inline Nullable_1_t3952353088  get_MyRoundScore_2() const { return ___MyRoundScore_2; }
	inline Nullable_1_t3952353088 * get_address_of_MyRoundScore_2() { return &___MyRoundScore_2; }
	inline void set_MyRoundScore_2(Nullable_1_t3952353088  value)
	{
		___MyRoundScore_2 = value;
	}
};

#ifdef __clang__
#pragma clang diagnostic pop
#endif
// Native definition for marshalling of: SkillzSDK.TurnBasedRound
struct TurnBasedRound_t1071595552_marshaled_pinvoke
{
	int32_t ___Outcome_0;
	Nullable_1_t3952353088  ___OpponentRoundScore_1;
	Nullable_1_t3952353088  ___MyRoundScore_2;
};
// Native definition for marshalling of: SkillzSDK.TurnBasedRound
struct TurnBasedRound_t1071595552_marshaled_com
{
	int32_t ___Outcome_0;
	Nullable_1_t3952353088  ___OpponentRoundScore_1;
	Nullable_1_t3952353088  ___MyRoundScore_2;
};
