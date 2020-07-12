using System;
using System.IO;
using System.Linq;

namespace Task_69
{
    class Program
    {
        static void Main(string[] args)
        {
            long n;
            long[] val;
            long[] f;

            using (StreamReader input = new StreamReader("Files/input.txt"))
            {
                n = long.Parse(input.ReadLine());

                val = new long[n + 1];
                f = new long[n + 1];

                val[0] = long.MinValue;
                var arr = input.ReadLine().Split(' ').Select(x => long.Parse(x)).ToArray();

                for (long i = 1; i <= n; ++i)
                {
                    val[i] = arr[i - 1];
                }

                if (n <= 2)
                {
                    using (StreamWriter output = new StreamWriter("Files/output.txt"))
                    {
                        output.WriteLine(n == 1 ? val[1] : -1);
                    }

                    return;
                }
            }

            f[0] = -1;
            f[1] = val[1];
            f[2] = -1;
            f[3] = val[1] + val[3];

            for (long i = 4; i <= n; ++i)
            {
                if (f[i - 2] != -1 || f[i - 3] != -1)
                {
                    f[i] = Math.Max(f[i - 2], f[i - 3]) + val[i];
                }
            }

            using (StreamWriter output = new StreamWriter("Files/output.txt"))
            {
                output.WriteLine(f[n]);
            }
        }
    }
}
