#include <iostream>
#include <iomanip>

using namespace std;

int main()
{
	setlocale(LC_ALL, "Russian");

	int m, n, **Matrix;
	bool CorrectInput = false;

	cout << "========================[Lab 6]=======================" << endl << endl;


	while (!CorrectInput)
	{	
		cout << " ������� ���������� �����: ";
		cin >> m;

		cout << " ������� ���������� ��������: ";
		cin >> n;

		if ((m > 20) || (n > 20) || (m < 1) || (n < 1))
		{
			cout << endl << " �� ����� ������������ ����������� ������� " << endl << endl;
			m = 0;
			n = 0;
		}

		else
		{
			CorrectInput = true;
		}
	}


	Matrix = new int*[m];
	for (int i = 0; i < m; i++)
	{
		Matrix[i] = new int[n];
	}


	cout << endl << " ������� " << n << " �������a(��) ����� ������ ��� ���������� ������. ����� - ������ Enter ��� �������� � ���������� ���������. ��������� ���� ����� "<< m << " ���(a)." << endl << endl;
	for (int i = 0; i < m; i++)
	{
		cout << " ";

		for (int g = 0; g < n; g++)
		{
			cin >> Matrix[i][g];
		}
	}


	cout << endl << " �������� �������: ";
	for (int i = 0; i < m; i++)
	{
		cout << endl << endl;

		for (int g = 0; g < n; g++)
		{
			cout << setw(4) << Matrix[i][g];
		}
	}


	float *StringsWithNull = new float[m]();
	for (int i = 0; i < m; i++)
	{
		bool HasNull = false;
		int Summ = 0;

		for (int g = 0; g < n; g++)
		{
			Summ += Matrix[i][g];

			if ((Matrix[i][g] == 0) && !HasNull)
			{
				HasNull = true;
			}
		}

		if ( HasNull )
		{
			StringsWithNull[i] = (float)Summ;
		}

		else
		{
			StringsWithNull[i] = 0.5;
		}
	}

	cout << endl << endl << " ����� � ������� *����� ������ � ���� - ����� ��������� � ���* :" << endl;
	for (int i = 0; i < m; i++)
	{
		if ((StringsWithNull[i] - (int)StringsWithNull[i]) == 0)
		{
			cout << " " << i + 1 << " - " << StringsWithNull[i] << endl;
		}
	}
	

	cout << endl << " ���������� �������� ����� �������: ";
	int Count = 0;

	for (int i = 0; i < m; i++)
	{
		for (int g = 0; g < n; g++)
		{
			bool IsSaddleElement = true;

			for (int k = 0; k < n; k++)
			{
				if (Matrix[i][k] < Matrix[i][g])
				{
					IsSaddleElement = false;
				}
			}

			for (int k = 0; k < m; k++)
			{
				if (Matrix[k][g] > Matrix[i][g])
				{
					IsSaddleElement = false;
				}
			}

			if (IsSaddleElement)
			{
				Count++;
			}
		}
	}

	cout << Count;

	delete StringsWithNull;

	for (int i = 0; i < m; i++)
	{
		delete[] Matrix[i];
	}
	delete [] Matrix;

	cout << endl << endl << "=====================================[�����]====================================" << endl;
	system("pause");
}
