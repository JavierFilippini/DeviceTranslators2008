// FireTrans.h : Declaration of the CFireTrans

#ifndef __FIRETRANS_H_
#define __FIRETRANS_H_

#include "resource.h"       // main symbols
#include "DeviceTranslator\DeviceTranslator.h"

/////////////////////////////////////////////////////////////////////////////
// CFireTrans
class ATL_NO_VTABLE CFireTrans : 
	public CDeviceTranslator<CFireTrans, &CLSID_FireTrans>
{
public:
	CFireTrans();
	~CFireTrans();

DECLARE_REGISTRY_RESOURCEID(IDR_FIRETRANS)

DECLARE_PROTECT_FINAL_CONSTRUCT()

BEGIN_COM_MAP(CFireTrans)
	COM_INTERFACE_ENTRY(ITranslate)
	COM_INTERFACE_ENTRY(IComConfig)
	COM_INTERFACE_ENTRY(IConnectionPointContainer)
END_COM_MAP()
BEGIN_CONNECTION_POINT_MAP(CFireTrans)
	CONNECTION_POINT_ENTRY(IID_IDistributeEvent)
END_CONNECTION_POINT_MAP()


// ITranslate
public:
};

#endif //__FIRETRANS_H_
