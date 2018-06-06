// MixedManagedHandHeldTracker.h


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

class NATIVEDLL_API CManagedTracker
{
public:
   // Initialization
   CManagedTracker(LPCTSTR pszName);
   virtual ~CManagedTracker();

   //// Métodos publicos
      tstring get_Nombre() const;
	  //void show_Image(unsigned int serial, LPCTSTR pszName);
	  void show_Event(unsigned int serialNum, unsigned int deviceID);
	  void show_Message(LPCTSTR pszMessage);

	  void live_Tracking(unsigned int deviceID);
	  void define_Zone(unsigned int deviceID);
	  void define_Virtual_Gate(unsigned int deviceID, unsigned int readerID,unsigned int v_orgID );
	  void device_Properties(unsigned int deviceID);
	  bool is_HandHeld_Installed();
	  bool is_VZone_Installed();

  	  void extended_Features(unsigned int deviceID);


private:
   // Embedded wrapper of an instance of a CLR class
   void* m_manTrackClr;
};
