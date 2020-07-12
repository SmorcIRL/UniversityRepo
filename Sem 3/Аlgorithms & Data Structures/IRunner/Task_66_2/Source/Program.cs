using System.IO;
using System.Linq;

namespace Task_66_2
{
    class Program
    {
        static void Main(string[] args)
        {
            int n;
            int[] OmegaArray;

            using (StreamReader input = new StreamReader("Files/input.txt"))
            {
                var f_str_arr = input.ReadLine().Split(' ').Select(x => int.Parse(x)).ToArray();

                n = f_str_arr[0];

                OmegaArray = new int[n];

                for (int i = 0; i < n; i++)
                {
                    f_str_arr = input.ReadLine().Split(' ').Select(x => int.Parse(x)).ToArray();

                    for (int j = 0; j < n; j++)
                    {
                        if (f_str_arr[j] == 1) OmegaArray[j] = i + 1;
                    }
                }
            }

            using (StreamWriter output = new StreamWriter("Files/output.txt"))
            {
                foreach (var el in OmegaArray)
                {
                    output.Write($"{el} ");
                }
            }
        }
    }
}