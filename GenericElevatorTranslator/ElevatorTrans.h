// ElevatorTrans.h : Declaration of the CElevatorTrans

#ifndef __ELEVATORTRANS_H_
#define __ELEVATORTRANS_H_

#include "resource.h"       // main symbols
#include "DeviceTranslator\DeviceTranslator.h"

/////////////////////////////////////////////////////////////////////////////
// CElevatorTrans
class ATL_NO_VTABLE CElevatorTrans : 
	public IElevatorDispatching,
	public CDeviceTranslator<CElevatorTrans, &CLSID_ElevatorTrans>
{
public:
	CElevatorTrans();
	~CElevatorTrans();

DECLARE_REGISTRY_RESOURCEID(IDR_ELEVATORTRANS)

DECLARE_PROTECT_FINAL_CONSTRUCT()


BEGIN_COM_MAP(CElevatorTrans)
	COM_INTERFACE_ENTRY(ITranslate)
	COM_INTERFACE_ENTRY(IComConfig)
	COM_INTERFACE_ENTRY(IElevatorDispatching)
	COM_INTERFACE_ENTRY(IConnectionPointContainer)
END_COM_MAP()
BEGIN_CONNECTION_POINT_MAP(CElevatorTrans)
	CONNECTION_POINT_ENTRY(IID_IDistributeEvent)
END_CONNECTION_POINT_MAP()


// ITranslate
public:
	STDMETHOD(Lnl_PollPanelForEvents)(void);
	STDMETHOD(Lnl_GetSystemStatus)(SYSTEM_STATUS *p_Status);
	STDMETHOD(Lnl_CloseCommunication)(void);


// IElevatorDispatching
public:
	STDMETHOD(Lnl_SetPanelTerminals)(ELEVATOR_KEYPAD_TERMINALS *terminalsToManage);
	STDMETHOD(Lnl_SetTerminalOperationalMode)(long terminalID, short operationalMode);
	STDMETHOD(Lnl_SetTerminalAllowedFloors)(long terminalID, ELEVATOR_FLOORLIST *allowedFloorsCfg);
	STDMETHOD(Lnl_SendTerminalCredentialData)(long terminalID, ELV_DISPATCHING_CREDENTIAL_DATA *credentialData);
	STDMETHOD(Lnl_GetElevatorDispatchingPanelStatus)(BYTE *sb_ComStatus, BYTE *sb_AsyncStatus, long *vl_PanelStatus);
	STDMETHOD(Lnl_GetElevatorKeypadTerminalStatus)(BYTE *sb_AsyncStatus, long vl_NumTerminals, BOOL sb_ComStatus[], 
		short operationalMode[], ELEVATOR_FLOORLIST allowedFloorsCfg[]);
	STDMETHOD(Lnl_UpdateTerminal)(ELEVATOR_TERMINAL_CONFIG *terminalConfiguration);

};

#endif //__ELEVATORTRANS_H_