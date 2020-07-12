#include "OldWeight.h"
#include <fstream>
#include <exception>

using namespace std;

const double ZolWeightInKilos = 0.004265625;
const int PoundsInPood = 40, ZolsInPound = 96, ZolsInPood = 3840;


OldWeight::OldWeight(long Summ)
{
	if (Summ < 0)
	{
		throw exception("Attempt to get negative weight");
	}

	else
	{
		SummInZol = (int)Summ;
	}
}

OldWeight::OldWeight(int Poods, int Pounds, int Zols)
{
	SetPoods(Poods);
	SetPounds(Pounds);
	SetZols(Zols);
}


int OldWeight::GetPoods() const
{
	return (SummInZol / ZolsInPood);
}

int OldWeight::GetPounds() const
{
	return ((SummInZol % ZolsInPood) / ZolsInPound);
}

int OldWeight::GetZols() const
{
	return (SummInZol % ZolsInPound);
}

double OldWeight::GetWeightInKilo() const
{
	return (double)SummInZol * ZolWeightInKilos;
}


void OldWeight::SetPoods(int Poods)
{
	Poods < 0 ? throw exception("Wrong poods parameter") : SummInZol += (Poods - GetPoods()) * 3840;
}

void OldWeight::SetPounds(int Pounds)
{
	Pounds < 0 || Pounds > 39 ? throw exception("Wrong pounds parameter") : SummInZol += (Pounds - GetPounds()) * 96;
}

void OldWeight::SetZols(int Zols)
{
	Zols < 0 || Zols > 95 ? throw exception("Wrong zolotniks parameter") : SummInZol += Zols - GetZols();
}


bool OldWeight::operator == (const OldWeight &RightOperator) const
{
	return (SummInZol == RightOperator.SummInZol);
}

bool OldWeight::operator != (const OldWeight &RightOperator) const
{
	return !((*this) == RightOperator);
}

bool OldWeight::operator > (const OldWeight &RightOperator) const
{
	return (SummInZol > RightOperator.SummInZol);
}

bool OldWeight::operator >= (const OldWeight &RightOperator) const
{
	return ((*this) > RightOperator) || ((*this) == RightOperator);
}

bool OldWeight::operator < (const OldWeight &RightOperator) const
{
	return !((*this) >= RightOperator);
}

bool OldWeight::operator <= (const OldWeight &RightOperator) const
{
	return !((*this) > RightOperator);
}


OldWeight OldWeight::operator + (const OldWeight &RightOperator) const
{
	OldWeight Buffer((long)SummInZol + RightOperator.SummInZol);
	return Buffer;
}

OldWeight OldWeight::operator - (const OldWeight &RightOperator) const
{
	OldWeight Buffer((long)SummInZol - RightOperator.SummInZol);
	return Buffer;
}

OldWeight OldWeight::operator += (const OldWeight &RightOperator)
{
	OldWeight Buffer((long)SummInZol + RightOperator.SummInZol);
	(*this) = Buffer;
	return Buffer;
}

OldWeight OldWeight::operator -= (const OldWeight &RightOperator)
{
	OldWeight Buffer((long)SummInZol - RightOperator.SummInZol);
	(*this) = Buffer;
	return Buffer;
}

ostream& operator << (ostream &OutStream, const OldWeight &Weight)
{
	if (Weight.SummInZol == 0)
	{
		OutStream << "0 zl.";
		return OutStream;
	}

	int Poods = Weight.GetPoods(), Pounds = Weight.GetPounds(), Zols = Weight.GetZols();

	if (Poods)
	{
		OutStream << Poods << " pd. ";
	}

	if (Pounds)
	{
		OutStream << Pounds << " lb. ";
	}

	if (Zols)
	{
		OutStream << Zols << " zl. ";
	}

	return OutStream;
}


OldWeight::~OldWeight()
{
}