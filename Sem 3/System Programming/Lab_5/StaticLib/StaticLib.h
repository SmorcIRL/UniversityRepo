#include "pch.h"
#include "framework.h"

#include "windows.h"
#include <psapi.h>
#include <tchar.h>
#include <stdio.h>
#include <iostream>


void __stdcall  Execute();

BOOLEAN __stdcall GetAuthor(LPSTR buffer, DWORD dwBufferSize, DWORD* pdwBytesWritten);

BOOLEAN __stdcall GetDescription(LPSTR buffer, DWORD dwBufferSize, DWORD* pdwBytesWritten);