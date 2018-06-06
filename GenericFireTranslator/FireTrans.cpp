// FireTrans.cpp : Implementation of CFireTrans
#include "stdafx.h"
#include "GenericFireTranslator.h"
#include "FireTrans.h"
#include "Interfaces\ITransport_i.c"

/////////////////////////////////////////////////////////////////////////////
// CFireTrans

/*
 * Constructor
 *
 */
CFireTrans::CFireTrans()
{
}

/*
 * Destructor
 *
 */
CFireTrans::~CFireTrans()
{
	int intPCount = 0;

	// Release the interface pointers if needed
	if (pTransport)
	{
		intPCount = pTransport->Release();
		pTransport = NULL;
	}
}
