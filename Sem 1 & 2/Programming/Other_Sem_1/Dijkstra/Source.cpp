#include <fstream>
#include <vector>
#include <map>

using namespace std;

const int Infinity = 2000000000;

struct Node
{
	int distance = Infinity, mark = 0, father = 0;
};

int FindMin(vector<Node> &Nodes)
{
	int Index = 0;
	int Value = Infinity;

	for (int i = 1; i < Nodes.size(); i++)
	{
		if (Nodes[i].mark == 0 && Nodes[i].distance < Value)
		{
			Index = i;
			Value = Nodes[i].distance;
		}
	}

	Nodes[Index].mark = 1;
	return Index;
};

int main()
{
	ifstream Input("minpath.in");
	ofstream Output("minpath.out");

	int N, M, A, B;
	Input >> N >> M >> A >> B;

	map<pair<int, int>, int> Arcs;

	vector<Node> Nodes(N + 1);

	Nodes[A].distance = 0;
	Nodes[A].father = A;

	for (int i = 1; i <= M; i++)
	{
		int k, j, value;
		Input >> k >> j >> value;

		pair<int, int> Pair(k, j);


		if (Arcs.find(Pair) == Arcs.end())
		{
			Arcs[Pair] = value;
		}
		else if (Arcs.at(Pair) > value)
		{
			Arcs.at(Pair) = value;
		}
	}

	int a = FindMin(Nodes);
	while (a != 0)
	{
		for (int i = 1; i < Nodes.size(); i++)
		{
			pair<int, int> Pair(a, i);

			if ((Arcs.find(Pair) != Arcs.end()) && (Nodes[a].distance + Arcs[Pair] < Nodes[i].distance))
			{
				Nodes[i].distance = Nodes[a].distance + Arcs[Pair];
				Nodes[i].father = a;
			}
		}

		a = FindMin(Nodes);
	}


	if (Nodes[B].distance == Infinity)
	{
		Output << 0;
	}
	else
	{
		a = B;

		int *OutputArray = new int[N]();
		int i = -1;
		Output << Nodes[B].distance << endl;

		while (a != A)
		{
			i++;
			OutputArray[i] = a;
			a = Nodes[a].father;
		}

		Output << A << " ";

		for (; i != -1; i--)
		{
			Output << OutputArray[i] << " ";
		}
	}

	Input.close();
	Output.close();
}