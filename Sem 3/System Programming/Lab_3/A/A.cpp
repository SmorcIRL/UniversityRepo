#include <windows.h>
#include <stdio.h>
#include <TlHelp32.h>
#include <iostream>

using namespace std;

#define MAXLENGTH 256
#define PIPE_NAME L"\\\\.\\pipe\\Pipe_1"

void write_OK()
{
	cout << "OK" << endl;
}


DWORD GetProcessIdByName(const wstring& processName)
{
	PROCESSENTRY32 processInfo;
	processInfo.dwSize = sizeof(processInfo);

	HANDLE processesSnapshot = CreateToolhelp32Snapshot(TH32CS_SNAPPROCESS, NULL);
	if (processesSnapshot == INVALID_HANDLE_VALUE)
	{
		return -1;
	}

	Process32First(processesSnapshot, &processInfo);
	if (!processName.compare(processInfo.szExeFile))
	{
		CloseHandle(processesSnapshot);
		return processInfo.th32ProcessID;
	}

	while (Process32Next(processesSnapshot, &processInfo))
	{
		if (!processName.compare(processInfo.szExeFile))
		{
			CloseHandle(processesSnapshot);
			return processInfo.th32ProcessID;
		}
	}

	CloseHandle(processesSnapshot);
	return -1;
}


int main(void)
{
	HANDLE handle_b = INVALID_HANDLE_VALUE, handle_pipe = INVALID_HANDLE_VALUE;

	LPVOID ptr_to_write;
	DWORD count_write, count_read, mem_ptr;

	int szSize = sizeof(char) * MAXLENGTH;
	char str[sizeof(char) * MAXLENGTH];


	cout << "[FINDING B PROCESS]:      ";
	DWORD id_b = GetProcessIdByName(L"B.exe");
	if (id_b == -1) goto invalid; else write_OK();


	cout << "[OPENING B PROCESS]:      ";
	handle_b = OpenProcess(PROCESS_ALL_ACCESS, FALSE, id_b);
	if (handle_b == INVALID_HANDLE_VALUE) goto invalid; else write_OK();


	cout << "[ENTER STRING]:           ";
	cin >> str;


	cout << "[ALLOCATING MEMORY IN B]: ";
	ptr_to_write = VirtualAllocEx(handle_b, NULL, szSize, MEM_RESERVE | MEM_COMMIT, PAGE_EXECUTE_READWRITE);
	if (ptr_to_write == NULL) goto invalid; else write_OK();


	cout << "[WRITING IN B MEMORY]:    ";
	if (!WriteProcessMemory(handle_b, ptr_to_write, str, szSize, 0)) goto invalid; else write_OK();


	cout << "[CONNECTING TO PIPE]:     ";
	handle_pipe = CreateFile(PIPE_NAME, GENERIC_READ | GENERIC_WRITE, 0, NULL, OPEN_EXISTING, 0, NULL);
	if (handle_pipe == INVALID_HANDLE_VALUE) goto invalid; else write_OK();

	cout << "[WRITING IN PIPE]:        ";
	if (!WriteFile(handle_pipe, &ptr_to_write, sizeof(mem_ptr), &count_write, NULL)) goto invalid; else write_OK();


	cout << "[WAITING B ANSWER]:       ";
	if (!ReadFile(handle_pipe, &ptr_to_write, sizeof(ptr_to_write), &count_read, NULL)) goto invalid; else write_OK();


	cout << "[CLEARING B MEMORY]:      ";
	if (!VirtualFreeEx(handle_b, ptr_to_write, 0, MEM_RELEASE)) goto invalid; else write_OK();


	cout << "[CLOSING PIPE]:           ";
	if (!CloseHandle(handle_pipe)) goto invalid; else write_OK();


	cout << "[CLOSING B]:              ";
	if (!CloseHandle(handle_b)) goto invalid; else write_OK();


	cout << "[END OF PROCESS]" << endl;
	system("pause");
	return 0;

	invalid:
	cout << "ERROR" << endl;
	system("pause");
	return -1;
}