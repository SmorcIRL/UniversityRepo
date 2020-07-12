#include <iostream>
#include <string>
#include <random>
#include <chrono>

double MyRound(double Value, int Accuracy)
{
	return (round(Value * pow(10, Accuracy)) / pow(10, Accuracy));
}

double MyFunction(double x, int a)
{
	double Sum = 1, Slag = 1;
	int n = 1;

	const int Accuracy = 5;

	while (n <= a)
	{
		Slag *= ((a - n + 1) * (x / n));

		Slag = MyRound(Slag, Accuracy);

		Sum += Slag;
		n++;
	}

	return MyRound(Sum, Accuracy);
}

using namespace std;
using namespace chrono;

int main()
{

	setlocale(LC_ALL, "Russian");

	cout << "===============[������������ ������ � 3]=============" << endl;


#pragma region ��������� x �� [-1,1]

	const double from = -1;
	const double to = 1;

	default_random_engine engine(system_clock::to_time_t(system_clock::now()));
	uniform_real_distribution<> distr(from, to);

	auto gen_number = [&engine, &distr]()
	{
		return distr(engine);
	};

	double x = gen_number();

	cout << " ����: ��������� x ����� " << x << endl << endl;

#pragma endregion


	cout << " ����: ������� ����������� ����� ��� �������" << endl << endl << " ����: ";

	int CorrectInput = false;
	int a;

	while (!CorrectInput)
	{
		cin >> a;

		if (a < 1)
		{
			cout << endl << " ����: �� ����� �� ����������� �����" << endl << endl << " ����: ";
			a = 0;
		}

		else
		{
			CorrectInput = true;
		}
	}

	cout << endl << " ����: ����������� �������: " << pow((1 + x), a) << endl << endl;
	cout << " ����: �� �������: " << MyFunction(x, a) << endl;
	cout << endl << "=====================================[�����]====================================" << endl << " ";
	system("pause");
}