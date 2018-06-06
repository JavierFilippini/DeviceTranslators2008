// IntercomTrans.h : Declaration of the CIntercomTrans

#ifndef __INTERCOMTRANS_H_
#define __INTERCOMTRANS_H_

#include "resource.h"       // main symbols
#include "DeviceTranslator\DeviceTranslator.h"

/////////////////////////////////////////////////////////////////////////////
// CIntercomTrans
class ATL_NO_VTABLE CIntercomTrans : 
	public IIntercom,
	public CDeviceTranslator<CIntercomTrans, &CLSID_IntercomTrans>
{
public:
	CIntercomTrans();
	~CIntercomTrans();

DECLARE_REGISTRY_RESOURCEID(IDR_INTERCOMTRANS)

DECLARE_PROTECT_FINAL_CONSTRUCT()

BEGIN_COM_MAP(CIntercomTrans)
	COM_INTERFACE_ENTRY(ITranslate)
	COM_INTERFACE_ENTRY(IComConfig)
	COM_INTERFACE_ENTRY(IIntercom)
	COM_INTERFACE_ENTRY(IConnectionPointContainer)
END_COM_MAP()
BEGIN_CONNECTION_POINT_MAP(CIntercomTrans)
	CONNECTION_POINT_ENTRY(IID_IDistributeEvent)
END_CONNECTION_POINT_MAP()


// ITranslate Methods
public:
	STDMETHOD(Lnl_GetSystemStatus)(SYSTEM_STATUS *p_Status);
	STDMETHOD(Lnl_CloseCommunication)(void);
	STDMETHOD(Lnl_PollPanelForEvents)(void);

// IIntercom Methods
public:
	STDMETHOD(Lnl_CancelIntercomCall)(/*[in]*/ long StationNum);
	STDMETHOD(Lnl_PlaceIntercomCall)(/*[in]*/ long Station1,/*[in]*/ long Station2,/*[in]*/ long Priority);

public:
	virtual BOOL InitCommunication(void);

};

#endif //__INTERCOMTRANS_H_
