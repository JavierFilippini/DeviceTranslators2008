// ElevatorTrans.cpp : Implementation of CElevatorTrans
#include "stdafx.h"
#include "GenericElevatorTranslator.h"
#include "ElevatorTrans.h"

#include "Interfaces\ITransport_i.c"



/////////////////////////////////////////////////////////////////////
//CElevatorTrans 


CElevatorTrans::CElevatorTrans()
{
}

CElevatorTrans::~CElevatorTrans()
{
}


/******************************************************************************/
/* ITranslate Methods - These are the methods that probably will need to be
/*                      overridden.
/******************************************************************************/


/*
 * Lnl_GetSystemStatus - This method is used to return the status of the
 * hardware devices.  It gets called from Alarm Monitoring so that the hardware
 * tree can properly get updated and can also be called manually from Alarm Monitoring.
 *
 */
STDMETHODIMP CElevatorTrans::Lnl_GetSystemStatus(SYSTEM_STATUS *p_Status)
{
	return S_OK;
}

/*
 * Lnl_CloseCommunication
 *
 */
STDMETHODIMP CElevatorTrans::Lnl_CloseCommunication()
{
	// TO DO: Implement this method to close the communication.
	return E_NOTIMPL;
}

/*
 * Lnl_PollPanelForEvents
 *
 */
STDMETHODIMP CElevatorTrans::Lnl_PollPanelForEvents()
{
	// TO DO: Implement this method to poll the elevator system for events.
	// The PollPanelForEvents method can be overridden to perform any unique
	// polling that might be necessary.
	if (!PollPanelForEvents())
	{
		return (E_FAIL);
	}

	return S_OK;
}





/******************************************************************************/
/* IElevatorDispatching Methods - These are the methods that need to be 
/*                                implemented for the IElevatorDispatching
/*                                Interface.
/******************************************************************************/

STDMETHODIMP CElevatorTrans::Lnl_SetPanelTerminals(ELEVATOR_KEYPAD_TERMINALS *terminalsToManage)
{
	return E_NOTIMPL;
}

STDMETHODIMP CElevatorTrans::Lnl_SetTerminalOperationalMode(long terminalID, short operationalMode)
{
	return E_NOTIMPL;
}

STDMETHODIMP CElevatorTrans::Lnl_SetTerminalAllowedFloors(long terminalID, ELEVATOR_FLOORLIST *allowedFloorsCfg)
{
	return S_OK;
}

STDMETHODIMP CElevatorTrans::Lnl_SendTerminalCredentialData(long terminalID, ELV_DISPATCHING_CREDENTIAL_DATA *credentialData)
{
	return E_NOTIMPL;
}

STDMETHODIMP CElevatorTrans::Lnl_GetElevatorDispatchingPanelStatus(BYTE *sb_ComStatus, BYTE *sb_AsyncStatus, long *vl_PanelStatus)
{
	*sb_ComStatus = 1;
	*vl_PanelStatus = 1;
	*sb_AsyncStatus = FALSE;
	return S_OK;
}

STDMETHODIMP CElevatorTrans::Lnl_GetElevatorKeypadTerminalStatus(BYTE *sb_AsyncStatus, long vl_NumTerminals, BOOL sb_ComStatus[], 
	short operationalMode[], ELEVATOR_FLOORLIST allowedFloorsCfg[])
{
	*sb_AsyncStatus = FALSE;

	int num_terminals;
	
//	ELEVATOR_FLOORLIST m_TerminalAllowedFloors;
	ELEVATOR_FLOORLIST	m_TerminalAllowedFloors[256];
	memset( m_TerminalAllowedFloors, 0, sizeof( m_TerminalAllowedFloors ) );


	vl_NumTerminals > 256 - 1 ? num_terminals = 256 : num_terminals = vl_NumTerminals;

	memset( allowedFloorsCfg, 0, sizeof( num_terminals+1 ) );

//	memset(&m_TerminalAllowedFloors, 0, sizeof(ELEVATOR_FLOORLIST) * num_terminals + 1);
	
	m_TerminalAllowedFloors[0].si_ElevatorFloorListID = 1;
	m_TerminalAllowedFloors[1].si_ElevatorFloorListID = 1;
	m_TerminalAllowedFloors[2].si_ElevatorFloorListID = 1;
	m_TerminalAllowedFloors[3].si_ElevatorFloorListID = 1;

	memcpy( allowedFloorsCfg, m_TerminalAllowedFloors, sizeof(ELEVATOR_FLOORLIST) * num_terminals + 1 );
	
	return S_OK;
}

STDMETHODIMP CElevatorTrans::Lnl_UpdateTerminal(ELEVATOR_TERMINAL_CONFIG *terminalConfiguration)
{
	return E_NOTIMPL;
}

