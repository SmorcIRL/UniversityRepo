#include "OpenCL.h"
#include "DeviceEx.h"
#include <iostream>
#include "KernelEx.h"
#include <iomanip>

using namespace std;
using namespace cl;

class Matrix
{
public:
	int Rows;
	int Columns;
	int* Data;

	Matrix(int rows, int columns, int* data) : Rows(rows), Columns(columns), Data(data) { }

	Matrix Transpose() const
	{
		auto data = new int[Rows * Columns];

		for (auto i = 0; i < Rows; ++i)
			for (auto j = 0; j < Columns; ++j)
				data[j * Rows + i] = Get(i, j);

		return Matrix(Columns, Rows, data);
	}

	int Get(int i, int j) const
	{
		return Data[i * Columns + j];
	}

	void Dispose()
	{
		delete[] Data;
		Data = nullptr;
	}
};

class Main
{
public:
	static void RunOnDevice(int n_p, int m_p, int l_p)
	{
		auto device = DeviceEx::Find("Intel");

		int n = n_p, m = m_p, l = l_p;

		Matrix matrix_1 = CreateMatrix(n, m);
		Matrix matrix_2 = CreateMatrix(m, l);
		Matrix matrix_2_T = matrix_2.Transpose();

		PrintMatrix(matrix_1);
		PrintMatrix(matrix_2);

		Context context = DeviceEx::CreateContext(device);

		Buffer buffer_matrix_1(context, CL_MEM_READ_ONLY | CL_MEM_COPY_HOST_PTR, matrix_1.Rows * matrix_1.Columns * sizeof(int), matrix_1.Data);
		Buffer buffer_matrix_2_T(context, CL_MEM_READ_ONLY | CL_MEM_COPY_HOST_PTR, matrix_2_T.Rows * matrix_2_T.Columns * sizeof(int), matrix_2_T.Data);

		RunKernel(device, context, buffer_matrix_1, buffer_matrix_2_T, n, m, l, "MultiplyMatrices");
		RunKernel(device, context, buffer_matrix_1, buffer_matrix_2_T, n, m, l, "MultiplyByVectors");

		matrix_1.Dispose();
		matrix_2.Dispose();
		matrix_2_T.Dispose();
	}

private:

	static void RunKernel(const Device& device, const Context& context, Buffer buffer_matrix_1, Buffer buffer_matrix_2, int n, int m, int l, string kernelName)
	{
		Matrix matrix_3(n, l, new int[n * l]{});
		Buffer buffer_matrix_3(context, CL_MEM_READ_WRITE | CL_MEM_COPY_HOST_PTR, matrix_3.Rows * matrix_3.Columns * sizeof(int), matrix_3.Data);

		Kernel kernel = KernelEx::BuildFromFile(device, context, "Kernel.cl", kernelName);

		kernel.setArg(0, buffer_matrix_1);
		kernel.setArg(1, buffer_matrix_2);
		kernel.setArg(2, buffer_matrix_3);
		kernel.setArg(3, n);
		kernel.setArg(4, m);
		kernel.setArg(5, l);

		Event event = Event();
		CommandQueue queue = CommandQueue(context, device, CL_QUEUE_PROFILING_ENABLE);

		queue.enqueueNDRangeKernel(kernel, NullRange, NDRange(matrix_3.Rows, matrix_3.Columns), NullRange, nullptr, &event);
		queue.finish();

		auto start = event.getProfilingInfo<CL_PROFILING_COMMAND_START>();
		auto end = event.getProfilingInfo<CL_PROFILING_COMMAND_END>();

		cout << fixed << setprecision(3) << (end - start) / 1000000.0 << endl;

		queue.enqueueReadBuffer(buffer_matrix_3, CL_TRUE, 0, matrix_3.Rows * matrix_3.Columns * sizeof(int), matrix_3.Data);

		PrintMatrix(matrix_3);
	}

	static Matrix CreateMatrix(int n, int m)
	{
		srand(0);

		int size = n * m;
		int* matrix_data = new int[size];

		for (auto k = 0; k < size; ++k)
			matrix_data[k] = (rand() % 2) * (rand() % 2 == 1 ? -1 : 1);

		return Matrix(n, m, matrix_data);
	}

	static void PrintMatrix(Matrix matrix)
	{
		for (auto i = 0; i < matrix.Rows; ++i)
		{
			for (auto j = 0; j < matrix.Columns; ++j)
				cout << setw(2) << matrix.Get(i, j) << " ";

			cout << endl;
		}
		cout << endl;
	}
};

int main(int argc, char* argv[])
{
	try
	{
		if (argc != 4)
		{
			cout << "Wrong args!" << endl;
			return 1;
		}

		Main::RunOnDevice(stoi(argv[1]), stoi(argv[2]), stoi(argv[3]));
	}
	catch (const exception & e)
	{
		cout << "Exeption: " << e.what() << endl;
	}
}