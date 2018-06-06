// AccessControlTrans.h : Declaration of the CAccessControlTrans

#ifndef __ACCESSCONTROLTRANS_H_
#define __ACCESSCONTROLTRANS_H_

#include "resource.h"       // main symbols
#include "DeviceTranslator\DeviceTranslator.h"
#include "MixedAccessControl.h"
#include<vector>
#include<map>
#include <algorithm>

/////////////////////////////////////////////////////////////////////////////
// CAccessControlTrans
class ATL_NO_VTABLE CAccessControlTrans : 
	public IOutput,
	public IInput,
	public IAccessControl,
	public CDeviceTranslator<CAccessControlTrans, &CLSID_AccessControlTrans>
{

private: 
	vector<int> m_readers;				// vector de iDs de readers. Se carga en Lnl_SetControllerReaderDef		 
	
	bool isInitCommSuccess;				// Indica si el proceso de inicializacion de la comunicacion fue exitoso
	bool isDatabaseUpdated;				// Flag que indica si la base de datos del panel ha sido actualizada
	bool isInitDownloadDB;				// Indica si el downloadDatabaseInicial fue lanzado (sin CardHoloders)
	//bool isFullDownload;				// indica si el downloadDatabase debe ser full o parcial

	//static const int MAXFAILSBEFOREDB= 10;		// Maxima cantidad de FAILCON antes de volver a generar un downloadDatabase
	//int failConCounter;							// Contador de la cantidad de FAILCON

	static const int CANT_ALARM_RETRY= 8;		// Cantidad de retry de envio de alarma al hacer AddMobile
	int contAlarma;								// Contador de retry de alarma

	BYTE* downloadArray;

	CManagedAccessControl* miManTraker;
	bool setearReaderStatus;			// Flag para indicar que deben actualizarse los Reader Status
	bool isAlwaysOffLine;				// Flag para poner el panel offLine si su dada de alta fallo.
public:
	CAccessControlTrans();
	~CAccessControlTrans();

DECLARE_REGISTRY_RESOURCEID(IDR_ACCESSCONTROLTRANS)

DECLARE_PROTECT_FINAL_CONSTRUCT()

BEGIN_COM_MAP(CAccessControlTrans)
	COM_INTERFACE_ENTRY(IOutput)
	COM_INTERFACE_ENTRY(IInput)
	COM_INTERFACE_ENTRY(IAccessControl)
	COM_INTERFACE_ENTRY(ITranslate)
	COM_INTERFACE_ENTRY(IComConfig)
	COM_INTERFACE_ENTRY(IConnectionPointContainer)
END_COM_MAP()
BEGIN_CONNECTION_POINT_MAP(CAccessControlTrans)
	CONNECTION_POINT_ENTRY(IID_IDistributeEvent)
END_CONNECTION_POINT_MAP()


/////////////////////////////////////////////////////
// DECLARACION DE METODOS   --  ALUTEL
/////////////////////////////////////////////////////

// ITranslate Methods
public:
	//STDMETHOD(Lnl_OnDBDownloadEvent)(DBDownloadEvent downloadEvent, DBDownloadObjectType compareObjType);
	STDMETHOD(Lnl_SetPanelID)(INT panel_id);
	STDMETHOD(Lnl_SetPanelName)(BSTR nombre);
	BOOL PollPanelForEvents(void);		// Llamada desde Lnl_PollPanelForEvents
	STDMETHOD(Lnl_InitCommunication)();
	BOOL InitPanel(void);				// Llamada desde Lnl_InitializePanel
	//STDMETHOD(Lnl_GetReaderStatus2)( READER_STATUS *pro_Status );
	//STDMETHOD(Lnl_IssueColdStartCmd)(void);

	//bool OnDatabaseDownloadEnd(DBDownloadEvent downloadEvent, DBDownloadObjectType compareObjType);
	//bool OnDatabaseDownloadBegin(DBDownloadEvent downloadEvent, DBDownloadObjectType compareObjType);

	bool addEventToAlarmMonitoring(CString dataToAdd, long v_serialNum);

	void setearAllReadersOnLine();
	void setearAllReadersOffLine();		// No se llama: quedan offLine automaticamente cuando el panel queda offLine.
	void lanzarThreadDownloadDB();		// Para informar al translator los readerIDs que se usan en el seteo del status

	void setDBArrayReadersOnly();

	void PollAndActualizeEvents();		// Hace un poll y si hay datos de accesos los levanta a Lenel
	bool updatePanelStatus();
	
	time_t convertirFechaHora(CStringA v_FechaHora);
	CString altaPanelFromLenel(CString v_panelName, int v_PanelID); 

	CString multipleAddEvents(CString eventData);

	DWORD enviarAlarmaAOnGuard(CString MessageText, BYTE event_type, short event_id, CStringA v_textoHora);

	CString construirStringAccessLevels(BADGE2* prs_Badge);
	CString obtenerFechaActivacion(BADGE2* prs_Badge);
	CString obtenerFechaDesactivacion(BADGE2* prs_Badge);

	void parsearYEnviarAlarma(CString encodedAlarm);

// IAccessControl Methods
public:
	STDMETHOD(Lnl_SetDownloadableReader)(READER_DWNSPEC *prs_RM ,  BOOL vb_Wait );
	STDMETHOD(Lnl_BulkAddBadge)( BULKBADGE  *prs_Badge ,  BOOL vb_CheckForDup ,  BOOL vb_WaitForRsp );
	STDMETHOD(Lnl_DeleteBadge)( long vl_CardNumber ,  BOOL vb_WaitForRsp );
	STDMETHOD(Lnl_AddBadge)( BADGE  *prs_Badge ,  BOOL vb_CheckForDup ,  BOOL vb_WaitForRsp );
	STDMETHOD(Lnl_SetCardFormat)( CARD_FORMAT_CFG *prs_CardCfg ,  BOOL vb_WaitForRsp );
	STDMETHOD(Lnl_SetReaderModeControl)( READER_MODE *prs_RM ,  BOOL vb_Wait );
	STDMETHOD(Lnl_SetReaderOutputControl)( READER_OUTPUT_CTRL *prs_RM ,  BOOL vb_Wait );
	STDMETHOD(Lnl_SetReaderTimezoneList)( READER_TZLIST *prs_List ,  BOOL vb_Wait );
	STDMETHOD(Lnl_GetReaderStatus)( READER_STATUS *pro_Status );
	STDMETHOD(Lnl_ResetAPBForAllCardholders)( long vb_TimeZone ,  BOOL vb_WaitForRsp );
	STDMETHOD(Lnl_SetReaderAlarmMask)( READER_ALARM_MASK *prs_RM ,  BOOL vb_Wait );
	STDMETHOD(Lnl_SetReaderDefinition)( READERDEF *prs_RDR ,  BOOL vb_Wait );
	STDMETHOD(Lnl_AddAccessLevels)( ACCESS_LEVEL *prs_AC ,  BOOL vb_Wait );
	STDMETHOD(Lnl_AddDownloadAccessLevels)( DWN_ACCESS_LEVEL *prs_AC ,  BOOL vb_Wait );
	STDMETHOD(Lnl_DeleteAccessLevel)( long vb_AccessLevel ,  BOOL vb_Wait );
	STDMETHOD(Lnl_AddElevatorAccessLevel)( ELEVATOR_ACCLEVEL *prs_Cmd,  BOOL vb_Wait );
	STDMETHOD(Lnl_AddElevatorAccessLevelPerTZ)(  ELEVATOR_ACCLEVEL_PERTZ *prs_Cmd,  BOOL vb_Wait );
	STDMETHOD(Lnl_SetAreaAPBTable)( AREA_APB_CFGMSG *prs_Area,  BOOL vb_Wait );
	STDMETHOD(Lnl_ManualOpenDoor)( int vi_ReaderID );
	STDMETHOD(Lnl_SetAreaAPB)( AREAAPB *prs_Area,  BOOL vb_Wait);
	STDMETHOD(Lnl_AddCommandAccessLevel)( CMD_ACCESS_LEVEL *prs_Cmd,  BOOL vb_Wait );
	STDMETHOD(Lnl_DeleteCommandAccessLevel)( long vb_AccessLevel ,  BOOL vb_Wait );
	STDMETHOD(Lnl_DeleteDownloadAccessLevel)( long vb_AccessLevel ,  BOOL vb_Wait );
	STDMETHOD(Lnl_DeleteReaderDefinition)( int vb_Reader ,  BOOL vb_Wait );	
	STDMETHOD(Lnl_SetControllerReaderDef)( READERDEF *prs_RDR ,  BOOL vb_Wait );
	STDMETHOD(Lnl_SetControllerAlarmDef)( ALARM_PANEL *prs_AP ,  BOOL vb_Wait );
	STDMETHOD(Lnl_ResetCardholderUseLimits)(RESET_CARDHOLDER_USELIMITS  *prs_Badge , BOOL vb_WaitForRsp );
	STDMETHOD(Lnl_ActivateElevatorOutput)(long readerID, long outputID);
	STDMETHOD(Lnl_AddHolidays)( HOLIDAYMSG *prs_Holiday , BOOL vb_Wait );
	STDMETHOD(Lnl_AddTimezone)( TIMEZONE *prs_TZ , BOOL vb_Wait );
	STDMETHOD(Lnl_ActivateTimezone)( TIMEZONE *prs_TZ , BOOL vb_Wait );
	STDMETHOD(Lnl_GetAreaStatus)( AREA_STATUSRPT *prs_List, BOOL vb_Wait);
	STDMETHOD(Lnl_ExecuteIV)( EXECUTE_IV_MSG *prs_List, BOOL vb_Wait);
	STDMETHOD(Lnl_SetFunctionList)( IV_FUNCTION_LIST *prs_List, BOOL vb_Wait);
	STDMETHOD(Lnl_SetZoneToIVLinks)( ZONE_IV_LINKAGE *prs_List, BOOL vb_Wait);
	STDMETHOD(Lnl_SetInterPanelLinks)( ALARM_OUTPUT_LINKS_MSG *prs_API, BOOL vb_Wait);
	STDMETHOD(Lnl_DeleteTimezone)( long vb_Timezone , BOOL vb_Wait );
	STDMETHOD(Lnl_SetCardExType)(int vi_Value);
	STDMETHOD(Lnl_GetCardExType)(int *Value);
	STDMETHOD(Lnl_SetCardRecCfg)(CARD_REC_CFG *prs_Cfg);
	STDMETHOD(Lnl_SetCardholderConfiguration)( CARD_REC_CFG *prs_CardRecFmt , BOOL vb_WaitForRsp );
	STDMETHOD(Lnl_SetUseTwoWireRS485)(int vi_Value);
	STDMETHOD(Lnl_GetUseTwoWireRS485)(int *Value);
	STDMETHOD(Lnl_GrantOneFreePass)(GRANT_ONE_FREE_PASS *prs_FP, BOOL vb_Wait );
	STDMETHOD(Lnl_DownloadReaderFirmware)(long readerID,long readerType);
	STDMETHOD(Lnl_ReaderDownload)(READER_DOWNLOAD *pReaderDownload);
	STDMETHOD(Lnl_HostAccessResponse)(long readerID, long badgeID, long command);


// IOutput Methods
public:
	STDMETHOD(Lnl_SetAlarmOutputTZ)(ALARM_OUTPUT_MSG *prs_API,  BOOL vb_Wait );
	STDMETHOD(Lnl_SetAlarmOutputCfg)(ALARM_OUTPUT_MSG *prs_API,  BOOL vb_Wait );
	STDMETHOD(Lnl_SetAlarmOutputMode)(ALARM_OUTPUT_MSG *prs_API,  BOOL vb_Wait );
	STDMETHOD(Lnl_SetAlarmOutputLinkage)(ALARM_OUTPUT_MSG *prs_API,  BOOL vb_Wait );
	STDMETHOD(Lnl_GetAlarmOutputStatus)(ALARMPANEL_STATUS *prs_API);

// IInput Methods
public:	
	STDMETHOD(Lnl_SetAlarmInputCfg)( ALARM_INPUT_MSG *prs_API ,  BOOL vb_Wait );
	STDMETHOD(Lnl_SetAlarmInputTZ)( ALARM_INPUT_MSG *prs_API ,  BOOL vb_Wait );
	STDMETHOD(Lnl_SetAlarmInputMask)( ALARM_INPUT_MSG *prs_API ,  BOOL vb_Wait );
	STDMETHOD(Lnl_SetZoneGroupDef)( ZONE_GROUP_CFGMSG *prs_List,  BOOL vb_Wait);
	STDMETHOD(Lnl_GetAlarmMaskGroupStatus)( ALARMZONE_MASK_STATUSRPT *prs_List,  BOOL vb_Wait);
	STDMETHOD(Lnl_GetAlarmInputStatus)( ALARMPANEL_STATUS *pro_Status );
	STDMETHOD(Lnl_SetAlarmInputEntryDelay)(ALARM_INPUT_MSG *prs_API , BOOL vb_Wait);
	STDMETHOD(Lnl_SetAlarmMaskGroupMaskLevel)(long vl_MaskGroupID, long vl_MaskLevel);
};

#endif //__ACCESSCONTROLTRANS_H_
