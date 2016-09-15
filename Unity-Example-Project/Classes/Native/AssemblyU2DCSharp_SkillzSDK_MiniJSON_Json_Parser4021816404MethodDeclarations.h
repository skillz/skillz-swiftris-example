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

// SkillzSDK.MiniJSON.Json/Parser
struct Parser_t4021816404;
// System.String
struct String_t;
// System.Object
struct Il2CppObject;
// System.Collections.Generic.Dictionary`2<System.String,System.Object>
struct Dictionary_2_t696267445;
// System.Collections.Generic.List`1<System.Object>
struct List_1_t1244034627;

#include "codegen/il2cpp-codegen.h"
#include "mscorlib_System_String7231557.h"
#include "AssemblyU2DCSharp_SkillzSDK_MiniJSON_Json_Parser_T3238200414.h"

// System.Void SkillzSDK.MiniJSON.Json/Parser::.ctor(System.String)
extern "C"  void Parser__ctor_m2264591259 (Parser_t4021816404 * __this, String_t* ___jsonString0, const MethodInfo* method) IL2CPP_METHOD_ATTR;
// System.Boolean SkillzSDK.MiniJSON.Json/Parser::IsWordBreak(System.Char)
extern "C"  bool Parser_IsWordBreak_m1717634913 (Il2CppObject * __this /* static, unused */, Il2CppChar ___c0, const MethodInfo* method) IL2CPP_METHOD_ATTR;
// System.Object SkillzSDK.MiniJSON.Json/Parser::Parse(System.String)
extern "C"  Il2CppObject * Parser_Parse_m2036100191 (Il2CppObject * __this /* static, unused */, String_t* ___jsonString0, const MethodInfo* method) IL2CPP_METHOD_ATTR;
// System.Void SkillzSDK.MiniJSON.Json/Parser::Dispose()
extern "C"  void Parser_Dispose_m3959981380 (Parser_t4021816404 * __this, const MethodInfo* method) IL2CPP_METHOD_ATTR;
// System.Collections.Generic.Dictionary`2<System.String,System.Object> SkillzSDK.MiniJSON.Json/Parser::ParseObject()
extern "C"  Dictionary_2_t696267445 * Parser_ParseObject_m2283189275 (Parser_t4021816404 * __this, const MethodInfo* method) IL2CPP_METHOD_ATTR;
// System.Collections.Generic.List`1<System.Object> SkillzSDK.MiniJSON.Json/Parser::ParseArray()
extern "C"  List_1_t1244034627 * Parser_ParseArray_m398114210 (Parser_t4021816404 * __this, const MethodInfo* method) IL2CPP_METHOD_ATTR;
// System.Object SkillzSDK.MiniJSON.Json/Parser::ParseValue()
extern "C"  Il2CppObject * Parser_ParseValue_m3360627920 (Parser_t4021816404 * __this, const MethodInfo* method) IL2CPP_METHOD_ATTR;
// System.Object SkillzSDK.MiniJSON.Json/Parser::ParseByToken(SkillzSDK.MiniJSON.Json/Parser/TOKEN)
extern "C"  Il2CppObject * Parser_ParseByToken_m3613924067 (Parser_t4021816404 * __this, int32_t ___token0, const MethodInfo* method) IL2CPP_METHOD_ATTR;
// System.String SkillzSDK.MiniJSON.Json/Parser::ParseString()
extern "C"  String_t* Parser_ParseString_m2277472070 (Parser_t4021816404 * __this, const MethodInfo* method) IL2CPP_METHOD_ATTR;
// System.Object SkillzSDK.MiniJSON.Json/Parser::ParseNumber()
extern "C"  Il2CppObject * Parser_ParseNumber_m624027436 (Parser_t4021816404 * __this, const MethodInfo* method) IL2CPP_METHOD_ATTR;
// System.Void SkillzSDK.MiniJSON.Json/Parser::EatWhitespace()
extern "C"  void Parser_EatWhitespace_m3419124378 (Parser_t4021816404 * __this, const MethodInfo* method) IL2CPP_METHOD_ATTR;
// System.Char SkillzSDK.MiniJSON.Json/Parser::get_PeekChar()
extern "C"  Il2CppChar Parser_get_PeekChar_m4111056693 (Parser_t4021816404 * __this, const MethodInfo* method) IL2CPP_METHOD_ATTR;
// System.Char SkillzSDK.MiniJSON.Json/Parser::get_NextChar()
extern "C"  Il2CppChar Parser_get_NextChar_m2655340237 (Parser_t4021816404 * __this, const MethodInfo* method) IL2CPP_METHOD_ATTR;
// System.String SkillzSDK.MiniJSON.Json/Parser::get_NextWord()
extern "C"  String_t* Parser_get_NextWord_m1200785510 (Parser_t4021816404 * __this, const MethodInfo* method) IL2CPP_METHOD_ATTR;
// SkillzSDK.MiniJSON.Json/Parser/TOKEN SkillzSDK.MiniJSON.Json/Parser::get_NextToken()
extern "C"  int32_t Parser_get_NextToken_m201548099 (Parser_t4021816404 * __this, const MethodInfo* method) IL2CPP_METHOD_ATTR;
