#pragma once

#ifdef DLL_EXPORTS
#define DLL_API __declspec(dllexport)
#else
#define DLL_API __declspec(dllimport)
#endif

#include <iostream>
#include <tlhelp32.h>

using namespace std;

extern "C" DLL_API void __stdcall Execute();
extern "C" DLL_API BOOLEAN __stdcall GetAuthor(LPSTR buffer, DWORD dwBufferSize, DWORD * pdwBytesWritten);
extern "C" DLL_API BOOLEAN __stdcall GetDescription(LPSTR buffer, DWORD dwBufferSize, DWORD * pdwBytesWritten);