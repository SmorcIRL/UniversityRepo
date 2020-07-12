#include <iostream>

using namespace std;

int main()
{
	int OriginalCount(char* Word);

	char* InputString = new char[301], * Word = nullptr, * BufferWord = nullptr;
	int BufferOrigninal = 0, i = 0;

	cout << "========================[Lab 4]=======================" << endl;
	cout << " Info: Enter a string" << endl << endl << " Input: ";
	cin.getline(InputString, 300);

	while (InputString[i] != '\0')
	{
		if (InputString[i] != ' ')
		{
			int r = i;

			while ((InputString[i] != ' ') && (InputString[i] != '\0'))
			{
				i++;
			}

			Word = new char[i - r + 1];

			for (int k = 0; k < (i - r); k++)
			{
				Word[k] = InputString[r + k];
			}

			Word[i - r] = '\0';

			int CurrentWordOrigninal = OriginalCount(Word);

			if (CurrentWordOrigninal >= BufferOrigninal)
			{
				BufferOrigninal = CurrentWordOrigninal;

				int q = 0;

				BufferWord = new char[i - r];

				while (Word[q] != '\0')
				{
					BufferWord[q] = Word[q];
					q++;
				}

				BufferWord[q] = '\0';

				delete[] Word;
			}

			else
			{
				i++;
				continue;
			}
		}

		else
		{
			i++;
		}
	}

	cout << endl << " Info: The word with the most unique characters (" << BufferOrigninal << ") is: " << BufferWord;

	cout << endl << endl << endl << "====================================[The End]===================================" << endl;

	system("pause");
}

int OriginalCount(char* Word)
{
	int Count = 0, k = 0;

	while (Word[k] != '\0')
	{
		k++;
	}

	char* WordCopy = new char[k + 1];

	k = 0;

	while (Word[k] != '\0')
	{
		WordCopy[k] = (Word[k]);
		k++;
	}

	WordCopy[k] = '\0';

	for (int i = 0; WordCopy[i] != '\0'; i++)
	{
		if (WordCopy[i] == ' ')
		{
			continue;
		}

		char Buffer = WordCopy[i];

		for (int g = i; WordCopy[g] != '\0'; g++)
		{
			if (WordCopy[g] == Buffer)
			{
				WordCopy[g] = ' ';
			}
		}

		Count++;
	}

	delete[] WordCopy;

	return Count;
}