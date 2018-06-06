// TransPOS.h : Declaration of the CTransPOS

#ifndef __TRANSPOS_H_
#define __TRANSPOS_H_

#include "resource.h"       // main symbols
#include "DeviceTranslator\DeviceTranslator.h"

/////////////////////////////////////////////////////////////////////////////
// CTransPOS
class ATL_NO_VTABLE CTransPOS : 
	public CDeviceTranslator<CTransPOS, &CLSID_TransPOS>
{
public:
	CTransPOS();
	~CTransPOS();

DECLARE_REGISTRY_RESOURCEID(IDR_TRANSPOS)

DECLARE_PROTECT_FINAL_CONSTRUCT()

BEGIN_COM_MAP(CTransPOS)
	COM_INTERFACE_ENTRY(ITranslate)
	COM_INTERFACE_ENTRY(IComConfig)
	COM_INTERFACE_ENTRY(IConnectionPointContainer)
END_COM_MAP()
BEGIN_CONNECTION_POINT_MAP(CTransPOS)
	CONNECTION_POINT_ENTRY(IID_IDistributeEvent)
END_CONNECTION_POINT_MAP()


// ITranslate
public:
};

#endif //__TRANSPOS_H_
