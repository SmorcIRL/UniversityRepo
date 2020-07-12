using System;
using System.Linq;
using System.Windows.Forms;

namespace Lab_1
{
    class Program
    {
        const long M = 2147483648;

        const long a_1 = 261463909;
        const long b_1 = c_1 > M ? c_1 : M - c_1;
        const long c_1 = 474379977;

        const long a_2 = 234289925;
        const long b_2 = c_2 > M ? c_2 : M - c_2;
        const long c_2 = 3097871;

        const int K = 192;
        const int N = 1000;

        static void Main(string[] args)
        {
            var res_1 = MultiplicativeCongruentialMethod();
            var res_2 = MacLarenMarsagliaMethod();

            Console.WriteLine("===========[CongruentialMethod]===========");
            Console.WriteLine($"100:            {res_1[99]}");
            Console.WriteLine($"900:            {res_1[899]}");
            Console.WriteLine($"1000:           {res_1[999]}");
            Console.WriteLine($"Test_1:         {Test_CoincidenceOfMoments(res_1)}");
            Console.WriteLine($"Test_2:         {Test_Сovariance(res_1)}");
            Console.Write($"Correl coeffs:  ");
            foreach (var coef in CorrelationCoefficients(res_1, 0, 30))
            {
                Console.Write($"{coef} ");
            }
            Console.WriteLine($"\nPeriod:         {CalculatePeriod(565465)}");
            Console.WriteLine("==========================================\n");

            Console.WriteLine("=========[MacLarenMarsagliaMethod]========");
            Console.WriteLine($"100:            {res_2[99]}");
            Console.WriteLine($"900:            {res_2[899]}");
            Console.WriteLine($"1000:           {res_2[999]}");
            Console.WriteLine($"Test_1:         {Test_CoincidenceOfMoments(res_2)}");
            Console.WriteLine($"Test_2:         {Test_Сovariance(res_2)}");
            Console.Write($"Correl coeffs:  ");
            foreach (var coef in CorrelationCoefficients(res_2, 0, 30))
            {
                Console.Write($"{coef} ");
            }
            Console.WriteLine("\n==========================================\n");

            Application.Run(new СhartForm(res_1, res_2));

            Console.WriteLine("[End]");
            Console.Read();
        }


        private static double[] MultiplicativeCongruentialMethod(long a_0 = a_1, long b = b_1, long M = M, long n = N)
        {
            double[] res = new double[n];

            double a_t = a_0;

            for (int t = 0; t < n; t++)
            {
                a_t = (a_t * b) % M;
                res[t] = a_t / M;
            }

            return res;
        }
        private static double[] MacLarenMarsagliaMethod(long M = M, long n = N, int K = K)
        {
            double[] res = new double[n];
            double[] V = new double[K];
            double[] B = MultiplicativeCongruentialMethod(a_1, b_2);
            double[] C = MultiplicativeCongruentialMethod(a_2, b_2);

            Array.Copy(B, V, K);

            for (int t = 0; t < n; t++)
            {
                int s = (int)Math.Truncate(C[t] * K);
                res[t] = V[s];
                V[s] = B[(t + K) % n];
            }

            return res;
        }

        // Константы в тестах взяты из соответствующих таблиц
        private static bool Test_CoincidenceOfMoments(double[] arr, int n = N)
        {
            double av = arr.Sum() / n;
            double disp = 0;

            for (int i = 0; i < n; i++)
            {
                disp += Math.Pow(arr[i] - av, 2);
            }

            disp /= n - 1;

            double
                xi_1 = Math.Abs(av - 0.5),
                xi_2 = Math.Abs(disp - 1 / 12),
                c_1 = Math.Sqrt(12 * n),
                c_2 = ((n - 1) / n) * (1 / Math.Sqrt((0.0056 / Math.Pow(n, 1)) + (0.0028 / Math.Pow(n, 2)) - 0.0083 / Math.Pow(n, 3)));

            return c_1 * xi_1 < 0.825 || c_2 * xi_2 < 0.825;
        }
        private static bool Test_Сovariance(double[] arr, int n = N)
        {
            double av = arr.Sum() / n;
            double r1 = 0.083333;
            double r2 = 0;

            for (int i = 0; i < n; i++)
            {
                r2 += (arr[0] * arr[0]) - (1.001001 * av);
            }

            r2 /= n - 1;

            return Math.Abs(r2 - r1) < 0.00308;
        }

        public static double[] CorrelationCoefficients(double[] arr, int t, int tau, int n = N)
        {
            double av = arr.Sum() / n;

            double[] res = new double[tau];

            for (int i = 0; i < tau; i++)
            {
                res[i] = (arr[t] - av) * (arr[t + i + 1] - av) / Math.Sqrt(Math.Pow(arr[t] - av, 2) * Math.Pow(arr[t + i + 1] - av, 2));
            }

            return res;
        }

        public static long CalculatePeriod(long i_to_start_with)
        {
            double average_period = 0;

            double[] res_3 = MultiplicativeCongruentialMethod(a_1, b_1, M, 100000000);

            long prev_i = i_to_start_with, count = 0;
            double eps = 0.000001, val = res_3[i_to_start_with];

            for (int i = 0; i < res_3.Length; i++)
            {
                if (Math.Abs(res_3[i] - val) < eps)
                {
                    average_period += i - prev_i;
                    prev_i = i;
                    count++;
                }
            }

            return (long)(average_period / (count - 1));
        }
    }
}