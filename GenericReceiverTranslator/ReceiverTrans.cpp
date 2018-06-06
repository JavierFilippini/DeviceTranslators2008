// ReceiverTrans.cpp : Implementation of CReceiverTrans
#include "stdafx.h"
#include "GenericReceiverTranslator.h"
#include "ReceiverTrans.h"
#include "Interfaces\ITransport_i.c"

/////////////////////////////////////////////////////////////////////////////
// CReceiverTrans

/*
 * Constructor
 *
 */

CReceiverTrans::CReceiverTrans(){

}

/*
 * Destructor
 *
 */

CReceiverTrans::~CReceiverTrans(){
	int intPCount = 0;

	// Release the interface pointers if needed
	if (pTransport)
	{
		intPCount = pTransport->Release();
		pTransport = NULL;
	}
}