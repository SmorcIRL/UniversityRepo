#define CAPACITY 12841


typedef struct Item
{
	char *Key;
	unsigned int Value;

	struct Item *NextItem;

} Item;

typedef struct HashTable
{
	struct Item *Items[CAPACITY];

} HashTable;


unsigned int ht_GetHash(char *s);


void ht_Initialize(HashTable *table);

void ht_Clear(HashTable *table);


void ht_Add(HashTable *table, char *key, unsigned int value);

void ht_Remove(HashTable *table, char *key);


Item* ht_Find(HashTable *table, char *key);