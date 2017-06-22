#pragma once
#ifdef A_EXPORTS
#define DLL_API __declspec(dllexport)
#else
#define DLL_API __declspec(dllimport)
#endif

#include <string>
using namespace std;

extern "C" DLL_API bool checkUri(string uri);