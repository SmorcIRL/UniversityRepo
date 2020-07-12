#pragma once
#include <fstream>
#include <exception>
class MoneyX
{
	//private:

public:
	long long hpence;
	//конструктор
	MoneyX(long countOfPound, long countOfShilling, double countOfPence);
	//конструктора по умолчанию
	MoneyX();
	//проверка в операторах
	void checkResult();

	//операторы присваивания
	MoneyX& operator=(const MoneyX& money);
	//операция +=
	MoneyX& operator+=(const MoneyX& money);
	//операция -=
	MoneyX& operator-=(const MoneyX& money);


	//операция +
	MoneyX operator+(const MoneyX& money);
	//операция -
	MoneyX operator-(const MoneyX& money);

	bool operator == (const MoneyX& money)const;
	bool operator != (const MoneyX& money)const;
	bool operator >  (const MoneyX& money)const;
	bool operator >= (const MoneyX& money)const;
	bool operator <  (const MoneyX& money)const;
	bool operator <= (const MoneyX& money)const;

	friend std::ostream& operator << (std::ostream&, const MoneyX&);
	virtual ~MoneyX();
};

