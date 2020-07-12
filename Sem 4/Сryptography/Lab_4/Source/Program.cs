using System;
using System.Numerics;

namespace Lab_4
{
    class Program
    {
        static void Main(string[] args)
        {
            var k = RSA.GenerateKeys();
            Console.WriteLine("[KEY]");
            Console.WriteLine($"[E] {k.Public.Exp}");
            Console.WriteLine($"[D] {k.Private.D}");
            Console.WriteLine($"[N] {k.Public.Mod}");

            BigInteger source = BigInteger.Parse("987654321123456789987654321123456789987654321123456789");
            var encr = RSA.Encrypt(source, k.Public);
            var decr = RSA.Decrypt(encr, k.Private);

            Console.WriteLine($"\n[SOURCE   ] {source}");
            Console.WriteLine($"[ENCRYPTED] {encr}");
            Console.WriteLine($"[DECRYPTED] {decr}");

            Console.Read();
        }
    }
}