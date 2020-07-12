using System;
using System.Linq;

namespace Lab_3
{
    class Program
    {
        static void Main(string[] args)
        {
            var NormalDG = new NormalDG();
            var ExponentialDG = new ExponentialDG();
            var LogisticDG = new LogisticDG();
            var CauchyDG = new CauchyDG();
            var StudentsDG = new StudentsDG();

            string Format(double val)
            {
                return string.Format("{0:0.###}", val);
            }
            double[] PrintDGMainInfo(DistributionGenerator dg, string dg_name)
            {
                var arr = dg.GenerateSequence();

                if (dg.GetType() != typeof(CauchyDG))
                {
                    DistributionGenerator.GetRealExpectedValueAndDispersion(arr, out double expval, out double disp);

                    Console.WriteLine($"[{dg_name}]         \n" +
                        $"Expected value: {Format(dg.ExpectedValue)}     Real: {Format(expval)}\n" +
                        $"Dispersion:     {Format(dg.Dispersion)}     Real: {Format(disp)}\n\n");
                }
                else
                {
                    CauchyDG cauchyDG = (CauchyDG)dg;

                    Console.WriteLine($"[{dg_name}]         \n" +
                        $"Selective median: {Format(CauchyDG.SMV)}   Real: {Format(cauchyDG.CalculateSMV(arr))}\n\n");

                }

                return arr;
            }

            Console.WriteLine("[==================[Main task]==================]");
            PrintDGMainInfo(NormalDG, "NormalDG");
            PrintDGMainInfo(ExponentialDG, "ExponentialDG");
            PrintDGMainInfo(LogisticDG, "LogisticDG");
            PrintDGMainInfo(CauchyDG, "CauchyDG");
            PrintDGMainInfo(StudentsDG, "StudentsDG");

            var MixedExponentialLogisticDG = new MixedExponentialLogisticDG();
            var BoxMullerDG = new BoxMullerDG();
            var TruncatedExponentialDG = new TruncatedExponentialDG(1.9, 2.1);

            BoxMullerDG.GenerateSequence(out double corr);

            Console.WriteLine("[==================[Additional tasks]==================]");

            PrintDGMainInfo(MixedExponentialLogisticDG, "MixedExponentialLogisticDG");
            PrintDGMainInfo(TruncatedExponentialDG, "TruncatedExponentialDistribution(a = 1.9, b = 2.1)");

            Console.WriteLine($"[BoxMullerDistribution]    \nEven/odd correlation: {Format(corr)}");

            Console.Read();
        }

        // Вариант 11
        class NormalDG : DistributionGenerator
        {
            public const double M = 0;
            public const double S_Sqrd = 1;
            public const int BrvCount = 48;

            public override double ExpectedValue => M;
            public override double Dispersion => S_Sqrd;

            public override double[] GenerateSequence(int n = N)
            {
                return CreateAndFill(() => Math.Sqrt(12.0 / BrvCount) * (new double[BrvCount].Select(_ => Random.NextDouble()).Sum() - (BrvCount / 2.0)), n);
            }
        }

        // Вариант 5
        class ExponentialDG : DistributionGenerator
        {
            public const double A = 0.5;

            public override double ExpectedValue => 1 / A;
            public override double Dispersion => 1 / (A * A);

            public override double[] GenerateSequence(int n = N)
            {
                return CreateAndFill(() => -(1 / A) * Math.Log(Random.NextDouble()), n);
            }
        }
        class LogisticDG : DistributionGenerator
        {
            public const double A = 0;
            public const double B = 1.5;

            public override double ExpectedValue => A;
            public override double Dispersion => (Math.PI * Math.PI / 3) * B * B;

            public override double[] GenerateSequence(int n = N)
            {
                return CreateAndFill(() => { var x = Random.NextDouble(); return A + B * Math.Log(x / (1 - x)); }, n);
            }
        }

        // Вариант 6
        class CauchyDG : DistributionGenerator
        {
            public const double A = -1;
            public const double B = 3;
            public const double SMV = A;

            public override double ExpectedValue => double.NaN;
            public override double Dispersion => double.NaN;

            public double CalculateSMV(double[] arr) => arr.OrderBy(x => x).ToArray()[arr.Length / 2];

            public override double[] GenerateSequence(int n = N)
            {
                return CreateAndFill(() => { var x = Random.NextDouble(); return A + B * Math.Log(x / (1 - x)); }, n);
            }
        }
        class StudentsDG : DistributionGenerator
        {
            public const int M = 6;

            public override double ExpectedValue => 0;
            public override double Dispersion => M / (M - 2);
            public double CalculateSMV(double[] arr) => arr.OrderBy(x => x).ToArray()[arr.Length / 2];

            public override double[] GenerateSequence(int n = N)
            {
                double[] norm = new NormalDG().GenerateSequence();
                double[] res = new double[n]
                    .Select(_ => norm[Random.Next(0, n)] * Math.Sqrt(Enumerable.Repeat(0, M)
                    .Select(y => Math.Pow(norm[Random.Next(0, n)], 2)).Sum() / M))
                    .ToArray();

                return res;
            }
        }

        // Допы
        class MixedExponentialLogisticDG : DistributionGenerator
        {
            public const double PI = 0.3;
            public override double ExpectedValue => PI * ExpExpectedValue + (1 - PI) * LogExpectedValue;
            public override double Dispersion => PI * (Math.Pow(ExpExpectedValue, 2) + ExpDispersion) + (1 - PI) * (Math.Pow(LogExpectedValue, 2) + LogDispersion) - Math.Pow(ExpectedValue, 2);

            public const double ExpA = 0.5;
            public const double ExpExpectedValue = 1 / ExpA;
            public const double ExpDispersion = 1 / (ExpA * ExpA);

            public const double LogA = 0;
            public const double LogB = 1.5;
            public const double LogExpectedValue = LogA;
            public const double LogDispersion = (Math.PI * Math.PI / 3) * LogB * LogB;

            public override double[] GenerateSequence(int n = N)
            {
                return CreateAndFill(() => { double x = Random.NextDouble(); return Random.NextDouble() < PI ? -(1 / ExpA) * Math.Log(x) : LogA + LogB * Math.Log(x / (1 - x)); }, n);
            }
        }
        class BoxMullerDG
        {
            public double[] GenerateSequence(out double EvenOddCorr, int n = DistributionGenerator.N)
            {
                Random random = new Random();

                double[] res = new double[n];

                for (int i = 0; i < n / 2; i++)
                {
                    double root = Math.Sqrt(-2 * Math.Log(random.NextDouble()));
                    double angle = 2 * Math.PI * random.NextDouble();

                    res[2 * i] = Math.Cos(angle) * root;
                    res[2 * i + 1] = Math.Sin(angle) * root;
                }

                var even = res.Where((_, i) => i % 2 == 0).ToArray();
                var odd = res.Where((_, i) => i % 2 == 1).ToArray();
                double m_even = even.Sum() / (n / 2);
                double m_odd = odd.Sum() / (n / 2);
                double cov = 0;
                for (int i = 0; i < n / 2; i++)
                {
                    cov += (even[i] - m_even) * (odd[i] - m_odd);
                }

                EvenOddCorr = cov / Math.Sqrt((even.Select(x => Math.Pow(x - m_even, 2)).Sum() * odd.Select(x => Math.Pow(x - m_even, 2)).Sum()));

                return res;
            }
        }
        class TruncatedExponentialDG : DistributionGenerator
        {
            readonly double A;
            readonly double B;

            public TruncatedExponentialDG(double a, double b)
            {
                A = a;
                B = b;
            }

            public override double ExpectedValue => double.NaN;
            public override double Dispersion => double.NaN;

            public override double[] GenerateSequence(int n = 10000)
            {
                Random random = new Random();

                // Выражаю границы БСВ из неравенства a <= -(1 / E.A) * ln(БСВ) <= b, где E.A = 0.5 - параметр распределения
                double lower = Math.Exp(-0.5 * B);
                double upper = Math.Exp(-0.5 * A);
                double delta = upper - lower;

                double[] res = new double[n].Select(x => -2 * Math.Log(lower + delta * random.NextDouble())).ToArray();

                return res;
            }
        }
        class LognormalDG : DistributionGenerator
        {
            public const double U = 1;
            public const double S = 9;

            public override double ExpectedValue => U * Math.Sqrt(Math.Exp(S));
            public override double Dispersion => U * U * Math.Exp(S) * (Math.Exp(S) - 1);

            public override double[] GenerateSequence(int n = N)
            {
                var norm = new NormalDG().GenerateSequence();

                double[] res = new double[n].Select((x, i) => U * Math.Exp(U * (Math.Log(U) + Math.Sqrt(S) * norm[i]))).ToArray();

                return res;
            }
        }
    }
}