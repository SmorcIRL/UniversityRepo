#pragma once
#include <string>
#include <fstream>
#include <exception>

using namespace std;

enum CarbonationDegree { non_carbonated, low_carbonated, highly_carbonated };
enum BeerRawMaterial { barley, wheat, ginger };
enum TypeOfWine { dry, semi_dry, sweet };
enum LemonadeColor { red, yellow, green };
struct Date
{
	Date(unsigned int Day, unsigned int Month, unsigned int Year) : day(Day), month(Month), year(Year)
	{
		if (Day == 0 || Month == 0 || Day > 31 || Month > 12)
		{
			throw exception("Wrong date");
		}
	}

	unsigned int day;
	unsigned int month;
	unsigned int year;
};


class BottledDrinks
{
protected:
	const Date issue_date;
	const string drink_name;
	const double tare_volume;

public:
	BottledDrinks(Date IssueDate, string Drink_name, double Tare_volume);

	string GetDrinkName() const;
	double GetVolume() const;
	virtual void GetInfo(ostream& Stream) const;

	virtual ~BottledDrinks() = 0;
};


class AlcoholicDrinks : BottledDrinks
{
protected:
	double alcohol_degree;

public:
	AlcoholicDrinks(Date IssueDate, string Drink_name, double Tare_volume, double Alcohol_degree);

	double GetAlcoholDegree() const;
	virtual void GetInfo(ostream& Stream) const override;

	virtual ~AlcoholicDrinks() = 0;
};

class SoftDrinks : BottledDrinks
{
public:
	SoftDrinks(Date IssueDate, string Drink_name, double Tare_volume);

	virtual void GetInfo(ostream& Stream) const;

	virtual ~SoftDrinks() = 0;
};


class Beer : AlcoholicDrinks
{
private:

	BeerRawMaterial beer_raw_material;
	CarbonationDegree carbonation_degree;

public:
	Beer(Date IssueDate, string Drink_name, double Tare_volume, double Alcohol_degree, BeerRawMaterial Beer_raw_material, CarbonationDegree Carbonation_degree);

	BeerRawMaterial GetRawMaterial()const;
	CarbonationDegree GetCarbonationDegree()const;
	void GetInfo(ostream& Stream) const override;

	~Beer();
};

class Wine : AlcoholicDrinks
{
private:
	TypeOfWine type;

public:
	Wine(Date IssueDate, string Drink_name, double Tare_volume, double Alcohol_degree, TypeOfWine Type);

	TypeOfWine GetTypeOfWine() const;
	void GetInfo(ostream& Stream) const override;

	~Wine();
};

class Cognac : AlcoholicDrinks
{
private:
	unsigned int stars;

public:
	Cognac(Date IssueDate, string Drink_name, double Tare_volume, double Alcohol_degree, unsigned int Stars);

	unsigned int GetStars() const;
	void GetInfo(ostream& Stream) const override;

	~Cognac();
};


class Milk : SoftDrinks
{
private:
	double fat_degree;

public:
	Milk(Date IssueDate, string Drink_name, double Tare_volume, double Fat_degree);

	double GetFatDegree() const;
	void GetInfo(ostream& Stream) const override;

	~Milk();
};

class MineralWater : SoftDrinks
{
private:
	CarbonationDegree carbonation_degree;

public:
	MineralWater(Date IssueDate, string Drink_name, double Tare_volume, CarbonationDegree Carbonation_degree);

	CarbonationDegree GetCarbonationDegree() const;
	void GetInfo(ostream& Stream) const override;

	~MineralWater();
};

class Lemonade : SoftDrinks
{
private:
	LemonadeColor color;

public:
	Lemonade(Date IssueDate, string Drink_name, double Tare_volume, LemonadeColor Color);

	LemonadeColor GetColor() const;
	void GetInfo(ostream& Stream) const override;

	~Lemonade();
};
