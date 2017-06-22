#include "stdafx.h"
#include "UriChecker.h"




bool checkUri(string uri) {

	string sensitive[] = {"create", "update", "drop", "select", "where", "from"};

	for (int i = 0; i < uri.length(); i++) {
		if (uri[i] <= 'Z' && uri[i] >= 'A') {
			uri[i] += 32;
		}
	}
	for (int j = 0; j < 6; j++) {
		if (uri.find(sensitive[j]) != string::npos) {
			return false;
		}
	}
	return true;
}

