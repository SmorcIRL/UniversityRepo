#include <iostream>
#include <cmath>

using namespace std;

double Integral(double A, double B, double Acc, double(*Function) (double), int& Count)
{
	double Integal = 0, FunctionValue, BufferFunctionValue = Function(A);
	Count = (int)abs(ceil((B - A) / Acc));

	for (int i = 1; i <= Count; i++)
	{
		double X = A + Acc * i;
		FunctionValue = Function(X);
		Integal += ((FunctionValue + BufferFunctionValue) / 2) * Acc;
		BufferFunctionValue = FunctionValue;
	}

	return Integal;
}

double First(double X)
{
	return X * sqrt(1 + X);
}

double Second(double X)
{
	return asin(sqrt(X)) / sqrt(X * (1 - X));
}

double Third(double X)
{
	return pow(2, (X)) / (1 + pow(4, (X)));
}

int main()
{
	setlocale(LC_ALL, "Russian");

	double(*FirstFunction)(double) = &First;
	double(*SecondFunction)(double) = &Second;
	double(*ThirdFunction)(double) = &Third;

	double Accuracy;
	bool CorrectInput = false;
	int F = 0, S = 0, T = 0;

	cout << "========================[Lab 5]=======================" << endl << endl;
	cout << " ������� �������� ����������: ";

	while (!CorrectInput)
	{
		cin >> Accuracy;

		if (Accuracy > 0.1 || Accuracy < 0.0000001)
		{
			cout << endl << " �� ����� ������������ ��������" << endl << endl << " ����: ";
			Accuracy = 0;
		}

		else
		{
			CorrectInput = true;
		}
	}

	cout << endl;

	cout << " ������ ��������: " << Integral(2, 7, Accuracy, First, F) << endl;
	cout << " ���������� ���������: " << F << endl << endl;

	cout << " ������ ��������: " << Integral(0.2, 0.3, Accuracy, Second, S) << endl;
	cout << " ���������� ���������: " << S << endl << endl;

	cout << " ������ ��������: " << Integral(-13, -2, Accuracy, Third, T) << endl;
	cout << " ���������� ���������: " << T << endl << endl;

	cout << endl << "=====================================[�����]====================================" << endl;
	system("pause");
}


