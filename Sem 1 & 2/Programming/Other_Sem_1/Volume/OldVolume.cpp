#include "OldVolume.h"
#include <fstream>
#include <exception>
#include <cmath>

using namespace std;

const double LittersInChark = 0.123;
const int BottlesInShtaf = 2, CharksInBottle = 5;

OldVolume::OldVolume(int Shtafs, int Bottles, double Charks)
{
	SetShtafs(Shtafs);
	SetBottles(Bottles);
	SetCharks(Charks);
}

int OldVolume::GetShtafs() const
{
	return (int)(SummInCharks / (BottlesInShtaf * CharksInBottle));
}
int OldVolume::GetBottles() const
{
	return (int)fmod(SummInCharks, (BottlesInShtaf * CharksInBottle)) / CharksInBottle;
}
double OldVolume::GetCharks() const
{
	return fmod(SummInCharks, CharksInBottle);
}

double OldVolume::GetVolumeInLiters() const
{
	return SummInCharks * LittersInChark;
}

void OldVolume::SetShtafs(int Shtafs)
{
	Shtafs < 0 ? throw exception("Wrong shtafs input") : SummInCharks += (Shtafs - GetShtafs()) * BottlesInShtaf * CharksInBottle;
}
void OldVolume::SetBottles(int Bottles)
{
	(Bottles < 0 || Bottles > 1) ? throw exception("Wrong bottles input") : SummInCharks += (Bottles - GetBottles()) * CharksInBottle;
}
void OldVolume::SetCharks(double Charks)
{
	(Charks < 0 || Charks > 3.75) ? throw exception("Wrong charks input") : SummInCharks += (Charks - fmod(Charks, 0.25)) - GetCharks();
}

OldVolume OldVolume::operator + (const OldVolume &RightOperand) const
{
	OldVolume Buffer;
	Buffer.SummInCharks = SummInCharks + RightOperand.SummInCharks;
	return Buffer;
}
OldVolume OldVolume::operator - (const OldVolume &RightOperand) const
{
	if (SummInCharks < RightOperand.SummInCharks) throw exception("Attempt to get negative volume");

	OldVolume Buffer;
	Buffer.SummInCharks = SummInCharks - RightOperand.SummInCharks;
	return Buffer;
}
OldVolume& OldVolume::operator += (const OldVolume &RightOperand)
{
	SummInCharks += RightOperand.SummInCharks;
	return *this;
}
OldVolume& OldVolume::operator -= (const OldVolume &RightOperand)
{
	SummInCharks < RightOperand.SummInCharks ? throw exception("Attempt to get negative volume") : SummInCharks -= RightOperand.SummInCharks;
	return *this;
}

bool OldVolume::operator == (const OldVolume &RightOperand) const
{
	return (SummInCharks == RightOperand.SummInCharks);
}
bool OldVolume::operator != (const OldVolume &RightOperand) const
{
	return !(*this == RightOperand);
}
bool OldVolume::operator > (const OldVolume &RightOperand) const
{
	return (SummInCharks > RightOperand.SummInCharks);
}
bool OldVolume::operator >= (const OldVolume &RightOperand) const
{
	return (*this == RightOperand) || (*this > RightOperand);
}
bool OldVolume::operator < (const OldVolume &RightOperand) const
{
	return !((*this == RightOperand) || (*this > RightOperand));
}
bool OldVolume::operator <= (const OldVolume &RightOperand) const
{
	return !(*this > RightOperand);
}

ostream& operator << (ostream &OutputStream, const OldVolume &Volume)
{
	if (Volume.SummInCharks == 0)
	{
		OutputStream << "0 ch.";
		return OutputStream;
	}

	int a = Volume.GetShtafs(), b = Volume.GetBottles();
	double c = Volume.GetCharks();

	if (a) OutputStream << a << " sht. ";
	if (b) OutputStream << b << " bt. ";
	if (c) OutputStream << c << " ch. ";

	return OutputStream;
}

OldVolume::~OldVolume()
{
}