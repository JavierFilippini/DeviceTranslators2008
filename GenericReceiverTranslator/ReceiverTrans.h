// ReceiverTrans.h : Declaration of the CReceiverTrans

#ifndef __RECEIVERTRANS_H_
#define __RECEIVERTRANS_H_

#include "resource.h"       // main symbols
#include "ReceiverCmn/ReceiverTemplate.h"

/////////////////////////////////////////////////////////////////////////////
// CReceiverTrans
class ATL_NO_VTABLE CReceiverTrans : 
	public CReceiverTemplate<CReceiverTrans, &CLSID_ReceiverTrans>
{
public:
	CReceiverTrans();
	~CReceiverTrans();

DECLARE_REGISTRY_RESOURCEID(IDR_RECEIVERTRANS)

DECLARE_PROTECT_FINAL_CONSTRUCT()

BEGIN_COM_MAP(CReceiverTrans)
	COM_INTERFACE_ENTRY(ITranslate)
	COM_INTERFACE_ENTRY(IComConfig)
	COM_INTERFACE_ENTRY(IConnectionPointContainer)
END_COM_MAP()
BEGIN_CONNECTION_POINT_MAP(CReceiverTrans)
	CONNECTION_POINT_ENTRY(IID_IDistributeEvent)
END_CONNECTION_POINT_MAP()


// ITranslate
public:
};

#endif //__RECEIVERTRANS_H_
