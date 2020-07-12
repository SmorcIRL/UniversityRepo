#include <thread>
#include <string>
#include <iostream>
#include "Writer.h"

using namespace std;

int main()
{
	string
		First_string = "",
		Second_string = "",
		Third_string = "",
		Fourth_string = "",
		Fifth_string = "";

	unsigned int
		Counter = 0,
		First_count = 1, First_delta = 1000,
		Second_count = 1, Second_delta = 1000,
		Third_count = 1, Third_delta = 1000,
		Fourth_count = 1, Fourth_delta = 1000,
		Fifth_count = 1, Fifth_delta = 1000;

	cout << "Max records: " << endl;
	cin >> Counter;
	Writer::RecordLeft = Counter;

	cout << endl << "Write 5 lines using the following format: <string uint uint>" << endl;
	cin >> First_string >> First_count >> First_delta;
	cin >> Second_string >> Second_count >> Second_delta;
	cin >> Third_string >> Third_count >> Third_delta;
	cin >> Fourth_string >> Fourth_count >> Fourth_delta;
	cin >> Fifth_string >> Fifth_count >> Fifth_delta;

	cout << endl;
	//First_string = "1"; First_count = 20; First_delta = 1000;
	//Second_string = "2"; Second_count = 15; Second_delta = 1500;
	//Third_string = "3"; Third_count = 15; Third_delta = 2000;
	//Fourth_string = "4"; Fourth_count = 10; Fourth_delta = 2500;
	//Fifth_string = "5"; Fifth_count = 5; Fifth_delta = 3000;

	Writer
		FirstWriter(First_string, First_count, First_delta),
		SecondWriter(Second_string, Second_count, Second_delta),
		ThirdWriter(Third_string, Third_count, Third_delta),
		FourthWriter(Fourth_string, Fourth_count, Fourth_delta),
		FifthWriter(Fifth_string, Fifth_count, Fifth_delta);

	thread
		FirstThread(&Writer::StartWriting, &FirstWriter),
		SecondThread(&Writer::StartWriting, &SecondWriter),
		ThirdThread(&Writer::StartWriting, &ThirdWriter),
		FourthThread(&Writer::StartWriting, &FourthWriter),
		FifthThread(&Writer::StartWriting, &FifthWriter);


	FirstThread.join();
	SecondThread.join();
	ThirdThread.join();
	FourthThread.join();
	FifthThread.join();

	system("pause");
}