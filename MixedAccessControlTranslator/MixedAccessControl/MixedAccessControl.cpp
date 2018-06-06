// This is the main DLL file.

#include "stdafx.h"

#include "MixedAccessControl.h"

// Utilities for Native<>CLI conversions
#include <vcclr.h>

using namespace System;
using namespace Runtime::InteropServices;

using namespace ManagedAccessControlTranslator;

CManagedAccessControl::CManagedAccessControl()
{
	 ManagedAccessControl^ manAccessControlClr = gcnew ManagedAccessControl();

   // Managed type conversion into unmanaged pointer is not allowed unless
   // we use "gcroot<>" wrapper.
   gcroot<ManagedAccessControl^> *pp = new gcroot<ManagedAccessControl^>(manAccessControlClr);

   this->m_manAccessControlClr = (void*)pp;

}

CManagedAccessControl::~CManagedAccessControl()
{
	   if (this->m_manAccessControlClr)
   {
      // Get the CLR handle wrapper
      gcroot<ManagedAccessControl^> *pp = reinterpret_cast<gcroot<ManagedAccessControl^>*>(this->m_manAccessControlClr);
      this->m_manAccessControlClr = 0;
      // Delete the wrapper; this will release the underlying CLR instance
      delete pp;
   }

}

void CManagedAccessControl::Test_Call(LPCTSTR pszPanelName) const
{
if (this->m_manAccessControlClr != 0)
	{
		// Get the CLR handle wrapper
		gcroot<ManagedAccessControl^> *pp = reinterpret_cast<gcroot<ManagedAccessControl^>*>(this->m_manAccessControlClr);

		String^ strPanelName = gcnew String(pszPanelName);

		// Llamada a la version Managed en C#
		((ManagedAccessControl^)*pp)->TestCall(strPanelName);
		delete strPanelName;
	}
}

tstring CManagedAccessControl::alta_Panel_WS(LPCTSTR pszPanelName, unsigned int panelID) const
{
	tstring strResult;

	if (this->m_manAccessControlClr != 0)
	{
		// Get the CLR handle wrapper
		gcroot<ManagedAccessControl^> *pp = reinterpret_cast<gcroot<ManagedAccessControl^>*>(this->m_manAccessControlClr);

		String^ strPanelName = gcnew String(pszPanelName);

		// Llamada a la version Managed en C#
		strResult = (const TCHAR*)Marshal::StringToHGlobalAuto(((ManagedAccessControl^)*pp)->altaPanelWS(strPanelName,panelID)).ToPointer();

		delete strPanelName;
	}
	return strResult;

}


tstring CManagedAccessControl::add_Reader( LPCTSTR pszPanelName, unsigned int panelID,LPCTSTR pszReaderName,unsigned int readerID ,unsigned int readerEntranceType, unsigned int organizationID,LPCTSTR pszCardFormats, int v_isDownloadingDB) const
{
	tstring strResult;

	if (this->m_manAccessControlClr != 0)
	{
		// Get the CLR handle wrapper
		gcroot<ManagedAccessControl^> *pp = reinterpret_cast<gcroot<ManagedAccessControl^>*>(this->m_manAccessControlClr);

		String^ strPanelName = gcnew String(pszPanelName);
		String^ strReaderName = gcnew String(pszReaderName);
		String^ strCardFormats = gcnew String(pszCardFormats);

		// Llamada a la version Managed en C#
		strResult = (const TCHAR*)Marshal::StringToHGlobalAuto(((ManagedAccessControl^)*pp)->addReader(strPanelName,panelID,strReaderName,readerID,readerEntranceType,organizationID,strCardFormats,v_isDownloadingDB)).ToPointer();

		delete strPanelName;
		delete strReaderName; 
		delete strCardFormats;
	}

	return strResult;
}

//void CManagedAccessControl::reset_Badge_Accesslevels() const
//{
//	if (this->m_manAccessControlClr != 0)
//	{
//		// Get the CLR handle wrapper
//		gcroot<ManagedAccessControl^> *pp = reinterpret_cast<gcroot<ManagedAccessControl^>*>(this->m_manAccessControlClr);
//
//		// Llamada a la version Managed en C#
//		 ((ManagedAccessControl^)*pp)->resetBadgeAccesslevels();
//	}
//}

//tstring CManagedAccessControl::add_Employee(LPCTSTR v_badge, unsigned int v_panelID, LPCTSTR v_accessLevels,LPCTSTR v_fechaActivacion,LPCTSTR v_fechaDesactivacion ,LPCTSTR v_PIN, int v_isDownloading) const
//{
//	tstring eventInfo;
//	if (this->m_manAccessControlClr != 0)
//	{
//		// Get the CLR handle wrapper
//		gcroot<ManagedAccessControl^> *pp = reinterpret_cast<gcroot<ManagedAccessControl^>*>(this->m_manAccessControlClr);
//
//		String^ strBadgeNumber = gcnew String(v_badge);
//		String^ strAccessLevels = gcnew String(v_accessLevels);
//
//		String^ strFechaActivacion = gcnew String(v_fechaActivacion);
//		String^ strFechaDesactivacion = gcnew String(v_fechaDesactivacion);
//
//		String^ strPIN = gcnew String(v_PIN);
//
//		eventInfo = (const TCHAR*)Marshal::StringToHGlobalAuto(((ManagedAccessControl^)*pp)->addEmployee(strBadgeNumber, v_panelID,strAccessLevels, strFechaActivacion, strFechaDesactivacion,strPIN,v_isDownloading)).ToPointer();
//		// Llamada a la version Managed en C#
// 
//		delete strBadgeNumber;
//		delete strAccessLevels;
//		delete strFechaActivacion;
//		delete strFechaDesactivacion;
//		delete strPIN;
//	}
//	return eventInfo;
//}


//tstring CManagedAccessControl::add_AccessLevel(unsigned int panelID,unsigned int OrganizationID, unsigned int accessLevelID,LPCTSTR readerTZString, int isDownloading ) const
//{
//	tstring eventInfo;
//	if (this->m_manAccessControlClr != 0)
//	{
//		// Get the CLR handle wrapper
//		gcroot<ManagedAccessControl^> *pp = reinterpret_cast<gcroot<ManagedAccessControl^>*>(this->m_manAccessControlClr);
//
//		String^ strTZReader = gcnew String(readerTZString);
//
//		eventInfo = (const TCHAR*)Marshal::StringToHGlobalAuto(((ManagedAccessControl^)*pp)->addAccessLevel(panelID,OrganizationID,accessLevelID,strTZReader,isDownloading)).ToPointer();
//		// Llamada a la version Managed en C#
//		//((ManagedAccessControl^)*pp)->addAccessLevel(panelID,OrganizationID,accessLevelID,strTZReader,isDownloading);
//
//		delete strTZReader;
//	}
//	return eventInfo;
//}


tstring CManagedAccessControl::add_Holidays(unsigned int PanelID,unsigned int m_OrganizationID,LPCTSTR stringToSend, int isDownloading) const
{
	tstring eventInfo;
	if (this->m_manAccessControlClr != 0)
	{
		// Get the CLR handle wrapper
		gcroot<ManagedAccessControl^> *pp = reinterpret_cast<gcroot<ManagedAccessControl^>*>(this->m_manAccessControlClr);

		String^ strHolidayData = gcnew String(stringToSend);
		
		eventInfo = (const TCHAR*)Marshal::StringToHGlobalAuto(((ManagedAccessControl^)*pp)->addHolidays(PanelID,m_OrganizationID,strHolidayData,isDownloading)).ToPointer();
		// Llamada a la version Managed en C#
		//((ManagedAccessControl^)*pp)->addHolidays(PanelID,m_OrganizationID,strHolidayData,isDownloading);
		delete strHolidayData;
	}

	return eventInfo;
}

WCHAR * CManagedAccessControl::get_Conn_Status(unsigned int PanelID, unsigned int isDownloading) const
{
	//tstring eventInfo;
	WCHAR * ptrString;
	if (this->m_manAccessControlClr != 0)
	{
		// Get the CLR handle wrapper
		gcroot<ManagedAccessControl^> *pp = reinterpret_cast<gcroot<ManagedAccessControl^>*>(this->m_manAccessControlClr);

		ptrString = (WCHAR*)Marshal::StringToHGlobalAuto(((ManagedAccessControl^)*pp)->getConnStatus(PanelID,isDownloading)).ToPointer();
	
	}

	return ptrString;
}

void CManagedAccessControl::delete_Reader(unsigned int v_panelID,unsigned int v_readerID) const
{
	if (this->m_manAccessControlClr != 0)
	{
		// Get the CLR handle wrapper
		gcroot<ManagedAccessControl^> *pp = reinterpret_cast<gcroot<ManagedAccessControl^>*>(this->m_manAccessControlClr);

		// Llamada a la version Managed en C#
		((ManagedAccessControl^)*pp)->deleteReader(v_panelID,v_readerID);
	}
}

void CManagedAccessControl::delete_Panel(unsigned int v_panelID, LPCTSTR panelName) const
{
	if (this->m_manAccessControlClr != 0)
	{
		// Get the CLR handle wrapper
		gcroot<ManagedAccessControl^> *pp = reinterpret_cast<gcroot<ManagedAccessControl^>*>(this->m_manAccessControlClr);

		String^ strPanelName = gcnew String(panelName);

		// Llamada a la version Managed en C#
		((ManagedAccessControl^)*pp)->deletePanel(v_panelID, strPanelName);

		delete strPanelName;
	}
}

void CManagedAccessControl::enviar_Borrar_CF(unsigned int panel_id ) const
{
	if (this->m_manAccessControlClr != 0)
	{
		// Get the CLR handle wrapper
		gcroot<ManagedAccessControl^> *pp = reinterpret_cast<gcroot<ManagedAccessControl^>*>(this->m_manAccessControlClr);

		// Llamada a la version Managed en C#
		((ManagedAccessControl^)*pp)->enviarBorrarCF(panel_id);
	}
}

void CManagedAccessControl::Asignar_Serial_A_Alarma(unsigned int v_alarmID,unsigned int v_serialNum, LPCTSTR tipoAlarma) const
{
	if (this->m_manAccessControlClr != 0)
	{
		// Get the CLR handle wrapper
		gcroot<ManagedAccessControl^> *pp = reinterpret_cast<gcroot<ManagedAccessControl^>*>(this->m_manAccessControlClr);

		String^ strtipoAlarma = gcnew String(tipoAlarma);

		// Llamada a la version Managed en C#
		((ManagedAccessControl^)*pp)->AsignarSerialAAlarma(v_alarmID,v_serialNum,strtipoAlarma);
		delete strtipoAlarma;
	}
}


void CManagedAccessControl::send_ID_Serials(LPCTSTR IDSerialnums) const
{
	if (this->m_manAccessControlClr != 0)
	{
		// Get the CLR handle wrapper
		gcroot<ManagedAccessControl^> *pp = reinterpret_cast<gcroot<ManagedAccessControl^>*>(this->m_manAccessControlClr);

		String^ strIDSerials = gcnew String(IDSerialnums);

		// Llamada a la version Managed en C#
		((ManagedAccessControl^)*pp)->sendIDSerials(strIDSerials);
		delete strIDSerials;
	}
}

tstring CManagedAccessControl::add_Timezone(unsigned int m_PanelID,unsigned int m_OrganizationID,unsigned int TZNumber,LPCTSTR stringToSend, int isDownloading) const
{
	tstring eventInfo;
	if (this->m_manAccessControlClr != 0)
	{
		// Get the CLR handle wrapper
		gcroot<ManagedAccessControl^> *pp = reinterpret_cast<gcroot<ManagedAccessControl^>*>(this->m_manAccessControlClr);

		String^ strTimezoneData = gcnew String(stringToSend);

		eventInfo = (const TCHAR*)Marshal::StringToHGlobalAuto(((ManagedAccessControl^)*pp)->addTimezone(m_PanelID,m_OrganizationID,TZNumber,strTimezoneData,isDownloading)).ToPointer();

		// Llamada a la version Managed en C#
		//((ManagedAccessControl^)*pp)->addTimezone(m_PanelID,m_OrganizationID,TZNumber,strTimezoneData,isDownloading);
		delete strTimezoneData;
	}

	return eventInfo;
}

//void CManagedAccessControl::del_Employee(LPCTSTR v_badge, unsigned int m_PanelID) const
//{
//	if (this->m_manAccessControlClr != 0)
//	{
//		// Get the CLR handle wrapper
//		gcroot<ManagedAccessControl^> *pp = reinterpret_cast<gcroot<ManagedAccessControl^>*>(this->m_manAccessControlClr);
//
//		String^ strBadge = gcnew String(v_badge);
//
//		// Llamada a la version Managed en C#
//		((ManagedAccessControl^)*pp)->delEmployee(strBadge,m_PanelID);
//		delete strBadge;
//	}
//}


//tstring CManagedAccessControl::poll_Alutrack_For_Event(LPCTSTR pszPanelName, unsigned int panelID, unsigned int organizationID) const
WCHAR * CManagedAccessControl::poll_Alutrack_For_Event(LPCTSTR pszPanelName, unsigned int panelID, unsigned int organizationID) const
{
	//tstring eventInfo;
	WCHAR* ptrString ;
	if (this->m_manAccessControlClr != 0)
	{
		// Get the CLR handle wrapper
		gcroot<ManagedAccessControl^> *pp = reinterpret_cast<gcroot<ManagedAccessControl^>*>(this->m_manAccessControlClr);

		String^ strPanelName = gcnew String(pszPanelName);
		
		// Llamada a la version Managed en C#
		ptrString = (WCHAR*) Marshal::StringToHGlobalAuto(((ManagedAccessControl^)*pp)->pollAlutrackForEvent(strPanelName,panelID,organizationID)).ToPointer();
		delete strPanelName;
	}
	//return eventInfo;
	return ptrString; 
}

void CManagedAccessControl::FreeMarshalString (WCHAR* strToFree) const
{
	Marshal::FreeHGlobal(IntPtr(strToFree));
}


WCHAR * CManagedAccessControl::poll_Alutrack_For_Alarm(unsigned int panelID, unsigned int organizationID) const
{
	//tstring eventInfo;
	WCHAR* ptrString ;
	if (this->m_manAccessControlClr != 0)
	{
		// Get the CLR handle wrapper
		gcroot<ManagedAccessControl^> *pp = reinterpret_cast<gcroot<ManagedAccessControl^>*>(this->m_manAccessControlClr);

		// Llamada a la version Managed en C#
		//eventInfo = (const TCHAR*)Marshal::StringToHGlobalAuto(((ManagedAccessControl^)*pp)->pollAlutrackForAlarm(panelID,organizationID)).ToPointer();
		  ptrString = (WCHAR*) Marshal::StringToHGlobalAuto(((ManagedAccessControl^)*pp)->pollAlutrackForAlarm(panelID,organizationID)).ToPointer();
		
	}
	return ptrString;
}


void CManagedAccessControl::add_CardFormat(unsigned int FormatID, unsigned int m_PanelID,unsigned int BitSize,unsigned int FC,unsigned int Offset,unsigned int BitsFC,unsigned int PositionStartFC,unsigned int BitsCardNum,unsigned int PositionStartCN,unsigned int BitsIssueCode,unsigned int PositionStartIC,int m_IsDownloadInProgress ) const
{
	if (this->m_manAccessControlClr != 0)
	{
		// Get the CLR handle wrapper
		gcroot<ManagedAccessControl^> *pp = reinterpret_cast<gcroot<ManagedAccessControl^>*>(this->m_manAccessControlClr);

		// Llamada a la version Managed en C#
		((ManagedAccessControl^)*pp)->addCardFormat(FormatID, m_PanelID,BitSize,FC,Offset,BitsFC,PositionStartFC,BitsCardNum,PositionStartCN,BitsIssueCode,PositionStartIC,m_IsDownloadInProgress);
	}
}


bool CManagedAccessControl::is_Download_Sent() const
{
	bool res = false;
	if (this->m_manAccessControlClr != 0)
	{
		// Get the CLR handle wrapper
		gcroot<ManagedAccessControl^> *pp = reinterpret_cast<gcroot<ManagedAccessControl^>*>(this->m_manAccessControlClr);

		// Llamada a la version Managed en C#
		res = ((ManagedAccessControl^)*pp)->isDownloadSent();
	}

	return res;
}

void CManagedAccessControl::set_Is_Download_Sent() const
{
	if (this->m_manAccessControlClr != 0)
	{
		// Get the CLR handle wrapper
		gcroot<ManagedAccessControl^> *pp = reinterpret_cast<gcroot<ManagedAccessControl^>*>(this->m_manAccessControlClr);

		// Llamada a la version Managed en C#
		((ManagedAccessControl^)*pp)->setIsDownloadSent();
	}
}

void CManagedAccessControl::reset_Is_Download_Sent() const
{
	if (this->m_manAccessControlClr != 0)
	{
		// Get the CLR handle wrapper
		gcroot<ManagedAccessControl^> *pp = reinterpret_cast<gcroot<ManagedAccessControl^>*>(this->m_manAccessControlClr);

		// Llamada a la version Managed en C#
		((ManagedAccessControl^)*pp)->resetIsDownloadSent();
	}
}

void CManagedAccessControl::add_Panel_ID(unsigned int panelID) const
{
	if (this->m_manAccessControlClr != 0)
	{
		// Get the CLR handle wrapper
		gcroot<ManagedAccessControl^> *pp = reinterpret_cast<gcroot<ManagedAccessControl^>*>(this->m_manAccessControlClr);

		// Llamada a la version Managed en C#
		((ManagedAccessControl^)*pp)->addPanelID(panelID);
	}
}


void CManagedAccessControl::add_Panel_Name(LPCTSTR pszPanelName) const
{
	if (this->m_manAccessControlClr != 0)
	{
		// Get the CLR handle wrapper
		gcroot<ManagedAccessControl^> *pp = reinterpret_cast<gcroot<ManagedAccessControl^>*>(this->m_manAccessControlClr);

		String^ strPanelName = gcnew String(pszPanelName);

		// Llamada a la version Managed en C#
		((ManagedAccessControl^)*pp)->addPanelName(strPanelName);
		delete strPanelName;
	}
}










