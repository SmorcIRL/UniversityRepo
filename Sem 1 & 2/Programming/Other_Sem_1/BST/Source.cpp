#include "Tree.h"
#include <iostream>

using namespace std;

int main()
{
	setlocale(0, "Rus");
	Tree *tr = new Tree();
	
	(*tr).Insert(43)   ?   cout << "43 ��������� �������!" << endl   :	 cout << "43 �� ������� ��������!" << endl;
	(*tr).Insert(143)  ?   cout << "143 ��������� �������!" << endl  :	 cout << "143 �� ������� ��������!" << endl;
	(*tr).Insert(42)   ?   cout << "42 ��������� �������!" << endl   :	 cout << "42 �� ������� ��������!" << endl;
	(*tr).Insert(43)   ?   cout << "43 ��������� �������!" << endl   :	 cout << "43 �� ������� ��������!" << endl;
	(*tr).Insert(45)   ?   cout << "45 ��������� �������!" << endl   :	 cout << "45 �� ������� ��������!" << endl;
	(*tr).Delete(45)   ?   cout << "45 ������� �������!" << endl     :	 cout << "45 �� ������� �������!" << endl;
	(*tr).Delete(145)  ?   cout << "145 ������� �������!" << endl    :	 cout << "145 �� ������� �������!" << endl;
	(*tr).Delete(43)   ?   cout << "43 ������� �������!" << endl     :	 cout << "43 �� ������� �������!" << endl;
	(*tr).Delete(43)   ?   cout << "43 ������� �������!" << endl     :	 cout << "43 �� ������� �������!" << endl;

	delete tr;

	system("pause");
}