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

int main(void)
{
	DWORD mem_ptr, read_ptr, write_ptr;

	cout << "[CREATING PIPE]:           ";
	HANDLE handle_pipe = CreateNamedPipe(PIPE_NAME, PIPE_ACCESS_DUPLEX, PIPE_TYPE_MESSAGE | PIPE_READMODE_MESSAGE | PIPE_WAIT, PIPE_UNLIMITED_INSTANCES, sizeof(DWORD), sizeof(DWORD), 100, NULL);
	if (handle_pipe == INVALID_HANDLE_VALUE) goto invalid; else write_OK();


	ConnectNamedPipe(handle_pipe, NULL);


	cout << "[READING FROM PIPE]:       ";
	if (!ReadFile(handle_pipe, &mem_ptr, sizeof(mem_ptr), &read_ptr, NULL)) goto invalid; else write_OK();


	cout << "[GOT STRING]:              " << string((char*)mem_ptr) << endl;


	cout << "[REPLYING TO A]:           ";
	if (!WriteFile(handle_pipe, &mem_ptr, sizeof(mem_ptr), &write_ptr, NULL)) goto invalid; else write_OK();


	cout << "[DISCONNECTING FROM PIPE]: ";
	if (!DisconnectNamedPipe(handle_pipe)) goto invalid; else write_OK();


	cout << "[CLOSING PIPE]:            ";
	if (!CloseHandle(handle_pipe)) goto invalid; else write_OK();


	cout << "[END OF PROCESS]" << endl;
	system("pause");
	return 0;

	invalid:
	cout << "ERROR" << endl;
	system("pause");
	return -1;
}