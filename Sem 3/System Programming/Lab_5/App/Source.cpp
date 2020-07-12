#include "StaticLib.h"
#include <iostream>
#include "windows.h"

#define AUTHOR_LENGTH 64
#define DESCRIPTION_LENGTH 256
#define MODULE_NAME_LENGTH 64

using namespace std;

int main()
{
	DWORD
		author_real_length = 0,
		description_real_length = 0;

	LPSTR	
		author_lib = new CHAR[AUTHOR_LENGTH],
		description_lib = new CHAR[DESCRIPTION_LENGTH];

	cout << "==============[Dynamic lib]==============" << endl;

	HMODULE dll = LoadLibrary("Dll.dll");

	if (dll != NULL)
	{
		char mod_name[MODULE_NAME_LENGTH];

		GetModuleFileName(dll, (LPTSTR)mod_name, MODULE_NAME_LENGTH);
		cout << endl << "Module name: "; printf("%s \n", mod_name);

		void(__stdcall * ExecuteFoo)();
		BOOLEAN(__stdcall * GetAuthorFoo)(LPSTR buffer, DWORD dwBufferSize, DWORD * pdwBytesWritten);
		BOOLEAN(__stdcall * GetDescriptionFoo)(LPSTR buffer, DWORD dwBufferSize, DWORD * pdwBytesWritten);

		(FARPROC&)ExecuteFoo = GetProcAddress(dll, "_Execute@0");
		(FARPROC&)GetAuthorFoo = GetProcAddress(dll, "_GetAuthor@12");
		(FARPROC&)GetDescriptionFoo = GetProcAddress(dll, "_GetDescription@12");

		char res_a = 1, res_d = 1, res_e = 1;

		if (GetAuthorFoo != NULL)
		{
			res_a = GetAuthorFoo(author_lib, AUTHOR_LENGTH, &author_real_length);
		}

		if (GetDescriptionFoo != NULL)
		{
			res_d = GetDescriptionFoo(description_lib, DESCRIPTION_LENGTH, &description_real_length);
		}

		if (ExecuteFoo != NULL)
		{
			res_e = 0;
		}

		cout << endl << "Author:      "; res_a == 0 ? printf("%s \n", author_lib) : printf("Error \n");
		cout << endl << "Description: "; res_d == 0 ? printf("%s \n", description_lib) : printf("Error \n");
		cout << endl << "Execute:     " << endl; res_e == 0 ? ExecuteFoo() : (void)printf("Error \n");

		FreeLibrary(dll);
	}
	else
	{
		cout << endl << "Error! DLL not found!";
	}

	system("pause");
}