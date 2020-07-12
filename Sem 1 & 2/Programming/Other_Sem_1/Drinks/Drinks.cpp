#include "Drinks.h"
#include <string>
#include <fstream>
#include <exception>



BottledDrinks::BottledDrinks(Date IssueDate, string Drink_name, double Tare_volume) : issue_date(IssueDate), drink_name(Drink_name), tare_volume(Tare_volume)
{
}

string BottledDrinks::GetDrinkName() const
{
	return drink_name;
}

double BottledDrinks::GetVolume() const
{
	return tare_volume;
}

void BottledDrinks::GetInfo(ostream& Stream) const
{
	Stream << "Date: " << issue_date.day << '.' << issue_date.month << '.' << issue_date.year << endl;
	Stream << "Title: " << drink_name << endl;
	Stream << "Volume: " << tare_volume << endl;
	return;
}

BottledDrinks::~BottledDrinks()
{
}



AlcoholicDrinks::AlcoholicDrinks(Date IssueDate, string Drink_name, double Tare_volume, double Alcohol_degree) : BottledDrinks(IssueDate, Drink_name, Tare_volume), alcohol_degree(Alcohol_degree)
{
	if (Alcohol_degree < 0 || Alcohol_degree>100)
	{
		throw exception("Impossible alcohol degree");
	}
}

double AlcoholicDrinks::GetAlcoholDegree() const
{
	return alcohol_degree;
}

void AlcoholicDrinks::GetInfo(ostream& Stream) const
{
	BottledDrinks::GetInfo(Stream);
	Stream << "Alcohol degree: " << alcohol_degree << endl;
	return;
}

AlcoholicDrinks::~AlcoholicDrinks()
{
}



SoftDrinks::SoftDrinks(Date IssueDate, string Drink_name, double Tare_volume) : BottledDrinks(IssueDate, Drink_name, Tare_volume)
{
}

void SoftDrinks::GetInfo(ostream& Stream) const
{
	BottledDrinks::GetInfo(Stream);
	return;
}

SoftDrinks::~SoftDrinks()
{
}


Beer::Beer(Date IssueDate, string Drink_name, double Tare_volume, double Alcohol_degree, BeerRawMaterial Beer_raw_material, CarbonationDegree Carbonation_degree) : AlcoholicDrinks(IssueDate, Drink_name, Tare_volume, Alcohol_degree), beer_raw_material(Beer_raw_material), carbonation_degree(Carbonation_degree)
{
}

BeerRawMaterial Beer::GetRawMaterial() const
{
	return beer_raw_material;
}

CarbonationDegree Beer::GetCarbonationDegree() const
{
	return carbonation_degree;
}

void Beer::GetInfo(ostream& Stream) const
{
	AlcoholicDrinks::GetInfo(Stream);

	if (beer_raw_material == BeerRawMaterial::barley)
	{
		Stream << "Raw material: barley" << endl;
	}
	else if (beer_raw_material == BeerRawMaterial::wheat)
	{
		Stream << "Raw material: wheat" << endl;

	}
	else
	{
		Stream << "Raw material: ginger" << endl;
	}

	if (carbonation_degree == CarbonationDegree::non_carbonated)
	{
		Stream << "Carbonation degree: non-carbonated" << endl;
	}
	else if (carbonation_degree == CarbonationDegree::low_carbonated)
	{
		Stream << "Carbonation degree: low carbonated" << endl;
	}
	else
	{
		Stream << "Carbonation degree: highly carbonated" << endl;
	}

	Stream << endl;

	return;
}

Beer::~Beer()
{
}


Wine::Wine(Date IssueDate, string Drink_name, double Tare_volume, double Alcohol_degree, TypeOfWine Type) : AlcoholicDrinks(IssueDate, Drink_name, Tare_volume, Alcohol_degree), type(Type)
{
}

TypeOfWine Wine::GetTypeOfWine() const
{
	return type;
}

void Wine::GetInfo(ostream& Stream) const
{
	AlcoholicDrinks::GetInfo(Stream);

	if (type == TypeOfWine::dry)
	{
		Stream << "Wine type: dry" << endl;
	}
	else if (type == TypeOfWine::semi_dry)
	{
		Stream << "Wine type: semi-dry" << endl;

	}
	else
	{
		Stream << "Wine type: sweet" << endl;
	}

	Stream << endl;


	return;
}

Wine::~Wine()
{
}


Cognac::Cognac(Date IssueDate, string Drink_name, double Tare_volume, double Alcohol_degree, unsigned int Stars) : AlcoholicDrinks(IssueDate, Drink_name, Tare_volume, Alcohol_degree), stars(Stars)
{
}

unsigned int Cognac::GetStars() const
{
	return stars;
}

void Cognac::GetInfo(ostream& Stream) const
{
	AlcoholicDrinks::GetInfo(Stream);

	Stream << "Stars count: " << stars << endl << endl;
	return;
}

Cognac::~Cognac()
{
}



Milk::Milk(Date IssueDate, string Drink_name, double Tare_volume, double Fat_degree) : SoftDrinks(IssueDate, Drink_name, Tare_volume), fat_degree(Fat_degree)
{
	if (fat_degree < 0 || fat_degree > 100)
	{
		throw exception("Impossible fat degree");
	}
}

double Milk::GetFatDegree() const
{
	return fat_degree;
}

void Milk::GetInfo(ostream& Stream) const
{
	SoftDrinks::GetInfo(Stream);

	Stream << "Fat degree: " << fat_degree << endl << endl;

	return;
}

Milk::~Milk()
{
}



MineralWater::MineralWater(Date IssueDate, string Drink_name, double Tare_volume, CarbonationDegree Carbonation_degree) : SoftDrinks(IssueDate, Drink_name, Tare_volume), carbonation_degree(Carbonation_degree)
{
}

CarbonationDegree MineralWater::GetCarbonationDegree() const
{
	return carbonation_degree;
}

void MineralWater::GetInfo(ostream& Stream) const
{
	SoftDrinks::GetInfo(Stream);

	if (carbonation_degree == CarbonationDegree::non_carbonated)
	{
		Stream << "Carbonation degree: non-carbonated" << endl;
	}
	else if (carbonation_degree == CarbonationDegree::low_carbonated)
	{
		Stream << "Carbonation degree: low carbonated" << endl;
	}
	else
	{
		Stream << "Carbonation degree: highly carbonated" << endl;
	}
	Stream << endl;
	return;
}

MineralWater::~MineralWater()
{
}



Lemonade::Lemonade(Date IssueDate, string Drink_name, double Tare_volume, LemonadeColor Color) : SoftDrinks(IssueDate, Drink_name, Tare_volume), color(Color)
{

}

LemonadeColor Lemonade::GetColor() const
{
	return color;
}

void Lemonade::GetInfo(ostream& Stream) const
{
	SoftDrinks::GetInfo(Stream);

	if (color == LemonadeColor::red)
	{
		Stream << "Color: red" << endl;
	}
	else if (color == LemonadeColor::yellow)
	{
		Stream << "Color: yellow" << endl;
	}
	else
	{
		Stream << "Color: green" << endl;
	}

	Stream << endl;

	return;
}

Lemonade::~Lemonade()
{
}