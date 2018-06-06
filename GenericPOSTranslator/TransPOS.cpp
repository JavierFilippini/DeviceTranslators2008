// TransPOS.cpp : Implementation of CTransPOS
#include "stdafx.h"
#include "GenericPOSTranslator.h"
#include "TransPOS.h"
#include "Interfaces\ITransport_i.c"

/////////////////////////////////////////////////////////////////////////////
// CTransPOS

/*
 * Constructor
 *
 */
CTransPOS::CTransPOS()
{
}

/*
 * Destructor
 *
 */
CTransPOS::~CTransPOS()
{
	int intPCount = 0;

	// Release the interface pointers if needed
	if (pTransport)
	{
		intPCount = pTransport->Release();
		pTransport = NULL;
	}
}
