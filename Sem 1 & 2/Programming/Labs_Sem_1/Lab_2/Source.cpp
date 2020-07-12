#include <iostream>
#include <string>

using namespace std;

int main()
{
	setlocale(LC_ALL, "Russian");

	int Length = 0, First = 0, Second = 0, ValueLessThanFive = 0;
	double* MyArray, OddSumm = 0, FSSumm = 0;
	bool CorrectInput = false;

	cout << "===============[������������ ������ � 2]=============" << endl;
	cout << " ����: ������� ������ �������" << endl << endl << " ����: ";

	while (!CorrectInput)
	{
		cin >> Length;

		if (Length <= 0 || Length > 30000)
		{
			cout << endl << " ����: ������� ���������� ������ [1;30000]" << endl << endl << " ����: " << endl;
			Length = 0;
		}

		else
		{
			CorrectInput = true;
		}
	}

	MyArray = new double[Length + 1];

	cout << endl << endl << " ����: ������� �������� ������� ����� Enter" << endl << endl;

	for (int i = 1; i <= Length; i++)
	{
		cout << " ���� �������� " << i << ": ";
		cin >> MyArray[i];

		if (i % 2 != 0)
		{
			OddSumm += MyArray[i];
		}

		if (MyArray[i] >= -5 && MyArray[i] <= 5)
		{
			ValueLessThanFive++;
		}


		if (First == 0 && MyArray[i] < 0)
		{
			First = i;
		}

		else if (Second == 0 && MyArray[i] < 0)
		{
			Second = i;
		}
	}

	cout << endl << endl << "=====================================[����]=====================================" << endl;
	cout << " ����: �������� ������ ������� " << Length << ":" << endl << endl << " [";

	for (int i = 1; i <= Length; i++)
	{
		cout << MyArray[i];

		if (i != Length)
		{
			cout << ", ";
		}

		else
		{
			cout << "]";
		}
	}

	cout << endl << endl << "==================================[����������]==================================" << endl;

	if (First != 0 && Second != 0 && (Second - First) != 1)
	{

		for (int i = First + 1; i < Second; i++)
		{
			FSSumm += MyArray[i];
		}

		cout << " ����: ����� ��������� ����� ������� ����� �������������� ����� " << FSSumm << endl;
	}

	else
	{
		cout << " ����: � ������� �� ���������� ���� �� ��� ������������� ��������," << endl;
		cout << " ���� ����� ���� ��� ���������" << endl;
	}

	MyArray = new double[Length - ValueLessThanFive + 1]();

	cout << endl << " ����: � ������� ���� " << ValueLessThanFive << " ���������, ��� ������ �� �������� 5.";
	cout << endl << " ������ ��� ������ �������������� � ������ ������ �������� " << Length - ValueLessThanFive << " ��������(��).";
	cout << endl << " ������ ���������� ������� (���� ���� ���� �� ����) ������." << endl;
	cout << endl << " ����: ����� ��������� �� �������� ������ �����: " << OddSumm << endl;
	cout << endl << "=====================================[�����]====================================" << endl << " ";
	system("pause");
}