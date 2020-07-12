using System;
using System.IO;


namespace Task_56
{
    class Program
    {
        static void Main(string[] args)
        {
            long n;

            using (StreamReader input = new StreamReader("Files/input.txt"))
            {
                n = long.Parse(input.ReadLine());
            }

            string str = ReverseString(Convert.ToString(n, 2));

            using (StreamWriter output = new StreamWriter("Files/output.txt"))
            {
                for (long i = 0; i < str.Length; i++)
                {
                    if (str[(int)i] == '1') output.WriteLine(i);
                }
            }
        }

        public static string ReverseString(string s)
        {
            char[] arr = s.ToCharArray();
            Array.Reverse(arr);
            return new string(arr);
        }
    }
}
