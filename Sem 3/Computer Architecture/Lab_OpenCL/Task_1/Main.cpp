#include "OpenCL.h"
#include "KernelEx.h"
#include "DeviceEx.h"
#include <iostream>
#include <iomanip>
#include <fstream>

using namespace std;
using namespace cl;

class Matrix
{
public:
	int Rows;
	int Columns;
	int* DataArray;

	Matrix()
	{
	}

	Matrix(int rows, int columns, int* data_array)
	{
		Rows = rows;
		Columns = columns;
		DataArray = data_array;
	}

	int GetCell(int i, int j) const
	{
		return DataArray[i * Columns + j];
	}

	void Dispose()
	{
		delete[] DataArray;
		DataArray = nullptr;
	}
};

class Main
{
public:
	static int* CharsToInts(const char* str)
	{
		int size = strlen(str);
		int* result = new int[size];

		for (int i = 0; i < size; i++)
			result[i] = (int)str[i];

		return result;
	}

	static void Encode(const char* path, const char* string)
	{
		Device device;
		Context context;
		Matrix matrix;

		Init(device, context, path, matrix);

		int* int_text = CharsToInts(string);

		Buffer matrix_buffer(context, CL_MEM_READ_WRITE | CL_MEM_COPY_HOST_PTR, matrix.Rows * matrix.Columns * sizeof(int), matrix.DataArray);
		Buffer text_buffer(context, CL_MEM_READ_WRITE | CL_MEM_COPY_HOST_PTR, strlen(string) * sizeof(int), int_text);

		RunKernel(device, context, matrix_buffer, text_buffer, strlen(string), matrix.Rows, matrix.Columns, "Encode", path, "e");

		matrix.Dispose();
	}

	static void Decode(const char* path)
	{
		Device device;
		Context context;
		Matrix matrix;

		Init(device, context, path, matrix);

		Buffer matrix_buffer(context, CL_MEM_READ_WRITE | CL_MEM_COPY_HOST_PTR, matrix.Rows * matrix.Columns * sizeof(int), matrix.DataArray);
		Buffer text_buffer(context, CL_MEM_READ_WRITE | CL_MEM_COPY_HOST_PTR, matrix.Columns * sizeof(int), new int[matrix.Columns]);

		RunKernel(device, context, matrix_buffer, text_buffer, matrix.Columns * sizeof(int), matrix.Rows, matrix.Columns, "Decode", path, "d");

		matrix.Dispose();
	}

private:

	static void Init(Device& device, Context& context, const char* path, Matrix& matrix)
	{
		device = DeviceEx::Find("NVIDIA");
		context = DeviceEx::CreateContext(device);
		fstream bin_source(path, ios_base::in | ios_base::binary);

		int n, m;
		bin_source.read((char*)(&n), sizeof(int));
		bin_source.read((char*)(&m), sizeof(int));

		n = _byteswap_ulong(n);
		m = _byteswap_ulong(m);

		auto size = n * m;
		auto data_array = new int[size];

		for (auto k = 0; k < size; ++k)
			bin_source.read((char*)(&data_array[k]), sizeof(int));

		matrix = Matrix(n, m, data_array);

		bin_source.close();
	}

	static void RunKernel(const Device& device, const Context& context, Buffer matrix_buffer, Buffer text_buffer, int strLen, int n, int m, string kernelName, const char* path, const char* mode)
	{
		auto kernel = KernelEx::BuildFromFile(device, context, "Kernel.cl", kernelName);

		kernel.setArg(0, text_buffer);
		kernel.setArg(1, matrix_buffer);
		kernel.setArg(2, n);
		kernel.setArg(3, m);
		kernel.setArg(4, strLen);

		if (strcmp(mode, "e") == 0)
		{
			CommandQueue queue = CommandQueue(context, device);

			queue.enqueueNDRangeKernel(kernel, NullRange, NDRange(m));

			queue.finish();

			Matrix matrix(n, m, new int[n * m]{});

			queue.enqueueReadBuffer(matrix_buffer, CL_TRUE, 0, matrix.Rows * matrix.Columns * sizeof(int), matrix.DataArray);

			matrix.DataArray[strLen] = -1;

			WriteMatrixToBin(matrix, path);
		}
		else if (strcmp(mode, "d") == 0)
		{
			auto queue = CommandQueue(context, device);

			queue.enqueueNDRangeKernel(kernel, NullRange, NDRange(m));

			queue.finish();

			int* int_string = new int[m];

			queue.enqueueReadBuffer(text_buffer, CL_TRUE, 0, m * sizeof(int), int_string);

			for (int i = 0; int_string[i] != -1; ++i)
				cout << (char)int_string[i];

			cout << endl;
		}
	}

	static void WriteMatrixToBin(Matrix mat, const char* path)
	{
		ofstream bin_output(path, ios_base::binary);

		int n = _byteswap_ulong(mat.Rows);
		int m = _byteswap_ulong(mat.Columns);

		bin_output.write((char*)(&n), sizeof(int));
		bin_output.write((char*)(&m), sizeof(int));

		for (int i = 0; i < mat.Rows; i++)
		{
			for (int j = 0; j < mat.Columns; j++)
			{
				int c = mat.GetCell(i, j);
				bin_output.write((char*)(&c), sizeof(int));
			}
		}

		bin_output.close();
	}
};

int main(int argc, char* argv[])
{
	try
	{
		if (argc < 3)
		{
			cout << "Wrong args!" << endl;
		}
		else if (strcmp(argv[1], "e") == 0)
		{
			Main::Encode(argv[2], argv[3]);
		}
		else if (strcmp(argv[1], "d") == 0)
		{
			Main::Decode(argv[2]);
		}
		else
		{
			cout << "Wrong args!" << endl;
		}

		system("pause");
	}
	catch (const exception & e)
	{
		cout << "Exeption: " << e.what() << endl;
	}
}