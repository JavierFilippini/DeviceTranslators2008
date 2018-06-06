// TransCustomOption.h : Declaration of the CTransCustomOption

#ifndef __TRANSCUSTOMOPTIONS_H_
#define __TRANSCUSTOMOPTIONS_H_

#pragma once
#include "stdafx.h"
#include "resource.h"       // main symbols
#include "Interfaces\ICustomMenu.h"
#include "GenericCustomOptions_i.h"
#include "util32\datetime.h"

#include "MixedManagedHandHeldTracker.h"


class ATL_NO_VTABLE CTransCustomOptions:
	public CComObjectRootEx<CComMultiThreadModel>,
	public CComCoClass<CTransCustomOptions, &CLSID_LnlTransCustomOptions>,
	public ICustomMenu
{

	public:
		CTransCustomOptions();
		~CTransCustomOptions();

	DECLARE_REGISTRY_RESOURCEID(IDR_TRANSCUSTOMOPTIONS)

	BEGIN_COM_MAP(CTransCustomOptions)
		COM_INTERFACE_ENTRY(ICustomMenu)
	END_COM_MAP()

	DECLARE_PROTECT_FINAL_CONSTRUCT()

	public:
	STDMETHOD(Lnl_GetContextMenu)(BSTR* menuItems);
	STDMETHOD(Lnl_SetAlarmInfo)(BSTR alarmDescription, BSTR optionText, LNLMESSAGE lnlMessage );
	STDMETHOD(Lnl_SetDeviceInfo)(BSTR optionText, long AccessPanelID, long DeviceID, long InputDevID, BOOL status);


	CLnlTimeConversion g_oTimeConverter;
	CManagedTracker* miManTraker;

	//Textos del Menu Custom
	CString liveTracking;
	CString eventInfo;
	CString defineZone;
	CString defineVirtualGate;
	CString deviceProperties;
	CString extendedFeatures;
	CString menuDescriptor;
	BSTR ptrMenuDescriptor;

};

OBJECT_ENTRY_AUTO(__uuidof(LnlTransCustomOptions), CTransCustomOptions)
#endif