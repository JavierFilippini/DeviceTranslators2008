// BurglaryTrans.h : Declaration of the CBurglaryTrans

#ifndef __BURGLARYTRANS_H_
#define __BURGLARYTRANS_H_


#include "resource.h"       // main symbols
//#include "Interfaces\IIntrusion.h"
#include "DeviceTranslator\DeviceTranslator.h"

#include <afxtempl.h>
#include <afxdisp.h>

/////////////////////////////////////////////////////////////////////////////
// CBurglaryTrans
class ATL_NO_VTABLE CBurglaryTrans : 
	public IIntrusion,
	public CDeviceTranslator<CBurglaryTrans, &CLSID_BurglaryTrans>
{
public:
	CBurglaryTrans();
	~CBurglaryTrans();

DECLARE_REGISTRY_RESOURCEID(IDR_BURGLARYTRANS)

DECLARE_PROTECT_FINAL_CONSTRUCT()

BEGIN_COM_MAP(CBurglaryTrans)
	COM_INTERFACE_ENTRY(IIntrusion)
	COM_INTERFACE_ENTRY(ITranslate)
	COM_INTERFACE_ENTRY(IComConfig)
	COM_INTERFACE_ENTRY(IConnectionPointContainer)
END_COM_MAP()
BEGIN_CONNECTION_POINT_MAP(CBurglaryTrans)
	CONNECTION_POINT_ENTRY(IID_IDistributeEvent)
END_CONNECTION_POINT_MAP()


// ITranslate

//IIntrusion
public:
	STDMETHOD(Lnl_ExecuteFunction)(long keypadnumber, long functionNumber);
	STDMETHOD(Lnl_SetZoneBypassMode)(long zoneID, short bypassMode);
	STDMETHOD(Lnl_SetZoneOutputMode)(long zoneID, short outputMode);
	STDMETHOD(Lnl_SetAreaArmState)(long areaID, short armingState);
	STDMETHOD(Lnl_SilenceAreaAlarms)(long areaID);
	STDMETHOD(Lnl_SetOnboardRelayMode)(long relayID, short relayMode);
	STDMETHOD(Lnl_SetOffboardRelayMode)(long relayID, short relayMode);
	STDMETHOD(Lnl_OpenDoor)(long doorID);
	STDMETHOD(Lnl_SetDoorMode)(long doorID, short doorMode);
	STDMETHOD(Lnl_GetIntrusionPanelStatus)(BYTE *sb_ComStatus, BYTE *sb_AsyncStatus, long *vl_PanelStatus);
	STDMETHOD(Lnl_GetIntrusionZoneStatus)(BYTE *sb_AsyncStatus, long vl_NumZones, long vl_ZoneAlarmStatus[], long vl_ZoneOtherStatus[]);
	STDMETHOD(Lnl_GetIntrusionOnboardRelayStatus)(BYTE *sb_AsyncStatus, long vl_NumOnBoardRelays, BYTE vl_OnBoardRelayStatus[]);
	STDMETHOD(Lnl_GetIntrusionOffboardRelayStatus)(BYTE *sb_AsyncStatus, long vl_NumOffBoardRelays, BYTE vl_OffBoardRelayStatus[]);
	STDMETHOD(Lnl_GetIntrusionDoorStatus)(BYTE *sb_AsyncStatus, long vl_NumDoors, long vl_DoorMode[], long vl_DoorOtherStatus[]);
	STDMETHOD(Lnl_GetIntrusionAreaStatus)(BYTE *sb_AsyncStatus, long vl_NumAreas, long vl_AreaArmingStatus[], long vl_AreaAlarmStatus[], long vl_AreaOtherStatus[]);


private:
	BYTE			m_FirmwareHigh;
	BYTE			m_FirmwareMiddle;
	BYTE			m_FirmwareLow;

};

#endif //__BURGLARYTRANS_H_
