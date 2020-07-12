using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Task_30
{
    class Program
    {
        static void Main(string[] args)
        {
            int n, first = 0;
            bool flag = false;

            List<int> deg = Enumerable.Repeat(0, 7).ToList();
            int[,] graph = new int[7, 7];
            Stack<int> stack = new Stack<int>();

            using (StreamReader input = new StreamReader("Files/input.txt"))
            {
                n = int.Parse(input.ReadLine());

                for (int i = 0; i < n; i++)
                {
                    var f_str_arr = input.ReadLine().Split(' ').Select(x => int.Parse(x)).ToArray();
                    int a = f_str_arr[0], b = f_str_arr[1];

                    graph[a, b]++;
                    graph[b, a]++;
                }
            }

            for (int i = 0; i < 7; ++i)
            {
                for (int j = 0; j < 7; ++j)
                {
                    deg[i] += graph[i, j];
                }
            }

            for (int i = 0; i < 7; ++i)
            {
                if (deg[i] % 2 != 0)
                {
                    using (StreamWriter output = new StreamWriter("Files/output.txt"))
                    {
                        output.Write("No");
                    }

                    return;
                }
            }

            while (deg[first] == 0) ++first;

            stack.Push(first);
            while (stack.Count > 0)
            {
                int top = stack.Peek();
                int i;

                for (i = 0; i < 7; ++i)
                {
                    if (graph[top, i] > 0)
                    {
                        break;
                    }
                }

                if (i != 7)
                {
                    --graph[top, i];
                    --graph[i, top];
                    stack.Push(i);
                }
                else
                {
                    stack.Pop();
                }
            }

            for (int i = 0; i < 7; ++i)
            {
                for (int j = 0; j < 7; ++j)
                {
                    if (graph[i, j] > 0)
                    {
                        flag = true;
                    }
                }
            }

            using (StreamWriter output = new StreamWriter("Files/output.txt"))
            {
                output.Write(flag ? "No" : "Yes");
            }
        }
    }
}