using System;
using System.Numerics;

namespace Lab_5
{
    class Program
    {
        static void Main(string[] args)
        {
            Random rnd = new Random();

            for (int i = 0; i < 100; i++)
            {
                var num = rnd.Next();
                var k = ElGamal.GenerateKeys();
                var encr = ElGamal.Encrypt(num, k.Public);
                var decr = ElGamal.Decrypt(encr, k.Private);
                Console.WriteLine($"[{num,12}] [{decr,12}] [{num == decr}]");
            }

            Console.Read();
        }
    }

    public static class Utils
    {
        public static BigInteger ParseHex(string str)
        {
            return BigInteger.Parse("0" + str, System.Globalization.NumberStyles.HexNumber);
        }
    }
}