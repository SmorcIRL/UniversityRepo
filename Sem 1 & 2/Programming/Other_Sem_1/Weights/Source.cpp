#include <iostream>
#include <exception>
#include "OldWeight.h"

using namespace std;

int main()
{
	try
	{
		OldWeight First(13, 37, 80);
		OldWeight Second(16, 0, 19);

		First.SetPoods(18);
		First.SetPounds(15);
		First.SetZols(10);

		cout << First << endl;
		cout << Second << endl << endl;

		cout << "1 == 2 ? " << (First == Second) << endl;
		cout << "1 != 2 ? " << (First != Second) << endl;
		cout << "1 > 2  ? " << (First > Second) << endl;
		cout << "1 >= 2 ? " << (First >= Second) << endl;
		cout << "1 < 2  ? " << (First < Second) << endl;
		cout << "1 <= 2 ? " << (First <= Second) << endl << endl;

		cout << "1 + 2 ? " << First + Second << endl;
		cout << "1 - 2 ? " << First - Second << endl;

		cout << "1 += 2 ? " << (First += Second) << endl;
		First -= Second;
		cout << "1 -= 2 ? " << (First -= Second) << endl << endl;

		OldWeight Array[10] = { OldWeight(12,0,0),OldWeight(1,23,33), OldWeight(10,10,10), OldWeight(13,39,0), OldWeight(0,0,0), OldWeight(2,15,0), OldWeight(19,5,0), OldWeight(18,19,18), OldWeight(5,12,0), OldWeight(12,6,5) };

		OldWeight Summ;
		cout << "Array: " << endl;

		for (int i = 0; i < 10; i++)
		{
			cout << Array[i] << endl;
			Summ += Array[i];
		}

		cout << endl << "The total weight is " << Summ << endl;
		cout <<"In kilo: " << Summ.GetWeightInKilo()<< endl;
	}

	catch (exception &exc)
	{
		cout << exc.what();
	}

	system("pause");
}