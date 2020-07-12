#include <iostream>
#include <string>
#include <conio.h>

using namespace std;

int main()
{
	void First();
	void Second();
	void Third();
	void Fourth();

	setlocale(LC_ALL, "Russian");

	system("cls");

	cout << "===============[Лабораторная работа №1]==============" << endl;
	cout << " Инфо: Нажмите клавиши 1/2/3/4 для проверки соответствующего задания." << endl;
	cout << endl << "================================================================================";

#pragma region Проверка ввода

	int mainkey = 0;

	while (_getch)
	{
		mainkey = _getch();
		if (mainkey == 49 || mainkey == 50 || mainkey == 51 || mainkey == 52)
		{
			break;
		}
	}

	switch (mainkey)
	{
	case 49:	// 1 задание части А
	{
		First();
	}

	case 50:	// 2 задание части А
	{
		Second();
	}

	case 51:	// 3 задание части А
	{
		Third();
	}

	case 52:	// 14 задание части B
	{
		Fourth();
	}
	}

#pragma endregion

}

void First()
{
	setlocale(LC_ALL, "RUS");

	string FirstInputString, SecondInputString;
	bool CorrectInput = false;

	int First = 0, Second = 0, FirstCopy, SecondCopy;

	cout << endl << endl << endl << "=========================[Введите два натуральных числа]========================";

#pragma region Проверка входных данных

	while (!CorrectInput)
	{
		cout << endl << " Ввод: ";

		cin >> FirstInputString >> SecondInputString;

		First, Second = 0;

		// Проверка на ввод натурального числа
		for (unsigned int i = 0; i < FirstInputString.length(); i++)
		{
			if ((int)FirstInputString[i] < 48 || (int)FirstInputString[i] > 57)
			{
				First = -1;
				break;
			}
			First = 1;
		}

		// Проверка на ввод натурального числа
		for (unsigned int i = 0; i < SecondInputString.length(); i++)
		{
			if ((int)SecondInputString[i] < 48 || (int)SecondInputString[i] > 57)
			{
				Second = -1;
				break;
			}
			Second = 1;
		}

		// Если ввод адекватный
		if ((First > 0) && (Second > 0))
		{
			First = atoi(FirstInputString.c_str());
			Second = atoi(SecondInputString.c_str());
			CorrectInput = true;
			break;
		}

		else
		{
			cout << endl << " Инфо : Некорректный ввод. Введены символы либо ненатуральные числа" << endl << endl << endl << "==============================[Начните ввод заново]=============================";
		}

		// Здесь и в последующих - очистка буфера ввода
		cin.clear();
		cin.ignore(numeric_limits<streamsize>::max(), '\n');
	}

#pragma endregion

#pragma region Основная логика

	FirstCopy = First;
	SecondCopy = Second;

	while (First != Second)
	{
		if (First > Second)
		{
			First -= Second;
		}

		else
		{
			Second -= First;
		}
	}

	cout << endl << " Наибольший общий делитель для Ваших чисел: " << First << endl << " Наименьшее общее кратное для Ваших чисел: " << FirstCopy * SecondCopy / First;

#pragma endregion

	cout << endl << endl << "==============================[Вот собственно и всё]============================";

	while (_getch() != 13)
	{

	}

	main();
}

void Second()
{
	setlocale(LC_ALL, "RUS");

	string InputString;
	bool CorrectInput = false;

	int Number = 0, Summ = 0, k = 0;

	cout << endl << endl << endl << "===============[Введите трёхзначное число для подсчёта суммы цифр]==============";

	cout << endl << " Инфо: Десятичная дробь также может считаться трёхзначным числом, если обладает тремя значащими разрядами" << endl;

#pragma region Проверка входных данных

	while (!CorrectInput)
	{
		cout << endl << " Ввод: ";
		cin >> InputString;

		// Проверка на наличие и количество в вводимой строке . и , для оценки является ли число дробью или нет
		for (unsigned int i = 0; i < InputString.length(); i++)
		{
			if (InputString[i] == '.' || InputString[i] == ',')
			{
				k++;

				if (k > 1)
				{
					Number = -1;
					break;
				}

				InputString.erase(i, 1);
			}
		}

		// Проверка остальных символов
		for (unsigned int i = 0; i < InputString.length(); i++)
		{
			if ((int)InputString[i] < 48 || (int)InputString[i] > 57)
			{
				Number = -1;
				break;
			}
		}

		// Если вводимая строка прошла проверки и может обрабатываться дальше
		if (Number != -1)
		{
			Number = atoi(InputString.c_str());
		}

		// Проверка на количество цифр в полученном выше числе, если строка прошла
		if (Number > 99 && Number <= 999)
		{
			CorrectInput = true;
			break;
		}

		else
		{
			cout << endl << " Инфо: Некорректный ввод. Возможно ведены символы или нетрёхзначное число " << endl << endl << endl << "==============================[Начните ввод заново]=============================";
		}

		Number = 0;
		k = 0;
		cin.clear();
		cin.ignore(numeric_limits<streamsize>::max(), '\n');
	}

#pragma endregion

#pragma region Основная логика

	while (Number != 0)
	{
		Summ += Number % 10;
		Number /= 10;
	}

	cout << endl << " Сумма цифр вашего числа: " << Summ;

#pragma endregion

	cout << endl << endl << "==============================[Вот собственно и всё]============================";

	while (_getch() != 13)
	{

	}

	main();
}

void Third()
{
	setlocale(LC_ALL, "RUS");

	string InputString;
	bool CorrectInput = false;

	int FactorialArray[5000];
	int Focus;

	int Number = 0;

	cout << endl << endl << endl << "=============[Введите число от 0 до 1000 для вычисления факториала]=============";

#pragma region Проверка входных данных

	while (!CorrectInput)
	{
		cout << endl << " Ввод: ";

		cin >> InputString;

		// Проверка на ввод натурального числа
		for (unsigned int i = 0; i < InputString.length(); i++)
		{
			if ((int)InputString[i] < 48 || (int)InputString[i] > 57)
			{
				Number = -1;
				break;
			}
		}

		// Строка прошла проверку выше, то проверяется число после преобразования в int, потому что при слишком большом обязательно будут ошибки
		if (Number != -1 && atoi(InputString.c_str()) <= 1000)
		{
			Number = atoi(InputString.c_str());
			CorrectInput = true;
			break;
		}

		else
		{
			cout << endl << " Инфо: Некорректный ввод. Возможно ведены символы, ненатуральное число, либо число больше 1000" << endl << endl << endl << "==============================[Начните ввод заново]=============================";
		}

		Number = 0;
		cin.clear();
		cin.ignore(numeric_limits<streamsize>::max(), '\n');
	}

#pragma endregion

#pragma region Основная логика

	cout << endl << " Вот вам Ваш факториал: ";

	FactorialArray[0] = 1;
	Focus = 1;

	// По сути алгоритм, которым мы пользуемся при умножении в столбик, реализованный в цикле
	// Число записанное поразрядно в обратном порядке умножается на следующее число, записывая "то что мы держим в уме" в буфер
	// Буфер умножается на следующий разряд, берётся остаток от деления на десять, изменяется и разряд и буфер
	// И так с каждым разрядом до конца массива 
	for (int i = 2; i <= Number; i++)
	{
		int InMemory = 0;
		for (int g = 0; g < Focus; g++)
		{
			int IntermediateValue = FactorialArray[g] * i + InMemory;
			FactorialArray[g] = IntermediateValue % 10;
			InMemory = IntermediateValue / 10;
		}

		while (InMemory)
		{
			FactorialArray[Focus] = InMemory % 10;
			InMemory /= 10;
			Focus++;
		}

	}

	// Вывод массива разрядов в обратном порядке от хранения в массиве
	for (int i = Focus - 1; i >= 0; i--)
	{
		cout << FactorialArray[i];
	}

#pragma endregion

	cout << endl << endl << "==============================[Вот собственно и всё]============================";

	while (_getch() != 13)
	{

	}

	main();
}

void Fourth()
{
	setlocale(LC_ALL, "RUS");

	string InputString, CopyInputString;
	bool CorrectInput = false;
	bool CorrectNumber = true;

	cout << endl << endl << endl << "==================[Введите число для формирования нового числа]=================";

#pragma region Проверка входных данных

	while (!CorrectInput)
	{
		cout << endl << " Ввод: ";

		cin >> InputString;

		// Проверка на ввод натурального числа
		for (unsigned int i = 0; i < InputString.length(); i++)
		{
			if ((int)InputString[i] < 48 || (int)InputString[i] > 57)
			{
				CorrectNumber = false;
				break;
			}
		}

		// Конкретно тут незачем работать с числом, достаточно входящей строки
		// Поэтому нам достаточно bool, а не int как в других функциях с проверкой ввода
		if (CorrectNumber == true)
		{
			CorrectInput = true;
			break;
		}

		else
		{
			cout << endl << " Инфо: Некорректный ввод. Возможно ведены символы или ненатуральное число " << endl << endl << endl << "==============================[Начните ввод заново]=============================";
		}

		CorrectNumber = true;

		cin.clear();
		cin.ignore(numeric_limits<streamsize>::max(), '\n');
	}

#pragma endregion

#pragma region Основная логика

	CopyInputString = InputString;

	for (unsigned int i = 0; i < InputString.length();)
	{
		if (InputString[i] % 2 == 0)
		{
			InputString.erase(i, 1);
		}
		else
		{
			i++;
		}
	}

	if (InputString.length() != 0)
	{
		cout << endl << " Число после преобразования: " << InputString;

	}

	else
	{
		cout << endl << " Инфо: Упс, похоже от вашего числа ничего не осталось ";
	}

#pragma endregion

	cout << endl << endl << "==============================[Вот собственно и всё]============================";

	while (_getch() != 13)
	{

	}

	main();
}