using System;
using System.Globalization;
using System.IO;

namespace Task_26
{
    class Program
    {
        static void Main(string[] args)
        {
            float[] source, f_not_con, f_con;
            long n;

            var culture = (CultureInfo)CultureInfo.CurrentCulture.Clone();
            culture.NumberFormat.NumberDecimalSeparator = ".";

            using (StreamReader input = new StreamReader("Files/input.txt"))
            {
                n = long.Parse(input.ReadLine());

                source = new float[n + 1];

                for (long i = 1; i <= n; ++i)
                {
                    source[i] = float.Parse(input.ReadLine(), culture);
                }
            }

            if (n == 2 || n == 3)
            {
                using (StreamWriter output = new StreamWriter("Files/output.txt"))
                {
                    output.WriteLine((source[n] - source[1]).ToString("0.00", culture));
                }

                return;
            }

            f_not_con = new float[n + 1];
            f_con = new float[n + 1];

            f_not_con[3] = source[2] - source[1];
            f_con[3] = source[3] - source[1];

            for (long i = 4; i <= n; ++i)
            {
                f_not_con[i] = f_con[i - 1];
                f_con[i] = Math.Min(f_not_con[i - 1], f_con[i - 1]) + (source[i] - source[i - 1]);
            }

            using (StreamWriter output = new StreamWriter("Files/output.txt"))
            {
                output.WriteLine(f_con[n].ToString("0.00", culture));
            }
        }
    }
}