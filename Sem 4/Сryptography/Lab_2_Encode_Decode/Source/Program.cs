using MathNet.Numerics.LinearAlgebra;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Lab_2
{
    class Program
    {
        #region Paths

        const string FilesFolderPath = @"Files\";
        const string KeyFilename = FilesFolderPath + "key.txt";
        const string DataFilename = FilesFolderPath + "in.txt";
        const string CryptFilename = FilesFolderPath + "crypt.txt";
        const string DecryptFilename = FilesFolderPath + "decrypt.txt";

        #endregion

        static void Main(string[] args)
        {
            Tuple<int, int> key_Affine = null;
            string key_SimpleReplacement = null;
            string key_Hill = null;

            string data = null;

            using (var keyReader = new StreamReader(KeyFilename))
            {
                key_Affine = new Tuple<int, int>(int.Parse(keyReader.ReadLine()), int.Parse(keyReader.ReadLine()));
                key_SimpleReplacement = keyReader.ReadLine().ToUpper();
                key_Hill = keyReader.ReadLine().ToUpper();
            }

            using (var dataReader = new StreamReader(DataFilename))
            {
                data = Ciphers.ClearString(dataReader.ReadToEnd());
            }

            string crypt_Affine = data.Affine_Encrypt(key_Affine);
            string crypt_SimpleReplacement = data.SimpleReplacement_Encrypt(key_SimpleReplacement);
            string crypt_Hill = data.Hill_Encrypt(key_Hill);

            using (var cryptWriter = new StreamWriter(CryptFilename))
            {
                cryptWriter.WriteLine(crypt_Affine);
                cryptWriter.WriteLine(crypt_SimpleReplacement);
                cryptWriter.WriteLine(crypt_Hill);
            }

            using (var decryptWriter = new StreamWriter(DecryptFilename))
            {
                decryptWriter.WriteLine(crypt_Affine.Affine_Decrypt(key_Affine));
                decryptWriter.WriteLine(crypt_SimpleReplacement.SimpleReplacement_Decrypt(key_SimpleReplacement));
                decryptWriter.WriteLine(crypt_Hill.Hill_Decrypt(key_Hill));
            }
        }
    }

    public static class Ciphers
    {
        #region Alphabet stuff

        public static readonly Dictionary<char, int> Alphabet_PosByChar = new Dictionary<char, int>()
        {
            {' ', 0},
            {'A', 1},
            {'B', 2},
            {'C', 3},
            {'D', 4},
            {'E', 5},
            {'F', 6},
            {'G', 7},
            {'H', 8},
            {'I', 9},
            {'J', 10},
            {'K', 11},
            {'L', 12},
            {'M', 13},
            {'N', 14},
            {'O', 15},
            {'P', 16},
            {'Q', 17},
            {'R', 18},
            {'S', 19},
            {'T', 20},
            {'U', 21},
            {'V', 22},
            {'W', 23},
            {'X', 24},
            {'Y', 25},
            {'Z', 26}
        };
        public static readonly Dictionary<int, char> Alphabet_CharByPos = new Dictionary<int, char>()
        {
            { 0 ,' '},
            { 1 ,'A'},
            { 2 ,'B'},
            { 3 ,'C'},
            { 4 ,'D'},
            { 5 ,'E'},
            { 6 ,'F'},
            { 7 ,'G'},
            { 8 ,'H'},
            { 9 ,'I'},
            { 10,'J'},
            { 11,'K'},
            { 12,'L'},
            { 13,'M'},
            { 14,'N'},
            { 15,'O'},
            { 16,'P'},
            { 17,'Q'},
            { 18,'R'},
            { 19,'S'},
            { 20,'T'},
            { 21,'U'},
            { 22,'V'},
            { 23,'W'},
            { 24,'X'},
            { 25,'Y'},
            { 26,'Z'}
        };
        public static readonly int AlphabetLength = Alphabet_CharByPos.Count;
        public static readonly char PadCharacter = Alphabet_CharByPos[PadIndex];
        public static readonly int PadIndex = 0;

        #endregion

        #region Additional methods

        static int GreatestCommonDivisor(int x, int y) => y == 0 ? x : GreatestCommonDivisor(y, x % y);
        static int ModularMultiplicativeInverse(int a, int m)
        {
            a %= m;

            for (int x = 1; x < m; x++)
                if ((a * x) % m == 1)
                    return x;

            return 1;
        }
        public static Tuple<int, int, int> ExtendedEuclideanAlgorithm(int a, int b)
        {
            if (a == 0)
                return new Tuple<int, int, int>(b, 0, 1);

            int gcd, x, y;

            var r = ExtendedEuclideanAlgorithm(b % a, a);

            gcd = r.Item1;
            x = r.Item2;
            y = r.Item3;

            return new Tuple<int, int, int>(gcd, (y - (b / a) * x), x);
        }
        public static string RemoveSpaces(string sourse)
        {
            return sourse.Replace(" ", String.Empty);
        }
        public static string ClearString(string sourse)
        {
            return new string(sourse.ToUpper().Where(x => Alphabet_PosByChar.ContainsKey(x)).ToArray());
        }

        #endregion

        public static string Affine_Encrypt(this string str_to_encrypt, Tuple<int, int> key)
        {
            int a = key.Item1,
                b = key.Item2,
                m = AlphabetLength;

            if (GreatestCommonDivisor(a, m) != 1)
                throw new ArgumentException("A and M must be coprime");

            return new string(str_to_encrypt.Select(x => Alphabet_CharByPos[(Alphabet_PosByChar[x] * a + b) % m]).ToArray());
        }
        public static string Affine_Decrypt(this string str_to_decrypt, Tuple<int, int> key)
        {
            int a = key.Item1,
                b = key.Item2,
                m = AlphabetLength;

            if (GreatestCommonDivisor(a, m) != 1)
                throw new ArgumentException("A and M must be coprime");

            int a_inversed = ModularMultiplicativeInverse(a, m);

            return new string(str_to_decrypt.Select(x => Alphabet_CharByPos[a_inversed * (Alphabet_PosByChar[x] + m - b) % m]).ToArray());
        }

        public static string SimpleReplacement_Encrypt(this string str_to_encrypt, string key)
        {
            var mapping = key.Select((x, i) => new KeyValuePair<char, char>(Alphabet_CharByPos[i], x)).ToDictionary(x => x.Key, x => x.Value);

            return new string(str_to_encrypt.Select(x => mapping[x]).ToArray());
        }
        public static string SimpleReplacement_Decrypt(this string str_to_decrypt, string key)
        {
            var mapping = key.Select((x, i) => new KeyValuePair<char, char>(x, Alphabet_CharByPos[i])).ToDictionary(x => x.Key, x => x.Value);

            return new string(str_to_decrypt.Select(x => mapping[x]).ToArray());
        }

        public static string Hill_Encrypt(this string str_to_encrypt, string key)
        {
            if (key.Length != 4)
            {
                throw new ArgumentException("Key must have 4 elements");
            }
            if (str_to_encrypt.Length % 2 != 0)
            {
                str_to_encrypt += PadCharacter.ToString();
            }

            StringBuilder builder = new StringBuilder(str_to_encrypt.Length);

            Matrix<double> cipher_matrix = Matrix<double>.Build.Dense(2, 1);
            Matrix<double> key_matrix = Matrix<double>.Build.Dense(2, 2, key.Select(x => (double)Alphabet_PosByChar[x]).ToArray());
            Matrix<double> data_matrix = Matrix<double>.Build.Dense(2, 1);

            if (key_matrix.Determinant() == 0.0 || GreatestCommonDivisor((int)key_matrix.Determinant(), AlphabetLength) != 1)
            {
                throw new ArgumentException();
            }

            for (int i = 0; i < str_to_encrypt.Length; i += 2)
            {
                data_matrix[0, 0] = Alphabet_PosByChar[str_to_encrypt[i]];
                data_matrix[1, 0] = Alphabet_PosByChar[str_to_encrypt[i + 1]];

                cipher_matrix = (key_matrix * data_matrix).Modulus(AlphabetLength);

                builder.Append(Alphabet_CharByPos[(int)cipher_matrix[0, 0]]);
                builder.Append(Alphabet_CharByPos[(int)cipher_matrix[1, 0]]);
            }

            return builder.ToString();
        }
        public static string Hill_Decrypt(this string str_to_decrypt, string key)
        {
            if (key.Length != 4)
            {
                throw new ArgumentException("Key must have 4 elements");
            }
            if (str_to_decrypt.Length % 2 != 0)
            {
                str_to_decrypt += PadCharacter.ToString();
            }

            StringBuilder builder = new StringBuilder(str_to_decrypt.Length);

            Matrix<double> cipher_matrix = Matrix<double>.Build.Dense(2, 1);
            Matrix<double> key_matrix = Matrix<double>.Build.Dense(2, 2, key.Select(x => (double)Alphabet_PosByChar[x]).ToArray());
            Matrix<double> key_matrix_inversed = Matrix<double>.Build.Dense(2, 2);
            Matrix<double> data_matrix = Matrix<double>.Build.Dense(2, 1);

            if (key_matrix.Determinant() == 0.0 || GreatestCommonDivisor((int)key_matrix.Determinant(), AlphabetLength) != 1)
            {
                throw new ArgumentException();
            }

            key_matrix_inversed[0, 0] = key_matrix[1, 1];
            key_matrix_inversed[1, 1] = key_matrix[0, 0];
            key_matrix_inversed[0, 1] = -key_matrix[0, 1];
            key_matrix_inversed[1, 0] = -key_matrix[1, 0];

            key_matrix_inversed = (key_matrix_inversed * ExtendedEuclideanAlgorithm((int)key_matrix.Determinant(), AlphabetLength).Item2).Modulus(AlphabetLength);

            for (int i = 0; i < str_to_decrypt.Length; i += 2)
            {
                cipher_matrix[0, 0] = Alphabet_PosByChar[str_to_decrypt[i]];
                cipher_matrix[1, 0] = Alphabet_PosByChar[str_to_decrypt[i + 1]];

                data_matrix = (key_matrix_inversed * cipher_matrix).Modulus(AlphabetLength);

                builder.Append(Alphabet_CharByPos[(int)data_matrix[0, 0]]);
                builder.Append(Alphabet_CharByPos[(int)data_matrix[1, 0]]);
            }

            return builder.ToString();
        }
    }
}