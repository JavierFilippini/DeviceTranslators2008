// dllmain.cpp : Implementation of DllMain.

#include "stdafx.h"
#include "resource.h"
#include "GenericCustomOptions_i.h"
#include "GenericCustomOptions_i.c"
#include "dllmain.h"
#include "TransCustomOptions.h"



CGenericCustomOptionsModule _AtlModule;



class CGenericCustomOptionsApp : public CWinApp
{
public:

// Overrides
	virtual BOOL InitInstance();
	virtual int ExitInstance();

	DECLARE_MESSAGE_MAP()
};

BEGIN_MESSAGE_MAP(CGenericCustomOptionsApp, CWinApp)
END_MESSAGE_MAP()

CGenericCustomOptionsApp theApp;

BOOL CGenericCustomOptionsApp::InitInstance()
{
	#ifdef _MERGE_PROXYSTUB
    hProxyDll = m_hInstance;
	#endif
//    _Module.Init(ObjectMap, m_hInstance, &LIBID_CustomOptionsLib);
    return CWinApp::InitInstance();
}

int CGenericCustomOptionsApp::ExitInstance()
{
	return CWinApp::ExitInstance();
}
