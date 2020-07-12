#include "Money.h"
#include <iostream>
using namespace std;

int main()
{
	setlocale(0, "");
	MoneyX* Test1, * Test2;
	long  a, b, d, e;
	double c, f;
	cout << "1: \n";
	cin >> a >> b >> c;
	cout << "2: \n";
	cin >> d >> e >> f;

	try
	{

		Test1 = new MoneyX(a, b, c);
		Test2 = new MoneyX(d, e, f);

		cout << *Test1 << endl;
		cout << *Test2 << endl;

		cout << "Сумма: \n" << *Test1 + *Test2 << "\n";
		cout << "Разность: \n" << *Test1 - *Test2 << "\n";


		cout << "< \n" << (*Test1 < *Test2) << "\n";
		cout << "<= \n" << (*Test1 <= *Test2) << "\n";
		cout << "> \n" << (*Test1 > * Test2) << "\n";
		cout << ">= \n" << (*Test1 >= *Test2) << "\n";
		cout << "== \n" << (*Test1 == *Test2) << "\n";
		cout << "!= \n" << (*Test1 != *Test2) << "\n";
	}
	catch (exception & exc)
	{
		cout << exc.what();
		system("pause");
		return 0;
	}
	system("pause");
}