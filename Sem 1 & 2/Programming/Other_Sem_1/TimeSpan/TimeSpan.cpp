#include "TimeSpan.h"
#include <exception>
#include <fstream>

using namespace std;


TimeSpan::TimeSpan(unsigned int Hours, unsigned int Minutes, unsigned int Seconds)
{
	SetHours(Hours);
	SetMinutes(Minutes);
	SetSeconds(Seconds);
}


void TimeSpan::SetHours(unsigned int Hours)
{
	Hours < 24 ? hours = Hours : throw exception("Wrong hours input");
}

void TimeSpan::SetMinutes(unsigned int Minutes)
{
	Minutes < 60 ? minutes = Minutes : throw exception("Wrong minutes input");
}

void TimeSpan::SetSeconds(unsigned int Seconds)
{
	Seconds < 60 ? seconds = Seconds : throw exception("Wrong seconds input");
}


unsigned int TimeSpan::GetHours() const
{
	return hours;
}

unsigned int TimeSpan::GetMinutes() const
{
	return minutes;
}

unsigned int TimeSpan::GetSeconds() const
{
	return seconds;
}


double TimeSpan::GetDayPercent() const
{
	return (double)(hours * 3600 + minutes * 60 + seconds) / 86400;
}


bool TimeSpan::operator == (const TimeSpan &RightOperand) const
{
	if ((*this) - RightOperand == 0)
	{
		return true;
	}

	return false;
}

bool TimeSpan::operator != (const TimeSpan &RightOperand) const
{
	return !((*this) == RightOperand);
}

bool TimeSpan::operator > (const TimeSpan &RightOperand) const
{
	if ((*this) - RightOperand > 0)
	{
		return true;
	}

	return false;
}

bool TimeSpan::operator >= (const TimeSpan &RightOperand) const
{
	return (*this) == RightOperand || (*this) > RightOperand;
}

bool TimeSpan::operator < (const TimeSpan &RightOperand) const
{
	return !((*this) == RightOperand || (*this) > RightOperand);
}

bool TimeSpan::operator <= (const TimeSpan &RightOperand) const
{
	return !((*this) > RightOperand);
}


int TimeSpan::operator - (const TimeSpan &RightOperand) const
{
	return (hours * 3600 + minutes * 60 + seconds) - (RightOperand.hours * 3600 + RightOperand.minutes * 60 + RightOperand.seconds);
}


void TimeSpan::Output(ostream &OutputStream) const
{
	hours >= 10 ? OutputStream << hours : OutputStream << 0 << hours;
	OutputStream << ".";

	minutes >= 10 ? OutputStream << minutes : OutputStream << 0 << minutes;
	OutputStream << ":";

	seconds >= 10 ? OutputStream << seconds : OutputStream << 0 << seconds;
	OutputStream << endl;
}


TimeSpan::~TimeSpan()
{
}
