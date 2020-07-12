using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Task_67_1
{
    class Program
    {
        static int count = 0;
        static int mark = 1;

        static void Main(string[] args)
        {
            int n;
            int[] Marks;
            List<List<int>> OmegaList = new List<List<int>>();

            using (StreamReader input = new StreamReader("Files/input.txt"))
            {
                var f_str_arr = input.ReadLine().Split(' ').Select(x => int.Parse(x)).ToArray();

                count = n = f_str_arr[0];

                Marks = new int[n + 1];

                OmegaList = new List<List<int>>();
                for (int i = 0; i <= n; i++)
                {
                    OmegaList.Add(new List<int>());
                }

                for (int i = 1; i <= n; i++)
                {
                    Marks[i] = -1;

                    f_str_arr = input.ReadLine().Split(' ').Select(x => int.Parse(x)).ToArray();

                    for (int j = 1; j <= n; j++)
                    {
                        if (f_str_arr[j - 1] == 1)
                        {
                            OmegaList[i].Add(j);
                        }
                    }
                }
            }

            List<int> queue = new List<int>();
            List<int> new_elements = new List<int>();

            while (count > 0)
            {
                int x = 0;

                for (int i = 1; i <= n; i++)
                {
                    if (Marks[i] < 0)
                    {
                        x = i;
                        break;
                    }
                }

                queue.Add(x);
                Marks[x] = mark++;
                count--;

                while (queue.Count != 0)
                {
                    int current = queue.First();
                    queue.RemoveAt(0);
                    new_elements.Clear();

                    foreach (var neig in OmegaList[current])
                    {
                        if (Marks[neig] < 0)
                        {
                            new_elements.Add(neig);
                            Marks[neig] = mark++;
                            count--;
                        }
                    }

                    for (int i = 0; i < new_elements.Count; i++)
                    {
                        queue.Add(new_elements[i]);
                    }
                }
            }

            using (StreamWriter output = new StreamWriter("Files/output.txt"))
            {
                for (int i = 1; i <= n; i++)
                {
                    output.Write($"{Marks[i]} ");
                }
            }
        }
    }
}