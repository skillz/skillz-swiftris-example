#pragma once

#include "il2cpp-config.h"

#ifndef _MSC_VER
# include <alloca.h>
#else
# include <malloc.h>
#endif



#include "mscorlib_System_Array1146569071.h"
#include "AssemblyU2DCSharp_SkillzSDK_TurnBasedRound1071595552.h"

#pragma once
// SkillzSDK.TurnBasedRound[]
struct TurnBasedRoundU5BU5D_t164633441  : public Il2CppArray
{
public:
	ALIGN_FIELD (8) TurnBasedRound_t1071595552  m_Items[1];

public:
	inline TurnBasedRound_t1071595552  GetAt(il2cpp_array_size_t index) const { return m_Items[index]; }
	inline TurnBasedRound_t1071595552 * GetAddressAt(il2cpp_array_size_t index) { return m_Items + index; }
	inline void SetAt(il2cpp_array_size_t index, TurnBasedRound_t1071595552  value)
	{
		m_Items[index] = value;
	}
};
