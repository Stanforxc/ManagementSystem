// CLI.h

#pragma once

using namespace System;
#using<system.dll>  
using namespace System;
using namespace System::IO;

namespace CLI {

	public ref class ExceptionLog
	{
		// TODO:  �ڴ˴���Ӵ���ķ�����
		String^ fileName = "ExceptionLog.log";
	public:
		void log(String^ content);
	};
}
