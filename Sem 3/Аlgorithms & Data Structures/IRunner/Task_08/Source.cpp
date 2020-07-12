#include <fstream>
#include <queue>

using namespace std;

struct Slot
{
public:
	int i;
	int j;
	int prior;

	Slot(int _i, int _j, int _prior)
	{
		i = _i;
		j = _j;
		prior = _prior;
	}

	bool operator<(const Slot& rhs) const
	{
		return prior > rhs.prior;
	}
};

int main()
{
	int n, m;
	long long result = 0;

	priority_queue<Slot> queue;

	auto input = ifstream("input.txt");
	auto output = ofstream("output.txt");

	input >> n >> m;

	vector<vector<int>> A(n), B(n);

	for (int i = 0; i < n; ++i)
	{
		A[i] = vector<int>(m);
		B[i] = vector<int>(m, INT_MAX);

		for (int j = 0; j < m; ++j)
		{
			input >> A[i][j];

			if (i == 0 || i == n - 1 || j == 0 || j == m - 1)
			{
				queue.push(Slot(i, j, A[i][j]));
			}
		}
	}

	input.close();

	if (n < 3 || m < 3)
	{
		output << 0;
		output.close();
		return 0;
	}

	while (!queue.empty())
	{
		Slot slot = queue.top();
		queue.pop();

		int i = slot.i, j = slot.j;

		if (B[i][j] != INT_MAX)
		{
			continue;
		}

		B[i][j] = slot.prior;

		if (i > 0 && B[i - 1][j] > B[i][j])
		{
			queue.push(Slot(i - 1, j, max(B[i][j], A[i - 1][j])));
		}

		if (i < n - 1 && B[i + 1][j] > B[i][j])
		{
			queue.push(Slot(i + 1, j, max(B[i][j], A[i + 1][j])));
		}

		if (j > 0 && B[i][j - 1] > B[i][j])
		{
			queue.push(Slot(i, j - 1, max(B[i][j], A[i][j - 1])));
		}

		if (j < m - 1 && B[i][j + 1] > B[i][j])
		{
			queue.push(Slot(i, j + 1, max(B[i][j], A[i][j + 1])));
		}
	}

	for (int i = 0; i < n; ++i)
	{
		for (int j = 0; j < m; ++j)
		{
			result += B[i][j] - A[i][j];
		}
	}

	output << result;

	output.close();
}