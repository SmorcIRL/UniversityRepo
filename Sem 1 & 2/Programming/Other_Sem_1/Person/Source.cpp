#include <iostream>
#include <exception>
#include <string>
#include "Person.h"

using namespace std;


int main()
{
	try
	{
		Person Eve("Eve", Gender::female);
		Person Adam("Adam", Gender::male);

		OrdinaryPerson TestMale("TestMale", Gender::male, &Eve, &Adam);
		OrdinaryPerson TestFemale("TestFemale", Gender::female, &Eve, &Adam);

		cout << Eve.GetName() << endl;
		cout << Adam.GetGender() << endl;
		cout << TestMale.GetMothersName() << endl;
		cout << TestFemale.GetFathersName() << endl;
	}

	catch (const exception & ex)
	{
		cout << ex.what() << endl;
	}
	system("pause");
}