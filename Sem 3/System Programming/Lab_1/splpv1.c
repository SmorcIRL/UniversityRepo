/*
 * SPLPv1.c
 * The file is part of practical task for System programming course.
 * This file contains validation of SPLPv1 protocol.
 */


 //#error Specify your name and group
 /*

 */



 /*
 ---------------------------------------------------------------------------------------------------------------------------
 # |      STATE      |         DESCRIPTION       |           ALLOWED MESSAGES            | NEW STATE | EXAMPLE
 --+-----------------+---------------------------+---------------------------------------+-----------+----------------------
 1 | INIT            | initial state             | A->B     CONNECT                      |     2     |
 --+-----------------+---------------------------+---------------------------------------+-----------+----------------------
 2 | CONNECTING      | client is waiting for con-| A<-B     CONNECT_OK                   |     3     |
   |                 | nection approval from srv |                                       |           |
 --+-----------------+---------------------------+---------------------------------------+-----------+----------------------
 3 | CONNECTED       | Connection is established | A->B     GET_VER                      |     4     |
   |                 |                           |        -------------------------------+-----------+----------------------
   |                 |                           |          One of the following:        |     5     |
   |                 |                           |          - GET_DATA                   |           |
   |                 |                           |          - GET_FILE                   |           |
   |                 |                           |          - GET_COMMAND                |           |
   |                 |                           |        -------------------------------+-----------+----------------------
   |                 |                           |          GET_B64                      |     6     |
   |                 |                           |        ------------------------------------------------------------------
   |                 |                           |          DISCONNECT                   |     7     |
 --+-----------------+---------------------------+---------------------------------------+-----------+----------------------
 4 | WAITING_VER     | Client is waiting for     | A<-B     VERSION ver                  |     3     | VERSION 2
   |                 | server to provide version |          Where ver is an integer (>0) |           |
   |                 | information               |          value. Only a single space   |           |
   |                 |                           |          is allowed in the message    |           |
 --+-----------------+---------------------------+---------------------------------------+-----------+----------------------
 5 | WAITING_DATA    | Client is waiting for a   | A<-B     CMD data CMD                 |     3     | GET_DATA a GET_DATA
   |                 | response from server      |                                       |           |
   |                 |                           |          CMD - command sent by the    |           |
   |                 |                           |           client in previous message  |           |
   |                 |                           |          data - string which contains |           |
   |                 |                           |           the following allowed cha-  |           |
   |                 |                           |           racters: small latin letter,|           |
   |                 |                           |           digits and '.'              |           |
 --+-----------------+---------------------------+---------------------------------------+-----------+----------------------
 6 | WAITING_B64_DATA| Client is waiting for a   | A<-B     B64: data                    |     3     | B64: SGVsbG8=
   |                 | response from server.     |          where data is a base64 string|           |
   |                 |                           |          only 1 space is allowed      |           |
 --+-----------------+---------------------------+---------------------------------------+-----------+----------------------
 7 | DISCONNECTING   | Client is waiting for     | A<-B     DISCONNECT_OK                |     1     |
   |                 | server to close the       |                                       |           |
   |                 | connection                |                                       |           |
 ---------------------------------------------------------------------------------------------------------------------------

 IN CASE OF INVALID MESSAGE THE STATE SHOULD BE RESET TO 1 (INIT)

 */


#include "splpv1.h"
#include "string.h"

char chars_ver[131] =
{ 0,
0,0,0,0,0,0,0,0,0,0,
0,0,0,0,0,0,0,0,0,0,
0,0,0,0,0,0,0,0,0,0,
0,0,0,0,0,0,0,0,0,0,
0,0,0,0,0,0,0,0,1,1,
1,1,1,1,1,1,1,0,0,0,
0,0,0,0,0,0,0,0,0,0,
0,0,0,0,0,0,0,0,0,0,
0,0,0,0,0,0,0,0,0,0,
0,0,0,0,0,0,0,0,0,0,
0,0,0,0,0,0,0,0,0,0,
0,0,0,0,0,0,0,0,0,0,
0,0,0,0,0,0,0,0,0,0,
};
char chars_reply[131] =
{ 0,
0,0,0,0,0,0,0,0,0,0,
0,0,0,0,0,0,0,0,0,0,
0,0,0,0,0,0,0,0,0,0,
0,0,0,0,0,0,0,0,0,0,
0,0,0,0,0,1,0,1,1,1,
1,1,1,1,1,1,1,0,0,0,
0,0,0,0,0,0,0,0,0,0,
0,0,0,0,0,0,0,0,0,0,
0,0,0,0,0,0,0,0,0,0,
0,0,0,0,0,0,1,1,1,1,
1,1,1,1,1,1,1,1,1,1,
1,1,1,1,1,1,1,1,1,1,
1,1,0,0,0,0,0,0,0,0,
};
char chars_base64[131] =
{ 0,
0,0,0,0,0,0,0,0,0,0,
0,0,0,0,0,0,0,0,0,0,
0,0,0,0,0,0,0,0,0,0,
0,0,0,0,0,0,0,0,0,0,
0,0,1,0,0,0,1,1,1,1,
1,1,1,1,1,1,1,0,0,0,
0,0,0,0,1,1,1,1,1,1,
1,1,1,1,1,1,1,1,1,1,
1,1,1,1,1,1,1,1,1,1,
0,0,0,0,0,0,1,1,1,1,
1,1,1,1,1,1,1,1,1,1,
1,1,1,1,1,1,1,1,1,1,
1,1,0,0,0,0,0,0,0,0,
};

char* commands[11] = { "CONNECT", "CONNECT_OK", "GET_VER", "GET_DATA", "GET_COMMAND", "GET_FILE", "GET_B64", "DISCONNECT", "VERSION", "B64:", "DISCONNECT_OK" };


int get_com_id(char* com_first, char* com_last)
{
	for (int i = 0; i < 11; ++i)
	{
		char* iter_1 = com_first, * iter_2 = commands[i];

		while (com_first != com_last && *iter_2)
		{
			if (*(iter_1++) != *(iter_2++))	break;
		}

		if (iter_1 == com_last && *iter_2 == '\0')
		{
			return i;
		}
	}

	return -1;
}

typedef enum State
{
	INIT = 0,
	CONNECTING = 1,
	CONNECTED = 2,
	WAITING_VER = 3,
	WAITING_DATA = 4,
	WAITING_B64_DATA = 5,
	DISCONNECTING = 6

} State;

State state = INIT;
int com_prev = -1, com_op = -1, com_end = -1, state_new, dir = 0;
char* com_op_start, * data_start, * com_end_start, * char_ptr;

enum test_status validate_message(struct Message* msg)
{
	com_op = com_end = -1;
	dir = msg->direction;
	com_op_start = msg->text_message;
	data_start = com_end_start = NULL;
	char_ptr = com_op_start;

	while (*char_ptr != ' ' && *char_ptr)
	{
		++char_ptr;
	}

	com_op = get_com_id(com_op_start, char_ptr);

	if (*char_ptr)
	{
		data_start = ++char_ptr;

		if (com_op > 2 && com_op < 6)
		{
			while (*char_ptr != ' ')
			{
				if (chars_reply[*(char_ptr++)] == 0)
				{
					goto invalid;
				}
			}
		}
		else if (com_op == 8)
		{
			while (*char_ptr)
			{
				if (chars_ver[*(char_ptr++)] == 0)
				{
					goto invalid;
				}
			}
		}
		else if (com_op == 9)
		{
			while (*char_ptr)
			{
				if (chars_base64[*char_ptr] == 0)
				{
					if (*char_ptr != '=')
					{
						goto invalid;
					}

					if (*(char_ptr + 1) == '\0')
					{
						++char_ptr;
						break;
					}
					else if (*(char_ptr + 2) == '\0')
					{
						++char_ptr;
						if (chars_base64[*char_ptr] != 1 && *char_ptr != '=')
						{
							goto invalid;
						}
						++char_ptr;

						break;
					}
					else
					{
						goto invalid;
					}
				}

				++char_ptr;
			}

			if ((data_start - char_ptr) % 4 != 0)
			{
				goto invalid;
			}
		}
		else
		{
			goto invalid;
		}

		if (*char_ptr)
		{
			if (com_op < 3 || com_op > 5)
			{
				goto invalid;
			}
			com_end_start = char_ptr + 1;

			while (*(++char_ptr));

			com_end = get_com_id(com_end_start, char_ptr);

			if (com_op != com_end)
			{
				goto invalid;
			}
		}
	}

	switch (state)
	{
	case (INIT):
	{
		if (dir != 0 || com_op != 0)
		{
			goto invalid;
		}

		state_new = CONNECTING;

		break;
	}
	case (CONNECTING):
	{
		if (dir != 1 || com_op != 1)
		{
			goto invalid;
		}

		state_new = CONNECTED;

		break;
	}
	case (CONNECTED):
	{
		if (dir != 0)
		{
			goto invalid;
		}

		if (com_op == 2) state_new = WAITING_VER;
		else if (com_op > 2 && com_op < 6) state_new = WAITING_DATA;
		else if (com_op == 6) state_new = WAITING_B64_DATA;
		else if (com_op == 7) state_new = DISCONNECTING;
		else
		{
			goto invalid;
		}

		break;
	}
	case (WAITING_VER):
	{
		if (dir != 1 || com_op != 8)
		{
			goto invalid;
		}

		state_new = CONNECTED;

		break;
	}
	case (WAITING_DATA):
	{
		if (dir != 1 || com_op != com_prev || com_op < 3 || com_op > 5)
		{
			goto invalid;
		}

		state_new = CONNECTED;

		break;
	}
	case (WAITING_B64_DATA):
	{
		if (dir != 1 || com_op != 9)
		{
			goto invalid;
		}

		state_new = CONNECTED;

		break;
	}
	case (DISCONNECTING):
	{
		if (dir != 1 || com_op != 10)
		{
			goto invalid;
		}

		state_new = INIT;

		break;
	}
	}

	state = state_new;
	com_prev = com_op;
	return MESSAGE_VALID;

invalid:
	state = INIT;
	com_prev = -1;
	return MESSAGE_INVALID;
}