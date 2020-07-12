using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using MathNet.Numerics.LinearAlgebra;

namespace Lab_2
{
    class Program
    {
        const string InputFilename = "Files/Input.txt";

        static void Main(string[] args)
        {
            double[,] A_Source;
            double[] B_Source;

            using (var reader = new StreamReader(InputFilename))
            {
                int n = int.Parse(reader.ReadLine());

                A_Source = new double[n, n];

                for (int i = 0; i < n; i++)
                {
                    double[] arr = reader.ReadLine().Split(' ').Select(y => double.Parse(y)).ToArray();

                    for (int j = 0; j < n; j++)
                    {
                        A_Source[i, j] = arr[j];
                    }
                }

                B_Source = reader.ReadLine().Split(' ').Select(y => double.Parse(y)).ToArray();
            }

            Matrix<double> A = Matrix<double>.Build.DenseOfArray(A_Source);
            Vector<double> B = Vector<double>.Build.Dense(B_Source);

            A.PrintBounds("A");
            B.PrintBounds("b");

            if (!CheckMatrix(A, B))
            {
                throw new Exception();
            }  
            
            Vector<double> x = ReflectionMethod(A, B, out double det);
            Vector<double> x_right = A.Solve(B);

            x.PrintBounds("MY X");
            x_right.PrintBounds("RIGHT X");

            #region Chart

            //double[] results = new double[100];

            //Matrix<double> A = null;
            //Vector<double> B = null;

            //Stopwatch sw = new Stopwatch();

            //for (int i = 0; i < results.Length; i++)
            //{
            //    do
            //    {
            //        A = Matrix<double>.Build.Random(5 * (i + 1), 5 * (i + 1));
            //        B = Vector<double>.Build.Random(5 * (i + 1));

            //    } while (!CheckMatrix(A, B));

            //    sw.Restart();
            //    ReflectionMethod(A, B, out double _);
            //    results[i] = sw.Elapsed.TotalSeconds;
            //}

            //Application.Run(new ChartForm(results));

            #endregion

            Console.Read();
        }

        static Vector<double> ReflectionMethod(Matrix<double> A_Param, Vector<double> B_Param, out double detA)
        {
            int n = B_Param.Count;

            Matrix<double> A = A_Param.Clone();
            Vector<double> B = B_Param.Clone();
            Matrix<double> Identity = Matrix<double>.Build.DenseIdentity(n, n);

            for (int j = 0; j < n - 1; j++)
            {
                int diag = n - j;

                Vector<double> b = A.Column(j, j, diag);
                Vector<double> c = Vector<double>.Build.Dense(diag);
                c[0] = 1;

                double det_b = b.L2Norm();

                Vector<double> v = b - det_b * c;

                if (v.L2Norm() == 0)
                {
                    continue;
                }

                Vector<double> w = v / Math.Sqrt(2 * b * v);

                Matrix<double> Buffer = Identity.Clone();

                for (int i = j; i < n; i++)
                {
                    for (int k = j; k < n; k++)
                    {
                        Buffer[i, k] -= 2 * w[i - j] * w[k - j];
                    }
                }

                Buffer.Multiply(A, A);
                Buffer.Multiply(B, B);
            }

            detA = Math.Pow(-1, n - 1) * A.Determinant(); // На данном этапе A уже преобразована в R

            return SolveTriagonal(A, B);
        }
        static Vector<double> SolveTriagonal(Matrix<double> M, Vector<double> B)
        {
            int n = B.Count();
            Matrix<double> A = M.Clone();
            Vector<double> X = B.Clone();

            for (int i = n - 1; i >= 0; i--)
            {
                X[i] /= A[i, i];

                for (int k = 0; k < i; k++)
                {
                    X[k] -= A[k, i] * X[i];
                }
            }

            return X;
        }
        // Теорема Кронекера – Капелли
        static bool CheckMatrix(Matrix<double> A, Vector<double> B)
        {
            int n = B.Count;
            Matrix<double> A_Extended = Matrix<double>.Build.DenseIdentity(n, n + 1);
            A_Extended.SetSubMatrix(0, 0, A);
            A_Extended.SetColumn(n, B);

            var A_Rank = A.Rank();
            var AE_Rank = A_Extended.Rank();

            return A.RowCount == A.ColumnCount && A_Rank == n && A_Rank == AE_Rank;
        }
        static double MaxNorm(Matrix<double> M)
        {
            return M.RowSums().AbsoluteMaximum();
        }
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