// dllmain.h : Declaration of module class.

class CGenericCustomOptionsModule : public CAtlDllModuleT< CGenericCustomOptionsModule >
{
public :
	//DECLARE_LIBID(LIBID_CustomOptionsLib)
	DECLARE_REGISTRY_APPID_RESOURCEID(IDR_CUSTOMOPTIONS, "{03C785AE-B667-4794-AD98-28F3091F8C30}")
};

extern class CGenericCustomOptionsModule _AtlModule;
