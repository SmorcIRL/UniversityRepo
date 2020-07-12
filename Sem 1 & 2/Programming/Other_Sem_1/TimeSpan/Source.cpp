#include "TimeSpan.h"
#include <exception>
#include <iostream>

using namespace std;

int main()
{
	try
	{
		TimeSpan First(0, 0, 0);
		TimeSpan Second(16, 0, 19);

		First.SetHours(12);
		First.SetMinutes(15);
		First.SetSeconds(10);

		First.Output(cout);
		Second.Output(cout);

		cout << endl;

		cout << "1 == 2 ? " << (First == Second) << endl;
		cout << "1 != 2 ? " << (First != Second) << endl;
		cout << "1 > 2  ? " << (First > Second) << endl;
		cout << "1 >= 2 ? " << (First >= Second) << endl;
		cout << "1 < 2  ? " << (First < Second) << endl;
		cout << "1 <= 2 ? " << (First <= Second) << endl << endl;

		cout << "1 - 2 ? " << First - Second << endl;
		cout << "2 - 1 ? " << Second - First << endl << endl;

		cout << "Percent of the day (1): " << First.GetDayPercent() << endl;
		cout << "Percent of the day (2): " << Second.GetDayPercent() << endl << endl;

		TimeSpan Array[10] = { TimeSpan(12,0,0),TimeSpan(1,23,33), TimeSpan(10,10,10), TimeSpan(13,47,0), TimeSpan(0,7,7), TimeSpan(2,15,0), TimeSpan(19,5,0), TimeSpan(18,19,18), TimeSpan(5,12,0), TimeSpan(12,6,5) };

		int Pointer = 0;
		cout << endl << "Array: " << endl;

		for (int i = 0; i < 10; i++)
		{
			Array[i].Output(cout);

			if (Array[Pointer] > Array[i])
			{
				Pointer = i;
			}
		}

		cout << endl << "The shortest period is ";
		Array[Pointer].Output(cout);
	}

	catch (exception &exc)
	{
		cout << exc.what();
	}

	system("pause");
}
