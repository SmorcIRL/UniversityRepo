#include <fstream>
#include <vector>
#include <string>
#include <algorithm>

using namespace std;

string res;
bool check = false;

void Recurrent(int i, int j, string source, vector<vector<int>>& matrix)
{
	if (i <= j)
	{
		if (source[i] == source[j])
		{
			res += source[i];

			Recurrent(i + 1, j - 1, source, matrix);

			if (matrix[i][j] == 3)
			{
				check = true;
			}
		}
		else
		{
			int x = max(matrix[i + 1][j], matrix[i][j - 1]);

			if (x == matrix[i][j - 1])
			{
				Recurrent(i, j - 1, source, matrix);
			}
			else
			{
				Recurrent(i + 1, j, source, matrix);
			}
		}
	}
}

int main()
{
	ifstream input("input.txt");
	ofstream output("output.txt");
	string line;

	getline(input, line);

	vector<vector<int>> matrix(line.length());

	for (size_t i = 0; i < line.length(); i++)
	{
		vector<int> tmp(line.length(), 0);
		tmp[i] = 1;
		matrix[i] = tmp;
	}

	for (size_t count = 0; count < line.length(); count++)
	{
		int i = 0;

		for (size_t j = count + 1; j < line.length(); j++, i++)
		{
			if (line[i] == line[j])
			{
				matrix[i][j] = matrix[i + 1][j - 1] + 2;
			}
			else
			{
				matrix[i][j] = max(matrix[i + 1][j], matrix[i][j - 1]);
			}
		}
	}

	if (matrix[0][line.length() - 1] == 1)
	{
		output << 1 << endl << line[0];
		return 0;
	}

	Recurrent(0, line.length() - 1, line, matrix);

	output << matrix[0][line.length() - 1] << endl;

	string temp = res.substr(0, res.size() - int(check));
	reverse(res.begin(), res.end());

	output << (temp + res);

	output.close();
	input.close();
}