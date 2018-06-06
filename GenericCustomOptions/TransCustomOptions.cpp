#include "stdafx.h"
#include "TransCustomOptions.h"
#include "GenericCustomOptions_i.h"
#include <fstream>
#include <iostream>
#include <string>
#include <comdef.h>
using namespace std;




/////////////////////////////////////////////////////////////////////////////
// COAAPVideoTestTrans
CTransCustomOptions::CTransCustomOptions()
{
	liveTracking = _T("Live Tracking");
	eventInfo = _T("Event Information");
	defineZone =_T("Define Virtual Zone");
	deviceProperties = _T("Device properties");
	extendedFeatures = _T("Extended Features");

	const TCHAR* pszName = _T("Se llamó a: Lnl_GetContextMenu");
	miManTraker= new CManagedTracker(pszName);
	ptrMenuDescriptor = NULL;

}

CTransCustomOptions::~CTransCustomOptions()
{
}




STDMETHODIMP CTransCustomOptions::Lnl_GetContextMenu(BSTR* menuItems)
{
	//*menuItems = _T("Test_A1;Test_B1;Test_C1;Test_D1;Test_E1");

	if (ptrMenuDescriptor!=NULL)
		::SysFreeString(ptrMenuDescriptor);

	CString menuDescriptor = _T("");

	//menuDescriptor+= liveTracking + _T(";")+eventInfo+_T(";")+deviceProperties;

	//menuDescriptor+= deviceProperties;
	menuDescriptor+= liveTracking;
	menuDescriptor+= _T(";")+defineZone;

	menuDescriptor+= _T(";") + extendedFeatures;

	if (!menuDescriptor.IsEmpty())
		ptrMenuDescriptor = ::SysAllocString(menuDescriptor);

	//*menuItems =::SysAllocString(menuDescriptor);
	*menuItems = ptrMenuDescriptor;

	return S_OK;
}

STDMETHODIMP CTransCustomOptions::Lnl_SetAlarmInfo(BSTR alarmDescription, BSTR optionText, LNLMESSAGE lnlMessage)
{
	
	DWORD panelID =  lnlMessage.ss_AccessPanelID;
	DWORD serialNum = lnlMessage.sl_SerialNumber;
	
	if(eventInfo.Compare(optionText)==0)		// Opcion Event Information
	{
		const TCHAR* pszName = _bstr_t(alarmDescription);

		(*miManTraker).show_Event((unsigned int)serialNum, (unsigned int)panelID);
	}


	if(extendedFeatures.Compare(optionText)==0)		// Opcion Extended Features
	{
		(*miManTraker).extended_Features((unsigned int)panelID);
	}

	return S_OK;
}

STDMETHODIMP CTransCustomOptions::Lnl_SetDeviceInfo(BSTR optionText, long AccessPanelID, long DeviceID, long InputDevID, BOOL status)
{

	if(liveTracking.Compare(optionText)==0)		// Opcion Live Tracking
	{
		(*miManTraker).live_Tracking((unsigned int)AccessPanelID);
	}

	if(defineZone.Compare(optionText)==0)		// Opcion Define Virtual Zone
	{
		(*miManTraker).define_Zone((unsigned int)AccessPanelID);
	}


	if(deviceProperties.Compare(optionText)==0)		// Opcion device_Properties
	{
		(*miManTraker).device_Properties((unsigned int)AccessPanelID);
	}

	if(extendedFeatures.Compare(optionText)==0)		// Opcion Extended Features
	{
		(*miManTraker).extended_Features((unsigned int)AccessPanelID);
	}


	return S_OK;
}