#pragma once
#include <fstream>
#include <exception>

using namespace std;

class OldWeight
{
private:
	int SummInZol = 0;
	OldWeight(long Summ);

public:
	OldWeight(int Poods = 0, int Pounds = 0, int Zols = 0);

	double GetWeightInKilo() const;

	int GetPoods() const;
	int GetPounds() const;
	int GetZols() const;

	void SetPoods(int Poods);
	void SetPounds(int Pounds);
	void SetZols(int Zols);

	bool operator == (const OldWeight &RightOperator) const;
	bool operator != (const OldWeight &RightOperator) const;
	bool operator > (const OldWeight &RightOperator) const;
	bool operator >= (const OldWeight &RightOperator) const;
	bool operator < (const OldWeight &RightOperator) const;
	bool operator <= (const OldWeight &RightOperator) const;

	OldWeight operator + (const OldWeight &RightOperator) const;
	OldWeight operator - (const OldWeight &RightOperator) const;

	OldWeight operator += (const OldWeight &RightOperator);
	OldWeight operator -= (const OldWeight &RightOperator);

	friend ostream& operator << (ostream &, const OldWeight &);

	~OldWeight();
};
