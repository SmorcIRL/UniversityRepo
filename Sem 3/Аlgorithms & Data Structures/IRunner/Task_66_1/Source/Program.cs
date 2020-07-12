using System.IO;
using System.Linq;

namespace Task_66_1
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

                n = f_str_arr[0]; ;

                OmegaArray = new int[n];

                for (int i = 1; i < n; ++i)
                {
                    f_str_arr = input.ReadLine().Split(' ').Select(x => int.Parse(x)).ToArray();

                    int v_1 = f_str_arr[0], v_2 = f_str_arr[1] - 1;

                    OmegaArray[v_2] = v_1;
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