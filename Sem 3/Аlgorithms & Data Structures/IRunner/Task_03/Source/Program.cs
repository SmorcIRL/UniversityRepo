using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Task_03
{
    class Program
    {
        static void Main(string[] args)
        {
            List<Tuple<long, long>> sizes = new List<Tuple<long, long>>();
            long[,] f;
            long n;

            using (StreamReader input = new StreamReader("Files/input.txt"))
            {
                n = long.Parse(input.ReadLine());

                sizes.Add(new Tuple<long, long>(0, 0));

                while (!input.EndOfStream)
                {
                    var matrixSize = input.ReadLine().Split(' ').Select(x => long.Parse(x)).ToArray();

                    sizes.Add(new Tuple<long, long>(matrixSize[0], matrixSize[1]));
                }
            }

            f = new long[n + 1, n + 1];

            for (long diag = 2; diag <= n; ++diag)
            {
                for (long i = 1, j = diag; i <= n - diag + 1; ++i, ++j)
                {
                    f[i, j] = long.MaxValue;

                    for (long k = i; k < j; ++k)
                    {
                        f[i, j] = Math.Min(f[i, j], f[i, k] + f[k + 1, j] + sizes[(int)i].Item1 * sizes[(int)k].Item2 * sizes[(int)j].Item2);
                    }
                }
            }

            using (StreamWriter output = new StreamWriter("Files/output.txt"))
            {
                output.WriteLine(f[1, n]);
            }
        }
    }
}
