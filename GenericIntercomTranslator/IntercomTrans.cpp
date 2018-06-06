// IntercomTrans.cpp : Implementation of CIntercomTrans
#include "stdafx.h"
#include "GenericIntercomTranslator.h"
#include "IntercomTrans.h"

#include "Interfaces\ITransport_i.c"


/////////////////////////////////////////////////////////////////////////////
// CIntercomTrans

/*
 * Constructor
 *
 */
CIntercomTrans::CIntercomTrans()
{
}

/*
 * Destructor
 *
 */
CIntercomTrans::~CIntercomTrans()
{
	int intPCount = 0;

	// Release the interface pointers if needed
	if (pTransport)
	{
		intPCount = pTransport->Release();
		pTransport = NULL;
	}
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
STDMETHODIMP CIntercomTrans::Lnl_GetSystemStatus(SYSTEM_STATUS *p_Status)
{
	// Indicate results are returned synchronously, this means that the
	// status will be returned in the SYSTEM_STATUS structure.  If you need
	// to send back the status of the intercom stations you may need to send
	// back the status asynchronously.
	p_Status->sb_AsyncStatus = FALSE;

	// Set the online / offline status to the current panel state
	// ci_PanelState should be properly set to the state of the panel
	// from within other methods.
	p_Status->sb_Devices[0] = ci_PanelState;

	return S_OK;
}

/*
 * Lnl_CloseCommunication
 *
 */
STDMETHODIMP CIntercomTrans::Lnl_CloseCommunication()
{
	// TO DO: Implement this method to close the communication.
	return S_OK;
}

/*
 * Lnl_PollPanelForEvents
 *
 */
STDMETHODIMP CIntercomTrans::Lnl_PollPanelForEvents()
{
	// TO DO: Implement this method to poll the intercom system for events.
	// The PollPanelForEvents method can be overridden to perform any unique
	// polling that might be necessary.
	if (!PollPanelForEvents())
	{
		return (E_FAIL);
	}

	return S_OK;
}

/******************************************************************************/
/* IIntercom Methods - These are the methods that need to be implemented for
/*                     the IIntercom Interface.
/******************************************************************************/

/*
 * Lnl_CancelIntercomCall - Method used to cancel an intercom call.
 *
 */
STDMETHODIMP CIntercomTrans::Lnl_CancelIntercomCall( long StationNum)
{
	// TO DO: Need to implement this function to actually send the command to
	// cancel an intercom call.  The StationNum is the station number of the
	// intercom station that the call is to be cancelled on.

	return S_OK;
}

/*
 * Lnl_PlaceIntercomCall - Method used to place an intercom call.
 *
 */
STDMETHODIMP CIntercomTrans::Lnl_PlaceIntercomCall(long Station1,long Station2,long Priority)
{
	// TO DO: Need to implement this function to actually send the command to
	// place an intercom call.

	return S_OK;
}

/*
 * InitCommunication - This method is used to initialize communications.  This method
 * will most likely need to be overridden to perform the custom initialization
 *
 */
BOOL CIntercomTrans::InitCommunication(void)
{
	return TRUE;
}
