#include "OpenCL.h"
#include "DeviceEx.h"
#include <iostream>
#include "KernelEx.h"
#include <iomanip>
#include <fstream>

using namespace std;

const char* FILENAME = "data.bin";
const char* TEXT = "Hello world!";

void Encode(const char* filename, const char* text)
{

}

const char* Decode(const char* filename)
{
	return "123";
}


int main(int argc, char* argv[])
{
	if (argc < 2)
	{
		cout << "Wrong args!";
	}
	else
	{
		if (strcmp(argv[1], "e") == 0)
		{
			Encode(FILENAME, TEXT);
		}
		else if (strcmp(argv[1], "d") == 0)
		{
			cout << Decode(FILENAME);
		}
		else
		{
			cout << "Wrong args!";
		}
	}
}