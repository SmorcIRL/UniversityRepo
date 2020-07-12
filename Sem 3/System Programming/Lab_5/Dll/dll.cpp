#include "pch.h"
#include "dll.h"

#define AUTHOR "author"
#define DESCRIPTION "This is a very useful dll, honestly"

void __stdcall Execute()
{
	HANDLE handle_process = CreateToolhelp32Snapshot(TH32CS_SNAPPROCESS, 0);
	HANDLE process_handle;

	PROCESSENTRY32 process_entry;
	process_entry.dwSize = sizeof(PROCESSENTRY32);

	if (Process32First(handle_process, &process_entry))
	{
		do
		{
			wstring name(process_entry.szExeFile);
			cout << string(name.begin(), name.end()) << endl;
		} while (Process32Next(handle_process, &process_entry));
	}

	cout << endl;

	CloseHandle(handle_process);
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