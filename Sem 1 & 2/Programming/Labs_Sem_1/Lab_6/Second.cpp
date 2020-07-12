//#include <iostream>
//#include <iomanip>
//
//using namespace std;
//
//int main()
//{
//	setlocale(LC_ALL, "Russian");
//
//	int m, n, **Matrix;
//	bool CorrectInput = false;
//
//	cout << "========================[Lab 6]=======================" << endl << endl;
//
//
//	while (!CorrectInput)
//	{
//		cout << " Введите количество строк: ";
//		cin >> m;
//
//		cout << " Введите количество столбцов: ";
//		cin >> n;
//
//		if ((m > 20) || (n > 20) || (m < 1) || (n < 1))
//		{
//			cout << endl << " Вы ввели некорректную размерность матрицы " << endl << endl;
//			m = 0;
//			n = 0;
//		}
//
//		else
//		{
//			CorrectInput = true;
//		}
//	}
//
//
//	Matrix = new int*[m];
//	for (int i = 0; i < m; i++)
//	{
//		Matrix[i] = new int[n + 1]();
//	}
//
//
//	cout << endl << " Введите " << n << " элементa(ов) через пробел для заполнения строки. После - нажать Enter для перехода к заполнению следующей. Повторить ввод строк " << m << " раз(a)." << endl << endl;
//	for (int i = 0; i < m; i++)
//	{
//		cout << " ";
//
//		int BufferLastElement = 0;
//
//		for (int g = 0; g < n; g++)
//		{
//			int InputElement;
//
//			cin >> InputElement;
//			Matrix[i][g] = InputElement;
//
//			if (InputElement > 0)
//			{
//				BufferLastElement += InputElement;
//			}
//		}
//
//		Matrix[i][n] = BufferLastElement;
//	}
//
//
//	cout << endl << endl << " Введённая матрица: ";
//	for (int i = 0; i < m; i++)
//	{
//		cout << endl << endl;
//
//		for (int g = 0; g < n; g++)
//		{
//			cout << setw(4) << Matrix[i][g];
//		}
//	}
//
//
//	int *BufferString = new int[n + 1];
//	for (int i = 0; i < m - 1; i++)
//	{
//		for (int g = 0; g < m - i - 1; g++)
//		{
//			if (Matrix[g][n] > Matrix[g + 1][n])
//			{
//				for (int k = 0; k <= n; k++)
//				{
//					BufferString[k] = Matrix[g][k];
//				}
//
//				for (int k = 0; k <= n; k++)
//				{
//					Matrix[g][k] = Matrix[g + 1][k];
//				}
//
//				for (int k = 0; k <= n; k++)
//				{
//					Matrix[g + 1][k] = BufferString[k];
//				}
//			}
//		}
//	}
//
//
//	cout << endl << endl << endl << " Отсортированная матрица: ";
//	for (int i = 0; i < m; i++)
//	{
//		cout << endl << endl;
//
//		for (int g = 0; g < n; g++)
//		{
//			cout << setw(4) << Matrix[i][g];
//		}
//	}
//
//
//	cout << endl << endl << " Количество столбцов, не содержащих нулей: ";
//	int Count = 0;
//	for (int i = 0; i < n; i++)
//	{
//		bool HasNull = false;
//
//		for (int g = 0; g < m; g++)
//		{
//			if (Matrix[g][i] == 0)
//			{
//				HasNull = true;
//			}
//		}
//
//		if (!HasNull)
//		{
//			Count++;
//		}
//	}
//	cout << Count;
//
//
//	delete[] BufferString;
//	for (int i = 0; i < m; i++)
//	{
//		delete[] Matrix[i];
//	}
//	delete[] Matrix;
//
//
//	cout << endl << endl << "=====================================[Конец]====================================" << endl << " ";
//	system("pause");
//}
