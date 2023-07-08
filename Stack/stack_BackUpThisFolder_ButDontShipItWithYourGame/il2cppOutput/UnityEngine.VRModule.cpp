#include "pch-cpp.hpp"

#ifndef _MSC_VER
# include <alloca.h>
#else
# include <malloc.h>
#endif


#include <limits>



struct Action_1_t6F9EB113EB3F16226AEF811A2744F4111C116C87;
struct Action_1_t3CB5D1A819C3ED3F99E9E39F890F18633253949A;
struct DelegateU5BU5D_tC5AB7E8F745616680F337909D3A8E6C722CDF771;
struct DelegateData_t9B286B493293CD2D23A5B2B5EF0E5B1324C2B77E;
struct MethodInfo_t;
struct String_t;
struct Void_t4861ACF8F4594C3437BB48B6E56783494B843915;

IL2CPP_EXTERN_C RuntimeClass* XRDevice_tD076A68EFE413B3EEEEA362BE0364A488B58F194_il2cpp_TypeInfo_var;
struct Delegate_t_marshaled_com;
struct Delegate_t_marshaled_pinvoke;


IL2CPP_EXTERN_C_BEGIN
IL2CPP_EXTERN_C_END

#ifdef __clang__
#pragma clang diagnostic push
#pragma clang diagnostic ignored "-Winvalid-offsetof"
#pragma clang diagnostic ignored "-Wunused-variable"
#endif
struct U3CModuleU3E_t0F6AB019D77D717D42BE5AD848FFBD032B14CFFC 
{
};
struct String_t  : public RuntimeObject
{
	int32_t ____stringLength;
	Il2CppChar ____firstChar;
};
struct ValueType_t6D9B272BD21782F0A9A14F2E41F85A50E97A986F  : public RuntimeObject
{
};
struct ValueType_t6D9B272BD21782F0A9A14F2E41F85A50E97A986F_marshaled_pinvoke
{
};
struct ValueType_t6D9B272BD21782F0A9A14F2E41F85A50E97A986F_marshaled_com
{
};
struct XRDevice_tD076A68EFE413B3EEEEA362BE0364A488B58F194  : public RuntimeObject
{
};
struct XRSettings_t783533FF87B79D6D0C6A47FA8EC9B17EC0820D97  : public RuntimeObject
{
};
struct Boolean_t09A6377A54BE2F9E6985A8149F19234FD7DDFE22 
{
	bool ___m_value;
};
struct Int32_t680FF22E76F6EFAD4375103CBBFFA0421349384C 
{
	int32_t ___m_value;
};
struct IntPtr_t 
{
	void* ___m_value;
};
struct RenderTextureDescriptor_t69845881CE6437E4E61F92074F2F84079F23FA46 
{
	int32_t ___U3CwidthU3Ek__BackingField;
	int32_t ___U3CheightU3Ek__BackingField;
	int32_t ___U3CmsaaSamplesU3Ek__BackingField;
	int32_t ___U3CvolumeDepthU3Ek__BackingField;
	int32_t ___U3CmipCountU3Ek__BackingField;
	int32_t ____graphicsFormat;
	int32_t ___U3CstencilFormatU3Ek__BackingField;
	int32_t ___U3CdepthStencilFormatU3Ek__BackingField;
	int32_t ___U3CdimensionU3Ek__BackingField;
	int32_t ___U3CshadowSamplingModeU3Ek__BackingField;
	int32_t ___U3CvrUsageU3Ek__BackingField;
	int32_t ____flags;
	int32_t ___U3CmemorylessU3Ek__BackingField;
};
struct Single_t4530F2FF86FCB0DC29F35385CA1BD21BE294761C 
{
	float ___m_value;
};
struct Void_t4861ACF8F4594C3437BB48B6E56783494B843915 
{
	union
	{
		struct
		{
		};
		uint8_t Void_t4861ACF8F4594C3437BB48B6E56783494B843915__padding[1];
	};
};
struct Delegate_t  : public RuntimeObject
{
	intptr_t ___method_ptr;
	intptr_t ___invoke_impl;
	RuntimeObject* ___m_target;
	intptr_t ___method;
	intptr_t ___delegate_trampoline;
	intptr_t ___extra_arg;
	intptr_t ___method_code;
	intptr_t ___interp_method;
	intptr_t ___interp_invoke_impl;
	MethodInfo_t* ___method_info;
	MethodInfo_t* ___original_method_info;
	DelegateData_t9B286B493293CD2D23A5B2B5EF0E5B1324C2B77E* ___data;
	bool ___method_is_virtual;
};
struct Delegate_t_marshaled_pinvoke
{
	intptr_t ___method_ptr;
	intptr_t ___invoke_impl;
	Il2CppIUnknown* ___m_target;
	intptr_t ___method;
	intptr_t ___delegate_trampoline;
	intptr_t ___extra_arg;
	intptr_t ___method_code;
	intptr_t ___interp_method;
	intptr_t ___interp_invoke_impl;
	MethodInfo_t* ___method_info;
	MethodInfo_t* ___original_method_info;
	DelegateData_t9B286B493293CD2D23A5B2B5EF0E5B1324C2B77E* ___data;
	int32_t ___method_is_virtual;
};
struct Delegate_t_marshaled_com
{
	intptr_t ___method_ptr;
	intptr_t ___invoke_impl;
	Il2CppIUnknown* ___m_target;
	intptr_t ___method;
	intptr_t ___delegate_trampoline;
	intptr_t ___extra_arg;
	intptr_t ___method_code;
	intptr_t ___interp_method;
	intptr_t ___interp_invoke_impl;
	MethodInfo_t* ___method_info;
	MethodInfo_t* ___original_method_info;
	DelegateData_t9B286B493293CD2D23A5B2B5EF0E5B1324C2B77E* ___data;
	int32_t ___method_is_virtual;
};
struct MulticastDelegate_t  : public Delegate_t
{
	DelegateU5BU5D_tC5AB7E8F745616680F337909D3A8E6C722CDF771* ___delegates;
};
struct MulticastDelegate_t_marshaled_pinvoke : public Delegate_t_marshaled_pinvoke
{
	Delegate_t_marshaled_pinvoke** ___delegates;
};
struct MulticastDelegate_t_marshaled_com : public Delegate_t_marshaled_com
{
	Delegate_t_marshaled_com** ___delegates;
};
struct Action_1_t6F9EB113EB3F16226AEF811A2744F4111C116C87  : public MulticastDelegate_t
{
};
struct Action_1_t3CB5D1A819C3ED3F99E9E39F890F18633253949A  : public MulticastDelegate_t
{
};
struct String_t_StaticFields
{
	String_t* ___Empty;
};
struct XRDevice_tD076A68EFE413B3EEEEA362BE0364A488B58F194_StaticFields
{
	Action_1_t3CB5D1A819C3ED3F99E9E39F890F18633253949A* ___deviceLoaded;
};
struct Boolean_t09A6377A54BE2F9E6985A8149F19234FD7DDFE22_StaticFields
{
	String_t* ___TrueString;
	String_t* ___FalseString;
};
#ifdef __clang__
#pragma clang diagnostic pop
#endif


IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR void Action_1_Invoke_mF2422B2DD29F74CE66F791C3F68E288EC7C3DB9E_gshared_inline (Action_1_t6F9EB113EB3F16226AEF811A2744F4111C116C87* __this, RuntimeObject* ___0_obj, const RuntimeMethod* method) ;

IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void XRSettings_get_eyeTextureDesc_Injected_m2B01F9A50CE1E88530044A5D342C1AE151BA17B5 (RenderTextureDescriptor_t69845881CE6437E4E61F92074F2F84079F23FA46* ___0_ret, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR float XRSettings_get_renderViewportScaleInternal_mC9FFB83588F0865E76B78FB334AE6AAF0FF2EC24 (const RuntimeMethod* method) ;
inline void Action_1_Invoke_m690438AAE38F9762172E3AE0A33D0B42ACD35790_inline (Action_1_t3CB5D1A819C3ED3F99E9E39F890F18633253949A* __this, String_t* ___0_obj, const RuntimeMethod* method)
{
	((  void (*) (Action_1_t3CB5D1A819C3ED3F99E9E39F890F18633253949A*, String_t*, const RuntimeMethod*))Action_1_Invoke_mF2422B2DD29F74CE66F791C3F68E288EC7C3DB9E_gshared_inline)(__this, ___0_obj, method);
}
#ifdef __clang__
#pragma clang diagnostic push
#pragma clang diagnostic ignored "-Winvalid-offsetof"
#pragma clang diagnostic ignored "-Wunused-variable"
#endif
#ifdef __clang__
#pragma clang diagnostic pop
#endif
#ifdef __clang__
#pragma clang diagnostic push
#pragma clang diagnostic ignored "-Winvalid-offsetof"
#pragma clang diagnostic ignored "-Wunused-variable"
#endif
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR int32_t XRSettings_get_eyeTextureWidth_m3B18AF3F3382398E2A818B2B01AA1FE90FEB3AAF (const RuntimeMethod* method) 
{
	typedef int32_t (*XRSettings_get_eyeTextureWidth_m3B18AF3F3382398E2A818B2B01AA1FE90FEB3AAF_ftn) ();
	static XRSettings_get_eyeTextureWidth_m3B18AF3F3382398E2A818B2B01AA1FE90FEB3AAF_ftn _il2cpp_icall_func;
	if (!_il2cpp_icall_func)
	_il2cpp_icall_func = (XRSettings_get_eyeTextureWidth_m3B18AF3F3382398E2A818B2B01AA1FE90FEB3AAF_ftn)il2cpp_codegen_resolve_icall ("UnityEngine.XR.XRSettings::get_eyeTextureWidth()");
	int32_t icallRetVal = _il2cpp_icall_func();
	return icallRetVal;
}
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR int32_t XRSettings_get_eyeTextureHeight_mCF4B2EC6851A8B8A8C4E6FC085A621B3166DB67A (const RuntimeMethod* method) 
{
	typedef int32_t (*XRSettings_get_eyeTextureHeight_mCF4B2EC6851A8B8A8C4E6FC085A621B3166DB67A_ftn) ();
	static XRSettings_get_eyeTextureHeight_mCF4B2EC6851A8B8A8C4E6FC085A621B3166DB67A_ftn _il2cpp_icall_func;
	if (!_il2cpp_icall_func)
	_il2cpp_icall_func = (XRSettings_get_eyeTextureHeight_mCF4B2EC6851A8B8A8C4E6FC085A621B3166DB67A_ftn)il2cpp_codegen_resolve_icall ("UnityEngine.XR.XRSettings::get_eyeTextureHeight()");
	int32_t icallRetVal = _il2cpp_icall_func();
	return icallRetVal;
}
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR RenderTextureDescriptor_t69845881CE6437E4E61F92074F2F84079F23FA46 XRSettings_get_eyeTextureDesc_mFBE8F6D5D5A23E4DE1BCCD994ADFAB4FB11D7A19 (const RuntimeMethod* method) 
{
	RenderTextureDescriptor_t69845881CE6437E4E61F92074F2F84079F23FA46 V_0;
	memset((&V_0), 0, sizeof(V_0));
	{
		XRSettings_get_eyeTextureDesc_Injected_m2B01F9A50CE1E88530044A5D342C1AE151BA17B5((&V_0), NULL);
		RenderTextureDescriptor_t69845881CE6437E4E61F92074F2F84079F23FA46 L_0 = V_0;
		return L_0;
	}
}
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR float XRSettings_get_renderViewportScale_mB35A32F5FE6B2EEE0CEF95ADFC04F171B6E5F5D1 (const RuntimeMethod* method) 
{
	float V_0 = 0.0f;
	{
		float L_0;
		L_0 = XRSettings_get_renderViewportScaleInternal_mC9FFB83588F0865E76B78FB334AE6AAF0FF2EC24(NULL);
		V_0 = L_0;
		goto IL_0009;
	}

IL_0009:
	{
		float L_1 = V_0;
		return L_1;
	}
}
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR float XRSettings_get_renderViewportScaleInternal_mC9FFB83588F0865E76B78FB334AE6AAF0FF2EC24 (const RuntimeMethod* method) 
{
	typedef float (*XRSettings_get_renderViewportScaleInternal_mC9FFB83588F0865E76B78FB334AE6AAF0FF2EC24_ftn) ();
	static XRSettings_get_renderViewportScaleInternal_mC9FFB83588F0865E76B78FB334AE6AAF0FF2EC24_ftn _il2cpp_icall_func;
	if (!_il2cpp_icall_func)
	_il2cpp_icall_func = (XRSettings_get_renderViewportScaleInternal_mC9FFB83588F0865E76B78FB334AE6AAF0FF2EC24_ftn)il2cpp_codegen_resolve_icall ("UnityEngine.XR.XRSettings::get_renderViewportScaleInternal()");
	float icallRetVal = _il2cpp_icall_func();
	return icallRetVal;
}
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR int32_t XRSettings_get_stereoRenderingMode_mD66918C11E2216B1F8FA76934F79D5F85BC303FC (const RuntimeMethod* method) 
{
	typedef int32_t (*XRSettings_get_stereoRenderingMode_mD66918C11E2216B1F8FA76934F79D5F85BC303FC_ftn) ();
	static XRSettings_get_stereoRenderingMode_mD66918C11E2216B1F8FA76934F79D5F85BC303FC_ftn _il2cpp_icall_func;
	if (!_il2cpp_icall_func)
	_il2cpp_icall_func = (XRSettings_get_stereoRenderingMode_mD66918C11E2216B1F8FA76934F79D5F85BC303FC_ftn)il2cpp_codegen_resolve_icall ("UnityEngine.XR.XRSettings::get_stereoRenderingMode()");
	int32_t icallRetVal = _il2cpp_icall_func();
	return icallRetVal;
}
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void XRSettings_get_eyeTextureDesc_Injected_m2B01F9A50CE1E88530044A5D342C1AE151BA17B5 (RenderTextureDescriptor_t69845881CE6437E4E61F92074F2F84079F23FA46* ___0_ret, const RuntimeMethod* method) 
{
	typedef void (*XRSettings_get_eyeTextureDesc_Injected_m2B01F9A50CE1E88530044A5D342C1AE151BA17B5_ftn) (RenderTextureDescriptor_t69845881CE6437E4E61F92074F2F84079F23FA46*);
	static XRSettings_get_eyeTextureDesc_Injected_m2B01F9A50CE1E88530044A5D342C1AE151BA17B5_ftn _il2cpp_icall_func;
	if (!_il2cpp_icall_func)
	_il2cpp_icall_func = (XRSettings_get_eyeTextureDesc_Injected_m2B01F9A50CE1E88530044A5D342C1AE151BA17B5_ftn)il2cpp_codegen_resolve_icall ("UnityEngine.XR.XRSettings::get_eyeTextureDesc_Injected(UnityEngine.RenderTextureDescriptor&)");
	_il2cpp_icall_func(___0_ret);
}
#ifdef __clang__
#pragma clang diagnostic pop
#endif
#ifdef __clang__
#pragma clang diagnostic push
#pragma clang diagnostic ignored "-Winvalid-offsetof"
#pragma clang diagnostic ignored "-Wunused-variable"
#endif
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void XRDevice_InvokeDeviceLoaded_mBE2198DE44A72E2F5059566C46B9907D82782790 (String_t* ___0_loadedDeviceName, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&XRDevice_tD076A68EFE413B3EEEEA362BE0364A488B58F194_il2cpp_TypeInfo_var);
		s_Il2CppMethodInitialized = true;
	}
	bool V_0 = false;
	{
		Action_1_t3CB5D1A819C3ED3F99E9E39F890F18633253949A* L_0 = ((XRDevice_tD076A68EFE413B3EEEEA362BE0364A488B58F194_StaticFields*)il2cpp_codegen_static_fields_for(XRDevice_tD076A68EFE413B3EEEEA362BE0364A488B58F194_il2cpp_TypeInfo_var))->___deviceLoaded;
		V_0 = (bool)((!(((RuntimeObject*)(Action_1_t3CB5D1A819C3ED3F99E9E39F890F18633253949A*)L_0) <= ((RuntimeObject*)(RuntimeObject*)NULL)))? 1 : 0);
		bool L_1 = V_0;
		if (!L_1)
		{
			goto IL_001b;
		}
	}
	{
		Action_1_t3CB5D1A819C3ED3F99E9E39F890F18633253949A* L_2 = ((XRDevice_tD076A68EFE413B3EEEEA362BE0364A488B58F194_StaticFields*)il2cpp_codegen_static_fields_for(XRDevice_tD076A68EFE413B3EEEEA362BE0364A488B58F194_il2cpp_TypeInfo_var))->___deviceLoaded;
		String_t* L_3 = ___0_loadedDeviceName;
		NullCheck(L_2);
		Action_1_Invoke_m690438AAE38F9762172E3AE0A33D0B42ACD35790_inline(L_2, L_3, NULL);
	}

IL_001b:
	{
		return;
	}
}
#ifdef __clang__
#pragma clang diagnostic pop
#endif
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR void Action_1_Invoke_mF2422B2DD29F74CE66F791C3F68E288EC7C3DB9E_gshared_inline (Action_1_t6F9EB113EB3F16226AEF811A2744F4111C116C87* __this, RuntimeObject* ___0_obj, const RuntimeMethod* method) 
{
	typedef void (*FunctionPointerType) (RuntimeObject*, RuntimeObject*, const RuntimeMethod*);
	((FunctionPointerType)__this->___invoke_impl)((Il2CppObject*)__this->___method_code, ___0_obj, reinterpret_cast<RuntimeMethod*>(__this->___method));
}
