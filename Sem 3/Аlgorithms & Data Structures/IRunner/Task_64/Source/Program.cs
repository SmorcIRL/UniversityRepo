using System.IO;
using System.Linq;

namespace Task_64
{
    class Program
    {
        static void Main(string[] args)
        {
            int n, m;
            bool[,] Matrix;

            using (StreamReader input = new StreamReader("Files/input.txt"))
            {
                var f_str_arr = input.ReadLine().Split(' ').Select(x => int.Parse(x)).ToArray();

                n = f_str_arr[0];
                m = f_str_arr[1];

                Matrix = new bool[n, n];

                for (int i = 0; i < m; ++i)
                {
                    f_str_arr = input.ReadLine().Split(' ').Select(x => int.Parse(x)).ToArray();

                    int v_1 = f_str_arr[0] - 1, v_2 = f_str_arr[1] - 1;

                    Matrix[v_1, v_2] = Matrix[v_2, v_1] = true;
                }
            }

            using (StreamWriter output = new StreamWriter("Files/output.txt"))
            {
                for (int i = 0; i < n; ++i)
                {
                    for (int j = 0; j < n; ++j)
                    {
                        output.Write($"{(Matrix[i, j] ? 1 : 0)} ");
                    }

                    output.WriteLine();
                }
            }
        }
    }
}
