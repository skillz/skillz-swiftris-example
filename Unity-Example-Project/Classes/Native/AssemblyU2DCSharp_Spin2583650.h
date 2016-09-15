#pragma once

#include "il2cpp-config.h"

#ifndef _MSC_VER
# include <alloca.h>
#else
# include <malloc.h>
#endif

#include <stdint.h>

// UnityEngine.Transform
struct Transform_t1659122786;

#include "UnityEngine_UnityEngine_MonoBehaviour667441552.h"
#include "UnityEngine_UnityEngine_Vector34282066566.h"

#ifdef __clang__
#pragma clang diagnostic push
#pragma clang diagnostic ignored "-Winvalid-offsetof"
#pragma clang diagnostic ignored "-Wunused-variable"
#endif

// Spin
struct  Spin_t2583650  : public MonoBehaviour_t667441552
{
public:
	// UnityEngine.Vector3 Spin::Axis
	Vector3_t4282066566  ___Axis_2;
	// System.Single Spin::AnglePerSecond
	float ___AnglePerSecond_3;
	// UnityEngine.Transform Spin::tr
	Transform_t1659122786 * ___tr_4;

public:
	inline static int32_t get_offset_of_Axis_2() { return static_cast<int32_t>(offsetof(Spin_t2583650, ___Axis_2)); }
	inline Vector3_t4282066566  get_Axis_2() const { return ___Axis_2; }
	inline Vector3_t4282066566 * get_address_of_Axis_2() { return &___Axis_2; }
	inline void set_Axis_2(Vector3_t4282066566  value)
	{
		___Axis_2 = value;
	}

	inline static int32_t get_offset_of_AnglePerSecond_3() { return static_cast<int32_t>(offsetof(Spin_t2583650, ___AnglePerSecond_3)); }
	inline float get_AnglePerSecond_3() const { return ___AnglePerSecond_3; }
	inline float* get_address_of_AnglePerSecond_3() { return &___AnglePerSecond_3; }
	inline void set_AnglePerSecond_3(float value)
	{
		___AnglePerSecond_3 = value;
	}

	inline static int32_t get_offset_of_tr_4() { return static_cast<int32_t>(offsetof(Spin_t2583650, ___tr_4)); }
	inline Transform_t1659122786 * get_tr_4() const { return ___tr_4; }
	inline Transform_t1659122786 ** get_address_of_tr_4() { return &___tr_4; }
	inline void set_tr_4(Transform_t1659122786 * value)
	{
		___tr_4 = value;
		Il2CppCodeGenWriteBarrier(&___tr_4, value);
	}
};

#ifdef __clang__
#pragma clang diagnostic pop
#endif
