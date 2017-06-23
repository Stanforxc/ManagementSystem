// dllmain.h: 模块类的声明。

class CCOMXiaModule : public ATL::CAtlDllModuleT< CCOMXiaModule >
{
public :
	DECLARE_LIBID(LIBID_COMXiaLib)
	DECLARE_REGISTRY_APPID_RESOURCEID(IDR_COMXIA, "{5276ECB9-0BA6-4E02-B4F4-88BCB7786C82}")
};

extern class CCOMXiaModule _AtlModule;
