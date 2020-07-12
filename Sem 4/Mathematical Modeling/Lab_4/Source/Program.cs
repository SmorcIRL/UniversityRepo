using System;
using System.Linq;
using System.Windows.Forms;

namespace Lab_4
{
    class Program
    {
        static void Main(string[] args)
        {
            int N = 10000;

            double[] arr_1 = Task_1_Array(N).Select(x => (Math.Abs(accurateResult_T1 - x) / accurateResult_T1) * 100.0).ToArray();
            double[] arr_2 = Task_2_Array(N).Select(x => (Math.Abs(accurateResult_T2 - x) / accurateResult_T2) * 100.0).ToArray();

            Application.Run(new ChartForm(arr_1, arr_2));

            Console.WriteLine("[Variant 11]");
            Console.WriteLine($"[Task 1] Expected value: {accurateResult_T1}     Real: {Task_1(N)}");
            Console.WriteLine($"[Task 2] Expected value: {accurateResult_T2}     Real: {Task_2(N)}");

            Console.Read();
        }

        #region Task_1

        const double accurateResult_T1 = 8.50656;

        const double x_lower_T1 = 3.0;
        const double x_upper_T1 = 3.5;

        static readonly Func<double, double> f_x_T1 = x => Math.Pow(x, x) * (1 + Math.Log(x)) * Math.Tan(x);

        static double Task_1(int N)
        {
            return DefiniteIntegral_MonteCarlo(f_x_T1, x_lower_T1, x_upper_T1, N);
        }
        static double[] Task_1_Array(int N)
        {
            random = new Random();
            return DefiniteIntegral_MonteCarlo_Array(f_x_T1, x_lower_T1, x_upper_T1, N);
        }

        #endregion

        #region Task_2

        const double accurateResult_T2 = 618.542;

        const double x_lower_T2 = -2 + double.Epsilon;
        const double x_upper_T2 = 2 - double.Epsilon;
        const double y_lower_T2 = 0;
        const double y_upper_T2 = 4;

        static readonly Func<double, double, double> f_x_y_T2 = (x, y) => Math.Exp(x * y) * Math.Sqrt(y + Math.Pow(Math.Sin(x), 2));
        static readonly Func<double, Tuple<double, double>> get_y_bounds_T2 = x => new Tuple<double, double>(Math.Pow(x, 2), 4);

        static double Task_2(int N)
        {
            return DefiniteIntegral_MonteCarlo(f_x_y_T2, get_y_bounds_T2, x_lower_T2, x_upper_T2, y_lower_T2, y_upper_T2, N);
        }
        static double[] Task_2_Array(int N)
        {
            random = new Random();
            return DefiniteIntegral_MonteCarlo_Array(f_x_y_T2, get_y_bounds_T2, x_lower_T2, x_upper_T2, y_lower_T2, y_upper_T2, N);
        }

        #endregion

        #region MonteCarlo

        static Random random = new Random(Guid.NewGuid().GetHashCode());

        static double DefiniteIntegral_MonteCarlo(Func<double, double> f_x, double x_lb, double x_ub, int N)
        {
            double delta = x_ub - x_lb;
            double sum = 0;

            for (int i = 0; i < N; i++)
            {
                sum += f_x(x_lb + random.NextDouble() * delta);
            }

            return delta * sum / N;
        }
        static double[] DefiniteIntegral_MonteCarlo_Array(Func<double, double> f_x, double x_lb, double x_ub, int N)
        {

            return new double[N].Select((_, i) => DefiniteIntegral_MonteCarlo(f_x, x_lb, x_ub, i + 1)).ToArray();
        }

        static double DefiniteIntegral_MonteCarlo(Func<double, double, double> f_x_y, Func<double, Tuple<double, double>> y_bounds, double x_lb, double x_ub, double y_lb, double y_ub, int N)
        {
            double delta_x = x_ub - x_lb;
            double delta_y = y_ub - y_lb;
            double sum = 0;

            for (int i = 0; i < N; i++)
            {
                double val_1 = x_lb + random.NextDouble() * delta_x;
                double val_2 = y_lb + random.NextDouble() * delta_y;

                var bounds = y_bounds(val_1);

                if (val_2 >= bounds.Item1 && val_2 <= bounds.Item2)
                {
                    sum += f_x_y(val_1, val_2);
                }
            }

            return (delta_x * delta_y) / N * sum;
        }
        static double[] DefiniteIntegral_MonteCarlo_Array(Func<double, double, double> f_x_y, Func<double, Tuple<double, double>> y_bounds, double x_lb, double x_ub, double y_lb, double y_ub, int N)
        {
            return new double[N].Select((_, i) => DefiniteIntegral_MonteCarlo(f_x_y, y_bounds, x_lb, x_ub, y_lb, y_ub, i + 1)).ToArray();
        }

        #endregion
    }
}
