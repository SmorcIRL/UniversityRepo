#include <mpi.h>
#include <iostream>
#include <random>
#include <chrono> 

using namespace std::chrono;
using namespace std;

const int masterRank = 0;

int main(int argc, char** argv)
{
	MPI_Init(nullptr, nullptr);

	int rank, size;
	MPI_Comm_rank(MPI_COMM_WORLD, &rank);
	MPI_Comm_size(MPI_COMM_WORLD, &size);

	int K = atoi(argv[1]),
		L = atoi(argv[2]),
		M = atoi(argv[3]);

	int segmentLength = 2 * L,
		resultCount = K * M;

	int* megaMatrix = new int[K * M * segmentLength]();
	int* counts_s = new int[size]();
	int* displs_s = new int[size]();
	int* received_data = new int[K * M * segmentLength]();

	int* returned_data = new int[resultCount]();
	int* counts_g = new int[size]();
	int* displs_g = new int[size]();
	int* result_matrix = new int[resultCount]();

	if (rank == masterRank)
	{
		int* A = new int[K * L]();
		int* B = new int[K * L]();

		random_device rd;
		mt19937 gen(rd());

		for (int i = 0; i < K * L; ++i)
		{
			A[i] = uniform_int_distribution<>{ -5, 5 }(gen);

			cout << A[i];

			if (i % L == L - 1)
				cout << endl;
			else
				cout << " ";
		}

		cout << endl;

		for (int i = 0; i < L * M; ++i)
		{
			B[i] = uniform_int_distribution<>{ -5, 5 }(gen);

			cout << B[i];

			if (i % M == M - 1)
				cout << endl;
			else
				cout << " ";
		}

		cout << endl;

		int ind = 0;

		for (int k = 0; k < K; ++k)
		{
			for (int m = 0; m < M; ++m)
			{
				for (int l = 0; l < L; ++l)
					megaMatrix[ind++] = A[k * L + l];

				for (int l = 0; l < L; ++l)
					megaMatrix[ind++] = B[m + l * M];
			}
		}

		delete[] A;
		delete[] B;
	}

	for (int i = 0, p = 0; i < resultCount; ++i, p = (p == size) ? 0 : p)
	{
		counts_s[p] += segmentLength;
		counts_g[p++] += 1;
	}

	for (int i = 1; i < size; ++i)
	{
		displs_s[i] = displs_s[i - 1] + counts_s[i - 1];
		displs_g[i] = displs_s[i] / segmentLength;
	}

	auto start = high_resolution_clock::now();
	MPI_Scatterv(megaMatrix, counts_s, displs_s, MPI_INT, received_data, counts_s[rank], MPI_INT, masterRank, MPI_COMM_WORLD);

	for (int i = 0; i < counts_g[rank]; ++i)
	{
		int element = 0;

		for (int j = 0; j < L; ++j)
			element += received_data[i * segmentLength + j] * received_data[i * segmentLength + j + L];

		returned_data[i] = element;
	}


	MPI_Gatherv(returned_data, counts_g[rank], MPI_INT, result_matrix, counts_g, displs_g, MPI_INT, masterRank, MPI_COMM_WORLD);
	auto stop = high_resolution_clock::now();

	if (rank == masterRank)
	{
		auto duration = duration_cast<milliseconds>(stop - start);
		cout << endl << endl << duration.count() << endl << endl;
		//for (int i = 0; i < resultCount; ++i)
		//{
		//	cout << result_matrix[i];

		//	if (i % M == M - 1)
		//		cout << endl;
		//	else
		//		cout << " ";
		//}
	}


	delete[] megaMatrix;
	delete[] counts_s;
	delete[] displs_s;
	delete[] received_data;

	delete[] returned_data;
	delete[] result_matrix;
	delete[] counts_g;
	delete[] displs_g;

	MPI_Finalize();
}