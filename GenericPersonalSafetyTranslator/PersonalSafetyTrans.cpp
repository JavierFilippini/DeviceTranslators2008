// PersonalSafetyTrans.cpp : Implementation of CPersonalSafetyTrans
#include "stdafx.h"
#include "GenericPersonalSafetyTranslator.h"
#include "PersonalSafetyTrans.h"

#include "Interfaces\ITransport_i.c"

/////////////////////////////////////////////////////////////////////////////
// CPersonalSafetyTrans

/*
 * Constructor
 *
 */
CPersonalSafetyTrans::CPersonalSafetyTrans()
{
}

/*
 * Destructor
 *
 */
CPersonalSafetyTrans::~CPersonalSafetyTrans()
{
	int intPCount = 0;

	// Release the interface pointers if needed
	if (pTransport)
	{
		intPCount = pTransport->Release();
		pTransport = NULL;
	}
}

//////////////////////////////////////////////////////////////////////////////
// IPersonalSafety interface methods
//////////////////////////////////////////////////////////////////////////////

STDMETHODIMP CPersonalSafetyTrans::Lnl_ModifyBusDevice(int device,BYTE type,BOOL addDevice)
{
	// TO DO: May need to implement this method
	return E_NOTIMPL;
}

STDMETHODIMP CPersonalSafetyTrans::Lnl_SetTransmitter(TRANSMITTER_DEF *pTransmitterDef)
{
	// TO DO: May need to implement this method
	return E_NOTIMPL;
}

STDMETHODIMP CPersonalSafetyTrans::Lnl_ConfigureBusDevice(BUS_DEVICE_CFG *pBusDeviceCfg)
{
	// TO DO: May need to implement this method
	return E_NOTIMPL;
}

