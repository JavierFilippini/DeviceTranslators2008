// MixedAccessControl.h

#pragma once
#ifdef NATIVEDLL_EXPORTS
   #define NATIVEDLL_API __declspec(dllexport)
#else
   #define NATIVEDLL_API __declspec(dllimport)
#endif

#include <string>
using namespace std;

#ifdef _UNICODE
   typedef wstring tstring;
#else
   typedef string tstring;
#endif

class NATIVEDLL_API CManagedAccessControl
{
public:
   // Initialization
   CManagedAccessControl();
   virtual ~CManagedAccessControl();
   void Test_Call(LPCTSTR pszPanelName) const;

   //// Métodos publicos.
   // NOTACION; Los nombres de los metodos llevan el signo _ y se escriben igual que los de la clase Managed.
	void set_Name(LPCTSTR vp_Name);
	void set_PanelID(unsigned int v_ID);
    //tstring get_Name() const;
	//void show_Image(unsigned int serial, LPCTSTR vp_Name);
	//void show_Message(LPCTSTR vp_Message);
	tstring alta_Panel_WS(LPCTSTR pszPanelName, unsigned int panelID) const;
	//tstring add_Panel_From_Lenel(LPCTSTR pszPanelName, unsigned int panelID, int v_isDownloading) const;
	
	//tstring poll_Alutrack_For_Event(LPCTSTR pszPanelName,unsigned int panelID, unsigned int organizationID) const;
	WCHAR * poll_Alutrack_For_Event(LPCTSTR pszPanelName,unsigned int panelID, unsigned int organizationID) const;

	void FreeMarshalString (WCHAR* strToFree) const;

	//tstring poll_Alutrack_For_Alarm(unsigned int panelID, unsigned int organizationID) const;
	WCHAR * poll_Alutrack_For_Alarm(unsigned int panelID, unsigned int organizationID) const;
	
	tstring add_Reader( LPCTSTR pszPanelName, unsigned int panelID,LPCTSTR pszReaderName,unsigned int readerID ,unsigned int readerEntranceType,unsigned int organizationID,LPCTSTR pszCardFormats,int isDownloading) const;

	tstring add_Employee(LPCTSTR v_badge, unsigned int v_panelID, LPCTSTR v_accessLevels, LPCTSTR fechaActivacion,  LPCTSTR fechaDesactivacion, LPCTSTR PIN, int isDownloading) const; 

	void del_Employee(LPCTSTR vl_CardNumber, unsigned int v_panelID) const;
	tstring add_AccessLevel(unsigned int panelID,unsigned int OrganizationID, unsigned int accessLevelID,LPCTSTR readerTZString, int isDownloading ) const;

	tstring add_Holidays(unsigned int PanelID,unsigned int m_OrganizationID,LPCTSTR stringToSend, int isDownloading) const;
	tstring add_Timezone(unsigned int m_PanelID,unsigned int m_OrganizationID,unsigned int TZNumber,LPCTSTR stringToSend, int isDownloading) const;

	//void download_Lista_Empleados(unsigned int PanelID,unsigned int m_OrganizationID) const;
	/*void notify_Download(unsigned int PanelID) const;
	void notify_Finish_Download(unsigned int PanelID) const;*/

	void delete_Panel(unsigned int v_panelID, LPCTSTR panelName) const;
	void delete_Reader(unsigned int v_panelID,unsigned int readerID) const;

	//tstring get_Conn_Status(unsigned int PanelID, unsigned int isDownloading) const;
	WCHAR * get_Conn_Status(unsigned int PanelID, unsigned int isDownloading) const;

	//bool is_panel_active() const;
	
	void increment_Employees_Counter() const;	 // Para llevar el control de la licencia
	unsigned int remaining_Employees() const;    // Para llevar el control de la licencia

	void send_ID_Serials(LPCTSTR IDSerialnums) const;

	void reset_Badge_Accesslevels() const;		// Para resetear los BadgeAccesslevels antes de lanzar el DownoladDatabase

	void add_CardFormat(unsigned int FormatID, unsigned int m_PanelID,unsigned int BitSize,unsigned int FC,unsigned int Offset,unsigned int BitsFC,unsigned int PositionStartFC,unsigned int BitsCardNum,unsigned int PositionStartCN,unsigned int BitsIssueCode,unsigned int PositionStartIC,int m_IsDownloadInProgress) const;

	void enviar_Borrar_CF(unsigned int m_PanelID) const;

	void Asignar_Serial_A_Alarma(unsigned int v_alarmID, unsigned int v_serialNum, LPCTSTR tipoAlarma) const;
	
	bool is_Download_Sent() const;

	void set_Is_Download_Sent() const;

	void reset_Is_Download_Sent() const;

	void add_Panel_ID(unsigned int panelID) const;
	void add_Panel_Name(LPCTSTR pszPanelName) const;


private:
   // Embedded wrapper of an instance of a CLR class
   void* m_manAccessControlClr;
};

