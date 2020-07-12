#include "Tree.h"
#include <iostream>

using namespace std;

int main()
{
	setlocale(0, "Rus");
	Tree *tr = new Tree();
	
	(*tr).Insert(43)   ?   cout << "43 вставлено успешно!" << endl   :	 cout << "43 не удалось вставить!" << endl;
	(*tr).Insert(143)  ?   cout << "143 вставлено успешно!" << endl  :	 cout << "143 не удалось вставить!" << endl;
	(*tr).Insert(42)   ?   cout << "42 вставлено успешно!" << endl   :	 cout << "42 не удалось вставить!" << endl;
	(*tr).Insert(43)   ?   cout << "43 вставлено успешно!" << endl   :	 cout << "43 не удалось вставить!" << endl;
	(*tr).Insert(45)   ?   cout << "45 вставлено успешно!" << endl   :	 cout << "45 не удалось вставить!" << endl;
	(*tr).Delete(45)   ?   cout << "45 удалено успешно!" << endl     :	 cout << "45 не удалось удалить!" << endl;
	(*tr).Delete(145)  ?   cout << "145 удалено успешно!" << endl    :	 cout << "145 не удалось удалить!" << endl;
	(*tr).Delete(43)   ?   cout << "43 удалено успешно!" << endl     :	 cout << "43 не удалось удалить!" << endl;
	(*tr).Delete(43)   ?   cout << "43 удалено успешно!" << endl     :	 cout << "43 не удалось удалить!" << endl;

	delete tr;

	system("pause");
}