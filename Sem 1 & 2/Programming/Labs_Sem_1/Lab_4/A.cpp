//#include <iostream>
//
//using namespace std;
//
//int main()
//{
//	char *MyItoa(int N, char *OutOutStringing, int Basis);
//	int InputNumber, InputBasis;
//	char *ResultString = new char();
//
//	cout << "========================[Lab 4]=======================" << endl;
//
//	while (1)
//	{
//		cout << " Info: Enter an integer" << endl << endl << " Input: ";
//		cin >> InputNumber;
//
//		cout << endl << " Info: Enter base for conversion" << endl << endl << " Input: ";
//		cin >> InputBasis;
//
//		cout << endl << " Info: Result after conversion: " << MyItoa(InputNumber, ResultString, InputBasis);
//
//		cout << endl << endl << endl << "===================================[Try again]==================================" << endl;
//	}
//}
//
//char *MyItoa(int N, char *OutString, int Basis)
//{
//	int i = 0;
//	bool LessThenNull = false;
//
//	if (N == 0)
//	{
//		OutString[i++] = '0';
//		OutString[i] = '\0';
//		return OutString;
//	}
//
//	if (N < 0)
//	{
//		LessThenNull = true;
//		N = -N;
//	}
//
//	while (N != 0)
//	{
//		int Remainder = N % Basis;
//
//		if (Remainder > 9)
//		{
//			OutString[i++] = (Remainder - 10) + 'a';
//		}
//
//		else
//		{
//			OutString[i++] = Remainder + '0';
//		}
//
//		N /= Basis;
//	}
//
//	if (LessThenNull)
//	{
//		OutString[i++] = '-';
//	}
//
//	OutString[i] = '\0';
//
//	for (int g = 0; g <= (i - 1) / 2; g++)
//	{
//		char Buffer = *(OutString + g);
//		*(OutString + g) = *(OutString + i - g - 1);
//		*(OutString + i - g - 1) = Buffer;
//	}
//
//	return OutString;
//}