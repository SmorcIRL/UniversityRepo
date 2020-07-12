#pragma once
class Fraction
{
private:
	long Numerator, Denumerator;
	void Reduction();

public:

	Fraction(long = 0, long = 1);

	long GetNum() const;
	long GetDenum() const;

	void SetNum(long);
	void SetDenum(long);

	double Result() const;

	bool operator == (const Fraction &RightFraction);
	bool operator != (const Fraction &RightFraction);
	bool operator > (const Fraction &RightFraction);
	bool operator >= (const Fraction &RightFraction);
	bool operator < (const Fraction &RightFraction);
	bool operator <= (const Fraction &RightFraction);

	Fraction operator + (const Fraction &RightFraction);

	virtual ~Fraction();
};

