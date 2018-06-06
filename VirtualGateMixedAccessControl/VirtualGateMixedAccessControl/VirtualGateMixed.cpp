// This is the main DLL file.

#include "stdafx.h"

#include "VirtualGateMixed.h"

// Utilities for Native<>CLI conversions
#include <vcclr.h>

using namespace System;
using namespace Runtime::InteropServices;
//using namespace AdR::Samples::NativeCallingCLR::ClrAssembly;

using namespace VirtualGateManaged;


CVirtualGateManaged::CVirtualGateManaged()
{
   
   VirtualGateManagedTranslator^ manAccessControlClr = gcnew VirtualGateManagedTranslator();

   // Managed type conversion into unmanaged pointer is not allowed unless
   // we use "gcroot<>" wrapper.
   gcroot<VirtualGateManagedTranslator^> *pp = new gcroot<VirtualGateManagedTranslator^>(manAccessControlClr);

   this->m_manAccessControlClr = (void*)pp;
}

CVirtualGateManaged::~CVirtualGateManaged()
{
   if (this->m_manAccessControlClr)
   {
      // Get the CLR handle wrapper
      gcroot<VirtualGateManagedTranslator^> *pp = reinterpret_cast<gcroot<VirtualGateManagedTranslator^>*>(this->m_manAccessControlClr);
      this->m_manAccessControlClr = 0;
      // Delete the wrapper; this will release the underlying CLR instance
      delete pp;
   }
}


tstring CVirtualGateManaged::alta_Panel_WS(LPCTSTR pszPanelName, unsigned int panelID) const
{
	tstring strResult;

	if (this->m_manAccessControlClr != 0)
	{
		// Get the CLR handle wrapper
		gcroot<VirtualGateManagedTranslator^> *pp = reinterpret_cast<gcroot<VirtualGateManagedTranslator^>*>(this->m_manAccessControlClr);

		String^ strPanelName = gcnew String(pszPanelName);

		// Llamada a la version Managed en C#
		strResult = (const TCHAR*)Marshal::StringToHGlobalAuto(((VirtualGateManagedTranslator^)*pp)->altaPanelWS(strPanelName,panelID)).ToPointer();

		delete strPanelName;
	}
	return strResult;

}

tstring CVirtualGateManaged::add_Gate( LPCTSTR pszPanelName, unsigned int panelID,LPCTSTR pszReaderName,unsigned int readerID ,unsigned int readerEntranceType, int v_isDownloadingDB) const
{
	tstring strResult;

	if (this->m_manAccessControlClr != 0)
	{
		// Get the CLR handle wrapper
		gcroot<VirtualGateManagedTranslator^> *pp = reinterpret_cast<gcroot<VirtualGateManagedTranslator^>*>(this->m_manAccessControlClr);

		String^ strPanelName = gcnew String(pszPanelName);
		String^ strReaderName = gcnew String(pszReaderName);

		// Llamada a la version Managed en C#
		strResult = (const TCHAR*)Marshal::StringToHGlobalAuto(((VirtualGateManagedTranslator^)*pp)->addGate(strPanelName,panelID,strReaderName,readerID,readerEntranceType,v_isDownloadingDB)).ToPointer();

		delete strPanelName;
		delete strReaderName;
	}

	return strResult;
}

void CVirtualGateManaged::delete_Gate(unsigned int v_panelID,unsigned int v_readerID) const
{
	if (this->m_manAccessControlClr != 0)
	{
		// Get the CLR handle wrapper
		gcroot<VirtualGateManagedTranslator^> *pp = reinterpret_cast<gcroot<VirtualGateManagedTranslator^>*>(this->m_manAccessControlClr);

		// Llamada a la version Managed en C#
		((VirtualGateManagedTranslator^)*pp)->deleteGate(v_panelID,v_readerID);
		
	}
}

void CVirtualGateManaged::delete_Zone(unsigned int v_panelID) const
{
	if (this->m_manAccessControlClr != 0)
	{
		// Get the CLR handle wrapper
		gcroot<VirtualGateManagedTranslator^> *pp = reinterpret_cast<gcroot<VirtualGateManagedTranslator^>*>(this->m_manAccessControlClr);

		// Llamada a la version Managed en C#
		((VirtualGateManagedTranslator^)*pp)->deleteZone(v_panelID);
	}
}

//
//void CVirtualGateManaged::reset_Badge_Accesslevels() const
//{
//	if (this->m_manAccessControlClr != 0)
//	{
//		// Get the CLR handle wrapper
//		gcroot<VirtualGateManagedTranslator^> *pp = reinterpret_cast<gcroot<VirtualGateManagedTranslator^>*>(this->m_manAccessControlClr);
//
//		// Llamada a la version Managed en C#
//		 ((VirtualGateManagedTranslator^)*pp)->resetBadgeAccesslevels();
//	}
//}


//tstring CVirtualGateManaged::add_Employee(LPCTSTR v_badge, unsigned int v_panelID, LPCTSTR v_accessLevels,LPCTSTR v_fechaActivacion, LPCTSTR v_fechaDesactivacion ,LPCTSTR v_PIN, int v_isDownloading) const
//{
//	tstring eventInfo;
//	if (this->m_manAccessControlClr != 0)
//	{
//		// Get the CLR handle wrapper
//		gcroot<VirtualGateManagedTranslator^> *pp = reinterpret_cast<gcroot<VirtualGateManagedTranslator^>*>(this->m_manAccessControlClr);
//
//
//		String^ strBadgeNumber = gcnew String(v_badge);
//		String^ strAccessLevels = gcnew String(v_accessLevels);
//
//		String^ strFechaActivacion = gcnew String(v_fechaActivacion);
//		String^ strFechaDesactivacion = gcnew String(v_fechaDesactivacion);
//
//		String^ strPIN = gcnew String(v_PIN);
//
//		eventInfo = (const TCHAR*)Marshal::StringToHGlobalAuto(((VirtualGateManagedTranslator^)*pp)->addEmployee(strBadgeNumber, v_panelID,strAccessLevels, strFechaActivacion, strFechaDesactivacion,strPIN,v_isDownloading)).ToPointer();
//		
//		delete strBadgeNumber;
//		delete strAccessLevels;
//		delete strFechaActivacion;
//		delete strFechaDesactivacion;
//		delete strPIN;
//
//	}
//	return eventInfo;
//}

//tstring CVirtualGateManaged::add_AccessLevel(unsigned int panelID,unsigned int OrganizationID, unsigned int accessLevelID,LPCTSTR readerTZString, int isDownloading ) const
//{
//	tstring eventInfo;
//	if (this->m_manAccessControlClr != 0)
//	{
//		// Get the CLR handle wrapper
//		gcroot<VirtualGateManagedTranslator^> *pp = reinterpret_cast<gcroot<VirtualGateManagedTranslator^>*>(this->m_manAccessControlClr);
//
//		String^ strTZReader = gcnew String(readerTZString);
//
//		eventInfo = (const TCHAR*)Marshal::StringToHGlobalAuto(((VirtualGateManagedTranslator^)*pp)->addAccessLevel(panelID,OrganizationID,accessLevelID,strTZReader,isDownloading)).ToPointer();
//		// Llamada a la version Managed en C#
//		//((VirtualGateManagedTranslator^)*pp)->addAccessLevel(panelID,OrganizationID,accessLevelID,strTZReader,isDownloading);
//	}
//	return eventInfo;
//}


tstring CVirtualGateManaged::add_Holidays(unsigned int PanelID,unsigned int m_OrganizationID,LPCTSTR stringToSend, int isDownloading) const
{
	tstring eventInfo;
	if (this->m_manAccessControlClr != 0)
	{
		// Get the CLR handle wrapper
		gcroot<VirtualGateManagedTranslator^> *pp = reinterpret_cast<gcroot<VirtualGateManagedTranslator^>*>(this->m_manAccessControlClr);

		String^ strHolidayData = gcnew String(stringToSend);

		eventInfo = (const TCHAR*)Marshal::StringToHGlobalAuto(((VirtualGateManagedTranslator^)*pp)->addHolidays(PanelID,m_OrganizationID,strHolidayData,isDownloading)).ToPointer();
		// Llamada a la version Managed en C#
		//((VirtualGateManagedTranslator^)*pp)->addHolidays(PanelID,m_OrganizationID,strHolidayData,isDownloading);

		delete strHolidayData;
	}
	return eventInfo;
}


WCHAR*  CVirtualGateManaged::get_Conn_Status(unsigned int PanelID,unsigned int m_OrganizationID) const
{
	//tstring eventInfo;
	WCHAR * ptrString;
	if (this->m_manAccessControlClr != 0)
	{
		// Get the CLR handle wrapper
		gcroot<VirtualGateManagedTranslator^> *pp = reinterpret_cast<gcroot<VirtualGateManagedTranslator^>*>(this->m_manAccessControlClr);

		ptrString = (WCHAR*)Marshal::StringToHGlobalAuto(((VirtualGateManagedTranslator^)*pp)->getConnStatus(PanelID,m_OrganizationID)).ToPointer();
	
	}

	return ptrString;
}


void CVirtualGateManaged::send_ID_Serials(LPCTSTR IDSerialnums) const
{
	if (this->m_manAccessControlClr != 0)
	{
		// Get the CLR handle wrapper
		gcroot<VirtualGateManagedTranslator^> *pp = reinterpret_cast<gcroot<VirtualGateManagedTranslator^>*>(this->m_manAccessControlClr);

		String^ strIDSerials = gcnew String(IDSerialnums);

		// Llamada a la version Managed en C#
		((VirtualGateManagedTranslator^)*pp)->sendIDSerials(strIDSerials);
		delete strIDSerials;
	}
}


tstring CVirtualGateManaged::add_Timezone(unsigned int m_PanelID,unsigned int m_OrganizationID,unsigned int TZNumber,LPCTSTR stringToSend, int isDownloading) const
{
	tstring eventInfo;
	if (this->m_manAccessControlClr != 0)
	{
		// Get the CLR handle wrapper
		gcroot<VirtualGateManagedTranslator^> *pp = reinterpret_cast<gcroot<VirtualGateManagedTranslator^>*>(this->m_manAccessControlClr);

		String^ strTimezoneData = gcnew String(stringToSend);

		eventInfo = (const TCHAR*)Marshal::StringToHGlobalAuto(((VirtualGateManagedTranslator^)*pp)->addTimezone(m_PanelID,m_OrganizationID,TZNumber,strTimezoneData,isDownloading)).ToPointer();
		// Llamada a la version Managed en C#
		//((VirtualGateManagedTranslator^)*pp)->addTimezone(m_PanelID,m_OrganizationID,TZNumber,strTimezoneData,isDownloading);
		delete strTimezoneData;
	}
	return eventInfo;

}

//void CVirtualGateManaged::del_Employee(LPCTSTR v_badge, unsigned int m_PanelID) const
//{
//	if (this->m_manAccessControlClr != 0)
//	{
//		// Get the CLR handle wrapper
//		gcroot<VirtualGateManagedTranslator^> *pp = reinterpret_cast<gcroot<VirtualGateManagedTranslator^>*>(this->m_manAccessControlClr);
//
//		String^ strBadge = gcnew String(v_badge);
//
//		// Llamada a la version Managed en C#
//		((VirtualGateManagedTranslator^)*pp)->delEmployee(strBadge,m_PanelID);
//	}
//}

WCHAR * CVirtualGateManaged::poll_Alutrack_For_Event(LPCTSTR pszPanelName, unsigned int panelID, unsigned int organizationID) const
{
	//tstring eventInfo;
	WCHAR* ptrString;
	if (this->m_manAccessControlClr != 0)
	{
		// Get the CLR handle wrapper
		gcroot<VirtualGateManagedTranslator^> *pp = reinterpret_cast<gcroot<VirtualGateManagedTranslator^>*>(this->m_manAccessControlClr);

		String^ strPanelName = gcnew String(pszPanelName);

		// Llamada a la version Managed en C#
		ptrString= (WCHAR*)Marshal::StringToHGlobalAuto(((VirtualGateManagedTranslator^)*pp)->pollAlutrackForEvent(strPanelName,panelID,organizationID)).ToPointer();

		delete strPanelName;
	}
	return ptrString;
}

void CVirtualGateManaged::FreeMarshalString (WCHAR* strToFree) const
{
	Marshal::FreeHGlobal(IntPtr(strToFree));
}

WCHAR * CVirtualGateManaged::poll_Alutrack_For_Alarm(unsigned int panelID, unsigned int organizationID) const
{
	//tstring eventInfo;
	WCHAR* ptrString ;
	if (this->m_manAccessControlClr != 0)
	{
		// Get the CLR handle wrapper
		gcroot<VirtualGateManagedTranslator^> *pp = reinterpret_cast<gcroot<VirtualGateManagedTranslator^>*>(this->m_manAccessControlClr);

		// Llamada a la version Managed en C#
		//eventInfo = (const TCHAR*)Marshal::StringToHGlobalAuto(((VirtualGateManagedTranslator^)*pp)->pollAlutrackForAlarm(panelID,organizationID)).ToPointer();
		ptrString = (WCHAR*)Marshal::StringToHGlobalAuto(((VirtualGateManagedTranslator^)*pp)->pollAlutrackForAlarm(panelID,organizationID)).ToPointer();
	}
	return ptrString;
}



void CVirtualGateManaged::Asignar_Serial_A_Alarma(unsigned int v_alarmID,unsigned int v_serialNum, LPCTSTR tipoAlarma) const
{
	if (this->m_manAccessControlClr != 0)
	{
		// Get the CLR handle wrapper
		gcroot<VirtualGateManagedTranslator^> *pp = reinterpret_cast<gcroot<VirtualGateManagedTranslator^>*>(this->m_manAccessControlClr);

		String^ strtipoAlarma = gcnew String(tipoAlarma);

		// Llamada a la version Managed en C#
		((VirtualGateManagedTranslator^)*pp)->AsignarSerialAAlarma(v_alarmID,v_serialNum,strtipoAlarma);
		delete strtipoAlarma;
	}
}
void CVirtualGateManaged::add_Panel_ID(unsigned int panelID) const
{
	if (this->m_manAccessControlClr != 0)
	{
		// Get the CLR handle wrapper
		gcroot<VirtualGateManagedTranslator^> *pp = reinterpret_cast<gcroot<VirtualGateManagedTranslator^>*>(this->m_manAccessControlClr);

		// Llamada a la version Managed en C#
		((VirtualGateManagedTranslator^)*pp)->addPanelID(panelID);
	}
}


void CVirtualGateManaged::add_Panel_Name(LPCTSTR pszPanelName) const
{
	if (this->m_manAccessControlClr != 0)
	{
		// Get the CLR handle wrapper
		gcroot<VirtualGateManagedTranslator^> *pp = reinterpret_cast<gcroot<VirtualGateManagedTranslator^>*>(this->m_manAccessControlClr);

		String^ strPanelName = gcnew String(pszPanelName);

		// Llamada a la version Managed en C#
		((VirtualGateManagedTranslator^)*pp)->addPanelName(strPanelName);
		delete strPanelName;
	}
}

