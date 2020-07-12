#include <mpi.h>
#include <iostream>
#include <random>

using namespace std;

const int vectorLength = 8;
const int masterRank = 0;

int main()
{
	MPI_Init(nullptr, nullptr);

	int rank, size;
	MPI_Comm_rank(MPI_COMM_WORLD, &rank);
	MPI_Comm_size(MPI_COMM_WORLD, &size);

	int source_vectors[2 * vectorLength] {};

	int *received_data = new int[2 * vectorLength]();
	int *sendcounts = new int[size]();
	int *displs = new int[size]();

	int sum = 0;
	int local_sum = 0;

	if (rank == masterRank)
	{
		random_device rd;
		mt19937 gen(rd());

		for (int i = 0; i < 2 * vectorLength; ++i)
		{
			source_vectors[i] = uniform_int_distribution<> { 0, 1 }(gen);
			cout << source_vectors[i] << (i % 2 == 0 ? " " : "\n");
		}
	}

	for (int i = 0, p = 0; i < vectorLength; ++i, p = (p == size) ? 0 : p)
	{
		sendcounts[p++] += 2;
	}

	for (int i = 1; i < size; ++i)
	{
		displs[i] = displs[i - 1] + sendcounts[i - 1];
	}

	MPI_Scatterv(source_vectors, sendcounts, displs, MPI_INT, received_data, sendcounts[rank], MPI_INT, masterRank, MPI_COMM_WORLD);

	for (int i = 0; i < sendcounts[rank]; i += 2)
	{
		local_sum += received_data[i] * received_data[i + 1];
	}

	MPI_Reduce(&local_sum, &sum, 1, MPI_INT, MPI_SUM, masterRank, MPI_COMM_WORLD);

	if (rank == masterRank)
	{
		cout << endl << "Result: " << sum << endl;
	}

	delete[] received_data;
	delete[] sendcounts;
	delete[] displs;

	MPI_Finalize();
}