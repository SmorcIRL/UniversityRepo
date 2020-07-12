#include "Deque.h" 
#include <iostream> 
#include <exception> 
#include <string> 

using namespace std;

int main()
{
	setlocale(0, "rus");

	Deque<int> IntDecque;
	Deque<double> DoubleDeque;
	Deque<const char*> CharArrayDeque;

	IntDecque.AddToLeft(1);
	IntDecque.AddToLeft(5);
	IntDecque.AddToLeft(8);
	IntDecque.AddToRight(0);
	IntDecque.AddToRight(-2);
	IntDecque.AddToLeft(8);
	IntDecque.AddToLeft(6);
	IntDecque.AddToRight(0);
	IntDecque.AddToLeft(8);
	IntDecque.AddToLeft(132);
	IntDecque.AddToLeft(0);

	cout << "Original: " << endl;
	IntDecque.OutputFromLeft(cout);
	IntDecque.OutputFromRight(cout);
	IntDecque.SortAscending();
	cout << "Sorted: " << endl;
	IntDecque.OutputFromLeft(cout);
	IntDecque.OutputFromRight(cout);
	cout << endl << endl;

	DoubleDeque.AddToLeft(1.23);
	DoubleDeque.AddToLeft(5.11);
	DoubleDeque.AddToLeft(-8);
	DoubleDeque.AddToRight(0);
	DoubleDeque.AddToRight(-2);
	DoubleDeque.AddToLeft(38.44);
	DoubleDeque.AddToLeft(6);
	DoubleDeque.AddToRight(0);
	DoubleDeque.AddToLeft(811);
	DoubleDeque.AddToLeft(132);
	DoubleDeque.AddToLeft(38.45);

	cout << "Original: " << endl;
	DoubleDeque.OutputFromLeft(cout);
	DoubleDeque.OutputFromRight(cout);
	DoubleDeque.SortAscending();
	cout << "Sorted: " << endl;
	DoubleDeque.OutputFromLeft(cout);
	DoubleDeque.OutputFromRight(cout);
	cout << endl << endl;

	CharArrayDeque.AddToLeft("we");
	CharArrayDeque.AddToLeft(" ");
	CharArrayDeque.AddToLeft("ggg");
	CharArrayDeque.AddToRight("wew");
	CharArrayDeque.AddToRight("ab");
	CharArrayDeque.AddToLeft("aa");
	CharArrayDeque.AddToLeft("bc");
	CharArrayDeque.AddToRight("aaa");
	CharArrayDeque.AddToLeft("ssgsdfsdf");
	CharArrayDeque.AddToLeft("z");
	CharArrayDeque.AddToLeft("rrrrrr");

	cout << "Original: " << endl;
	CharArrayDeque.OutputFromLeft(cout);
	CharArrayDeque.OutputFromRight(cout);
	CharArrayDeque.SortAscending();
	cout << "Sorted: " << endl;
	CharArrayDeque.OutputFromLeft(cout);
	CharArrayDeque.OutputFromRight(cout);

	system("pause");
}