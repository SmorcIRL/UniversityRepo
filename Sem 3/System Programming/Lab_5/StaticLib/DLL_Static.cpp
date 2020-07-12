#include "pch.h"
#include "StaticLib.h"

#define AUTHOR "Timofei aka 12345 12345"
#define DESCRIPTION "This is a very useful static lib, honestly.\nIt can enumerate all device drivers in the system!"
#define ARRAY_SIZE 1024

using namespace std;

void __stdcall  Execute()
{
	LPVOID drivers[ARRAY_SIZE];
	DWORD cbNeeded;
	int cDrivers, i;

	if (EnumDeviceDrivers(drivers, sizeof(drivers), &cbNeeded) && cbNeeded < sizeof(drivers))
	{
		TCHAR szDriver[ARRAY_SIZE];

		cDrivers = cbNeeded / sizeof(drivers[0]);

		_tprintf(TEXT("There are %d drivers:\n"), cDrivers);

		for (i = 0; i < cDrivers; i++)
			if (GetDeviceDriverBaseName(drivers[i], szDriver, sizeof(szDriver) / sizeof(szDriver[0])))
				_tprintf(TEXT("%d: %s\n"), i + 1, szDriver);
	}

	cout << endl;
}

BOOLEAN __stdcall GetAuthor(LPSTR buffer, DWORD dwBufferSize, DWORD* pdwBytesWritten)
{
	int len = strlen(AUTHOR);

	if (len > dwBufferSize)
		return 1;

	strcpy_s(buffer, dwBufferSize, AUTHOR);
	*pdwBytesWritten = len;

	return 0;
}

BOOLEAN __stdcall GetDescription(LPSTR buffer, DWORD dwBufferSize, DWORD* pdwBytesWritten)
{
	int len = strlen(DESCRIPTION);

	if (len > dwBufferSize)
		return 1;

	strcpy_s(buffer, dwBufferSize, DESCRIPTION);
	*pdwBytesWritten = len;

	return 0;
}