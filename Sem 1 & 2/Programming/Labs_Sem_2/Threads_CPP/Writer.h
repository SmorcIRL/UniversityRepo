#pragma once
#include <iostream>
#include <string>
#include <limits>
#include <mutex>

using namespace std;


class Writer
{
private:
	string _stringToWrite;
	static mutex WriterMutex;

public:
	static unsigned int RecordLeft;
	int _count, _delta;


	Writer(string stringToWrite, unsigned int count, unsigned int  delta)
	{
		_stringToWrite = stringToWrite;
		_count = count > INT_MAX ? INT_MAX : (int)count;
		_delta = delta > INT_MAX ? INT_MAX : (int)delta;
	}

	void StartWriting()
	{
		while (_count != 0 && RecordLeft != 0)
		{
			WriterMutex.lock();

			if (RecordLeft == 0)
				return;
			else
			{
				--RecordLeft;
				--_count;
			}

			WriterMutex.unlock();


			this_thread::sleep_for(chrono::milliseconds(_delta));


			WriterMutex.lock();

			cout << _stringToWrite << endl;

			WriterMutex.unlock();
		}
	}

	~Writer()
	{

	}
};

mutex Writer::WriterMutex;

unsigned int Writer::RecordLeft = 0;