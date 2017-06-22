// Win32.cpp : 定义 DLL 应用程序的导出函数。
//

#include "stdafx.h"

using namespace std;

extern "C" __declspec(dllexport) BOOL checkUri(LPCTSTR lpRootPathName) {

	std::string sensitive[] = { "create", "update", "drop", "select", "where", "from" };

	int num = WideCharToMultiByte(CP_OEMCP, NULL, lpRootPathName, -1, NULL, 0, NULL, FALSE);
	char *pchar = new char[num];
	WideCharToMultiByte(CP_OEMCP, NULL, lpRootPathName, -1, pchar, num, NULL, FALSE);

	std::string str(pchar);

	for (int i = 0; i < 6; i++) {
		if (str.find(sensitive[i]) != string::npos) {
			return false;
		}
	}

	return true;
}


