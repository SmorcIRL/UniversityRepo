#include <mpi.h>
#include <iostream>
#include <random>

using namespace std;

const int masterRank = 0;

int main(int argc, char** argv)
{
	MPI_Init(nullptr, nullptr);

	int rank, size;
	MPI_Comm_rank(MPI_COMM_WORLD, &rank);
	MPI_Comm_size(MPI_COMM_WORLD, &size);

	int L = atoi(argv[1]),
		M = atoi(argv[2]);

	int matrixSize = L * M;

	int *source_matrices = new int[2 * matrixSize]();
	int *counts_s = new int[size]();
	int *displs_s = new int[size]();
	int *received_data = new int[2 * matrixSize]();

	int *returned_data = new int[matrixSize]();
	int *counts_g = new int[size]();
	int *displs_g = new int[size]();
	int *result_matrix = new int[matrixSize]();

	if (rank == masterRank)
	{
		int *A = new int[matrixSize];
		int *B = new int[matrixSize];

		random_device rd;
		mt19937 gen(rd());

		for (int i = 0; i < matrixSize; ++i)
		{
			A[i] = uniform_int_distribution<> { -5, 5 }(gen);

			cout << A[i];

			if (i % M == M - 1)
				cout << endl;
			else
				cout << " ";
		}

		cout << endl;

		for (int i = 0; i < matrixSize; ++i)
		{
			B[i] = uniform_int_distribution<> { -5, 5 }(gen);

			cout << B[i];

			if (i % M == M - 1)
				cout << endl;
			else
				cout << " ";
		}

		cout << endl;

		for (int i = 0; i < matrixSize; ++i)
		{
			source_matrices[2 * i] = A[i];
			source_matrices[2 * i + 1] = B[i];
		}

		delete[] A;
		delete[] B;
	}

	for (int i = 0, p = 0; i < matrixSize; ++i, p = (p == size) ? 0 : p)
	{
		counts_s[p] += 2;
		counts_g[p++] += 1;
	}

	for (int i = 1; i < size; ++i)
	{
		displs_s[i] = displs_s[i - 1] + counts_s[i - 1];
		displs_g[i] = displs_s[i] / 2;
	}

	MPI_Scatterv(source_matrices, counts_s, displs_s, MPI_INT, received_data, counts_s[rank], MPI_INT, masterRank, MPI_COMM_WORLD);

	for (int i = 0; i < counts_g[rank]; ++i)
		returned_data[i] = received_data[2 * i] - received_data[2 * i + 1];

	MPI_Gatherv(returned_data, counts_s[rank] / 2, MPI_INT, result_matrix, counts_g, displs_g, MPI_INT, masterRank, MPI_COMM_WORLD);

	if (rank == masterRank)
	{
		for (int i = 0; i < matrixSize; ++i)
		{
			cout << result_matrix[i];

			if (i % M == M - 1)
				cout << endl;
			else
				cout << " ";
		}
	}


	delete[] source_matrices;
	delete[] counts_s;
	delete[] displs_s;
	delete[] received_data;

	delete[] returned_data;
	delete[] result_matrix;
	delete[] counts_g;
	delete[] displs_g;

	MPI_Finalize();
}