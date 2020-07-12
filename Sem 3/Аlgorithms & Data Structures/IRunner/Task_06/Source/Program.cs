using System;
using System.IO;
using System.Linq;

namespace Task_6
{
    class Program
    {
        static void Main(string[] args)
        {
            long[,] matrix;
            long[] visited;
            long n, edges = 0, visited_count = 0;

            using (StreamReader input = new StreamReader("Files/input.txt"))
            {
                n = long.Parse(input.ReadLine());
                matrix = new long[n, n];
                visited = new long[n];

                for (long i = 0; i < n; ++i)
                {
                    var arr = input.ReadLine().Split(' ').Select(x => long.Parse(x)).ToArray();

                    for (long j = 0; j < n; ++j)
                    {
                        edges += (matrix[i, j] = arr[j]);
                    }
                }

                edges /= 2;
            }

            Action<long> depth_search = null;
            depth_search = (long v) =>
            {
                visited[v] = 1;
                ++visited_count;

                for (long i = 0; i < n; i++)
                {
                    if (matrix[v, i] == 1 && visited[i] == 0)
                    {
                        depth_search(i);
                    }
                }
            };

            depth_search(0);

            using (StreamWriter output = new StreamWriter("Files/output.txt"))
            {
                output.WriteLine((edges == n - 1) && (visited_count == n) ? Res.Yes : Res.No);
            }
        }
    }

    enum Res
    {
        No = 0,
        Yes = 1
    }
}