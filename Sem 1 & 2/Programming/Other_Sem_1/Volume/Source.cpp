#include "OldVolume.h"
#include <iostream>
#include <exception>

using namespace std;

int main()
{
	try
	{
		int a, b, c, d;
		double e, f;

		cout << "Input" << endl;
		cin >> a >> b >> e >> c >> d >> f;

		OldVolume First(a, b, e), Second;

		Second.SetShtafs(c);
		Second.SetBottles(d);
		Second.SetCharks(f);

		cout << endl << "First: " << First << " In liters: " << First.GetVolumeInLiters() << endl;
		cout << "Second: " << Second << " In liters: " << Second.GetVolumeInLiters() << endl << endl;

		cout << "1 == 2? " << (First == Second) << endl;
		cout << "1 != 2? " << (First != Second) << endl;
		cout << "1 > 2? " << (First > Second) << endl;
		cout << "1 >= 2? " << (First >= Second) << endl;
		cout << "1 < 2? " << (First < Second) << endl;
		cout << "1 <= 2? " << (First <= Second) << endl << endl;

		cout << "1 + 2? " << (First + Second) << endl;
		cout << "1 - 2? " << (First - Second) << endl;
		cout << "1 += 2? " << (First += Second) << endl;
		First -= Second;
		cout << "1 -= 2? " << (First -= Second) << endl << endl;


		OldVolume Array[10] = { OldVolume(12,1,2), OldVolume(2,0,2.3), OldVolume(1,1,1), OldVolume(0,0,0), OldVolume(4,1,3.75), OldVolume(3,0,0), OldVolume(0,0,2), OldVolume(0,1,0), OldVolume(2,1,3), OldVolume(23,1,3)};
		OldVolume Summ;

		cout << "Array: " << endl;
		for (int i = 0; i < 10; i++)
		{
			cout << Array[i] << endl;
			Summ += Array[i];
		}

		cout << endl << "The total volume is: " << Summ << endl;
		cout << "And in liters: " << Summ.GetVolumeInLiters() << endl << endl;
	}
	catch (exception &exc)
	{
		cout << exc.what() << endl;
	}

	system("pause");
}