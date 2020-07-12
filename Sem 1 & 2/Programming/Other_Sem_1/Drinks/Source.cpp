#include "Drinks.h"
#include <iostream>
#include <exception>

using namespace std;

int main()
{
	setlocale(0, "rus");

	try
	{
		Date First(12, 11, 2015);
		Date Second(23, 3, 2010);
		Date Third(2, 10, 1930);
		Date Fourth(19, 1, 2018);
		Date Fifth(15, 7, 2000);
		Date Sixth(12, 10, 2000);

		Beer Beer(First, "Zhigulevskoe", 1, 10, BeerRawMaterial::barley, CarbonationDegree::low_carbonated);
		Wine Wine(Second, "Bordo", 1, 25, TypeOfWine::dry);
		Cognac Cognac(Third, "Armjanskij", 0.5, 40, 5);

		Milk Milk(Fourth, "Milk", 1, 9);
		MineralWater MineralWater(Fifth, "Borzhomi", 1, CarbonationDegree::highly_carbonated);
		Lemonade Lemonade(Sixth, "Zolotoj kljuchik", 2, LemonadeColor::yellow);

		Beer.GetInfo(cout);
		Wine.GetInfo(cout);
		Cognac.GetInfo(cout);
		Milk.GetInfo(cout);
		MineralWater.GetInfo(cout);
		Lemonade.GetInfo(cout);
	}
	catch (exception * exc)
	{
		cout << exc->what();
	}

	system("pause");
}