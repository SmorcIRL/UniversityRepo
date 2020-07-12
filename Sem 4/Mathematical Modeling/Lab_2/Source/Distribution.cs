using System;
using System.Linq;

namespace Lab_2
{
    abstract class DistributionGenerator
    {
        public const int N = 1000;

        protected readonly Random Random = new Random();

        public abstract double ExpectedValue { get; }
        public abstract double Dispersion { get; }
        public abstract double[] GenerateSequence(int n = N);

        public static void GetRealExpectedValueAndDispersion(double[] arr, out double expval, out double disp)
        {
            int n = arr.Length;
            double av = arr.Average();

            expval = av;
            disp = arr.Select(x => Math.Pow(x - av, 2)).Sum() / (n - 1);
        }
        public static void GetRealAsymmetryAndExcess(double[] arr, out double asym, out double exc)
        {
            int n = arr.Length;

            GetRealExpectedValueAndDispersion(arr, out double expval, out double disp);

            asym = (arr.Select(x => Math.Pow(x - expval, 3)).Sum() / (n - 1)) / Math.Pow(Math.Sqrt(disp), 3);
            exc = (arr.Select(x => Math.Pow(x - expval, 4)).Sum() / (n - 1)) / Math.Pow(Math.Sqrt(disp), 4) - 3;
        }
        public static double[] CreateAndFill(Func<double> func, int n = N)
        {
            double[] arr = new double[n];

            for (int i = 0; i < arr.Length; i++)
            {
                arr[i] = func();
            }

            return arr;
        }
    }
}