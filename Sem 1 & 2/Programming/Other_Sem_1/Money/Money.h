#pragma once
#include <fstream>
#include <exception>
class MoneyX
{
	//private:

public:
	long long hpence;
	//�����������
	MoneyX(long countOfPound, long countOfShilling, double countOfPence);
	//������������ �� ���������
	MoneyX();
	//�������� � ����������
	void checkResult();

	//��������� ������������
	MoneyX& operator=(const MoneyX& money);
	//�������� +=
	MoneyX& operator+=(const MoneyX& money);
	//�������� -=
	MoneyX& operator-=(const MoneyX& money);


	//�������� +
	MoneyX operator+(const MoneyX& money);
	//�������� -
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

