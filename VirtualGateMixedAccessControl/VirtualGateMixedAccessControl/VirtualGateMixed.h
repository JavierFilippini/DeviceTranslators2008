// VirtualGateMixed.h


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

class NATIVEDLL_API CVirtualGateManaged
{
public:
   // Initialization
   CVirtualGateManaged();
   virtual ~CVirtualGateManaged();

   //// Métodos publicos.
   // NOTACION; Los nombres de los metodos llevan el signo _ y se escriben igual que los de la clase Managed.
	void show_Message(LPCTSTR vp_Message);

	tstring alta_Panel_WS(LPCTSTR pszPanelName, unsigned int panelID) const;
	//tstring add_Zone_From_Lenel(LPCTSTR pszPanelName, unsigned int panelID, int v_isDownloadingDB) const;
	
	void delete_Zone(unsigned int m_PanelID) const;

	tstring add_Gate( LPCTSTR pszPanelName, unsigned int panelID,LPCTSTR pszReaderName,unsigned int readerID ,unsigned int readerEntranceType, int v_isDownloadingDB) const;
	void delete_Gate(unsigned int v_panelID,unsigned int v_readerID) const;

	tstring add_Employee(LPCTSTR v_badge, unsigned int v_panelID, LPCTSTR v_accessLevels, LPCTSTR fechaActivacion,  LPCTSTR fechaDesactivacion,LPCTSTR v_PIN, int isDownloading) const; 
	void del_Employee(LPCTSTR vl_CardNumber, unsigned int v_panelID) const;
	tstring add_AccessLevel(unsigned int panelID,unsigned int OrganizationID, unsigned int accessLevelID,LPCTSTR readerTZString, int isDownloading ) const;

	tstring add_Holidays(unsigned int PanelID,unsigned int m_OrganizationID,LPCTSTR stringToSend, int isDownloading) const;
	tstring add_Timezone(unsigned int m_PanelID,unsigned int m_OrganizationID,unsigned int TZNumber,LPCTSTR stringToSend, int isDownloading) const;
    WCHAR* poll_Alutrack_For_Event(LPCTSTR pszPanelName,unsigned int panelID, unsigned int organizationID) const;

	void FreeMarshalString (WCHAR* strToFree) const;

	WCHAR* poll_Alutrack_For_Alarm(unsigned int panelID, unsigned int organizationID) const;

	WCHAR* get_Conn_Status(unsigned int PanelID,unsigned int m_OrganizationID) const;

	//bool is_panel_active() const;
	void increment_VG_Counter() const;		// Para llevar el control de la licencia
	unsigned int remaining_VGates() const;  // Para llevar el control de la licencia

	void send_ID_Serials(LPCTSTR IDSerialnums) const;

	void reset_Badge_Accesslevels() const;		// Para resetear los BadgeAccesslevels antes de lanzar el DownoladDatabase

	void Asignar_Serial_A_Alarma(unsigned int v_alarmID, unsigned int v_serialNum, LPCTSTR tipoAlarma) const;
	void add_Panel_ID(unsigned int panelID) const;
	void add_Panel_Name(LPCTSTR pszPanelName) const;

private:
   // Embedded wrapper of an instance of a CLR class
   void* m_manAccessControlClr;
};
