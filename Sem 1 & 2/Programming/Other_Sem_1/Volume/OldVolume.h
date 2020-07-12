#pragma once
#include <fstream>

using namespace std;

class OldVolume
{
private:
	double SummInCharks = 0;

public:
	OldVolume(int Shtafs = 0, int Bottles = 0, double Charks = 0);

	int GetShtafs() const;
	int GetBottles() const;
	double GetCharks() const;

	double GetVolumeInLiters() const;

	void SetShtafs(int Shtafs);
	void SetBottles(int Bottles);
	void SetCharks(double Charks);

	OldVolume operator + (const OldVolume &RightOperand) const;
	OldVolume operator - (const OldVolume &RightOperand) const;
	OldVolume& operator += (const OldVolume &RightOperand);
	OldVolume& operator -= (const OldVolume &RightOperand);

	bool operator == (const OldVolume &RightOperand) const;
	bool operator != (const OldVolume &RightOperand) const;
	bool operator > (const OldVolume &RightOperand) const;
	bool operator >= (const OldVolume &RightOperand) const;
	bool operator < (const OldVolume &RightOperand) const;
	bool operator <= (const OldVolume &RightOperand) const;

	friend ostream& operator << (ostream &OutputStream, const OldVolume &Volume);

	~OldVolume();
	
};