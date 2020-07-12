#pragma once
#include <exception>
#include <fstream>
#include <string>

using namespace std;

template<class T>
class Deque
{
private:

	int count;

	struct Element
	{
		T value;
		Element* next, * previous;
	};

	Element* left, * right;

public:

	Deque();
	Deque(const Deque&);

	int GetCount() const;
	T GetLeftValue() const;
	T GetRightValue() const;

	void OutputFromLeft(std::ostream&) const;
	void OutputFromRight(std::ostream&) const;

	void AddToLeft(T);
	void AddToRight(T);

	void DeleteFromLeft();
	void DeleteFromRight();
	void Clear();
	void SortAscending();

	Deque& operator = (const Deque&);

	~Deque();
};

template<class T>
Deque<T>::Deque() : left(nullptr), right(nullptr), count(0)
{
}

template<class T>
Deque<T>::Deque(const Deque& Original)
{
	Element* buffer = new Element();
	buffer = Original.right;

	while (buffer)
	{
		AddToLeft(buffer->value);
		buffer = buffer->next;
	}

	delete buffer;

	count = Original.count;
}

template<class T>
int Deque<T>::GetCount() const
{
	return count;
}

template<class T>
T Deque<T>::GetLeftValue() const
{
	if (count)
	{
		return (*left).value;
	}
	else
	{
		throw exception("Attempt to get value from an empty deque");
	}
}

template<class T>
T Deque<T>::GetRightValue() const
{
	if (count)
	{
		return (*right).value;
	}
	else
	{
		throw exception("Attempt to get value from an empty deque");
	}
}

template<class T>
void Deque<T>::OutputFromLeft(std::ostream& Stream) const
{
	Element* buffer = new Element;

	buffer = left;

	while (buffer)
	{
		Stream << buffer->value << " ";
		buffer = buffer->previous;
	}

	Stream << endl;

	delete buffer;
}

template<class T>
void Deque<T>::OutputFromRight(std::ostream& Stream) const
{
	Element* buffer = new Element;

	buffer = right;

	while (buffer)
	{
		Stream << buffer->value << " ";
		buffer = buffer->next;
	}

	Stream << endl;

	delete buffer;
}

template<class T>
void Deque<T>::AddToLeft(T NewElement)
{
	Element* buffer = new Element;

	buffer->next = nullptr;
	buffer->value = NewElement;

	if (count)
	{
		buffer->previous = left;
		left->next = buffer;
		left = buffer;
	}
	else
	{
		buffer->previous = nullptr;
		left = right = buffer;
	}

	count++;
}

template<class T>
void Deque<T>::AddToRight(T NewElement)
{
	Element* buffer = new Element;

	buffer->previous = nullptr;
	buffer->value = NewElement;

	if (count)
	{
		buffer->next = right;
		right->previous = buffer;
		right = buffer;
	}
	else
	{
		buffer->next = nullptr;
		left = right = buffer;
	}

	count++;
}

template<class T>
void Deque<T>::DeleteFromLeft()
{
	if (!count)
	{
		throw exception("Attempt to delete value from an empty deque");
	}
	else if (count == 1)
	{
		delete left, right;
		right = left = nullptr;
		count--;
		return;
	}

	Element* buffer = new Element;
	buffer = left;
	left = left->previous;
	left->next = nullptr;
	delete buffer;

	count--;
}

template<class T>
void Deque<T>::DeleteFromRight()
{
	if (!count)
	{
		throw exception("Attempt to delete value from an empty deque");
	}
	else if (count == 1)
	{
		delete left;
		right = left = nullptr;
		count--;
		return;
	}

	Element* buffer = new Element;

	buffer = right;
	right = right->next;
	right->previous = nullptr;
	delete buffer;
	count--;
}

template<class T>
void Deque<T>::Clear()
{
	while (count)
	{
		DeleteFromRight();
	}
}

template<class T>
void Deque<T>::SortAscending()
{
	if (count <= 1)
	{
		return;
	}

	Element* buffer = new Element();
	Element* iterator = new Element();

	buffer = left->previous;

	while (buffer)
	{
		iterator = buffer->next;
		while (iterator != nullptr)
		{
			if (iterator->value <= buffer->value)
			{
				Element* buffercopy = new Element();

				buffercopy->value = buffer->value;
				buffercopy->previous = iterator->previous;
				buffercopy->next = iterator;

				iterator->previous->next = buffercopy;
				iterator->previous = buffercopy;

				buffer->next->previous = buffer->previous;
				if (buffer->previous != nullptr)
				{
					buffer->previous->next = buffer->next;
				}
				else
				{
					right = buffer->next;
				}

				buffer = buffer->previous;
				break;
			}

			else if (iterator == left && iterator->value > buffer->value)
			{
				AddToLeft(buffer->value);
				count--;

				buffer->next->previous = buffer->previous;

				if (buffer->previous != nullptr)
				{
					buffer->previous->next = buffer->next;
				}
				else
				{
					right = buffer->next;
				}

				buffer = buffer->previous;
				break;
			}

			iterator = iterator->next;
		}

		if (buffer->previous == nullptr)
		{
			right = buffer;
		}
		buffer = buffer->previous;
	}

	buffer = nullptr;
	iterator = nullptr;
	delete buffer;
	delete iterator;
}
void Deque<const char*>::SortAscending()
{
	if (count <= 1)
	{
		return;
	}

	Element* buffer = new Element();
	Element* iterator = new Element();

	buffer = left->previous;

	while (buffer)
	{
		iterator = buffer->next;
		while (iterator != nullptr)
		{
			if (!(strcmp(iterator->value, buffer->value) > 0))
			{
				Element* buffercopy = new Element();

				buffercopy->value = buffer->value;
				buffercopy->previous = iterator->previous;
				buffercopy->next = iterator;

				iterator->previous->next = buffercopy;
				iterator->previous = buffercopy;

				buffer->next->previous = buffer->previous;
				if (buffer->previous != nullptr)
				{
					buffer->previous->next = buffer->next;
				}
				else
				{
					right = buffer->next;
				}

				buffer = buffer->previous;
				break;
			}

			else if (iterator == left && strcmp(iterator->value, buffer->value) > 0)
			{
				AddToLeft(buffer->value);
				count--;

				buffer->next->previous = buffer->previous;

				if (buffer->previous != nullptr)
				{
					buffer->previous->next = buffer->next;
				}
				else
				{
					right = buffer->next;
				}

				buffer = buffer->previous;
				break;
			}

			iterator = iterator->next;
		}

		if (buffer->previous == nullptr)
		{
			right = buffer;
		}
		buffer = buffer->previous;
	}

	buffer = nullptr;
	iterator = nullptr;
	delete buffer;
	delete iterator;
}
void Deque<string>::SortAscending()
{
	if (count <= 1)
	{
		return;
	}

	Element* buffer = new Element();
	Element* iterator = new Element();

	buffer = left->previous;

	while (buffer)
	{
		iterator = buffer->next;
		while (iterator != nullptr)
		{
			if (!(strcmp((iterator->value).c_str(), (buffer->value).c_str()) > 0))
			{
				Element* buffercopy = new Element();

				buffercopy->value = buffer->value;
				buffercopy->previous = iterator->previous;
				buffercopy->next = iterator;

				iterator->previous->next = buffercopy;
				iterator->previous = buffercopy;

				buffer->next->previous = buffer->previous;
				if (buffer->previous != nullptr)
				{
					buffer->previous->next = buffer->next;
				}
				else
				{
					right = buffer->next;
				}

				buffer = buffer->previous;
				break;
			}

			else if (iterator == left && strcmp((iterator->value).c_str(), (buffer->value).c_str()) > 0)
			{
				AddToLeft(buffer->value);
				count--;

				buffer->next->previous = buffer->previous;

				if (buffer->previous != nullptr)
				{
					buffer->previous->next = buffer->next;
				}
				else
				{
					right = buffer->next;
				}

				buffer = buffer->previous;
				break;
			}

			iterator = iterator->next;
		}

		if (buffer->previous == nullptr)
		{
			right = buffer;
		}
		buffer = buffer->previous;
	}

	buffer = nullptr;
	iterator = nullptr;
	delete buffer;
	delete iterator;
}

template<class T>
Deque<T>& Deque<T>::operator = (const Deque& RightDeque)
{
	Clear();

	Element* buffer = new Element();
	buffer = RightDeque.right;

	while (buffer)
	{
		AddToLeft(buffer->value);
		buffer = buffer->next;
	}

	delete buffer;
	count = RightDeque.count;

	return *this;
}

template<class T>
Deque<T>::~Deque()
{
	Clear();
}