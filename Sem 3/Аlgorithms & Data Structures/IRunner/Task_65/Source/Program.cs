using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Task_65
{
    class Program
    {
        static void Main(string[] args)
        {
            int n, m;
            List<HashSet<int>> OmegaList = new List<HashSet<int>>();

            using (StreamReader input = new StreamReader("Files/input.txt"))
            {
                var f_str_arr = input.ReadLine().Split(' ').Select(x => int.Parse(x)).ToArray();

                n = f_str_arr[0];
                m = f_str_arr[1];

                OmegaList = new List<HashSet<int>>(n + 1);

                for (int i = 0; i <= n; i++)
                {
                    OmegaList.Add(new HashSet<int>());
                }

                for (int i = 0; i < m; ++i)
                {
                    f_str_arr = input.ReadLine().Split(' ').Select(x => int.Parse(x)).ToArray();

                    int v_1 = f_str_arr[0], v_2 = f_str_arr[1];

                    OmegaList[v_1].Add(v_2);
                    OmegaList[v_2].Add(v_1);
                }
            }

            using (StreamWriter output = new StreamWriter("Files/output.txt"))
            {
                for (int i = 1; i <= n; ++i)
                {
                    output.Write($"{OmegaList[i].Count}");

                    foreach (var v in OmegaList[i])
                    {
                        output.Write($" {v}");
                    }

                    output.WriteLine();
                }
            }
        }
    }
}