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

// System.Collections.Generic.Dictionary`2<System.String,System.Object>
struct Dictionary_2_t696267445;
// System.String
struct String_t;
// SkillzSDK.Player
struct Player_t1109057033;
struct Player_t1109057033_marshaled_pinvoke;
struct Player_t1109057033_marshaled_com;

#include "codegen/il2cpp-codegen.h"
#include "AssemblyU2DCSharp_SkillzSDK_Player1109057033.h"

// System.Void SkillzSDK.Player::.ctor(System.Collections.Generic.Dictionary`2<System.String,System.Object>)
extern "C"  void Player__ctor_m3061444668 (Player_t1109057033 * __this, Dictionary_2_t696267445 * ___playerJSON0, const MethodInfo* method) IL2CPP_METHOD_ATTR;
// System.String SkillzSDK.Player::ToString()
extern "C"  String_t* Player_ToString_m3912642006 (Player_t1109057033 * __this, const MethodInfo* method) IL2CPP_METHOD_ATTR;

// Methods for marshaling
struct Player_t1109057033;
struct Player_t1109057033_marshaled_pinvoke;

extern "C" void Player_t1109057033_marshal_pinvoke(const Player_t1109057033& unmarshaled, Player_t1109057033_marshaled_pinvoke& marshaled);
extern "C" void Player_t1109057033_marshal_pinvoke_back(const Player_t1109057033_marshaled_pinvoke& marshaled, Player_t1109057033& unmarshaled);
extern "C" void Player_t1109057033_marshal_pinvoke_cleanup(Player_t1109057033_marshaled_pinvoke& marshaled);

// Methods for marshaling
struct Player_t1109057033;
struct Player_t1109057033_marshaled_com;

extern "C" void Player_t1109057033_marshal_com(const Player_t1109057033& unmarshaled, Player_t1109057033_marshaled_com& marshaled);
extern "C" void Player_t1109057033_marshal_com_back(const Player_t1109057033_marshaled_com& marshaled, Player_t1109057033& unmarshaled);
extern "C" void Player_t1109057033_marshal_com_cleanup(Player_t1109057033_marshaled_com& marshaled);
