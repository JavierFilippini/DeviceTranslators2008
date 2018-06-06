// PersonalSafetyTrans.h : Declaration of the CPersonalSafetyTrans

#ifndef __PERSONALSAFETYTRANS_H_
#define __PERSONALSAFETYTRANS_H_

#include "resource.h"       // main symbols
#include "DeviceTranslator\DeviceTranslator.h"

/////////////////////////////////////////////////////////////////////////////
// CPersonalSafetyTrans
class ATL_NO_VTABLE CPersonalSafetyTrans : 
	public IPersonalSafety,
	public CDeviceTranslator<CPersonalSafetyTrans, &CLSID_PersonalSafetyTrans>
{
public:
	CPersonalSafetyTrans();
	~CPersonalSafetyTrans();

DECLARE_REGISTRY_RESOURCEID(IDR_PERSONALSAFETYTRANS)

DECLARE_PROTECT_FINAL_CONSTRUCT()

BEGIN_COM_MAP(CPersonalSafetyTrans)
	COM_INTERFACE_ENTRY(ITranslate)
	COM_INTERFACE_ENTRY(IComConfig)
	COM_INTERFACE_ENTRY(IConnectionPointContainer)
	COM_INTERFACE_ENTRY(IPersonalSafety)
END_COM_MAP()
BEGIN_CONNECTION_POINT_MAP(CPersonalSafetyTrans)
	CONNECTION_POINT_ENTRY(IID_IDistributeEvent)
END_CONNECTION_POINT_MAP()


// ITranslate Methods
public:

// IPersonalSafety
public:
	STDMETHOD(Lnl_ModifyBusDevice)(int device,BYTE type,BOOL addDevice);
	STDMETHOD(Lnl_SetTransmitter)(TRANSMITTER_DEF *pTransmitterDef);
	STDMETHOD(Lnl_ConfigureBusDevice)(BUS_DEVICE_CFG *pBusDeviceCfg);
};

#endif //__PERSONALSAFETYTRANS_H_
