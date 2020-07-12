#include "dns.h"
#include "HashTable.h"
#include <stdio.h>
#include <stdlib.h>
#include <string.h>


DNSHandle InitDNS()
{
	HashTable *table = malloc(sizeof(HashTable));

	if (table == NULL)
		return INVALID_DNS_HANDLE;

	ht_Initialize(table);

	return (DNSHandle)table;
}



void LoadHostsFile(DNSHandle hDNS, const char *hostsFilePath)
{
	FILE *file = fopen(hostsFilePath, "r");

	char line[255];
	unsigned int size = 255;

	while (fgets(line, size, file) > 0)
	{
		unsigned int
			whiteSpaceInd = 0,
			hostNameStartInd = 0,
			hostNameLength = 0,
			lineLength = strlen(line);

		for (unsigned int i = 0; i < lineLength; ++i)
		{
			if (line[i] == ' ')
			{
				whiteSpaceInd = i;
				break;
			}
		}

		hostNameStartInd = whiteSpaceInd + 4;

		for (unsigned int i = hostNameStartInd; i < lineLength; ++i)
		{
			if (line[i] == '\n')
			{
				hostNameLength = i - hostNameStartInd + 1;
				break;
			}
		}

		char
			*addessString = malloc(sizeof(char) * (whiteSpaceInd + 1)),
			*hostName = malloc(sizeof(char) * hostNameLength);


		strncpy(addessString, line, whiteSpaceInd);
		strncpy(hostName, line + hostNameStartInd, hostNameLength - 1);

		addessString[whiteSpaceInd] = '\0';
		hostName[hostNameLength - 1] = '\0';

		IPADDRESS addess = GetIP(addessString);

		ht_Add(hDNS, hostName, addess);

		free(addessString);
	}

	fclose(file);
}



void ShutdownDNS(DNSHandle hDNS)
{
	ht_Clear(hDNS);
}



IPADDRESS DnsLookUp(DNSHandle hDNS, const char *hostName)
{
	Item *address = ht_Find(hDNS, hostName);

	if (address == NULL)
		return INVALID_IP_ADDRESS;
	else
		return address->Value;
}



unsigned int GetIP(char *addess_str)
{
	unsigned int num = 0, val;
	char *tok, *ptr;

	tok = strtok(addess_str, ".");

	while (tok != NULL)
	{
		val = strtoul(tok, &ptr, 0);
		num = (num << 8) + val;
		tok = strtok(NULL, ".");
	}

	return num;
}