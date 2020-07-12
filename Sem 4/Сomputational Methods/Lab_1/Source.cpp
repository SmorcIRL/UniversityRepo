#include <iostream>
#include <limits>
#include <bitset>

using namespace std;

int main()
{
	double norm = numeric_limits<double>::min();
	double denorm = numeric_limits<double>::denorm_min();

	cout << bitset<64>(*reinterpret_cast<unsigned long long*>(&norm)) << endl;
	cout << bitset<64>(*reinterpret_cast<unsigned long long*>(&denorm)) << endl;

	system("pause");
}