// This is the main DLL file.

#include "stdafx.h"

#include "MixedManagedHandHeldTracker.h"

// Utilities for Native<>CLI conversions
#include <vcclr.h>

using namespace System;
using namespace Runtime::InteropServices;
//using namespace AdR::Samples::NativeCallingCLR::ClrAssembly;

using namespace ManagedHandHeldTracker;


CManagedTracker::CManagedTracker(LPCTSTR pszName)
{
   
   String^ str    = gcnew String(pszName);
   ManagedTracker^ manTrack = gcnew ManagedTracker(str);

   // Managed type conversion into unmanaged pointer is not allowed unless
   // we use "gcroot<>" wrapper.
   gcroot<ManagedTracker^> *pp = new gcroot<ManagedTracker^>(manTrack);

   this->m_manTrackClr = (void*)pp;
}

CManagedTracker::~CManagedTracker()
{
   if (this->m_manTrackClr)
   {
      // Get the CLR handle wrapper
      gcroot<ManagedTracker^> *pp = reinterpret_cast<gcroot<ManagedTracker^>*>(this->m_manTrackClr);
      this->m_manTrackClr = 0;
      // Delete the wrapper; this will release the underlying CLR instance
      delete pp;
   }
}

tstring CManagedTracker::get_Nombre() const
{
   tstring strNombre;

   if (this->m_manTrackClr != 0)
   {
      // Get the CLR handle wrapper
      gcroot<ManagedTracker^> *pp = reinterpret_cast<gcroot<ManagedTracker^>*>(this->m_manTrackClr);
      // Convert to std::string
      // Note:
      // - Marshaling is mandatory
      // - Do not forget to get the string pointer...
	  // ACA está la llamada al método getNombre de la clase mnanaged.
      strNombre = (const TCHAR*)Marshal::StringToHGlobalAuto(((ManagedTracker^)*pp)->getNombre()).ToPointer();
   }

   return strNombre;
}

void CManagedTracker::extended_Features(unsigned int panelID)
{
	if (this->m_manTrackClr != 0)
	{
		// Get the CLR handle wrapper
	    gcroot<ManagedTracker^> *pp = reinterpret_cast<gcroot<ManagedTracker^>*>(this->m_manTrackClr);
		((ManagedTracker^)*pp)->extendedFeatures(panelID);
	}
}


void CManagedTracker::show_Event(unsigned int serialNum, unsigned int deviceID)
{

	if (this->m_manTrackClr != 0)
	{
		// Get the CLR handle wrapper
	    gcroot<ManagedTracker^> *pp = reinterpret_cast<gcroot<ManagedTracker^>*>(this->m_manTrackClr);
		((ManagedTracker^)*pp)->showEvent(serialNum, deviceID);
	}
}

void CManagedTracker::live_Tracking(unsigned int deviceID)
{
	if (this->m_manTrackClr != 0)
	{
		// Get the CLR handle wrapper
	    gcroot<ManagedTracker^> *pp = reinterpret_cast<gcroot<ManagedTracker^>*>(this->m_manTrackClr);
		((ManagedTracker^)*pp)->liveTracking(deviceID);
	}
}

bool CManagedTracker::is_HandHeld_Installed()
{
	bool r = false;
	if (this->m_manTrackClr != 0)
	{

		// Get the CLR handle wrapper
	    gcroot<ManagedTracker^> *pp = reinterpret_cast<gcroot<ManagedTracker^>*>(this->m_manTrackClr);
		r= ((ManagedTracker^)*pp)->isHandHeldInstalled();
	}

	return r;
}

bool CManagedTracker::is_VZone_Installed()
{
	bool r = false;
	if (this->m_manTrackClr != 0)
	{

		// Get the CLR handle wrapper
	    gcroot<ManagedTracker^> *pp = reinterpret_cast<gcroot<ManagedTracker^>*>(this->m_manTrackClr);
		r= ((ManagedTracker^)*pp)->isVZoneInstalled();
	}

	return r;
}








void CManagedTracker::show_Message(LPCTSTR pszMessage)
{
	if (this->m_manTrackClr != 0)
	{
		// Get the CLR handle wrapper
		gcroot<ManagedTracker^> *pp = reinterpret_cast<gcroot<ManagedTracker^>*>(this->m_manTrackClr);

		String^ str = gcnew String(pszMessage);

		((ManagedTracker^)*pp)->showMessage(str);
		delete str;
	}
}


void CManagedTracker::define_Zone(unsigned int deviceID)
{
	if (this->m_manTrackClr != 0)
	{
		// Get the CLR handle wrapper
	    gcroot<ManagedTracker^> *pp = reinterpret_cast<gcroot<ManagedTracker^>*>(this->m_manTrackClr);
		((ManagedTracker^)*pp)->defineZone(deviceID);
	}
}


void CManagedTracker::device_Properties(unsigned int deviceID)
{
	if (this->m_manTrackClr != 0)
	{
		// Get the CLR handle wrapper
	    gcroot<ManagedTracker^> *pp = reinterpret_cast<gcroot<ManagedTracker^>*>(this->m_manTrackClr);
		((ManagedTracker^)*pp)->deviceProperties(deviceID);
	}
}


void CManagedTracker::define_Virtual_Gate(unsigned int deviceID, unsigned int readerID, unsigned int v_orgID)
{
	if (this->m_manTrackClr != 0)
	{
		// Get the CLR handle wrapper
	   // gcroot<ManagedTracker^> *pp = reinterpret_cast<gcroot<ManagedTracker^>*>(this->m_manTrackClr);
	//	((ManagedTracker^)*pp)->defineVirtualGate(deviceID,readerID,v_orgID);
	}
}


