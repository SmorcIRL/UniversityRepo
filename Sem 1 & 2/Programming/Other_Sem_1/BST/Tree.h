#pragma once
class Tree
{
private:
	class Item
	{
	public:
		int info;
		Item(int);
		Item *lSon, *rSon, *father;
	};

	Item *root;
	bool Find(int, Item *&);
	void deleteItem(Item*);

public:

	Tree();
	bool Find(int);
	bool Insert(int);
	bool Delete(int);
	~Tree();
};