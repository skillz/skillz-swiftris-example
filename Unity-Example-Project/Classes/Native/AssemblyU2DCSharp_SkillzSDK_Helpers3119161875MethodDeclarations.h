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

// System.String
struct String_t;

#include "codegen/il2cpp-codegen.h"
#include "mscorlib_System_Nullable_1_gen560925241.h"
#include "mscorlib_System_String7231557.h"
#include "mscorlib_System_Nullable_1_gen72820554.h"
#include "mscorlib_System_Nullable_1_gen3952353088.h"
#include "mscorlib_System_Nullable_1_gen1237965023.h"
#include "mscorlib_System_Nullable_1_gen108794504.h"

// System.Nullable`1<System.Boolean> SkillzSDK.Helpers::SafeBoolParse(System.String,System.String,System.String,System.String,System.String)
extern "C"  Nullable_1_t560925241  Helpers_SafeBoolParse_m1211796172 (Il2CppObject * __this /* static, unused */, String_t* ___str0, String_t* ___trueStr1, String_t* ___falseStr2, String_t* ___trueInt3, String_t* ___falseInt4, const MethodInfo* method) IL2CPP_METHOD_ATTR;
// System.Nullable`1<System.DateTime> SkillzSDK.Helpers::SafeParseUnixTime(System.Nullable`1<System.Double>)
extern "C"  Nullable_1_t72820554  Helpers_SafeParseUnixTime_m2442063057 (Il2CppObject * __this /* static, unused */, Nullable_1_t3952353088  ___unixTime0, const MethodInfo* method) IL2CPP_METHOD_ATTR;
// System.Nullable`1<System.Double> SkillzSDK.Helpers::SafeDoubleParse(System.String)
extern "C"  Nullable_1_t3952353088  Helpers_SafeDoubleParse_m507458024 (Il2CppObject * __this /* static, unused */, String_t* ___str0, const MethodInfo* method) IL2CPP_METHOD_ATTR;
// System.Nullable`1<System.Int32> SkillzSDK.Helpers::SafeIntParse(System.String)
extern "C"  Nullable_1_t1237965023  Helpers_SafeIntParse_m1739971967 (Il2CppObject * __this /* static, unused */, String_t* ___str0, const MethodInfo* method) IL2CPP_METHOD_ATTR;
// System.Nullable`1<System.UInt32> SkillzSDK.Helpers::SafeUintParse(System.String)
extern "C"  Nullable_1_t108794504  Helpers_SafeUintParse_m301492969 (Il2CppObject * __this /* static, unused */, String_t* ___str0, const MethodInfo* method) IL2CPP_METHOD_ATTR;
