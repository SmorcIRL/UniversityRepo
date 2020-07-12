#include "Tree.h"
#include <iostream>

using namespace std;

Tree::Tree()
{
	root = nullptr;
}

Tree::Item::Item(int x)
{
	info = x;
	lSon = rSon = father = nullptr;
};

bool Tree::Find(int x, Item *&p)
{
	p = root;
	Item *q = p;
	while (q != nullptr)
	{
		p = q;
		if ((*q).info == x)
		{
			return true;
		}

		if ((*q).info < x)
		{
			q = (*q).rSon;
		}
		else
		{
			q = (*q).lSon;
		}
	}

	return false;
}

bool Tree::Find(int x)
{
	Item *p;
	return Find(x, p);
}

bool Tree::Insert(int x)
{
	Item *r, *p; 

	if (root == nullptr)
	{
		r = new Item(x);
		root = r;
		return true;
	}

	if (Find(x, r))
	{
		return false;
	}

	p = new Item(x);
	(*p).father = r;
	if ((*r).info < x)
	{
		(*r).rSon = p;
	}
	else
	{
		(*r).lSon = p;
	}

	return true;
}

void Tree::deleteItem(Item *x)
{
	if ((*x).father == nullptr)
	{		
		if ((*x).lSon != nullptr) 
		{
			root = (*x).lSon;
			(*(*x).lSon).father = nullptr;
		}
		else 
		{
			root = (*x).rSon;
			if ((*x).rSon != nullptr)
			{
				(*(*x).rSon).father = nullptr;
			}
		}
	}

	else
	{
		if ((*(*x).father).lSon == x)
		{
			if ((*x).lSon != nullptr) 
			{
				(*(*x).father).lSon = (*x).lSon;
				(*(*x).lSon).father = (*x).father;
			}
			else 
			{
				(*(*x).father).lSon = (*x).rSon;
				if ((*x).rSon != nullptr)
				{
					(*(*x).rSon).father = (*x).father;
				}
			}
		}
		else
		{
			if ((*x).lSon != nullptr) 
			{
				(*(*x).father).rSon = (*x).lSon;
				(*(*x).lSon).father = (*x).father;
			}
			else 
			{
				(*(*x).father).rSon = (*x).rSon;
				if ((*x).rSon != nullptr)
				{
					(*(*x).rSon).father = (*x).father;
				}
			}
		}
	}

	(*x).father = (*x).lSon = (*x).rSon = nullptr;

	delete x;
}

bool Tree::Delete(int x)
{
	Item *r, *p;
	if (!Find(x, r))
	{
		return false;
	}

	if (((*r).lSon == nullptr) || ((*r).rSon == nullptr))
	{
		deleteItem(r);
		return true;
	}

	p = (*r).lSon;

	while ((*p).rSon != nullptr)
	{
		p = (*p).rSon;
	}

	(*r).info = (*p).info;
	deleteItem(p);
	return true;
}

Tree::~Tree()
{
	while (root)
	{
		deleteItem(root);
	}

	delete root;
}
