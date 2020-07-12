using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using MathNet.Numerics.LinearAlgebra;

namespace Lab_5
{
    class Program
    {
        static MatrixBuilder<double> MatrixBuilder = Matrix<double>.Build;
        static VectorBuilder<double> VectorBuilder = Vector<double>.Build;
        static Random random = new Random();

        static readonly Matrix<double> A = MatrixBuilder.DenseOfArray(new double[3, 3]
        {
            {1.2,   0.5,    0.3},
            {-0.4,  1.2,    0.1},
            {0.3,  -0.1,    1.2}
        });
        static readonly Vector<double> b = VectorBuilder.DenseOfArray(new[]
        {
            4.0,
            1,
            -1
        });

        static void Main(string[] args)
        {
            var res_true = A.Solve(b);

            A.PrintBounds("A");
            b.PrintBounds("b");
            Console.WriteLine("[Точное решение]");
            res_true.PrintBounds();
            Console.WriteLine("[Моё решение c длинной цепи Маркова = 1000, количеством цепей = 10000]");
            MonteKarlo(A, b, 1000, 10000).PrintBounds();

            List<double> list = new List<double>();
            for (int i = 1; i < 5000; i += 100)
            {
                list.Add((res_true - MonteKarlo(A, b, 100, i)).L2Norm());
            }
            Application.Run(new ChartForm(list.ToArray()));

            Console.Read();
        }

        static Vector<double> MonteKarlo(Matrix<double> A, Vector<double> b, int N, int M)
        {
            int n = b.Count();

            Matrix<double> a = MatrixBuilder.Dense(n, n, (i, j) => i == j ? 0 : -A[i, j] / A[i, i]);
            Vector<double> f = VectorBuilder.Dense(n, i => b[i] / A[i, i]);
            Vector<double> x = VectorBuilder.Dense(n);

            Vector<double> h = VectorBuilder.Dense(n);
            Vector<double> pi = VectorBuilder.Dense(n, 1.0 / 3);
            Matrix<double> P = MatrixBuilder.Dense(n, n, (i, j) => 1.0 / 3);

            double[] E = new double[M];
            int[] MarkovСhain = new int[N + 1];
            double[] Weights = new double[N + 1];

            for (int i = 0; i < n; i++)
            {
                h.Clear();
                h[i] = 1;

                Array.Clear(E, 0, E.Length);

                for (int j = 0; j < M; j++)
                {
                    for (int k = 0; k <= N; k++)
                    {
                        MarkovСhain[k] = random.Next(0, 3);
                    }

                    Weights[0] = pi[MarkovСhain[0]] > 0 ? h[MarkovСhain[0]] / pi[MarkovСhain[0]] : 0;

                    for (int k = 1; k <= N; k++)
                    {
                        Weights[k] = P[MarkovСhain[k - 1], MarkovСhain[k]] > 0 ? Weights[k - 1] * a[MarkovСhain[k - 1], MarkovСhain[k]] / P[MarkovСhain[k - 1], MarkovСhain[k]] : 0;
                    }

                    for (int k = 0; k <= N; k++)
                    {
                        E[j] += Weights[k] * f[MarkovСhain[k]];
                    }
                }

                x[i] = E.Sum() / M;
            }

            return x;
        }
    }

    public static class Utils
    {
        public static double Round(this double value, int digits = 3)
        {
            return Math.Round(value, 3);
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