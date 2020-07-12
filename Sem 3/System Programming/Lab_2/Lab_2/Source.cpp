#include <iostream>
#include <windows.h>
#include <tlhelp32.h>

using namespace std;

void main()
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
			cout << endl << endl << "============[" << string(name.begin(), name.end()) << "]============" << endl;
			cout << "Process ID:         " << process_entry.th32ProcessID << endl;
			cout << "Thread count:       " << process_entry.cntThreads << endl;
			cout << "Parent process ID:  " << process_entry.th32ParentProcessID << endl;
			cout << "Priority:           " << process_entry.pcPriClassBase << endl << endl;

			HANDLE handle_modules = CreateToolhelp32Snapshot(TH32CS_SNAPMODULE, process_entry.th32ProcessID);
			HANDLE handle_threads = CreateToolhelp32Snapshot(TH32CS_SNAPTHREAD, process_entry.th32ProcessID);

			MODULEENTRY32 module_entry;
			THREADENTRY32 thread_entry;

			module_entry.dwSize = sizeof(MODULEENTRY32);
			thread_entry.dwSize = sizeof(THREADENTRY32);

			cout << "MODULES:" << endl << endl;
			if (Module32First(handle_modules, &module_entry))
			{
				do
				{
					wstring module(module_entry.szModule);
					wstring path(module_entry.szExePath);
					cout << "  Module name:  " << string(module.begin(), module.end()) << endl;
					cout << "  Path:         " << string(path.begin(), path.end()) << endl;
					cout << "  Process ID:   " << module_entry.th32ProcessID << endl;
					cout << "  Size:         " << module_entry.modBaseSize << endl << endl;
				}
				while (Module32Next(handle_modules, &module_entry));
			}

			cout << "THREADS:" << endl << endl;
			if (Thread32First(handle_threads, &thread_entry))
			{
				do
				{
					if (thread_entry.th32OwnerProcessID == process_entry.th32ProcessID)
					{
						cout << "  Thread ID:  " << thread_entry.th32ThreadID << endl;
						cout << "  Priority:   " << thread_entry.tpBasePri << endl << endl;
					}
				}
				while (Thread32Next(handle_threads, &thread_entry));
			}

			CloseHandle(handle_modules);
			CloseHandle(handle_threads);
		}

		while (Process32Next(handle_process, &process_entry));
	}

	CloseHandle(handle_process);

	system("pause");
}