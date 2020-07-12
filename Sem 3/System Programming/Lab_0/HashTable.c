#include "HashTable.h"
#include <stdlib.h>
#include <string.h>


unsigned int ht_GetHash(char *source)
{
	unsigned int preHash = 0;

	for (char* char_iterator = source; *char_iterator != '\0'; ++char_iterator)
	{
		preHash *= 17;
		preHash += (unsigned int)* char_iterator;
	}

	return preHash % CAPACITY;
}


void ht_Initialize(HashTable *table)
{
	Item **items = table->Items;

	for (int i = 0; i < CAPACITY; ++i)
		items[i] = NULL;
}

void ht_Clear(HashTable *table)
{
	Item **items = table->Items;
	Item *item = NULL;

	for (unsigned int i = 0; i < CAPACITY; ++i)
	{
		while (items[i] != NULL)
		{
			item = items[i];
			items[i] = item->NextItem;

			free(item->Key);
			free(item);
		}
	}

	free(items);
}


void ht_Add(HashTable *table, char *key, unsigned int value)
{
	Item **items = table->Items;
	Item *new_item = malloc(sizeof(Item));

	int hash = ht_GetHash(key);

	if (new_item != NULL)
	{
		new_item->Key = key;
		new_item->Value = value;
		new_item->NextItem = items[hash];

		items[hash] = new_item;
	}
}

void ht_Remove(HashTable *table, char *key)
{
	Item **items = table->Items;
	Item *root_item = NULL, *prev_item = NULL;

	int hash = ht_GetHash(key);

	for (root_item = items[hash]; root_item != NULL; root_item = root_item->NextItem)
	{
		if (strcmp(root_item->Key, key) == 0)
		{
			if (prev_item == NULL)
				items[hash] = root_item->NextItem;
			else
				prev_item->NextItem = root_item->NextItem;

			free(root_item);

			return;
		}

		prev_item = root_item;
	}
}


Item* ht_Find(HashTable *table, char *key)
{
	Item **items = table->Items;
	Item *item = items[ht_GetHash(key)];

	while (item != NULL)
	{
		if (strcmp(item->Key, key) == 0)
			break;

		item = item->NextItem;
	}

	return item;
}