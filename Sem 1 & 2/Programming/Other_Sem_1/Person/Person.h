#pragma once
#include <string>

using namespace std;

enum Gender { male = 0, female = 1 };

class Person
{
protected:
	static int next_ID;

	string name;
	const int ID;
	const Gender gender;

public:
	Person(string, Gender);

	int GetID() const;
	string GetName() const;
	string GetGender() const;
	void SetName(string);

	auto GiveBirth(string, Gender, Person*);
	auto GiveBirth(string, Gender);

	~Person();
};


class OrdinaryPerson : public Person
{
private:
	const Person* mother;
	Person* father;

public:
	OrdinaryPerson(string, Gender, const Person*);
	OrdinaryPerson(string, Gender, const Person*, Person*);

	string GetMothersName() const;
	string GetFathersName() const;

	OrdinaryPerson* GiveBirth(string, Gender, Person*);
	OrdinaryPerson* GiveBirth(string, Gender);
	~OrdinaryPerson();
};