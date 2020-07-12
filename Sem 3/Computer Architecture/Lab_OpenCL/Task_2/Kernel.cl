__kernel void MultiplyMatrices(__global int* m_1, __global int* m_2, __global int* m_3, int n, int m, int l)
{
	int i = get_global_id(0);
	int j = get_global_id(1);

	int sum = 0;
	int m_1_base = i * m;
	int m_2_base = j * m;

	for (int k = 0; k < m; ++k)
		sum += m_1[m_1_base + k] * m_2[m_2_base + k];

	m_3[i * l + j] = sum;
}


__kernel void MultiplyByVectors(__global int* m_1, __global int* m_2, __global int* m_3, int n, int m, int l)
{
	int i = get_global_id(0);
	int j = get_global_id(1);

	int8 sum = (int8)(0, 0, 0, 0, 0, 0, 0, 0);
	int m_1_base = i * m;
	int m_2_base = j * m;
	int bound = m / 8;

	for (int k = 0; k < bound; ++k)
	{
		int8 vector_1 = (int8)
			(
				m_1[m_1_base], m_1[m_1_base + 1],
				m_1[m_1_base + 2], m_1[m_1_base + 3],
				m_1[m_1_base + 4], m_1[m_1_base + 5],
				m_1[m_1_base + 6], m_1[m_1_base + 7]
				);

		int8 vector_2 = (int8)
			(
				m_2[m_2_base], m_2[m_2_base + 1],
				m_2[m_2_base + 2], m_2[m_2_base + 3],
				m_2[m_2_base + 4], m_2[m_2_base + 5],
				m_2[m_2_base + 6], m_2[m_2_base + 7]
				);


		sum += vector_1 * vector_2;

		m_1_base += 8;
		m_2_base += 8;
	}

	if (bound * 8 < m)
	{
		int8 vector_1 = (int8)
			(
				m_1[m_1_base], bound * 8 + 1 < m ? m_1[m_1_base + 1] : 0,
				bound * 8 + 2 < m ? m_1[m_1_base + 2] : 0, bound * 8 + 3 < m ? m_1[m_1_base + 3] : 0,
				bound * 8 + 4 < m ? m_1[m_1_base + 4] : 0, bound * 8 + 5 < m ? m_1[m_1_base + 5] : 0,
				bound * 8 + 6 < m ? m_1[m_1_base + 6] : 0, bound * 8 + 7 < m ? m_1[m_1_base + 7] : 0
				);

		int8 vector_2 = (int8)
			(
				m_2[m_2_base], bound * 8 + 1 < m ? m_2[m_2_base + 1] : 0,
				bound * 8 + 2 < m ? m_2[m_2_base + 2] : 0, bound * 8 + 3 < m ? m_2[m_2_base + 3] : 0,
				bound * 8 + 4 < m ? m_2[m_2_base + 4] : 0, bound * 8 + 5 < m ? m_2[m_2_base + 5] : 0,
				bound * 8 + 6 < m ? m_2[m_2_base + 6] : 0, bound * 8 + 7 < m ? m_2[m_2_base + 7] : 0
				);

		sum += vector_1 * vector_2;
	}

	m_3[i * l + j] = sum.s0 + sum.s1 + sum.s2 + sum.s3 + sum.s4 + sum.s5 + sum.s6 + sum.s7;
}
