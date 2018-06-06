// AccessControlTrans.cpp : Implementation of CAccessControlTrans
#include "stdafx.h"
#include "GenericAccessControlTranslator.h"
#include "AccessControlTrans.h"

#include "Interfaces\ITransport_i.c"


/*///////////////////////////////////////////////////////////////////////////
  CAccessControlTrans
 * Constructor
 *///////////////////////////////////////////////////////////////////////////
CAccessControlTrans::CAccessControlTrans()
{
	
	CString msgString;
	msgString = _T("*** Nueva instancia de VG Translator ***");
	DistributeDisplayTextMessage(msgString);

	// Crea el CVirtualGateManaged: Bridge entre C++ y C#
	miManTraker= new CVirtualGateManaged();

	
	// Inicialmente se cargan con -1 y "". 
	//Se actualizan en llamadas independientes y se da de alta en el server en InicCommunication().
	m_PanelID = -1;
	si_PanelName = "";

	isInitCommSuccess = false;			// Indica si el proceso de inicializacion de la comunicacion fue exitoso. Si no lo fue, hacer retry...
	isDatabaseUpdated= false;			// Inicialmente la base de datos no esta actualizada.
	//failConCounter =0;				// Contador de fallos en envios. 
	setearReaderStatus = false;			// Para indicar que hay que enviar los eventos para hacer el update del estado de los reades.
	isInitDownloadDB = false;			// Indica si el downloadDatabaseInicial fue lanzado (sin CardHoloders) 
	//isFullDownload = false;				// Se setea en true cuando se pide un Download desde el System Administrator.
	isAlwaysOffLine = false;			// Solo una falla en el alta lo pone en true
}

/*
 * Destructor
 *
 */
CAccessControlTrans::~CAccessControlTrans()
{

	if (miManTraker != NULL)
	{
		miManTraker->delete_Zone(m_PanelID);		// Notifica al server que se dio de baja la VZone
	}

	// Release the interface pointers if needed
	if (pTransport)
	{
		int intPCount = pTransport->Release();
		pTransport = NULL;
	}
}

//STDMETHODIMP CAccessControlTrans::Lnl_InitializePanel()
BOOL CAccessControlTrans::InitPanel(void)
{
	if (!isInitCommSuccess)
	{
		Lnl_InitCommunication();	// Antes de saber si esta o no activo HAY que inicializar la comunicacion
		return false;
	}
	else
	{
		// Mientras llama a InitPanel, igual se dan de alta accesos en Lenel, etc. Aunque esten offLine.
		PollAndActualizeEvents();

		bool resUpdate =  updatePanelStatus();

		if (isAlwaysOffLine)
			return false;

		return resUpdate;
	}
}


STDMETHODIMP  CAccessControlTrans::Lnl_InitCommunication()
{
	CString panelIDString;
	panelIDString.Format(_T("%i"),m_PanelID);

	CString total = _T(" Entra a InitCommunication de VZone: ") + si_PanelName + _T(" con panelID=") +panelIDString ;
	
	DistributeDisplayTextMessage(total);

	CString msgString ="";

	if (miManTraker != NULL)
	{
		if ((m_PanelID != 0 ) && ( si_PanelName !=""))
		{
			CString respuesta = altaPanelFromLenel(si_PanelName,m_PanelID);		// Solo a efectos de chequear la licencia

			if ((respuesta =="FAIL"))
			{
				msgString = _T("ERROR en Init_Communication() para la VG: ")+ si_PanelName;
				DistributeDisplayTextMessage(msgString);
			}
			else
			{
				msgString = _T("LnL_InitCommunication VG EXITOSO: ")+si_PanelName;
				DistributeDisplayTextMessage(msgString);
				isInitCommSuccess= true;
			}
		}
		else
		{
			CString msgString;
			msgString = _T("ERROR: Entró a  Lnl_InitCommunication VG sin tener PanelID Y panelName!! ");
			DistributeDisplayTextMessage(msgString);
		}
	}
	else
	{
		CString msgString;
		msgString = _T("ATENCION: Entró a Lnl_InitCommunication VG con ManagedTracker = NULL");
		DistributeDisplayTextMessage(msgString);
	}

	return S_OK;
}

CString CAccessControlTrans::altaPanelFromLenel(CString v_panelName, int v_PanelID)
{
	CString msgString;
	CString panelIDString;
	panelIDString.Format(_T("%i"),m_PanelID);
	msgString = _T("Dando de alta ZONA ");
	CString total = msgString + si_PanelName + _T(", PanelID=") +panelIDString;
	DistributeDisplayTextMessage(total);

	CString resAlta = miManTraker->alta_Panel_WS(si_PanelName, m_PanelID).c_str();		// Solo chequea la disponibilidad de la licencia para esta Zona.

	if (resAlta!= "OK")					// Si no es OK, dio error y en resAlta viene el mensaje a mandar en la alarma
	{
		contAlarma++;
		if (contAlarma<CANT_ALARM_RETRY)				
		{
			enviarAlarmaAOnGuard(resAlta,(BYTE)30,(short)0,"1999-10-10 10:10:10");  // Paso esta fecha/hora para que en la llamada utilice la fecha y hora Now()
			msgString = _T("ERROR al dar de alta ") + si_PanelName + _T(" ALARMA ENVIADA=") + resAlta;
			DistributeDisplayTextMessage(msgString);
			Sleep(500);
		}
	
	
		isAlwaysOffLine = true;			// Si hay un error al dar de alta, queda siempre en offLine.
	}
	else
		isAlwaysOffLine = false;


	return resAlta;
}


// Solo notifica el seteo del panelID en la ventana de debug.  
STDMETHODIMP CAccessControlTrans::Lnl_SetPanelID(INT panel_id)
{
	SetPanelID(panel_id);
	CString msgString;
	CString panelIDString;
	panelIDString.Format(_T("%i"),panel_id);
	msgString = _T("Seteo el PanelID de VZone con: ");

	miManTraker->add_Panel_ID(panel_id);
	
	return S_OK;
}
// Solo notifica el seteo del panel Name en la ventana de debug.  
STDMETHODIMP CAccessControlTrans::Lnl_SetPanelName(BSTR panel_Name)
{
	CString panelName = panel_Name;
	SetPanelName(panelName);

	CString msgString;
	msgString = _T("Seteo el PanelName de VZone con: ");
	CString total = msgString + si_PanelName;
	DistributeDisplayTextMessage(total);

	miManTraker->add_Panel_Name(panel_Name);

	return S_OK;
}


//STDMETHODIMP CAccessControlTrans::Lnl_PollPanelForEvents()
BOOL CAccessControlTrans::PollPanelForEvents(void)
{
	unsigned int m_OrganizationID =-1;		// No se usa

	if (!isInitCommSuccess)
	{
		CString msgString;
		msgString.Format(_T("ENTRO A POLL VZoneSIN HABER SIDO INICIALIZADO. Retry Lnl_InitCommunication()"));
		DistributeDisplayTextMessage(msgString);

		Lnl_InitCommunication();
	}
	
	if (isDatabaseUpdated)
	{	
		PollAndActualizeEvents();			// Hace el Poll y levanta Accesos
	}
	else									// La base de datos no esta actualizada...
	{
		if (!isInitDownloadDB)
		{
			lanzarThreadDownloadDB();		
			isInitDownloadDB = true;
		}
		else
		{
			if (!m_IsDownloadInProgress)	// Mientras hace DownloadDB se sigue llamando al POLL, por eso esto...
			{
				isDatabaseUpdated = true;
			}
		}
	}

	
	bool res = updatePanelStatus();		// Actualiza status en Alarm Monitoring y procesa ALARMAS
	
	if (isAlwaysOffLine)
		return false;

	return res;

}

// Hace Polling usando el ws getAcceso y si hay datos disponibles los levanta en Lenel
// Por ultimo informa al ws los idSerialnums asignados.
void CAccessControlTrans::PollAndActualizeEvents()
{
	
	unsigned int m_OrganizationID =-1;		// NO usado
	CString panelIDString;
	panelIDString.Format(_T("%i"),m_PanelID);

	//CString respuesta = miManTraker->poll_Alutrack_For_Event(si_PanelName,m_PanelID,m_OrganizationID).c_str();
	WCHAR*  ptrRespuesta = miManTraker->poll_Alutrack_For_Event(si_PanelName,m_PanelID,m_OrganizationID);

	CString respuesta(ptrRespuesta);

	if ((respuesta !="FAIL") && ( respuesta!="") && (respuesta !="NO_DATA"))
	{
		CString IDSerialnums = multipleAddEvents(respuesta);
		miManTraker->send_ID_Serials(IDSerialnums);
	}

	miManTraker->FreeMarshalString(ptrRespuesta);						// Liberar la memoria

	// Alarmas del panel
	//respuesta = miManTraker->poll_Alutrack_For_Alarm(m_PanelID,m_OrganizationID).c_str();
	WCHAR* ptrRespuestaAlarma = miManTraker->poll_Alutrack_For_Alarm(m_PanelID,m_OrganizationID);
	CString respuestaAlarma(ptrRespuestaAlarma);

	if ((respuestaAlarma !="FAIL") && ( respuestaAlarma!="") && (respuestaAlarma !="NO_DATA"))
	{
		parsearYEnviarAlarma(respuesta);
		
	}

	miManTraker->FreeMarshalString(ptrRespuestaAlarma);					// Liberar la memoria

}

CString CAccessControlTrans::multipleAddEvents(CString eventData)
{
	CString serialsIDs=_T("");;

	CString resToken;
	int curPos =0;

    resToken = eventData.Tokenize(_T("|"),curPos);

	long m_SerialNum=0;
	CString serialNumString=_T("");;

	while(resToken != _T(""))
	{
		m_SerialNum = GetPanelEventSerialNumber();
		
		serialNumString.Format(_T("%i"),m_SerialNum);

		int posisVisit = resToken.ReverseFind(',');

		if (posisVisit>0)
		{
			CString isVisit = resToken.Right(resToken.GetLength()-posisVisit-1);

			resToken = resToken.Left(posisVisit);
			
		    int posID = resToken.ReverseFind(',');
			if (posID>0)
			{
				CString IDstr = resToken.Right(resToken.GetLength()-posID-1);

				serialsIDs = serialsIDs + IDstr +_T(",") +  serialNumString +_T(",")+isVisit +  _T("|");

				addEventToAlarmMonitoring(resToken, m_SerialNum);		// ACA LEVANTA A LENEL EFECTIVAMENTE
			}
		}
		resToken = eventData.Tokenize(_T("|"),curPos);
	}

	return serialsIDs;
}


bool CAccessControlTrans::updatePanelStatus()
{
		bool onLine = false;
		
		//CString estado = miManTraker->get_Conn_Status(m_PanelID,m_IsDownloadInProgress).c_str();
		WCHAR * ptrRespuesta = miManTraker->get_Conn_Status(m_PanelID,m_IsDownloadInProgress);

		CString respuesta(ptrRespuesta);
		onLine = (respuesta == "YES");
		miManTraker->FreeMarshalString(ptrRespuesta);						// Liberar la memoria


		if (!onLine)					// Retry
		{
			Sleep(1000);				// Una pausa antes del retry
			WCHAR * ptrRespuesta2 = miManTraker->get_Conn_Status(m_PanelID,m_IsDownloadInProgress);
			CString respuesta2(ptrRespuesta2);
			onLine = (respuesta2 == "YES");		
			miManTraker->FreeMarshalString(ptrRespuesta2);						// Liberar la memoria
		}
		
		// El flag setearReaderStatus se setea en Lnl_GetReaderStatus2, al hacer updatePanelStatus desde el Alarm Monitoring...
		if ((setearReaderStatus) && onLine)
		{
			setearAllReadersOnLine();		// Pone OnLine a los readers.
			setearReaderStatus = false;		// Para no atomizar a la interfaz con una actualizacion constante del estado.
		}

		return onLine;
}

void CAccessControlTrans::parsearYEnviarAlarma(CString encodedAlarm)
{
	
		// NOTA: Hecho con la vieja estrategia por no tener regex aca.
		// Viene: ConnStatus(Y|N)COMMAND: xxxxx
		if ( encodedAlarm.GetLength() > 6)
		{
				// Viene: ConnStatus(Y|N)ALARM: xxxxx
				if (encodedAlarm.Mid(1,6) == "ALARM:")
				{

					CString AlarmData = encodedAlarm.Right(encodedAlarm.GetLength() - 6);

					int curPos =0;
					int item =0;
					BYTE eventType =0;
					short eventID =0;
					CString textMsg =_T("");
					CStringA textHora = _T("");
					CString tipoAlarm =_T("");
					DWORD alarmID = 0;

					CString resToken = AlarmData.Tokenize(_T("|"),curPos);

					while (resToken !=_T(""))
					{
						if (item ==0)
						{
							eventType = (BYTE)_wtoi(resToken);
						}
						if (item ==1)
						{
							eventID = (short)_wtoi(resToken);
						}
						if (item ==2)
						{
							textMsg = resToken;

						}
						if (item ==3)
						{
							textHora = resToken;
						}
						if (item ==4)
						{
							alarmID = (DWORD)_wtoi(resToken);
						}
						if (item ==5)
						{
							tipoAlarm = resToken;
						}

						item++;

						 resToken = AlarmData.Tokenize(_T("|"),curPos);
					}

					if (item >0)
					{
						DWORD serialNumAssigned = enviarAlarmaAOnGuard(textMsg,eventType,eventID,textHora);			// OK Enviar la alarma.

						// Nuevo: asignacion de serialnum a alarmas.
						miManTraker->Asignar_Serial_A_Alarma(alarmID,serialNumAssigned,tipoAlarm);
					}

			}
		}

}

DWORD CAccessControlTrans::enviarAlarmaAOnGuard(CString MessageText, BYTE event_type, short event_id,CStringA v_textHora)
{
	LNLMESSAGE ls_Event;
	memset( &ls_Event, '\0', sizeof(ls_Event));
	ls_Event.sl_Size = sizeof(LNLMESSAGE);
	ls_Event.sl_SerialNumber = GetPanelEventSerialNumber();
	
	DWORD localTime =  convertirFechaHora(v_textHora);
	ls_Event.sl_Time = localTime;
	
	ls_Event.ss_AccessPanelID = m_PanelID;
	ls_Event.sb_EventType = event_type;
	ls_Event.sb_EventID =event_id;

	ls_Event.sb_AssociatedText = 1;
	BSTR bstrMessageText = MessageText.AllocSysString();
	m_pDistributeEvent->Lnl_DistributeLnlMessageWithTextEx(&ls_Event, bstrMessageText );
	::SysFreeString( bstrMessageText );

	return ls_Event.sl_SerialNumber;
}


// Lanza el thread de DownloadDB parcial para el panel actual solo con la definicion de readers.
void CAccessControlTrans::lanzarThreadDownloadDB()
{

	if (m_pDistributeEvent != NULL)
	{
			CString msgString = _T("Lanzando Thread DownloadDB con READERS para el panel ") + si_PanelName;
			DistributeDisplayTextMessage(msgString);
			setDBArrayReadersOnly();
			HRESULT hr = m_pDistributeEvent->Lnl_StartDatabaseThread(m_PanelID,m_DatabaseDownloadArray);
	}
	return;
}




void CAccessControlTrans::setearAllReadersOffLine()
{
	LNLMESSAGE ls_Event;

	for each( const int readerID in m_readers)
	{
		memset( &ls_Event, '\0', sizeof(ls_Event));
		ls_Event.sl_Size = sizeof(LNLMESSAGE);
		ls_Event.sl_Time = g_oTimeConverter.GetCurrentGmtTime(); //time(NULL);
		ls_Event.sl_SerialNumber = GetPanelEventSerialNumber();

		ls_Event.sb_MessageType = LNLMSG_TYPE_STATUS;
		ls_Event.ss_AccessPanelID = m_PanelID;
		ls_Event.sb_DeviceID = readerID;
		ls_Event.sb_EventDataType = EVENT_DATA_TYPE_STATUSREQUEST;
		ls_Event.su_EventData.us_StatusRequest.sl_StatusType=DATA_SRQ_COMM_STATE;
		ls_Event.su_EventData.us_StatusRequest.sl_Status = false;

		WriteEventsToClientsEx(&ls_Event);
		//Sleep(100);
	}
}

void CAccessControlTrans::setearAllReadersOnLine()
{

	LNLMESSAGE ls_Event;

	for each( const int readerID in m_readers)
	{
		memset( &ls_Event, '\0', sizeof(ls_Event));
		ls_Event.sl_Size = sizeof(LNLMESSAGE);
		ls_Event.sl_Time = g_oTimeConverter.GetCurrentGmtTime(); //time(NULL);
		ls_Event.sl_SerialNumber = GetPanelEventSerialNumber();

		ls_Event.sb_MessageType = LNLMSG_TYPE_STATUS;
		ls_Event.ss_AccessPanelID = m_PanelID;
		ls_Event.sb_DeviceID = readerID;
		ls_Event.sb_EventDataType = EVENT_DATA_TYPE_STATUSREQUEST;
		ls_Event.su_EventData.us_StatusRequest.sl_StatusType=DATA_SRQ_COMM_STATE;
		ls_Event.su_EventData.us_StatusRequest.sl_Status = true;

		WriteEventsToClientsEx(&ls_Event);
	}
}
// La devolucion es: AccessType,Badge,PanelID,ReaderID,fechaHora
// AccessType: 0: Access Denied.
//             1: Access Granted
//             2: Access Denied, Badge expired
bool CAccessControlTrans::addEventToAlarmMonitoring(CString dataToAdd, long v_serialNum)
{
		CString resToken;
		int curPos=0;
		int dataPos =0;
		resToken = dataToAdd.Tokenize(_T(","),curPos);

		bool isAccessGranted = false;			 // por defecto es Denied
		bool deniedBadgeExpired=false;			 // Access Denied por tarjeta inactiva.

		__int64 badgeNumber = 0;
		int panelID =0;
		int readerID = 0;

		CStringA fechaHora;

		while(resToken != _T(""))
		{
			if (dataPos==0)		// es el AccessType
			{
				isAccessGranted = (resToken == _T("1"));
				deniedBadgeExpired = (resToken == _T("2"));		 // Si es 2 es accessDenied y deniedBadgeExpired
			}

			if (dataPos==1)		// es la Badge
			{
				badgeNumber = _ttoi64(resToken);
			}

			if (dataPos==2)		// es el PanelID
			{
				panelID = _ttoi(resToken);
			}

			if (dataPos==3)		// es el readerID
			{
				readerID = _ttoi(resToken);
			}

			if (dataPos==4)		// es la fechaHora
			{
				fechaHora = resToken;
			}

			resToken = dataToAdd.Tokenize(_T(","),curPos);
			dataPos++;
		}

		CString msgString =_T(" Va a agregar un acceso a VZone con fechaHora=: ") + fechaHora;
		DistributeDisplayTextMessage(msgString);

		//if ((readerID !=0) && (panelID !=0))			// Solo si vinieron datos validos: 3 numeros separados por coma
		if ((panelID !=0))
		{
			if (isAccessGranted)			// Access Granted
			{
				LNLMESSAGE ls_Event;

				memset( &ls_Event, '\0', sizeof(ls_Event));
				ls_Event.sl_Size = sizeof(LNLMESSAGE);

				DWORD localTime =  convertirFechaHora(fechaHora);
				ls_Event.sl_Time = localTime;

				ls_Event.sb_MessageType = LNLMSG_TYPE_EVENT;

				ls_Event.sl_SerialNumber = v_serialNum;			// Obtenido previamente y pasado por parametro

				ls_Event.sb_EventType = L_EVENTTYPE_GRANTED;
				ls_Event.sb_EventID = L_GRANTED_ACCESS;

				ls_Event.ss_AccessPanelID = panelID;
				ls_Event.sb_DeviceID = readerID;
				ls_Event.sb_InputArg =0;
				ls_Event.sb_EventDataType = EVENT_DATA_TYPE_CA;
				ls_Event.su_EventData.us_DataCA.sl_CardNumber = badgeNumber;
				ls_Event.su_EventData.us_DataCA.sl_IssueCode = -1;

				WriteEventsToClientsEx(&ls_Event);
			}
			else							// Access Denied
			{
				LNLMESSAGE ls_Event;

				memset( &ls_Event, '\0', sizeof(ls_Event));
				ls_Event.sl_Size = sizeof(LNLMESSAGE);

				DWORD localTime =  convertirFechaHora(fechaHora);
				ls_Event.sl_Time = localTime;

				ls_Event.sb_MessageType = LNLMSG_TYPE_EVENT;

				ls_Event.sl_SerialNumber = v_serialNum;			// Obtenido previamente y pasado por parametro

				ls_Event.sb_EventType = L_EVENTTYPE_DENIED;

				if (deniedBadgeExpired)
					ls_Event.sb_EventID = L_DENIED_INACTIVEBADGE_EXPIRED;
				else
					ls_Event.sb_EventID = L_DENIED_ACCESS;

				ls_Event.ss_AccessPanelID = panelID;
				ls_Event.sb_DeviceID = readerID;
				ls_Event.sb_InputArg =0;
				ls_Event.sb_EventDataType = EVENT_DATA_TYPE_CA;
				ls_Event.su_EventData.us_DataCA.sl_CardNumber = badgeNumber;
				ls_Event.su_EventData.us_DataCA.sl_IssueCode = -1;

				WriteEventsToClientsEx (&ls_Event);
			}
		}

		return true;
}


time_t CAccessControlTrans::convertirFechaHora(CStringA v_FechaHora)
{
	int year,month,day,hour,minute,seconds;

	const size_t bufferLength = (v_FechaHora.GetLength() + 1);		// +1 por el /0

	char* fechaHorabytes = new char[bufferLength];

	strcpy_s(fechaHorabytes, bufferLength, v_FechaHora);
	
	struct tm tm;

	sscanf(fechaHorabytes, "%u-%u-%u %u:%u:%u", &year,&month,&day,&hour,&minute,&seconds);

	CString yearStr;
	yearStr.Format(_T("%i"),year);

	CString monthStr;
	monthStr.Format(_T("%i"),month);

	CString dayStr;
	dayStr.Format(_T("%i"),day);

	CString hourStr;
	hourStr.Format(_T("%i"),hour);

	
	CString minuteStr;
	minuteStr.Format(_T("%i"),minute);

	CString secondsStr;
	secondsStr.Format(_T("%i"),seconds);
	if ((year < 2014) || (year > 2100) || (month<1) || (month>12) ||(day<1) || (day>31) || (hour<0) || (hour > 23) || (minute <0) || (minute>59) || (seconds<0) || (seconds>59))
	{
		CString msgString =_T("FechaHora fuera de rango: ") + v_FechaHora + _T(". usando currentTime");
		DistributeDisplayTextMessage(msgString);
		time_t t = g_oTimeConverter.GetCurrentGmtTime();
		delete fechaHorabytes;
		return t;
	}
	else
	{
		CString msgString =_T(" Va a agregar un evento de VZone con fechaHora=: ") + v_FechaHora;
		DistributeDisplayTextMessage(msgString);

		/*DistributeDisplayTextMessage(yearStr);
		DistributeDisplayTextMessage(monthStr);
		DistributeDisplayTextMessage(dayStr);
		DistributeDisplayTextMessage(hourStr);
		DistributeDisplayTextMessage(minuteStr);
		DistributeDisplayTextMessage(secondsStr);*/
	
		tm.tm_year = year-1900;
		tm.tm_mon  = month-1;
		tm.tm_mday = day;
		tm.tm_hour = hour;
		tm.tm_min = minute;
		tm.tm_sec = seconds;

		time_t t = mktime(&tm);  // t is now your desired time_t
		//time_t t = _mkgmtime(&tm);  // t is now your desired time_t

		delete fechaHorabytes;

		return t;
	}
}






//////////////////////////////////////////////////////////////////////////////
// IAccessControl Methods
//////////////////////////////////////////////////////////////////////////////

STDMETHODIMP CAccessControlTrans::Lnl_SetDownloadableReader(READER_DWNSPEC *prs_RM ,  BOOL vb_Wait )
{
	// TO DO: Need to implement this method
	return (E_NOTIMPL);
}

STDMETHODIMP CAccessControlTrans::Lnl_BulkAddBadge( BULKBADGE  *prs_Badge ,  BOOL vb_CheckForDup ,  BOOL vb_WaitForRsp )
{
	// TO DO: Need to implement this method
	return (E_NOTIMPL);
}

STDMETHODIMP CAccessControlTrans::Lnl_DeleteBadge( long vl_CardNumber ,  BOOL vb_WaitForRsp )
{
	// IMPLEMENTADA EN LA INTERFAZ IAccessControl2
	return (E_NOTIMPL);

}



STDMETHODIMP CAccessControlTrans::Lnl_AddBadge( BADGE  *prs_Badge ,  BOOL vb_CheckForDup ,  BOOL vb_WaitForRsp )
{
	// IMPLEMENTADA EN LA INTERFAZ IAccessControl2
	return (E_NOTIMPL);
}



STDMETHODIMP CAccessControlTrans::Lnl_SetCardFormat( CARD_FORMAT_CFG *prs_CardCfg ,  BOOL vb_WaitForRsp )
{
	// TO DO: Need to implement this method
	return (E_NOTIMPL);
}

STDMETHODIMP CAccessControlTrans::Lnl_SetReaderModeControl( READER_MODE *prs_RM ,  BOOL vb_Wait )
{
	// TO DO: Need to implement this method
	return (E_NOTIMPL);
}

STDMETHODIMP CAccessControlTrans::Lnl_SetReaderOutputControl( READER_OUTPUT_CTRL *prs_RM ,  BOOL vb_Wait )
{
	// TO DO: Need to implement this method
	return (E_NOTIMPL);
}

STDMETHODIMP CAccessControlTrans::Lnl_SetReaderTimezoneList( READER_TZLIST *prs_List ,  BOOL vb_Wait )
{
	// TO DO: Need to implement this method
	return (E_NOTIMPL);
}


STDMETHODIMP CAccessControlTrans::Lnl_GetReaderStatus( READER_STATUS *pro_Status )
{
	pro_Status->si_PanelID = m_PanelID;
	pro_Status->sb_AsyncStatus =1;
	//pro_Status->sb_AsyncStatus =0;
//	pro_Status->sb_Devices[1] = 0;
	//pro_Status->sb_DeviceMode[1] = 0x91;
	pro_Status->sb_DeviceMode[1] = 0xff;
	pro_Status->sb_AlternateReaderStatus[1] = 0;

// Para que en poll se llame a setearAllPanelsOnLine.
	setearReaderStatus = true;

	return  S_OK;
}

STDMETHODIMP CAccessControlTrans::Lnl_ResetAPBForAllCardholders( long vb_TimeZone ,  BOOL vb_WaitForRsp )
{
	// TO DO: Need to implement this method
	return (E_NOTIMPL);
}

STDMETHODIMP CAccessControlTrans::Lnl_SetReaderAlarmMask( READER_ALARM_MASK *prs_RM ,  BOOL vb_Wait )
{
	// TO DO: Need to implement this method
	return (E_NOTIMPL);
}

STDMETHODIMP CAccessControlTrans::Lnl_SetReaderDefinition( READERDEF *prs_RDR ,  BOOL vb_Wait )
{
	// TO DO: Need to implement this method
	return (E_NOTIMPL);
}

STDMETHODIMP CAccessControlTrans::Lnl_AddAccessLevels( ACCESS_LEVEL *prs_AC ,  BOOL vb_Wait )
{
	
	return (S_OK);

}

STDMETHODIMP CAccessControlTrans::Lnl_AddDownloadAccessLevels( DWN_ACCESS_LEVEL *prs_AC ,  BOOL vb_Wait )
{
	// TO DO: Need to implement this method
	return (E_NOTIMPL);
}

STDMETHODIMP CAccessControlTrans::Lnl_DeleteAccessLevel( long vb_AccessLevel ,  BOOL vb_Wait )
{
	// TO DO: Need to implement this method
	return (E_NOTIMPL);
}

STDMETHODIMP CAccessControlTrans::Lnl_AddElevatorAccessLevel( ELEVATOR_ACCLEVEL *prs_Cmd,  BOOL vb_Wait )
{
	// TO DO: Need to implement this method
	return (E_NOTIMPL);
}

STDMETHODIMP CAccessControlTrans::Lnl_AddElevatorAccessLevelPerTZ(  ELEVATOR_ACCLEVEL_PERTZ *prs_Cmd,  BOOL vb_Wait )
{
	// TO DO: Need to implement this method
	return (E_NOTIMPL);
}

STDMETHODIMP CAccessControlTrans::Lnl_SetAreaAPBTable( AREA_APB_CFGMSG *prs_Area,  BOOL vb_Wait )
{
	// TO DO: Need to implement this method
	return (E_NOTIMPL);
}

STDMETHODIMP CAccessControlTrans::Lnl_ManualOpenDoor( int vi_ReaderID )
{
	// TO DO: Need to implement this method
	return (E_NOTIMPL);
}

STDMETHODIMP CAccessControlTrans::Lnl_SetAreaAPB( AREAAPB *prs_Area,  BOOL vb_Wait)
{
	// TO DO: Need to implement this method
	return (E_NOTIMPL);
}

STDMETHODIMP CAccessControlTrans::Lnl_AddCommandAccessLevel( CMD_ACCESS_LEVEL *prs_Cmd,  BOOL vb_Wait )
{
	// TO DO: Need to implement this method
	return (E_NOTIMPL);
}

STDMETHODIMP CAccessControlTrans::Lnl_DeleteCommandAccessLevel( long vb_AccessLevel ,  BOOL vb_Wait )
{
	// TO DO: Need to implement this method
	return (E_NOTIMPL);
}

STDMETHODIMP CAccessControlTrans::Lnl_DeleteDownloadAccessLevel( long vb_AccessLevel ,  BOOL vb_Wait )
{
	// TO DO: Need to implement this method
	return (E_NOTIMPL);
}

// Se da de baja una gate
STDMETHODIMP CAccessControlTrans::Lnl_DeleteReaderDefinition( int vb_Reader ,  BOOL vb_Wait )	
{
	
	return (S_OK);
}

STDMETHODIMP CAccessControlTrans::Lnl_SetControllerReaderDef( READERDEF *prs_RDR ,  BOOL vb_Wait )
{
	CString t = _T("EN VZ: Entra a Lnl_SetControllerReaderDef");

	DistributeDisplayTextMessage(t);

	CString ReaderIDString;
	ReaderIDString.Format(_T("%i"),prs_RDR->sb_ReaderNumber);
	CString total = _T("Llama a Lnl_SetControllerReaderDef con panelName= " + si_PanelName + _T(" con ReaderID=") +ReaderIDString);
	DistributeDisplayTextMessage(total);

	int m_ReaderID = prs_RDR->sb_ReaderNumber;
	if ((m_PanelID != 0 ) && ( si_PanelName !=""))
	{
		if (std::find(m_readers.begin(),m_readers.end(),m_ReaderID)==m_readers.end())
			{
				m_readers.push_back(m_ReaderID);			// Lo guarda para poder enviarle status offLine u onLine 

			
				CString total = _T("En VZ: SetReaderID de ") + si_PanelName + _T(" con ReaderID=") +ReaderIDString ;
				DistributeDisplayTextMessage(total);
			}	
	}
	
	return S_OK;
}

STDMETHODIMP CAccessControlTrans::Lnl_SetControllerAlarmDef( ALARM_PANEL *prs_AP ,  BOOL vb_Wait )
{
	// TO DO: Need to implement this method
	return (E_NOTIMPL);
}

STDMETHODIMP CAccessControlTrans::Lnl_ResetCardholderUseLimits(RESET_CARDHOLDER_USELIMITS  *prs_Badge , BOOL vb_WaitForRsp )
{
	// TO DO: Need to implement this method
	return (E_NOTIMPL);
}

STDMETHODIMP CAccessControlTrans::Lnl_ActivateElevatorOutput(long readerID, long outputID)
{
	// TO DO: Need to implement this method
	return (E_NOTIMPL);
}

STDMETHODIMP CAccessControlTrans::Lnl_AddHolidays( HOLIDAYMSG *prs_Holiday , BOOL vb_Wait )
{

	return (S_OK);
}

STDMETHODIMP CAccessControlTrans::Lnl_AddTimezone( TIMEZONE *prs_TZ , BOOL vb_Wait )
{
	return (S_OK);
}

STDMETHODIMP CAccessControlTrans::Lnl_ActivateTimezone( TIMEZONE *prs_TZ , BOOL vb_Wait )
{
	// TO DO: Need to implement this method
	return (E_NOTIMPL);
}

STDMETHODIMP CAccessControlTrans::Lnl_GetAreaStatus( AREA_STATUSRPT *prs_List, BOOL vb_Wait)
{
	// TO DO: Need to implement this method
	return (E_NOTIMPL);
}

STDMETHODIMP CAccessControlTrans::Lnl_ExecuteIV( EXECUTE_IV_MSG *prs_List, BOOL vb_Wait)
{
	// TO DO: Need to implement this method
	return (E_NOTIMPL);
}

STDMETHODIMP CAccessControlTrans::Lnl_SetFunctionList( IV_FUNCTION_LIST *prs_List, BOOL vb_Wait)
{
	// TO DO: Need to implement this method
	return (E_NOTIMPL);
}

STDMETHODIMP CAccessControlTrans::Lnl_SetZoneToIVLinks( ZONE_IV_LINKAGE *prs_List, BOOL vb_Wait)
{
	// TO DO: Need to implement this method
	return (E_NOTIMPL);
}

STDMETHODIMP CAccessControlTrans::Lnl_SetInterPanelLinks( ALARM_OUTPUT_LINKS_MSG *prs_API, BOOL vb_Wait)
{
	// TO DO: Need to implement this method
	return (E_NOTIMPL);
}

STDMETHODIMP CAccessControlTrans::Lnl_DeleteTimezone( long vb_Timezone , BOOL vb_Wait )
{
	// TO DO: Need to implement this method
	return (E_NOTIMPL);
}

STDMETHODIMP CAccessControlTrans::Lnl_SetCardExType(int vi_Value)
{
	// TO DO: Need to implement this method
	return (E_NOTIMPL);
}

STDMETHODIMP CAccessControlTrans::Lnl_GetCardExType(int *Value)
{
	// TO DO: Need to implement this method
	return (E_NOTIMPL);
}

STDMETHODIMP CAccessControlTrans::Lnl_SetCardRecCfg(CARD_REC_CFG *prs_Cfg)
{
	// TO DO: Need to implement this method
	return (E_NOTIMPL);
}

STDMETHODIMP CAccessControlTrans::Lnl_SetCardholderConfiguration( CARD_REC_CFG *prs_CardRecFmt , BOOL vb_WaitForRsp )
{
	// TO DO: Need to implement this method
	return (E_NOTIMPL);
}

STDMETHODIMP CAccessControlTrans::Lnl_SetUseTwoWireRS485(int vi_Value)
{
	// TO DO: Need to implement this method
	return (E_NOTIMPL);
}

STDMETHODIMP CAccessControlTrans::Lnl_GetUseTwoWireRS485(int *Value)
{
	// TO DO: Need to implement this method
	return (E_NOTIMPL);
}

STDMETHODIMP CAccessControlTrans::Lnl_GrantOneFreePass(GRANT_ONE_FREE_PASS *prs_FP, BOOL vb_Wait )
{
	// TO DO: Need to implement this method
	return (E_NOTIMPL);
}

STDMETHODIMP CAccessControlTrans::Lnl_DownloadReaderFirmware(long readerID,long readerType)
{
	// TO DO: Need to implement this method
	return (E_NOTIMPL);
}

STDMETHODIMP CAccessControlTrans::Lnl_ReaderDownload(READER_DOWNLOAD *pReaderDownload)
{
	return E_NOTIMPL;
}

STDMETHODIMP CAccessControlTrans::Lnl_HostAccessResponse(
							           long readerID, long badgeID, long command)
{
	return E_NOTIMPL;
}

//////////////////////////////////////////////////////////////////////////////
// IOutput Methods
//////////////////////////////////////////////////////////////////////////////

STDMETHODIMP CAccessControlTrans::Lnl_SetAlarmOutputTZ(ALARM_OUTPUT_MSG *prs_API,  BOOL vb_Wait )
{
	// TO DO: Need to implement this method
	return (E_NOTIMPL);
}

STDMETHODIMP CAccessControlTrans::Lnl_SetAlarmOutputCfg(ALARM_OUTPUT_MSG *prs_API,  BOOL vb_Wait )
{
	// TO DO: Need to implement this method
	return (E_NOTIMPL);
}

STDMETHODIMP CAccessControlTrans::Lnl_SetAlarmOutputMode(ALARM_OUTPUT_MSG *prs_API,  BOOL vb_Wait )
{
	// TO DO: Need to implement this method
	return (E_NOTIMPL);
}

STDMETHODIMP CAccessControlTrans::Lnl_SetAlarmOutputLinkage(ALARM_OUTPUT_MSG *prs_API,  BOOL vb_Wait )
{
	// TO DO: Need to implement this method
	return (E_NOTIMPL);
}

STDMETHODIMP CAccessControlTrans::Lnl_GetAlarmOutputStatus(ALARMPANEL_STATUS *prs_API)
{
	// TO DO: Need to implement this method
	return (E_NOTIMPL);
}

//////////////////////////////////////////////////////////////////////////////
// IInput Methods
//////////////////////////////////////////////////////////////////////////////

STDMETHODIMP CAccessControlTrans::Lnl_SetAlarmInputCfg( ALARM_INPUT_MSG *prs_API ,  BOOL vb_Wait )
{
	// TO DO: Need to implement this method
	return (E_NOTIMPL);
}

STDMETHODIMP CAccessControlTrans::Lnl_SetAlarmInputTZ( ALARM_INPUT_MSG *prs_API ,  BOOL vb_Wait )
{
	// TO DO: Need to implement this method
	return (E_NOTIMPL);
}

STDMETHODIMP CAccessControlTrans::Lnl_SetAlarmInputMask( ALARM_INPUT_MSG *prs_API ,  BOOL vb_Wait )
{
	// TO DO: Need to implement this method
	return (E_NOTIMPL);
}

STDMETHODIMP CAccessControlTrans::Lnl_SetZoneGroupDef( ZONE_GROUP_CFGMSG *prs_List,  BOOL vb_Wait)
{
	// TO DO: Need to implement this method
	return (E_NOTIMPL);
}

STDMETHODIMP CAccessControlTrans::Lnl_GetAlarmMaskGroupStatus( ALARMZONE_MASK_STATUSRPT *prs_List,  BOOL vb_Wait)
{
	// TO DO: Need to implement this method
	return (E_NOTIMPL);
}

STDMETHODIMP CAccessControlTrans::Lnl_GetAlarmInputStatus( ALARMPANEL_STATUS *pro_Status )
{
	// TO DO: Need to implement this method
	return (E_NOTIMPL);
}

STDMETHODIMP CAccessControlTrans::Lnl_SetAlarmInputEntryDelay(ALARM_INPUT_MSG *prs_API , BOOL vb_Wait)
{
	// TO DO: Need to implement this method
	return (E_NOTIMPL);
}

STDMETHODIMP CAccessControlTrans::Lnl_SetAlarmMaskGroupMaskLevel(long vl_MaskGroupID, long vl_MaskLevel)
{
	// TO DO: Need to implement this method
	return (E_NOTIMPL);
}



// Inicializa la DownloadArray para el download solo de readers.
void CAccessControlTrans::setDBArrayReadersOnly()
{
	for (int i =0; i<255; i++)
	{
		m_DatabaseDownloadArray[i] = 0x00;
	}

	m_DatabaseDownloadArray[0] = DATABASE_DOWNLOAD_READERS;				// para que le llegue al translator los Ids de los readers para ponerlos OnLine 
	m_DatabaseDownloadArray[1] = DATABASE_DOWNLOAD_NONE;		
}
