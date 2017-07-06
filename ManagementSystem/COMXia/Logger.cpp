// Logger.cpp : CLogger µÄÊµÏÖ

#include "stdafx.h"
#include "Logger.h"
#include <iostream>
#include <fstream>
#include <time.h>
#include <comdef.h>
#include <comutil.h>

using namespace std;


// CLogger



STDMETHODIMP CLogger::serialize(BSTR method, BSTR uri)
{
	time_t t;
	char buf[100];
	t = time(NULL);
	strftime(buf, 100, "%Y-%m-%d-%H:%M:%S", localtime(&t));

	//[time] [method] url

	char* method_c = _com_util::ConvertBSTRToString(method);

	char* uri_c = _com_util::ConvertBSTRToString(uri);

	string log_line;

	log_line.append("[").append(buf).append("] ").append("[").append(method_c).append("] ").append(uri_c).append("\n");

	ofstream of;

	of.open("httpRequest.log", ios::out | ios::app);

	of << log_line.c_str();

	of.close();

	return S_OK;
}
