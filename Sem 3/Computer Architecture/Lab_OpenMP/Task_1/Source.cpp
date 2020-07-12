#include <omp.h>
#include <iostream>
#include <sstream>
#include <ctime>
#include <cmath>

using namespace std;

const double pi = 3.14159265;
const double pi_by_2 = 1.570796325;
const int ThreadsCount = 8;

class Program
{
public:
	static void Main(int n)
	{
		omp_set_nested(true);
		GetMax(0, 2, n);
	}

private:
	static double Foo(double x)
	{
		if (0 <= x && x <= pi_by_2)
		{
			return cos(x);
		}			
		else if (pi_by_2 < x && x <= 2)
		{
			return (pi_by_2 - x);
		}		
	}


	static void GetMax(double a, double b, int n)
	{
		double delta_1 = (b - a) / n;
		double delta_2 = delta_1 / 4;
		double max_1 = -10000000;
		double max_2 = -10000000;

		#pragma omp parallel sections
		{
			#pragma omp section
			{
				#pragma omp parallel for num_threads(ThreadsCount)
				for (unsigned int i = 0; i < n; i++)
				{
					double y = abs((Foo(i * delta_1 + delta_1) - Foo(i * delta_1)) / delta_1);
					if (max_1 < y)
					{
						#pragma omp critical
						{
							if (max_1 < y)
							{
								max_1 = y;
							}
						}
					}
				}
			}
			#pragma omp section
			{
				#pragma omp parallel for num_threads(ThreadsCount)

				for (unsigned int i = 0; i < n * 4; i++)
				{
					double y = abs((Foo((i + 1) * delta_2) - Foo(i * delta_2)) / delta_2);
					if (max_2 < y)
					{
						#pragma omp critical
						{
							if (max_2 < y)
							{
								max_2 = y;
							}
						}
					}
				}
			}

		}

		std::cout << "N result:   " << max_1 << endl;
		std::cout << "N*4 result: " << max_2 << endl;
	}
};

int main(int argc, char* argv[])
{
	if (argc == 2)
	{
		Program::Main(atoi(argv[1]));
	}
	else
	{
		cout << "Wrong args!" << endl;
	}
}