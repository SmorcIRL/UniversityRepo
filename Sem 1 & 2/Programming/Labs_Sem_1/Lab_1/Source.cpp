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

	cout << "===============[������������ ������ �1]==============" << endl;
	cout << " ����: ������� ������� 1/2/3/4 ��� �������� ���������������� �������." << endl;
	cout << endl << "================================================================================";

#pragma region �������� �����

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
	case 49:	// 1 ������� ����� �
	{
		First();
	}

	case 50:	// 2 ������� ����� �
	{
		Second();
	}

	case 51:	// 3 ������� ����� �
	{
		Third();
	}

	case 52:	// 14 ������� ����� B
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

	cout << endl << endl << endl << "=========================[������� ��� ����������� �����]========================";

#pragma region �������� ������� ������

	while (!CorrectInput)
	{
		cout << endl << " ����: ";

		cin >> FirstInputString >> SecondInputString;

		First, Second = 0;

		// �������� �� ���� ������������ �����
		for (unsigned int i = 0; i < FirstInputString.length(); i++)
		{
			if ((int)FirstInputString[i] < 48 || (int)FirstInputString[i] > 57)
			{
				First = -1;
				break;
			}
			First = 1;
		}

		// �������� �� ���� ������������ �����
		for (unsigned int i = 0; i < SecondInputString.length(); i++)
		{
			if ((int)SecondInputString[i] < 48 || (int)SecondInputString[i] > 57)
			{
				Second = -1;
				break;
			}
			Second = 1;
		}

		// ���� ���� ����������
		if ((First > 0) && (Second > 0))
		{
			First = atoi(FirstInputString.c_str());
			Second = atoi(SecondInputString.c_str());
			CorrectInput = true;
			break;
		}

		else
		{
			cout << endl << " ���� : ������������ ����. ������� ������� ���� ������������� �����" << endl << endl << endl << "==============================[������� ���� ������]=============================";
		}

		// ����� � � ����������� - ������� ������ �����
		cin.clear();
		cin.ignore(numeric_limits<streamsize>::max(), '\n');
	}

#pragma endregion

#pragma region �������� ������

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

	cout << endl << " ���������� ����� �������� ��� ����� �����: " << First << endl << " ���������� ����� ������� ��� ����� �����: " << FirstCopy * SecondCopy / First;

#pragma endregion

	cout << endl << endl << "==============================[��� ���������� � ��]============================";

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

	cout << endl << endl << endl << "===============[������� ���������� ����� ��� �������� ����� ����]==============";

	cout << endl << " ����: ���������� ����� ����� ����� ��������� ���������� ������, ���� �������� ����� ��������� ���������" << endl;

#pragma region �������� ������� ������

	while (!CorrectInput)
	{
		cout << endl << " ����: ";
		cin >> InputString;

		// �������� �� ������� � ���������� � �������� ������ . � , ��� ������ �������� �� ����� ������ ��� ���
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

		// �������� ��������� ��������
		for (unsigned int i = 0; i < InputString.length(); i++)
		{
			if ((int)InputString[i] < 48 || (int)InputString[i] > 57)
			{
				Number = -1;
				break;
			}
		}

		// ���� �������� ������ ������ �������� � ����� �������������� ������
		if (Number != -1)
		{
			Number = atoi(InputString.c_str());
		}

		// �������� �� ���������� ���� � ���������� ���� �����, ���� ������ ������
		if (Number > 99 && Number <= 999)
		{
			CorrectInput = true;
			break;
		}

		else
		{
			cout << endl << " ����: ������������ ����. �������� ������ ������� ��� ������������ ����� " << endl << endl << endl << "==============================[������� ���� ������]=============================";
		}

		Number = 0;
		k = 0;
		cin.clear();
		cin.ignore(numeric_limits<streamsize>::max(), '\n');
	}

#pragma endregion

#pragma region �������� ������

	while (Number != 0)
	{
		Summ += Number % 10;
		Number /= 10;
	}

	cout << endl << " ����� ���� ������ �����: " << Summ;

#pragma endregion

	cout << endl << endl << "==============================[��� ���������� � ��]============================";

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

	cout << endl << endl << endl << "=============[������� ����� �� 0 �� 1000 ��� ���������� ����������]=============";

#pragma region �������� ������� ������

	while (!CorrectInput)
	{
		cout << endl << " ����: ";

		cin >> InputString;

		// �������� �� ���� ������������ �����
		for (unsigned int i = 0; i < InputString.length(); i++)
		{
			if ((int)InputString[i] < 48 || (int)InputString[i] > 57)
			{
				Number = -1;
				break;
			}
		}

		// ������ ������ �������� ����, �� ����������� ����� ����� �������������� � int, ������ ��� ��� ������� ������� ����������� ����� ������
		if (Number != -1 && atoi(InputString.c_str()) <= 1000)
		{
			Number = atoi(InputString.c_str());
			CorrectInput = true;
			break;
		}

		else
		{
			cout << endl << " ����: ������������ ����. �������� ������ �������, ������������� �����, ���� ����� ������ 1000" << endl << endl << endl << "==============================[������� ���� ������]=============================";
		}

		Number = 0;
		cin.clear();
		cin.ignore(numeric_limits<streamsize>::max(), '\n');
	}

#pragma endregion

#pragma region �������� ������

	cout << endl << " ��� ��� ��� ���������: ";

	FactorialArray[0] = 1;
	Focus = 1;

	// �� ���� ��������, ������� �� ���������� ��� ��������� � �������, ������������� � �����
	// ����� ���������� ���������� � �������� ������� ���������� �� ��������� �����, ��������� "�� ��� �� ������ � ���" � �����
	// ����� ���������� �� ��������� ������, ������ ������� �� ������� �� ������, ���������� � ������ � �����
	// � ��� � ������ �������� �� ����� ������� 
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

	// ����� ������� �������� � �������� ������� �� �������� � �������
	for (int i = Focus - 1; i >= 0; i--)
	{
		cout << FactorialArray[i];
	}

#pragma endregion

	cout << endl << endl << "==============================[��� ���������� � ��]============================";

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

	cout << endl << endl << endl << "==================[������� ����� ��� ������������ ������ �����]=================";

#pragma region �������� ������� ������

	while (!CorrectInput)
	{
		cout << endl << " ����: ";

		cin >> InputString;

		// �������� �� ���� ������������ �����
		for (unsigned int i = 0; i < InputString.length(); i++)
		{
			if ((int)InputString[i] < 48 || (int)InputString[i] > 57)
			{
				CorrectNumber = false;
				break;
			}
		}

		// ��������� ��� ������� �������� � ������, ���������� �������� ������
		// ������� ��� ���������� bool, � �� int ��� � ������ �������� � ��������� �����
		if (CorrectNumber == true)
		{
			CorrectInput = true;
			break;
		}

		else
		{
			cout << endl << " ����: ������������ ����. �������� ������ ������� ��� ������������� ����� " << endl << endl << endl << "==============================[������� ���� ������]=============================";
		}

		CorrectNumber = true;

		cin.clear();
		cin.ignore(numeric_limits<streamsize>::max(), '\n');
	}

#pragma endregion

#pragma region �������� ������

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
		cout << endl << " ����� ����� ��������������: " << InputString;

	}

	else
	{
		cout << endl << " ����: ���, ������ �� ������ ����� ������ �� �������� ";
	}

#pragma endregion

	cout << endl << endl << "==============================[��� ���������� � ��]============================";

	while (_getch() != 13)
	{

	}

	main();
}