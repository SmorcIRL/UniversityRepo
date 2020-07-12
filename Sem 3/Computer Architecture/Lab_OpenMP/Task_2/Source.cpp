#include <omp.h>
#include <iostream>
#include <sstream>
#include <ctime>
#include <cmath>

using namespace std;
const int ThreadsCount = 8;

class Program
{
public:

	static void Main(int n)
	{
		omp_set_nested(true);
		getIntegral(0, 1, n);
	}

private:

	static double Foo(double x)
	{
		return pow(x, 2) - 2 * x;
	}

	static void getIntegral(double a, double b, int n)
	{
		double delta = (b - a) / n;
		double sum = 0;

		#pragma omp parallel sections
		{
			#pragma omp section
			{
				#pragma omp parallel for num_threads(ThreadsCount) reduction(+:sum)
				for (int i = 0; i <= n; i++)
				{
					double y = delta * Foo(i * delta);
					sum += y;
				}
			}
		}

		double better = (pow(b, 3) / 3 - pow(b, 2)) - (pow(a, 3) / 3 - pow(a, 2));

		std::cout << "Result: " << sum << endl;
		std::cout << "Better: " << better << endl;
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