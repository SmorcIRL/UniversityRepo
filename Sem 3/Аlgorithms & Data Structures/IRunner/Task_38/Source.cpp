#include <fstream>
#include <stack> 

using namespace std;

int main()
{
	auto input = ifstream("input.txt");
	auto output = ofstream("output.txt");

	int n;
	input >> n;

	int* Roots = new int[n + 1];
	int* Lengthes = new int[n + 1]{ 0 };

	for (int i = 1; i <= n; i++)
	{
		Roots[i] = i;
	}

	char c;

	bool nl = false;
	while (true)
	{
		input >> c;

		if (c == 'I')
		{
			int f, s;

			input >> f >> s;

			Roots[f] = Roots[s];
			Lengthes[f] += abs(f - s) % 1000 + Lengthes[s];
		}
		else if (c == 'E')
		{
			int f, branch;
			input >> f;

			branch = f;

			int length = Lengthes[branch],
				root = Roots[branch];

			stack<int> branches;
			stack<int> lengths;

			branches.push(branch);
			lengths.push(length);

			while (branch != root)
			{
				branch = root;
				root = Roots[root];

				branches.push(branch);
				lengths.push(Lengthes[branch]);
			}

			int buffer_length = 0;
			while (!branches.empty())
			{
				int b = branches.top();

				buffer_length += lengths.top();
				Lengthes[b] = buffer_length;
				Roots[b] = root;

				branches.pop();
				lengths.pop();
			}


			if (nl)
			{
				output << '\n' << Lengthes[f];
			}
			else
			{
				output << Lengthes[f];
				nl = true;
			}
		}
		else
		{
			break;
		}
	}

	input.close();
	output.close();
}