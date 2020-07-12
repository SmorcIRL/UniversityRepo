#pragma once
#include <fstream>

using namespace std;

class TimeSpan
{
private:
	int hours;
	int minutes;
	int seconds;

public:
	TimeSpan(unsigned int Hours, unsigned int Minutes, unsigned int Seconds);

	void SetHours(unsigned int Hours);
	void SetMinutes(unsigned int Minutes);
	void SetSeconds(unsigned int Seconds);

	unsigned int GetHours() const;
	unsigned int GetMinutes() const;
	unsigned int GetSeconds() const;

	double GetDayPercent() const;

	bool operator == (const TimeSpan &RightOperand) const;
	bool operator != (const TimeSpan &RightOperand) const;
	bool operator > (const TimeSpan &RightOperand) const;
	bool operator >= (const TimeSpan &RightOperand) const;
	bool operator < (const TimeSpan &RightOperand) const;
	bool operator <= (const TimeSpan &RightOperand) const;

	int operator - (const TimeSpan &RightOperand) const;

	void Output(ostream &OutputStream) const;

	~TimeSpan();
};

