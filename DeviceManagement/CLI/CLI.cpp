// 这是主 DLL 文件。

#include "stdafx.h"

#include "CLI.h"


void CLI::ExceptionLog::log(String^ content) {
	try {
		StreamWriter^ sw = gcnew StreamWriter(fileName);
		sw->Write("[");
		sw->Write(DateTime::Now);
		sw->Write("] " + content->ToCharArray() + "\n");
		sw->Close();
	}
	catch (Exception ^e) {
	}
}

