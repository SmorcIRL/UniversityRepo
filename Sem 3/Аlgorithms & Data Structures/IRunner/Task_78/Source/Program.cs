using System;
using System.IO;
using System.Linq;

namespace Task_78
{
    class Program
    {
        static void Main(string[] args)
        {
            long n, m;

            ulong[] f;

            using (StreamReader input = new StreamReader("Files/input.txt"))
            {
                var arr = input.ReadLine().Split(' ').Select(x => long.Parse(x)).ToArray();

                n = Math.Max(arr[0], arr[1]);
                m = Math.Min(arr[0], arr[1]);

                if (n == 1 || m == 1)
                {
                    using (StreamWriter output = new StreamWriter("Files/output.txt"))
                    {
                        output.WriteLine(1);
                    }

                    return;
                }
            }

            f = Enumerable.Range(1, (int)n).Select(x => (ulong)x).ToArray();

            for (long i = 2; i < m; ++i)
            {
                f[i] *= 2 % 1000000007;

                for (long j = i + 1; j < n; ++j)
                {
                    f[j] += f[j - 1] % 1000000007;
                }
            }

            using (StreamWriter output = new StreamWriter("Files/output.txt"))
            {
                output.WriteLine(f[n - 1] % 1000000007);
            }
        }
    }
}