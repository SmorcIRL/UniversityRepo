using System;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lab_2
{
    class Program
    {
        static void Main(string[] args)
        {
            var BernoulliDG = new BernoulliDG();
            var PoissonDG = new PoissonDG();
            var GeometricDG = new GeometricDG();
            var NegativeBinomialDG = new NegativeBinomialDG();

            string Format(double val)
            {
                return string.Format("{0:0.###}", val);
            }
            double[] PrintDGMainInfo(DistributionGenerator dg, string dg_name)
            {
                var arr = dg.GenerateSequence();
                DistributionGenerator.GetRealExpectedValueAndDispersion(arr, out double expval, out double disp);

                Console.WriteLine($"[{dg_name}]         \n" +
                    $"Expected value: {Format(dg.ExpectedValue)}     Real: {Format(expval)}\n" +
                    $"Dispersion:     {Format(dg.Dispersion)}     Real: {Format(disp)}\n\n");

                return arr;
            }

            Console.WriteLine("=================[Main task]=================\n");
            var arr_bern = PrintDGMainInfo(BernoulliDG, "BernoulliDistribution");
            var arr_poisson = PrintDGMainInfo(PoissonDG, "PoissonDistribution");
            PrintDGMainInfo(GeometricDG, "GeometricDistribution");
            PrintDGMainInfo(NegativeBinomialDG, "NegativeBinomialDistribution");

            Console.WriteLine("=========[Additional tasks(1,2,3,4)]=========\n");

            DistributionGenerator.GetRealAsymmetryAndExcess(arr_bern, out double asym_bern, out double exc_bern);
            DistributionGenerator.GetRealAsymmetryAndExcess(arr_poisson, out double asym_poisson, out double exc_poisson);

            Console.WriteLine($"[BernoulliDistribution]         \n" +
                $"Asymmetry:      {Format(BernoulliDG.Asymmetry)}    Real: {Format(asym_bern)}\n" +
                $"Excess:         {Format(BernoulliDG.Excess)}    Real: {Format(exc_bern)}\n" +
                $"ChiSquare:      {BernoulliDG.ChiSquareTest(arr_bern)}\n\n");

            Console.WriteLine($"[PoissonDistribution]           \n" +
                $"Asymmetry:      {Format(PoissonDG.Asymmetry)}    Real: {Format(asym_poisson)}\n" +
                $"Excess:         {Format(PoissonDG.Excess)}    Real: {Format(exc_poisson)}\n" +
                $"ChiSquare:      {PoissonDG.ChiSquareTest(arr_poisson)}\n\n");

            Application.Run(new СhartForm(DistributionGenerator.N, arr_bern, BernoulliDG.P, arr_poisson, PoissonDG.Lambda));

            Console.Read();
        }

        // 11 вариант
        class BernoulliDG : DistributionGenerator
        {
            public const double P = 0.75d;
            public const double CritXi = 3.8;

            public override double ExpectedValue => P;
            public override double Dispersion => P * (1 - P);
            public double Asymmetry => (P - 3 * Math.Pow(P, 2) + 2 * Math.Pow(P, 3)) / Math.Pow(Math.Sqrt(Dispersion), 3);
            public double Excess => ((P - 4 * Math.Pow(P, 2) + 6 * Math.Pow(P, 3) - 3 * Math.Pow(P, 4)) / Math.Pow(Math.Sqrt(Dispersion), 4)) - 3;

            public override double[] GenerateSequence(int n = N)
            {
                return CreateAndFill(() => Random.NextDouble() > P ? 0.0d : 1.0d, n);
            }

            public static bool ChiSquareTest(double[] arr)
            {
                int n = arr.Length;

                GetRealExpectedValueAndDispersion(arr, out double expval, out double disp);

                double count_1 = (n * expval);
                double count_0 = (n * (1 - expval));

                double xi_B = ((arr.Count(x => x == 0) - count_0) / count_0) + ((arr.Count(x => x == 1) - count_1) / count_1);

                return xi_B < CritXi;
            }
        }
        class PoissonDG : DistributionGenerator
        {
            public const double Lambda = 3;
            public const double CritXi = 16.9;

            public override double ExpectedValue => Lambda;
            public override double Dispersion => Lambda;
            public double Asymmetry => Math.Pow(Lambda, -0.5);
            public double Excess => Math.Pow(Lambda, -1);

            public override double[] GenerateSequence(int n = N)
            {
                double[] res = new double[n];

                double
                    e = Math.Exp(-Lambda),
                    k = 0,
                    pi_k = 1;

                for (int i = 0; i < n; i++, k = 0, pi_k = 1)
                {
                    while ((pi_k *= Random.NextDouble()) > e) ++k;

                    res[i] = k;
                }

                return res;
            }

            public static bool ChiSquareTest(double[] arr)
            {
                int n = arr.Length;

                double
                    xi_Pois = 0,
                    e = Math.Exp(-Lambda),
                    fact = 1;

                for (int i = 0; i < 10; i++, fact *= i)
                {
                    double count_i = ((e * Math.Pow(Lambda, i)) / fact) * n;
                    xi_Pois += ((arr.Count(x => (int)Math.Round(x, MidpointRounding.AwayFromZero) == i) - count_i) / count_i);
                }

                return xi_Pois < CritXi;
            }
        }

        // 6 вариант
        class GeometricDG : DistributionGenerator
        {
            public const double P = 0.1;

            public override double ExpectedValue => 1 / P;
            public override double Dispersion => (1 - P) / (P * P);

            public override double[] GenerateSequence(int n = N)
            {
                return CreateAndFill(() => Math.Round(Math.Log(Random.NextDouble()) / Math.Log(1 - P), MidpointRounding.AwayFromZero), n);
            }
        }
        class NegativeBinomialDG : DistributionGenerator
        {
            public const double R = 4.0;
            public const double P = 0.2;

            public override double ExpectedValue => (R * (1 - P)) / P;
            public override double Dispersion => (R * (1 - P)) / (P * P);

            public override double[] GenerateSequence(int n = N)
            {
                double[] res = new double[n];
                double[] bern = new double[n];

                Parallel.For(0, n, i =>
                {
                    for (int j = 0; j < n; j++)
                    {
                        bern[i] = Random.NextDouble() > P ? 0.0d : 1.0d;
                    }

                    res[i] = bern.Select((x, ind_1) => bern.Take(ind_1).Sum()).ToList().FindIndex(x => x == R) - R;
                });

                return res;
            }
        }
    }
}