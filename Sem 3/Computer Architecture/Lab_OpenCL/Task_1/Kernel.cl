__kernel void Encode(__global const int* str, __global int* mat, int n, int m, int strLen)
{
	int index = get_global_id(0);

	if (index >= strLen)
		return;

	mat[index] = str[index];
}

__kernel void Decode(__global int* str, __global int* mat, int n, int m, int strLen)
{
	int index = get_global_id(0);

	str[index] = mat[index];
}