using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using MathNet.Numerics.LinearAlgebra;

namespace Lab_3
{
    class Program
    {
        const string InputFilename = "Files/Input.txt";

        static MatrixBuilder<double> MatrixBuilder = Matrix<double>.Build;
        static VectorBuilder<double> VectorBuilder = Vector<double>.Build;
        const double E = 0.00000001;

        static void Main(string[] args)
        {
            // Задание 3
            #region Parse

            //double[,] A_Source;
            //double[] B_Source;
            //double[] X_0_Source;
            //double w;

            //using (var reader = new StreamReader(InputFilename))
            //{
            //    int n = int.Parse(reader.ReadLine());

            //    A_Source = new double[n, n];

            //    for (int i = 0; i < n; i++)
            //    {
            //        double[] arr = reader.ReadLine().Split(' ').Select(y => double.Parse(y)).ToArray();

            //        for (int j = 0; j < n; j++)
            //        {
            //            A_Source[i, j] = arr[j];
            //        }
            //    }

            //    B_Source = reader.ReadLine().Split(' ').Select(y => double.Parse(y)).ToArray();
            //    X_0_Source = reader.ReadLine().Split(' ').Select(y => double.Parse(y)).ToArray();
            //    w = double.Parse(reader.ReadLine());
            //}

            //Matrix<double> A = MatrixBuilder.DenseOfArray(A_Source);
            //Vector<double> B = VectorBuilder.Dense(B_Source);
            //Vector<double> X_0 = VectorBuilder.Dense(X_0_Source);

            ////(A * X_0).PrintBounds();
            //A.PrintBounds("A");
            //B.PrintBounds("B");

            //Vector<double> x_right = A.Solve(B);
            //x_right.PrintBounds("Right x");

            //Vector<double> x = RelaxationMethod(A, B, X_0, w, E, out var _, out var _);
            //x.PrintBounds("Answer x");

            #endregion

            #region Chart iterations / w

            //List<double> w_arr = new List<double>();
            //List<int> iter_arr = new List<int>();

            //for (w = 0.01; w <= 1; w += 0.01)
            //{
            //    Console.WriteLine($"\n\n[W] {w,4}");
            //    try
            //    {
            //        RelaxationMethod(A, B, X_0, w, E, out int it, out var _);

            //        w_arr.Add(w);
            //        iter_arr.Add(it);

            //        Console.WriteLine($"[Iterations] {it,4}");
            //    }
            //    catch (Exception)
            //    {
            //        Console.WriteLine("[Расходится]");
            //    }
            //}

            //Application.Run(new ChartForm(w_arr.ToArray(), iter_arr.ToArray()));

            #endregion

            #region Chart 5 lines

            //Dictionary<double, List<Tuple<int, double>>> data = new Dictionary<double, List<Tuple<int, double>>>();

            //RelaxationMethod(A, B, X_0, 0.15, E, out int _, out var r_1);
            //RelaxationMethod(A, B, X_0, 0.25, E, out int _, out var r_2);
            //RelaxationMethod(A, B, X_0, 0.5, E, out int _, out var r_3);
            //RelaxationMethod(A, B, X_0, 0.86, E, out int _, out var r_4);
            //RelaxationMethod(A, B, X_0, 0.95, E, out int _, out var r_5);

            //data[0.05] = r_1;
            //data[0.25] = r_2;
            //data[0.5] = r_3;
            //data[0.86] = r_4;
            //data[0.95] = r_5;

            //Application.Run(new ChartForm(data));

            #endregion Chart iterations / w

            // Задание 5
            #region Chart iterations / w

            //int n = 1000;
            //double w = 1;
            //SpecialMatrix A = new SpecialMatrix(-2, 1, n);
            //Vector<double> X = VectorBuilder.Dense(n, i => i + 1);
            //Vector<double> B = VectorBuilder.Dense(n, i => (n - 2) - i * 3);
            //Vector<double> X_0 = VectorBuilder.Dense(n);

            ////ToFullMatrix(A).PrintBounds("A");
            ////X.PrintBounds("X");
            ////B.PrintBounds("B");

            ////ToFullMatrix(A).Solve(B).PrintBounds("RIGHT X");
            ////RelaxationMethod_SpecialMatrix(A, B, X_0, w, E, out int it).PrintBounds("MY X");

            //List<double> w_arr = new List<double>();
            //List<int> iter_arr = new List<int>();

            //for (w = 0.01; w <= 2; w += 0.01)
            //{
            //    Console.WriteLine($"\n\n[W] {w,4}");
            //    try
            //    {
            //        RelaxationMethod_SpecialMatrix(A, B, X_0, w, E * 10, out int it, out var _);

            //        w_arr.Add(w);
            //        iter_arr.Add(it);

            //        Console.WriteLine($"[Iterations] {it,4}");
            //    }
            //    catch (Exception)
            //    {
            //        Console.WriteLine("[Расходится]");
            //    }
            //}

            //Application.Run(new ChartForm(w_arr.ToArray(), iter_arr.ToArray()));

            #endregion

            #region Chart 5 lines

            // Dictionary<double, List<Tuple<int, double>>> data = new Dictionary<double, List<Tuple<int, double>>>();

            // int n = 1000;
            // SpecialMatrix A = new SpecialMatrix(-2, 1, n);
            // Vector<double> X = VectorBuilder.Dense(n, i => i + 1);
            // Vector<double> B = VectorBuilder.Dense(n, i => (n - 2) - i * 3);
            // Vector<double> X_0 = VectorBuilder.Dense(n);

            //// RelaxationMethod_SpecialMatrix(A, B, X_0, 0.1, E, out int _, out var r_1);
            // RelaxationMethod_SpecialMatrix(A, B, X_0, 0.5, E, out int _, out var r_2);
            // RelaxationMethod_SpecialMatrix(A, B, X_0, 0.9, E, out int _, out var r_3);
            // RelaxationMethod_SpecialMatrix(A, B, X_0, 1.08, E, out int _, out var r_4);
            // RelaxationMethod_SpecialMatrix(A, B, X_0, 1.3, E, out int _, out var r_5);

            // //data[0.1] = r_1;
            // data[0.5] = r_2;
            // data[0.9] = r_3;
            // data[1.08] = r_4;
            // data[1.3] = r_5;


            // Application.Run(new ChartForm(data));

            #endregion

            #region Chart histogram

            //int n = 10;
            //SpecialMatrix A = new SpecialMatrix(-2, 1, n);
            //Vector<double> B = VectorBuilder.Dense(n, j => (n - 2) - j * 3);
            //Vector<double> X_0 = VectorBuilder.Dense(n);

            //double[] data = new double[6];
            //Stopwatch sw = new Stopwatch();

            //RelaxationMethod_SpecialMatrix(A, B, X_0, 1.08, E * 10, out int _, out var _);

            //for (int i = 0; i < 6; i++)
            //{
            //    n = (int)Math.Pow(10, i + 1);
            //    A = new SpecialMatrix(-2, 1, n);
            //    B = VectorBuilder.Dense(n, j => (n - 2) - j * 3);
            //    X_0 = VectorBuilder.Dense(n);

            //    sw.Restart();

            //    RelaxationMethod_SpecialMatrix(A, B, X_0, 1.08, E * 10, out int _, out var _);

            //    data[i] = sw.ElapsedMilliseconds;
            //}

            //Application.Run(new ChartForm(data));

            #endregion

            Console.Read();
        }

        #region Task 3

        static Vector<double> RelaxationMethod(Matrix<double> A, Vector<double> b, Vector<double> x_0, double w, int K)
        {
            RelaxationMethodCheckConvergence(A, w);

            Vector<double> x = x_0.Clone();

            for (int k = 0; k < K; k++)
            {
                x = RelaxationMethodIteration(A, b, x, w);
            }

            return x;
        }
        static Vector<double> RelaxationMethod(Matrix<double> A, Vector<double> b, Vector<double> x_0, double w, double e, out int iterations, out List<Tuple<int, double>> residuals)
        {
            RelaxationMethodCheckConvergence(A, w);

            Vector<double> x = x_0.Clone();

            iterations = 0;
            residuals = new List<Tuple<int, double>>();

            while ((A * x - b).L2Norm() >= e)
            {
                residuals.Add(new Tuple<int, double>(iterations, (A * x - b).L2Norm()));

                x = RelaxationMethodIteration(A, b, x, w);

                iterations++;
            }

            return x;
        }

        static Vector<double> RelaxationMethodIteration(Matrix<double> M, Vector<double> b, Vector<double> x_k, double w)
        {
            double w_inv = 1 - w;
            int n = b.Count();

            for (int i = 0; i < n; i++)
            {
                double sum = 0;

                for (int ind = 0; ind < i; ind++)
                {
                    sum += M[i, ind] * x_k[ind];
                }
                for (int ind = i + 1; ind < n; ind++)
                {
                    sum += M[i, ind] * x_k[ind];
                }

                x_k[i] = w_inv * x_k[i] + (w / M[i, i]) * (b[i] - sum);
            }

            return x_k;
        }
        static void RelaxationMethodCheckConvergence(Matrix<double> M, double w)
        {
            int n = M.RowCount;
            double w_inv = 1 - w;

            Matrix<double> D = MatrixBuilder.DenseDiagonal(n, n, i => M[i, i]);
            Matrix<double> L = M.LowerTriangle() - D;
            Matrix<double> R = M.UpperTriangle() - D;

            Matrix<double> B_W = (D + w * L).Inverse() * (w_inv * D - w * R);
            //Vector<double> g_W = (D + w * L).Inverse() * b;

            //B_W.PrintBounds();

            var values = B_W.Evd().EigenValues.Select(x => x.Magnitude);

            Console.Write($"[Values]");
            foreach (var val in values)
            {
                Console.Write($" {val.Round(5)}");
            }
            Console.WriteLine();

            if (values.Where(x => x >= 1).Count() != 0)
            {
                throw new ArgumentException("Метод не сходится");
            }
        }

        #endregion

        #region Task 5

        static Vector<double> RelaxationMethod_SpecialMatrix(SpecialMatrix A, Vector<double> b, Vector<double> x_0, double w, int K)
        {
            RelaxationMethodCheckConvergence_SpecialMatrix(A, w);

            Vector<double> x = x_0.Clone();

            for (int k = 0; k < K; k++)
            {
                x = RelaxationMethodIteration_SpecialMatrix(A, b, x, w);
            }

            return x;
        }
        static Vector<double> RelaxationMethod_SpecialMatrix(SpecialMatrix A, Vector<double> b, Vector<double> x_0, double w, double e, out int iterations, out List<Tuple<int, double>> residuals)
        {
            RelaxationMethodCheckConvergence_SpecialMatrix(A, w);

            Vector<double> x = x_0.Clone();

            iterations = 0;
            residuals = new List<Tuple<int, double>>();

            while (e < SpecialMatrix_L2Norm(A, x, b))
            {
                residuals.Add(new Tuple<int, double>(iterations, SpecialMatrix_L2Norm(A, x, b)));

                x = RelaxationMethodIteration_SpecialMatrix(A, b, x, w);

                iterations++;
            }

            return x;
        }

        static Vector<double> RelaxationMethodIteration_SpecialMatrix(SpecialMatrix M, Vector<double> b, Vector<double> x_k, double w)
        {
            int n = b.Count();
            double A = M.A;
            double B = M.B;
            double w_inv = 1 - w;
            double w_divided = w / A;

            for (int i = 0; i < n; i++)
            {
                x_k[i] = w_inv * x_k[i] + w_divided * (b[i] - x_k[n - i - 1] * B);
            }

            return x_k;
        }
        static void RelaxationMethodCheckConvergence_SpecialMatrix(SpecialMatrix M, double w)
        {
            double left_upper = 1 - w;
            double left_lower = ((-w * M.B) / M.A) * (1 - w);

            double right_upper = (1 / M.A) * (-w * M.B);
            double right_lower = Math.Pow((w * M.B) / M.A, 2) + 1 - w;

            double max_column_sum = Math.Max
                (
                    Math.Abs(Math.Abs(left_upper) + Math.Abs(left_lower)),
                    Math.Abs(Math.Abs(right_upper) + Math.Abs(right_lower))
                );

            double max_row_sum = Math.Max
                (
                    Math.Abs(Math.Abs(left_upper) + Math.Abs(right_upper)),
                    Math.Abs(Math.Abs(left_lower) + Math.Abs(right_lower))
                );

            if (max_row_sum >= 1 && max_column_sum >= 1)
            {
                throw new ArgumentException("Метод не сходится");
            }
        }
        static double SpecialMatrix_L2Norm(SpecialMatrix M, Vector<double> x, Vector<double> b)
        {
            int n = b.Count();
            double A = M.A;
            double B = M.B;

            double sum = 0;

            for (int i = 0; i < n; i++)
            {
                sum += Math.Pow(A * x[i] + B * x[n - i - 1] - b[i], 2);
            }

            return Math.Sqrt(sum);
        }
        static Matrix<double> ToFullMatrix(SpecialMatrix M)
        {
            var M_Full = MatrixBuilder.Dense(M.N, M.N);

            for (int i = 0; i < M.N; i++)
            {
                M_Full[i, i] = M.A;
                M_Full[i, M.N - i - 1] = M.B;
            }

            return M_Full;
        }

        class SpecialMatrix
        {
            public double A { get; private set; }
            public double B { get; private set; }
            public int N { get; private set; }

            public SpecialMatrix(double A, double B, int N)
            {
                if (N % 2 != 0)
                {
                    throw new ArgumentException();
                }

                this.A = A;
                this.B = B;
                this.N = N;
            }
        }

        #endregion
    }

    public static class Utils
    {
        public static double Round(this double value, int digits = 3)
        {
            return Math.Round(value, digits);
        }
        public static void PrintBounds(this Matrix<double> matrix, string caption = "")
        {
            Console.WriteLine($"[Matrix {caption}]:");
            Console.Write(".");
            for (int j = 0; j < matrix.ColumnCount; j++)
            {
                Console.Write("____________");
            }
            for (int i = 0; i < matrix.RowCount; i++)
            {
                Console.Write("\n|");
                for (int j = 0; j < matrix.ColumnCount; j++)
                {
                    Console.Write($"{matrix[i, j].Round(),-10} |");
                }
                Console.Write("\n|");

                for (int j = 0; j < matrix.ColumnCount; j++)
                {
                    Console.Write("___________|");
                }
            }

            Console.WriteLine("\n\n");
        }
        public static void PrintBounds(this Vector<double> vector, string caption = "")
        {
            Console.WriteLine($"[Vector {caption}]:");
            Console.Write(".");

            Console.Write("____________");

            for (int i = 0; i < vector.Count; i++)
            {
                Console.Write($"\n|{vector[i].Round(),-10} |");

                Console.Write("\n|");

                Console.Write("___________|");
            }

            Console.WriteLine("\n\n");
        }
        public static void Print(this Matrix<double> matrix, string caption = "")
        {
            Console.WriteLine($"[Matrix {caption}]:");

            for (int i = 0; i < matrix.RowCount; i++)
            {
                for (int j = 0; j < matrix.ColumnCount; j++)
                {
                    Console.Write($"{matrix[i, j].Round(),-10} ");
                }
                Console.Write("\n");
            }

            Console.WriteLine("\n\n");
        }
        public static void Print(this Vector<double> vector, string caption = "")
        {
            Console.WriteLine($"[Vector {caption}]:");

            for (int i = 0; i < vector.Count; i++)
            {
                Console.Write($"\n{vector[i].Round(),-10}");
            }

            Console.WriteLine("\n\n");
        }
    }
}