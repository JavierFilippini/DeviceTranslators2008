// BurglaryTrans.cpp : Implementation of CBurglaryTrans
#include "stdafx.h"
#include <stdlib.h>
#include "GenericBurglaryTranslator.h"
#include "BurglaryTrans.h"
#include "Interfaces\ITransport_i.c"

/////////////////////////////////////////////////////////////////////////////
// CBurglaryTrans

/*
 * Constructor
 *
 */
CBurglaryTrans::CBurglaryTrans()
{
}

/*
 * Destructor
 *
 */
CBurglaryTrans::~CBurglaryTrans()
{
	int intPCount = 0;

	// Release the interface pointers if needed
	if (pTransport)
	{
		intPCount = pTransport->Release();
		pTransport = NULL;
	}
}

STDMETHODIMP CBurglaryTrans::Lnl_ExecuteFunction(long keypadNumber, long functionNumber)
{
	return (E_NOTIMPL);
}

STDMETHODIMP CBurglaryTrans::Lnl_SetZoneBypassMode(long zoneID, short bypassMode)
{
	return (E_NOTIMPL);
}
STDMETHODIMP CBurglaryTrans::Lnl_SetZoneOutputMode(long zoneID, short outputMode)
{
	return (E_NOTIMPL);
}

STDMETHODIMP CBurglaryTrans::Lnl_SetAreaArmState(long areaID, short armingState)
{
	return (E_NOTIMPL);
}
STDMETHODIMP CBurglaryTrans::Lnl_SilenceAreaAlarms(long areaID)
{
	return (E_NOTIMPL);
}


STDMETHODIMP CBurglaryTrans::Lnl_SetOnboardRelayMode(long relayID, short relayMode)
{
	return (E_NOTIMPL);
}

STDMETHODIMP CBurglaryTrans::Lnl_SetOffboardRelayMode(long relayID, short relayMode)
{
	return (E_NOTIMPL);
}

STDMETHODIMP CBurglaryTrans::Lnl_OpenDoor(long doorID)
{
	return (E_NOTIMPL);
}
STDMETHODIMP CBurglaryTrans::Lnl_SetDoorMode(long doorID, short doorMode)
{
	return E_NOTIMPL;
}

STDMETHODIMP CBurglaryTrans::Lnl_GetIntrusionPanelStatus(
			BYTE *sb_ComStatus,
			BYTE *sb_AsyncStatus,
			long *vl_PanelStatus
			)
			{
				//send panel status event
				LNLMESSAGE ls_Event;
				memset( &ls_Event, '\0', sizeof(ls_Event));
				ls_Event.sl_Size = sizeof(LNLMESSAGE);
				ls_Event.sl_SerialNumber = GetPanelEventSerialNumber();
				ls_Event.sl_Time = (DWORD)g_oTimeConverter.GetCurrentGmtTime();
				ls_Event.ss_AccessPanelID = m_PanelID;
				ls_Event.sb_InputDevID = 0;
				ls_Event.sb_MessageType = LNLMSG_TYPE_STATUS;
				ls_Event.sb_EventType = L_EVENTTYPE_SYSTEM;
				ls_Event.sb_EventDataType = EVENT_DATA_TYPE_STATUSREQUEST;
				ls_Event.su_EventData.us_StatusRequest.sl_StatusType = DATA_SRQ_COMM_STATE;
				ls_Event.su_EventData.us_StatusRequest.sl_Status = (long)(GetPanelState() == PANEL_STATE_READY);
				WriteEventsToClients ( &ls_Event);

				if (GetPanelState() == PANEL_STATE_READY)
				{
					//send firmware version
					ZeroMemory( &ls_Event, sizeof(ls_Event));
					ls_Event.sl_Size = sizeof(LNLMESSAGE);
					ls_Event.sl_SerialNumber = GetPanelEventSerialNumber();
					ls_Event.sl_Time = g_oTimeConverter.GetCurrentGmtTime();
					ls_Event.ss_AccessPanelID = m_PanelID;
					ls_Event.sb_InputDevID = 0;
					ls_Event.sb_MessageType = LNLMSG_TYPE_STATUS;
					ls_Event.sb_EventType = L_EVENTTYPE_SYSTEM;
					ls_Event.sb_EventDataType = EVENT_DATA_TYPE_STATUSREQUEST;
					ls_Event.su_EventData.us_StatusRequest.sl_StatusType = DATA_SRQ_FIRMWARE_REV_STRING;
					sprintf( (char *)&ls_Event.su_EventData.us_StatusRequest.sc_String[0] , "%d.%d%d",m_FirmwareHigh, m_FirmwareMiddle, m_FirmwareLow);
					WriteEventsToClients ( &ls_Event );
				}

				return (S_OK);
			}

STDMETHODIMP CBurglaryTrans::Lnl_GetIntrusionZoneStatus(
			BYTE *sb_AsyncStatus,
			long vl_NumZones,
			long vl_ZoneAlarmStatus[],
			long vl_ZoneOtherStatus[]
			)
			{
				return (E_NOTIMPL);
			}

STDMETHODIMP CBurglaryTrans::Lnl_GetIntrusionOnboardRelayStatus(
			BYTE *sb_AsyncStatus,
			long vl_NumOnBoardRelays,
			BYTE vl_OnBoardRelayStatus[]
			)
			{
				return (E_NOTIMPL);
			}

STDMETHODIMP CBurglaryTrans::Lnl_GetIntrusionOffboardRelayStatus(
			BYTE *sb_AsyncStatus,
			long vl_NumOffBoardRelays,
			BYTE vl_OffBoardRelayStatus[]
			)
			{
				return (E_NOTIMPL);
			}

STDMETHODIMP CBurglaryTrans::Lnl_GetIntrusionDoorStatus(
			BYTE *sb_AsyncStatus,
			long vl_NumDoors,
			long vl_DoorMode[],
			long vl_DoorOtherStatus[]
			)
			{
				return (E_NOTIMPL);
			}

STDMETHODIMP CBurglaryTrans::Lnl_GetIntrusionAreaStatus(
			BYTE *sb_AsyncStatus,
			long vl_NumAreas,
			long vl_AreaArmingStatus[],
			long vl_AreaAlarmStatus[],
			long vl_AreaOtherStatus[]
			)
			{
				return (E_NOTIMPL);
			}