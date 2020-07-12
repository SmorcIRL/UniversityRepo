#include <iostream>
#include <string>

using namespace std;

int main()
{
	setlocale(LC_ALL, "Russian");

	int Length = 0, First = 0, Second = 0, ValueLessThanFive = 0;
	double* MyArray, OddSumm = 0, FSSumm = 0;
	bool CorrectInput = false;

	cout << "===============[Лабораторная работа № 2]=============" << endl;
	cout << " Инфо: Введите размер массива" << endl << endl << " Ввод: ";

	while (!CorrectInput)
	{
		cin >> Length;

		if (Length <= 0 || Length > 30000)
		{
			cout << endl << " Инфо: Введите адекватный размер [1;30000]" << endl << endl << " Ввод: " << endl;
			Length = 0;
		}

		else
		{
			CorrectInput = true;
		}
	}

	MyArray = new double[Length + 1];

	cout << endl << endl << " Инфо: Вводите элементы массива через Enter" << endl << endl;

	for (int i = 1; i <= Length; i++)
	{
		cout << " Ввод элемента " << i << ": ";
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

	cout << endl << endl << "=====================================[Ввод]=====================================" << endl;
	cout << " Инфо: Введённый массив размера " << Length << ":" << endl << endl << " [";

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

	cout << endl << endl << "==================================[Результаты]==================================" << endl;

	if (First != 0 && Second != 0 && (Second - First) != 1)
	{

		for (int i = First + 1; i < Second; i++)
		{
			FSSumm += MyArray[i];
		}

		cout << " Инфо: Сумма элементов между первыми двумя отрицательными равна " << FSSumm << endl;
	}

	else
	{
		cout << " Инфо: В массиве не обнаружено хотя бы два отрицательных элемента," << endl;
		cout << " либо между ними нет элементов" << endl;
	}

	MyArray = new double[Length - ValueLessThanFive + 1]();

	cout << endl << " Инфо: В массиве было " << ValueLessThanFive << " элементов, чей модуль не превышал 5.";
	cout << endl << " Память под массив переопределена и теперь массив включает " << Length - ValueLessThanFive << " элемента(ов).";
	cout << endl << " Каждый оставшийся элемент (если есть хотя бы один) занулён." << endl;
	cout << endl << " Инфо: Сумма элементов на нечётных местах равна: " << OddSumm << endl;
	cout << endl << "=====================================[Конец]====================================" << endl << " ";
	system("pause");
}