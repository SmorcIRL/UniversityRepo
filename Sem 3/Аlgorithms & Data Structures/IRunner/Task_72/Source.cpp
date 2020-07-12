#include <fstream>
#include <vector>
#include <algorithm>

using namespace std;

#define INFINITY LLONG_MAX

int main()
{
	ifstream input("input.txt");
	ofstream output("output.txt");

	int n, m;

	input >> n >> m;

	vector <vector<pair<int, int>>> ishod(n);
	vector <int> marks(n, 1);
	vector <long long> distance(n, INFINITY);

	for (int i = 0; i < m; i++)
	{
		int k, j, value;
		input >> k >> j >> value;
		ishod[k - 1].push_back(make_pair(j - 1, value));
		ishod[j - 1].push_back(make_pair(k - 1, value));
	}

	vector <pair<long long, int>> queue;
	make_heap(queue.begin(), queue.end(), greater<pair<long long, int>>());

	queue.push_back(make_pair(0, 0));
	distance[0] = 0;
	push_heap(queue.begin(), queue.end(), greater<pair<long long, int>>());

	while (!(queue.empty()))
	{
		pop_heap(queue.begin(), queue.end(), greater<pair<long long, int>>());
		int top = queue.back().second;
		queue.pop_back();

		if (marks[top] == 3) continue;

		marks[top] = 3;

		for (int i = 0; i < ishod[top].size(); i++)
		{
			int
				to = ishod[top][i].first,
				put = ishod[top][i].second;

			if (distance[to] > distance[top] + put)
			{
				distance[to] = distance[top] + put;
				queue.push_back(make_pair(distance[to], to));
				push_heap(queue.begin(), queue.end(), greater<pair<long long, int>>());
			}
		}
	}

	output << distance[n - 1];

	input.close();
	output.close();

	return 0;
}