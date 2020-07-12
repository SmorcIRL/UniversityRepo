#include "Person.h"
#include <exception>
#include <string>

using namespace std;

int Person::next_ID = 0;


Person::Person(string Name, Gender Gender) : name(Name), ID(++next_ID), gender(Gender)
{
	if (Name == "")
	{
		throw exception("Exception: Blank name");
	}
}

int Person::GetID() const
{
	return ID;
}

string Person::GetName() const
{
	return name;
}

string Person::GetGender() const
{
	if (bool(gender))
	{
		return "female";
	}
	return "male";
}

void Person::SetName(string NewName)
{
	name = NewName;
}

auto Person::GiveBirth(string Name, Gender Gen, Person* Father)
{
	if (this == Father)
	{
		throw exception("Exception: A woman cannot give birth to herself");
	}
	else if (gender == Gender::male)
	{
		throw exception("Exception: Men still can't give birth");
	}

	OrdinaryPerson* Child = new OrdinaryPerson(Name, Gen, this, Father);
	return Child;
}

auto Person::GiveBirth(string Name, Gender Gen)
{
	if (gender == Gender::male)
	{
		throw exception("Exception: Men still can't give birth");
	}

	OrdinaryPerson* Child = new OrdinaryPerson(Name, Gen, this);
	return Child;
}

Person::~Person()
{
}



OrdinaryPerson::OrdinaryPerson(string Name, Gender Gender, const Person* Mother) : Person(Name, Gender), mother(Mother), father(nullptr)
{

}

OrdinaryPerson::OrdinaryPerson(string Name, Gender Gender, const Person* Mother, Person* Father) : Person(Name, Gender), mother(Mother), father(Father)
{

}

string OrdinaryPerson::GetMothersName() const
{
	if (mother != nullptr)
	{
		return mother->GetName();
	}

	throw exception("Exception: No mother, so sad...");
}

string OrdinaryPerson::GetFathersName() const
{
	if (father != nullptr)
	{
		return father->GetName();
	}

	throw exception("Exception: Unknown father, so sad...");
}

OrdinaryPerson* OrdinaryPerson::GiveBirth(string Name, Gender Gen, Person* Father)
{
	if (this == Father)
	{
		throw exception("Exception: A woman cannot give birth to herself");
	}
	else if (gender == Gender::male)
	{
		throw exception("Exception: Men still can't give birth");
	}

	OrdinaryPerson* Child = new OrdinaryPerson(Name, Gen, this, Father);
	return Child;
}

OrdinaryPerson* OrdinaryPerson::GiveBirth(string Name, Gender Gen)
{
	if (gender == Gender::male)
	{
		throw exception("Exception: Men still can't give birth");
	}

	OrdinaryPerson* Child = new OrdinaryPerson(Name, Gen, this);
	return Child;
}

OrdinaryPerson::~OrdinaryPerson()
{
}