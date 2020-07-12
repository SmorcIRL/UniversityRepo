#include<map> 
#include<fstream> 
#include <iostream> 
using namespace std;

int main()
{
	ifstream IN("rect.in");
	ofstream OUT("rect.out");

	int Xmax, Ymax, N;
	IN >> Xmax >> Ymax >> N;

	multimap<double, pair<bool, bool>>map;

	int** dots = new int*[N];
	for (int i = 0; i < N; i++)
	{
		dots[i] = new int[4];
	}
	int* MASS = new int[4];
	for (int i = 0; i < N; i++)
	{
		bool est = false;

		IN >> MASS[0] >> MASS[1] >> MASS[2] >> MASS[3];

		for (int k = 0; k < 4; k++)
		{
			dots[i][k] = MASS[k];
		}
		if ((double)MASS[3] / MASS[0] >= double(Xmax / Ymax))
		{
			if (MASS[3] * Ymax % MASS[0] != 0)
			{
				map.insert(make_pair((double)(MASS[0] / MASS[3]), make_pair(true, true)));
			}
			else
			{
				map.insert(make_pair(double(MASS[0] / MASS[3]), make_pair(true, false)));
			}
		}
		else
		{
			if (MASS[0] * Xmax / MASS[3] != 0)
			{
				map.insert(make_pair(double(MASS[0] / MASS[3]), make_pair(true, true)));
			}
			else
			{
				map.insert(make_pair(double(MASS[0] / MASS[3]), make_pair(true, false)));
			}
		}
		if ((double)MASS[2] / MASS[1] >= double(Xmax / Ymax))
		{
			if (MASS[1] * Ymax%MASS[2] != 0)
			{
				map.insert(make_pair(double(MASS[2] / MASS[1]), make_pair(true, true)));
			}
			else
			{
				map.insert(make_pair(double(MASS[2] / MASS[1]), make_pair(true, false)));
			}
		}
		else
		{
			if (MASS[2] * Xmax / MASS[1] != 0)
			{
				map.insert(make_pair(double(MASS[2] / MASS[1]), make_pair(true, true)));
			}
			else
			{
				map.insert(make_pair(double(MASS[2] / MASS[1]), make_pair(true, false)));
			}
		}
	}

	int open = 0, close = 0;
	int Max = 0;
	auto index = map.begin();
	for (auto i = map.begin(); i != map.end(); i++)
	{
		auto j = i;
		while (i->first == j->first && i != map.end())
		{
			if ((i->second).first == false)
				open++;
			else
				close++;
			i++;
		}
		i--;
		if (open > Max)
		{
			Max = open;
			index = i;
		}
	}

	if (index->first >= (double)Ymax / Xmax)
	{
		if (index->second.first == false)
		{
			if (index->second.second == true)
			{
				OUT << Max << " " << int(Ymax / index->first) + 1 << " " << Ymax;
			}
			else
			{
				OUT << Max << " " << int(Ymax / index->first) << " " << Ymax;
			}
		}
		else
		{
			OUT << Max << " " << int(Ymax / index->first) << " " << Ymax;
		}
	}
	else
	{
		if (index->second.first == false)
		{
			OUT << Max << " " << Xmax << " " << Xmax * index->first;
		}
		else
		{
			if (index->second.second == true)
			{
				OUT << Max << " " << Xmax << " " << (int)Xmax * index->first + 1;
			}
			else
			{
				OUT << Max << " " << Xmax << " " << (int)Xmax * index->first + 1;
			}

		}
	}

}