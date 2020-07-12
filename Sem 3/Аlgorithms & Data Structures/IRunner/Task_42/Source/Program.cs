using System.IO;
using System.Linq;

namespace Task_42
{
    class Program
    {
        static void Main(string[] args)
        {
            int[] array = null;
            int[] source = null;
            int[,] f = null;
            int n;

            using (StreamReader input = new StreamReader("Files/input.txt"))
            {
                n = int.Parse(input.ReadLine());
                source = input.ReadLine().Split(' ').Select(x => int.Parse(x)).ToArray();
                array = new int[n + 1];
                f = new int[n, n];
            }

            for (int i = 0; i < n; i++)
            {
                array[i + 1] = array[i] + source[i];
                f[i, i] = source[i];
            }

            for (int diag = 1; diag < n; diag++)
            {
                for (int l = 0; l <= n - diag - 1; l++)
                {
                    int r = diag + l;

                    if ((source[l] + (array[r + 1] - array[l + 1] - f[l + 1, r])) > (source[r] + (array[r] - array[l] - f[l, r - 1])))
                    { 
                        f[l, r] = source[l] + (array[r + 1] - array[l + 1] - f[l + 1, r]);
                    }
                    else
                    {
                        f[l, r] = source[r] + (array[r] - array[l] - f[l, r - 1]);
                    }
                }
            }

            using (StreamWriter output = new StreamWriter("Files/output.txt"))
            {
                output.Write(f[0, n - 1]);
            }
        }
    }
}