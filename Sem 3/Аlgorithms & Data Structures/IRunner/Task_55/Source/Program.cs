using System.IO;
using System.Linq;

namespace Task_55
{
    class Program
    {
        static void Main(string[] args)
        {
            long[] arr;
            long n;

            using (StreamReader input = new StreamReader("Files/input.txt"))
            {
                n = long.Parse(input.ReadLine());
                arr = input.ReadLine().Split(' ').Select(x => long.Parse(x)).ToArray();

                if (n == 1)
                {
                    using (StreamWriter output = new StreamWriter("Files/output.txt"))
                    {
                        output.WriteLine(Res.Yes);
                        return;
                    }
                }
                else if (n == 2)
                {
                    using (StreamWriter output = new StreamWriter("Files/output.txt"))
                    {
                        output.WriteLine(arr[0] < arr[1] ? Res.Yes : Res.No);
                        return;
                    }
                }
            }

            using (StreamWriter output = new StreamWriter("Files/output.txt"))
            {
                for (long i = 0; i <= (n - 2) / 2; ++i)
                {
                    if (arr[i] > arr[2 * i + 1] || ((2 * i + 2 != n) && arr[i] > arr[2 * i + 2]))
                    {
                        output.WriteLine(Res.No);
                        return;
                    }
                }

                output.WriteLine(Res.Yes);
            }
        }

        enum Res
        {
            No = 0,
            Yes = 1
        }
    }
}
