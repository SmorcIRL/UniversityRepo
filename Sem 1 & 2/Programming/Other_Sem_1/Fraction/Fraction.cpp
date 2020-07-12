#include "Fraction.h"
#include <cmath>

Fraction::Fraction(long Num, long Denum)
{
	if (GetDenum() == 0)
	{
		throw "Division by zero";
	}
	else if (GetDenum() < 0)
	{
		throw "Denumerator is less then zero";
	}

	SetNum(Num);
	SetDenum(Denum);

	Reduction();
}

long Fraction::GetNum() const
{
	return Numerator;
}

long Fraction::GetDenum() const
{
	return Denumerator;
}

void Fraction::SetNum(long FormalNum)
{
	if (FormalNum == 0)
	{
		SetDenum(0);
	}

	Numerator = FormalNum;
	Reduction();
}

void Fraction::SetDenum(long FormalDenum)
{
	if (Denumerator == 0)
	{
		throw "Division by zero";
	}
	else if (Denumerator < 0)
	{
		throw "Denumerator is less then zero";
	}

	Denumerator = FormalDenum;
	Reduction();
}

double Fraction::Result() const
{
	return (GetNum() / GetDenum());
}

void Fraction::Reduction()
{
	long NumCopy = abs(GetNum()), DenumCopy = abs(GetDenum());

	while (NumCopy != DenumCopy)
	{
		if (NumCopy > DenumCopy)
		{
			long Buffer = NumCopy;
			NumCopy = DenumCopy;
			DenumCopy = Buffer;
		}

		DenumCopy = DenumCopy - NumCopy;
	}

	SetNum(Numerator / NumCopy);
	SetDenum(Denumerator / NumCopy);
}

#pragma region Comparative operators

bool Fraction::operator == (const Fraction &RightFraction)
{
	return this->Result() == RightFraction.Result();
}

bool Fraction::operator != (const Fraction &RightFraction)
{
	return !(this->Result() == RightFraction.Result());
}

bool Fraction::operator > (const Fraction &RightFraction)
{
	return this->Result() > RightFraction.Result();
}

bool Fraction::operator >= (const Fraction &RightFraction)
{
	return this->Result() >= RightFraction.Result();
}

bool Fraction::operator < (const Fraction &RightFraction)
{
	return this->Result() < RightFraction.Result();
}

bool Fraction::operator <= (const Fraction &RightFraction)
{
	return this->Result() <= RightFraction.Result();
}

#pragma endregion

#pragma region Arithmetic operators

Fraction Fraction::operator + (const Fraction &RightFraction)
{
	long a = GetDenum(), b = RightFraction.GetDenum(), c;

	while (a != b)
	{
		if (a > b) 
		{
			long tmp = a;
			a = b;
			b = tmp;
		}
		b = b - a;
	}

	c = (GetDenum()*RightFraction.GetDenum()) / a;

	Fraction Buffer(GetNum() * (GetDenum() / c) + RightFraction.GetNum() * (RightFraction.GetDenum() / c), c);
	return Buffer;
}

#pragma endregion

Fraction::~Fraction()
{
}
