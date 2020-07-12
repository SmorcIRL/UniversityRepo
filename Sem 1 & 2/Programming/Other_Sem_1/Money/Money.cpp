#include <iostream>
#include "Money.h"
#include <exception>
#include <fstream>
#include <cmath>
using namespace std;


MoneyX::MoneyX(long countOfPound, long countOfShilling, double countOfPence)
{
	if (countOfPound < 0 || countOfShilling < 0 || countOfPence < 0 || countOfShilling >= 20 || countOfPence >= 12)
	{
		throw exception("¬ведены неверные данные");
	}
	hpence = (countOfPence - fmod(countOfPence, 0.5)) * 2 + countOfShilling * 24 + countOfPound * 480;

	checkResult();
}


void MoneyX::checkResult()
{
	if (abs(hpence) > 480000000000)
	{
		if (hpence < 0)
		{
			hpence = -480000000000;
		}
		else
		{
			hpence = 480000000000;
		}
	}
}
MoneyX::MoneyX()
{
	hpence = 0;
}

MoneyX& MoneyX::operator+=(const MoneyX& money)
{
	hpence += money.hpence;
	checkResult();
	return *this;
}
MoneyX& MoneyX::operator-=(const MoneyX& money)
{
	hpence -= money.hpence;
	checkResult();
	return *this;
}
MoneyX MoneyX::operator+(const MoneyX& money)
{
	MoneyX newMoney;
	newMoney.hpence = hpence + money.hpence;
	newMoney.checkResult();
	return newMoney;
}
MoneyX MoneyX::operator-(const MoneyX& money)
{
	MoneyX newMoney;
	newMoney.hpence = hpence - money.hpence;
	newMoney.checkResult();
	return newMoney;
}

bool MoneyX::operator ==(const MoneyX& money)const
{
	return hpence == money.hpence;
}
bool MoneyX::operator !=(const MoneyX& money)const
{
	return hpence != money.hpence;
}
bool MoneyX::operator <(const MoneyX& money)const
{
	return hpence < money.hpence;
}
bool MoneyX::operator <=(const MoneyX& money)const
{
	return hpence <= money.hpence;
}
bool MoneyX::operator >(const MoneyX& money)const
{
	return hpence > money.hpence;
}
bool MoneyX::operator >= (const MoneyX& money)const
{
	return hpence >= money.hpence;
}


std::ostream& operator << (std::ostream& Stream, const MoneyX& Output)
{
	if (Output.hpence == 0)
	{
		Stream << "0p.";
		return Stream;
	}
	long long g, h;
	double i;
	g = (abs(Output.hpence) / 480);
	h = (abs(Output.hpence) - ((abs(Output.hpence) / 480) * 480)) / 24;

	i = (abs(Output.hpence) - ((abs(Output.hpence) / 24) * 24)) / 2;

	if (Output.hpence < 0)
	{
		Stream << "-";
	}
	if (g != 0)
	{
		Stream << g << "pd.";
	}
	if (h != 0)
	{
		Stream << h << "sh.";
	}
	if (i != 0)
	{
		if (fmod(i, 1) != 0)
		{
			Stream << int(i) << "," << 10 * fmod(i, 1) << "p.";
		}
		else
		{
			Stream << i << "p.";
		}
	}
	return Stream;
}

MoneyX::~MoneyX()
{};

