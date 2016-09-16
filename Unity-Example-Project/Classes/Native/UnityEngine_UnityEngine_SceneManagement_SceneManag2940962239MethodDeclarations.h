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
// UnityEngine.AsyncOperation
struct AsyncOperation_t3699081103;

#include "codegen/il2cpp-codegen.h"
#include "mscorlib_System_String7231557.h"
#include "UnityEngine_UnityEngine_SceneManagement_LoadSceneM3067001883.h"
#include "UnityEngine_UnityEngine_SceneManagement_Scene1080795294.h"

// System.Void UnityEngine.SceneManagement.SceneManager::LoadScene(System.String,UnityEngine.SceneManagement.LoadSceneMode)
extern "C"  void SceneManager_LoadScene_m3907168970 (Il2CppObject * __this /* static, unused */, String_t* ___sceneName0, int32_t ___mode1, const MethodInfo* method) IL2CPP_METHOD_ATTR;
// UnityEngine.AsyncOperation UnityEngine.SceneManagement.SceneManager::LoadSceneAsyncNameIndexInternal(System.String,System.Int32,System.Boolean,System.Boolean)
extern "C"  AsyncOperation_t3699081103 * SceneManager_LoadSceneAsyncNameIndexInternal_m3775081569 (Il2CppObject * __this /* static, unused */, String_t* ___sceneName0, int32_t ___sceneBuildIndex1, bool ___isAdditive2, bool ___mustCompleteNextFrame3, const MethodInfo* method) IL2CPP_METHOD_ATTR;
// System.Void UnityEngine.SceneManagement.SceneManager::Internal_SceneLoaded(UnityEngine.SceneManagement.Scene,UnityEngine.SceneManagement.LoadSceneMode)
extern "C"  void SceneManager_Internal_SceneLoaded_m1398790415 (Il2CppObject * __this /* static, unused */, Scene_t1080795294  ___scene0, int32_t ___mode1, const MethodInfo* method) IL2CPP_METHOD_ATTR;
// System.Void UnityEngine.SceneManagement.SceneManager::Internal_SceneUnloaded(UnityEngine.SceneManagement.Scene)
extern "C"  void SceneManager_Internal_SceneUnloaded_m3773648285 (Il2CppObject * __this /* static, unused */, Scene_t1080795294  ___scene0, const MethodInfo* method) IL2CPP_METHOD_ATTR;
// System.Void UnityEngine.SceneManagement.SceneManager::Internal_ActiveSceneChanged(UnityEngine.SceneManagement.Scene,UnityEngine.SceneManagement.Scene)
extern "C"  void SceneManager_Internal_ActiveSceneChanged_m3583151927 (Il2CppObject * __this /* static, unused */, Scene_t1080795294  ___previousActiveScene0, Scene_t1080795294  ___newActiveScene1, const MethodInfo* method) IL2CPP_METHOD_ATTR;
