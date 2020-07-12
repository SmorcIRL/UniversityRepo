using MathNet.Numerics.LinearAlgebra;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lab_3
{
    static class Program
    {
        #region Paths

        const string FilesFolderPath = @"Files\";

        const string QuadgramsPath = FilesFolderPath + "quadgrams.txt";
        const string QuadgramsFullInfo = FilesFolderPath + "quadgrams_full_info.txt";

        const string EncodedPath = FilesFolderPath + "encoded.txt";
        const string DecodedPath = FilesFolderPath + "decoded.txt";

        #endregion

        #region Common

        static readonly Dictionary<char, byte> PosByChar = new Dictionary<char, byte>()
        {
            {'A',  0},
            {'B',  1},
            {'C',  2},
            {'D',  3},  
            {'E',  4},
            {'F',  5},
            {'G',  6},
            {'H',  7},
            {'I',  8}, 
            {'J',  9},
            {'K', 10},
            {'L', 11},
            {'M', 12},
            {'N', 13}, 
            {'O', 14},
            {'P', 15},
            {'Q', 16},
            {'R', 17},
            {'S', 18}, 
            {'T', 19},
            {'U', 20},
            {'V', 21},
            {'W', 22},
            {'X', 23}, 
            {'Y', 24},
            {'Z', 25},
        };
        static readonly Dictionary<byte, char> CharByPos = new Dictionary<byte, char>()
        {
            { 0 ,'A'},  { 1 ,'B'},  { 2 ,'C'},  { 3 ,'D'},  { 4 ,'E'},
            { 5 ,'F'},  { 6 ,'G'},  { 7 ,'H'},  { 8 ,'I'},  { 9 ,'J'},
            { 10,'K'},  { 11,'L'},  { 12,'M'},  { 13,'N'},  { 14,'O'},
            { 15,'P'},  { 16,'Q'},  { 17,'R'},  { 18,'S'},  { 19,'T'},
            { 20,'U'},  { 21,'V'},  { 22,'W'},  { 23,'X'},  { 24,'Y'},
            { 25,'Z'}
        };

        static readonly int AlphabetLength = CharByPos.Count;

        static byte[] Encoded;

        #region Fitness function

        static readonly double[,,,] QuadData = new double[AlphabetLength, AlphabetLength, AlphabetLength, AlphabetLength];
        static double FitnessFloor;
        const int N = 40000;
        static long TotalQuadCount = 0;
        static double FitnessNormal = 0;


        static void InitFitness()
        {
            using (var reader = new StreamReader(QuadgramsPath))
            {
                for (int i = 0; i < N; i++)
                {
                    TotalQuadCount += long.Parse(reader.ReadLine().Split(' ')[1]);
                }
            }

            FitnessFloor = Math.Log10(1.0 / TotalQuadCount);

            for (int i_1 = 0; i_1 < AlphabetLength; i_1++)
            {
                for (int i_2 = 0; i_2 < AlphabetLength; i_2++)
                {
                    for (int i_3 = 0; i_3 < AlphabetLength; i_3++)
                    {
                        for (int i_4 = 0; i_4 < AlphabetLength; i_4++)
                        {
                            QuadData[i_1, i_2, i_3, i_4] = FitnessFloor;
                        }
                    }
                }
            }

            using (var reader = new StreamReader(QuadgramsPath))
            {
                double f_normal = 0;

                for (int i = 0; i < N; i++)
                {
                    var arr = reader.ReadLine().Split(' ');

                    long local_count = long.Parse(arr[1]);

                    f_normal += (QuadData[PosByChar[arr[0][0]], PosByChar[arr[0][1]], PosByChar[arr[0][2]], PosByChar[arr[0][3]]] = Math.Log10((double)local_count / TotalQuadCount));
                }

                FitnessNormal = Math.Abs(f_normal / N);
            }
        }

        static double GetFitness(byte[] arr)
        {
            double f_custom = 0;
            int K = arr.Length - 3;

            for (int i = 0; i < K; i++)
            {
                f_custom += QuadData[arr[i], arr[i + 1], arr[i + 2], arr[i + 3]];
            }

            return Math.Pow(((f_custom / K) - FitnessNormal) / FitnessNormal, 8);
        }

        #endregion

        #endregion

        #region SupMethods

        static byte[] ClearStringAndGetWrongSymbols(string str, Dictionary<int, char> symbols)
        {
            for (int i = 0; i < str.Length; i++)
            {
                if (!char.IsLetter(str[i]))
                {
                    symbols[i] = str[i];
                }
            }

            return str.Where(x => char.IsLetter(x)).Select(x => PosByChar[char.ToUpper(x)]).ToArray();
        }
        static string RepairString(string str, Dictionary<int, char> symbols)
        {
            StringBuilder builder = new StringBuilder(str);

            foreach (var pair in symbols)
            {
                builder.Insert(pair.Key, pair.Value);
            }

            return builder.ToString();
        }
        static void ViewQuadFrequencyDistribution()
        {
            using (var reader = new StreamReader(QuadgramsPath))
            {
                var arr = reader.ReadToEnd().Split('\n');

                var counts = arr.Select(x => long.Parse(x.Split(' ')[1])).ToArray();

                long sum = 0;

                for (int i = 0; i < counts.Count(); i++)
                {
                    sum += counts[i];
                }

                var frequencies = new double[counts.Length];

                using (var writer = new StreamWriter(QuadgramsFullInfo))
                {
                    for (int i = 0; i < counts.Count(); i++)
                    {
                        frequencies[i] = (double)counts[i] / sum;

                        var a = arr[i].Split(' ')[0];
                        writer.WriteLine($"{ a,-5 } {frequencies[i],-20} { Math.Log10(frequencies[i])}");
                    }
                }

                Application.Run(new ChartForm(frequencies));
            }
        }
        static int GreatestCommonDivisor(int a, int b)
        {
            while (b != 0)
            {
                int tmp = b;
                b = a % b;
                a = tmp;
            }

            return a;
        }
        static int ModularMultiplicativeInverse(int a, int m)
        {
            a %= m;

            for (int x = 1; x < m; x++)
            {
                if ((a * x) % m == 1)
                {
                    return x;
                }
            }

            return 1;
        }
        static string ByteArrToString(byte[] arr)
        {
            return new string(arr.Select(x => CharByPos[x]).ToArray());
        }
        public static Tuple<int, int, int> ExtendedEuclideanAlgorithm(int a, int b)
        {
            if (a == 0)
            {
                return new Tuple<int, int, int>(b, 0, 1);
            }

            int gcd, x, y;

            var r = ExtendedEuclideanAlgorithm(b % a, a);

            gcd = r.Item1;
            x = r.Item2;
            y = r.Item3;

            return new Tuple<int, int, int>(gcd, (y - (b / a) * x), x);
        }

        #endregion


        #region Affine

        static byte[] Affine_Encrypt(byte[] arr, int a, int b)
        {
            int m = AlphabetLength;

            if (GreatestCommonDivisor(a, m) != 1)
            {
                throw new ArgumentException("A and M must be coprime");
            }

            return arr.Select(x => (byte)((x * a + b) % m)).ToArray();
        }
        static byte[] Affine_Decrypt(byte[] arr, int a, int b)
        {
            int m = AlphabetLength;

            if (GreatestCommonDivisor(a, m) != 1)
            {
                throw new ArgumentException("A and M must be coprime");
            }

            int a_inversed = ModularMultiplicativeInverse(a, m);

            return arr.Select(x => (byte)(a_inversed * (x + m - b) % m)).ToArray();
        }
        static string Hack_Affine()
        {
            List<VariantAffine> vars = new List<VariantAffine>();
            VariantAffine best = null;

            for (byte i = 1; i < AlphabetLength; i++)
            {
                if (GreatestCommonDivisor(i, AlphabetLength) == 1)
                {
                    for (byte j = 0; j < AlphabetLength; j++)
                    {
                        vars.Add(new VariantAffine(i, j, GetFitness(Affine_Decrypt(Encoded, i, j))));
                    }
                }
            }

            vars.Sort((x, y) => x.Fitness.CompareTo(y.Fitness));

            best = vars[0];

            Console.WriteLine($"A: {best.A} B: {best.B} Fitness: {best.Fitness}");

            return ByteArrToString(Affine_Decrypt(Encoded, best.A, best.B));
        }

        class VariantAffine
        {
            public readonly byte A;
            public readonly byte B;
            public readonly double Fitness;

            public VariantAffine(byte a, byte b, double fitness)
            {
                A = a;
                B = b;
                Fitness = fitness;
            }
        }

        #endregion

        #region SimpleReplacement

        static byte[] SimpleReplacement_Decode(byte[] key)
        {
            byte[] arr = new byte[Encoded.Length];
            byte[] subkey = new byte[AlphabetLength];

            for (int i = 0; i < AlphabetLength; i++)
            {
                subkey[i] = key.First(x => key[x] == i);
            }

            for (int i = 0; i < Encoded.Length; i++)
            {
                arr[i] = subkey[Encoded[i]];
            }

            return arr;
        }
        static string SimpleReplacement_Hack()
        {
            List<Individual> generation = GetFirstGeneration();
            Individual best = new Individual(generation[0]);
            Stopwatch sw = Stopwatch.StartNew();

            best.CalculateFitness();

            int i = 0;

            while (true)
            {
                Parallel.ForEach(generation, x => x.CalculateFitness());
                generation.Sort((x, y) => x.Fitness.CompareTo(y.Fitness));

                Console.WriteLine($"[Gen {++i,4}] {generation[0].Fitness}");
                if (generation[0].Fitness < best.Fitness)
                {
                    best = generation[0];
                }
                if (Math.Abs(SourseFitness - generation[0].Fitness) < FitnessDelta || i >= MaxGenerations)
                {
                    break;
                }

                generation = Crossover(generation);

                Parallel.ForEach(generation, x => x.CalculateFitness());
                generation.Sort((x, y) => x.Fitness.CompareTo(y.Fitness));

                Mutation(generation);
            }

            Console.WriteLine();
            Console.WriteLine($"[Total generations]   {i}");
            Console.WriteLine($"[Total time(sec)]     {sw.Elapsed.TotalSeconds}");
            Console.WriteLine($"[Average ms/gen]      {(double)sw.ElapsedMilliseconds / i}");
            Console.WriteLine($"[Best fitness]        {best.Fitness}");

            return ByteArrToString(SimpleReplacement_Decode(best.Key));
        }

        #region Genetic algorithm

        const int PopulationSize = 30;
        const int MaxGenerations = 1000;
        const double MutationChance = 0.2d;
        const double RangeDelta = 1000;

        const int FirstPopulationMinFitness = 230;
        const double FitnessDelta = 10;
        const double SourseFitness = 40;

        static List<Individual> Crossover(List<Individual> generation)
        {
            Random random = new Random(Guid.NewGuid().GetHashCode());
            List<Individual> new_generation = new List<Individual>();
            Dictionary<int, Tuple<double, double>> ranges = new Dictionary<int, Tuple<double, double>>();

            double sum = generation.Select(x => x.Fitness).Sum();
            double[] chances = generation.Select(x => sum / x.Fitness).ToArray();
            double max = chances[0];
            double min = chances[AlphabetLength - 1];


            ranges[0] = new Tuple<double, double>(0, max / min + RangeDelta);
            double prev_top_bound;
            for (int i = 1; i < PopulationSize; i++)
            {
                prev_top_bound = ranges[i - 1].Item2;
                ranges[i] = new Tuple<double, double>(prev_top_bound, prev_top_bound + chances[0] / min + (RangeDelta - (RangeDelta / PopulationSize) * i));
            }

            double delta_bounds = ranges[PopulationSize - 1].Item2;

            for (int i = 0; i < PopulationSize / 2; i++)
            {
                Individual parent_1 = null;
                Individual parent_2 = null;

                while (parent_1 == parent_2)
                {
                    int indiv_index_1 = 0;
                    int indiv_index_2 = 0;

                    double random_value_1 = random.NextDouble() * delta_bounds;
                    double random_value_2 = random.NextDouble() * delta_bounds;

                    for (int j = 0; j < PopulationSize; j++)
                    {
                        if (ranges[j].Item1 < random_value_1 && random_value_1 <= ranges[j].Item2)
                            indiv_index_1 = j;
                    }

                    for (int j = 0; j < PopulationSize; j++)
                    {
                        if (ranges[j].Item1 < random_value_2 && random_value_2 <= ranges[j].Item2)
                            indiv_index_2 = j;
                    }

                    parent_1 = generation[indiv_index_1];
                    parent_2 = generation[indiv_index_2];
                }

                var new_pair = CrossoverOperator(parent_1, parent_2);

                new_generation.Add(new_pair.Item1);
                new_generation.Add(new_pair.Item2);
            }

            return new_generation;
        }
        static Tuple<Individual, Individual> CrossoverOperator(Individual ind_1, Individual ind_2)
        {
            Random random = new Random(Guid.NewGuid().GetHashCode());

            double ind_1_f = ind_1.Fitness;
            double ind_2_f = ind_2.Fitness;

            Individual better = ind_1_f < ind_2_f ? ind_1 : ind_2;
            Individual worse = ind_1_f >= ind_2_f ? ind_1 : ind_2;

            double betterChance = worse.Fitness / (ind_1_f + ind_2_f);

            byte[] key_1 = new byte[AlphabetLength];
            byte[] key_2 = new byte[AlphabetLength];

            HashSet<byte> left = Enumerable.Range(0, AlphabetLength).Select(x => (byte)x).ToHashSet();
            HashSet<byte> used = new HashSet<byte>();

            for (int i = 0; i < AlphabetLength; i++)
            {
                bool u_1 = used.Contains(better.Key[i]);
                bool u_2 = used.Contains(worse.Key[i]);
                byte value;

                if ((u_1 && !u_2) || (u_2 && !u_1))
                {
                    value = u_2 ? better.Key[i] : worse.Key[i];
                }
                else if (!u_1 && !u_2)
                {
                    value = random.NextDouble() < betterChance ? better.Key[i] : worse.Key[i];
                }
                else
                {
                    value = left.ElementAt(random.Next(0, left.Count));
                }

                key_1[i] = value;
                used.Add(value);
                left.Remove(value);
            }

            left = Enumerable.Range(0, AlphabetLength).Select(x => (byte)x).ToHashSet();
            used.Clear();

            for (int i = 0; i < AlphabetLength; i++)
            {
                bool u_1 = used.Contains(better.Key[i]);
                bool u_2 = used.Contains(worse.Key[i]);
                byte value;

                if ((u_1 && !u_2) || (u_2 && !u_1))
                {
                    value = u_2 ? better.Key[i] : worse.Key[i];
                }
                else if (!u_1 && !u_2)
                {
                    value = random.NextDouble() < betterChance ? better.Key[i] : worse.Key[i];
                }
                else
                {
                    value = left.ElementAt(random.Next(0, left.Count));
                }

                key_2[i] = value;
                used.Add(value);
                left.Remove(value);
            }

            return new Tuple<Individual, Individual>(new Individual(key_1), new Individual(key_2));
        }

        static void Mutation(List<Individual> generation)
        {
            Random random = new Random(Guid.NewGuid().GetHashCode());

            Parallel.For(0, PopulationSize, i => { if (random.NextDouble() < (MutationChance + (i / PopulationSize))) MutationOperator(generation[i]); });
            //Parallel.For(0, PopulationSize, i => { if (random.NextDouble() < (MutationChance)) MutationOperator(generation[i]); });
        }
        static void MutationOperator(Individual ind)
        {
            Random random = new Random(Guid.NewGuid().GetHashCode());

            int gen_index_1 = 0;
            int gen_index_2 = 0;

            while (gen_index_1 == gen_index_2)
            {
                gen_index_1 = random.Next(0, AlphabetLength);
                gen_index_2 = random.Next(0, AlphabetLength);
            }

            byte buff = ind.Key[gen_index_1];
            ind.Key[gen_index_1] = ind.Key[gen_index_2];
            ind.Key[gen_index_2] = buff;
        }
        static List<Individual> GetFirstGeneration()
        {
            Random random = new Random(Guid.NewGuid().GetHashCode());
            Individual[] keys = new Individual[PopulationSize];

            Parallel.For(0, PopulationSize, i =>
            {
                byte[] arr = { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24, 25 };

                Individual ind = new Individual(new List<byte>(arr).OrderBy(x => random.Next()).ToArray());
                ind.CalculateFitness();

                while (ind.Fitness > FirstPopulationMinFitness)
                {
                    ind = new Individual(new List<byte>(arr).OrderBy(x => random.Next()).ToArray());
                    ind.CalculateFitness();
                }

                keys[i] = ind;
            });

            return keys.ToList();
        }

        class Individual
        {
            public byte[] Key = new byte[AlphabetLength];
            public double Fitness;

            public Individual(Individual ind)
            {
                ind.Key.CopyTo(Key, 0);
            }

            public Individual(byte[] gen)
            {
                Key = gen;
            }

            public void CalculateFitness()
            {
                Fitness = GetFitness(SimpleReplacement_Decode(Key));
            }
        }

        #endregion

        #endregion

        #region Hill 

        static Matrix<double> cipher_matrix = Matrix<double>.Build.Dense(2, 1);
        static Matrix<double> key_matrix = Matrix<double>.Build.Dense(2, 2);
        static Matrix<double> key_matrix_inversed = Matrix<double>.Build.Dense(2, 2);
        static Matrix<double> data_matrix = Matrix<double>.Build.Dense(2, 1);

        const double CriticalFitness = 60.0;

        static byte[] Hill_Encrypt(byte[] arr, byte[] key)
        {
            if (key.Length != 4 || arr.Length % 2 != 0)
            {
                throw new ArgumentException();
            }

            byte[] res = new byte[arr.Length];

            Matrix<double> cipher_matrix = Matrix<double>.Build.Dense(2, 1);
            Matrix<double> key_matrix = Matrix<double>.Build.Dense(2, 2, key.Select(x => (double)x).ToArray());
            Matrix<double> data_matrix = Matrix<double>.Build.Dense(2, 1);

            if (key_matrix.Determinant() == 0.0 || GreatestCommonDivisor((int)key_matrix.Determinant(), AlphabetLength) != 1)
            {
                throw new ArgumentException();
            }

            for (int i = 0; i < arr.Length; i += 2)
            {
                data_matrix[0, 0] = arr[i];
                data_matrix[1, 0] = arr[i + 1];

                cipher_matrix = (key_matrix * data_matrix).Modulus(AlphabetLength);

                res[i] = (byte)Math.Round(cipher_matrix[0, 0], MidpointRounding.AwayFromZero);
                res[i + 1] = (byte)Math.Round(cipher_matrix[1, 0], MidpointRounding.AwayFromZero);
            }

            return res;
        }
        static byte[] Hill_Decrypt(byte[] arr, byte[] key)
        {
            //if (key.Length != 4 || arr.Length % 2 != 0)
            //{
            //    throw new ArgumentException();
            //}

            byte[] res = new byte[arr.Length];

            key_matrix[0, 0] = key[0];
            key_matrix[1, 0] = key[1];
            key_matrix[0, 1] = key[2];
            key_matrix[1, 1] = key[3];


            //if (key_matrix.Determinant() == 0.0 || GreatestCommonDivisor((int)key_matrix.Determinant(), AlphabetLength) != 1)
            if (GreatestCommonDivisor((int)key_matrix.Determinant(), AlphabetLength) != 1)
            {
                return null; //throw new ArgumentException();
            }

            key_matrix_inversed[0, 0] = key_matrix[1, 1];
            key_matrix_inversed[1, 1] = key_matrix[0, 0];
            key_matrix_inversed[0, 1] = -key_matrix[0, 1];
            key_matrix_inversed[1, 0] = -key_matrix[1, 0];

            key_matrix_inversed = (key_matrix_inversed * ExtendedEuclideanAlgorithm((int)key_matrix.Determinant(), AlphabetLength).Item2).Modulus(AlphabetLength);

            for (int i = 0; i < arr.Length; i += 2)
            {
                cipher_matrix[0, 0] = arr[i];
                cipher_matrix[1, 0] = arr[i + 1];

                data_matrix = (key_matrix_inversed * cipher_matrix).Modulus(AlphabetLength);

                res[i] = (byte)data_matrix[0, 0];
                res[i + 1] = (byte)data_matrix[1, 0];
            }

            return res;
        }
        static string Hack_Hill()
        {
            if (Encoded.Length % 2 != 0)
                throw new ArgumentException();

            VariantHill best = new VariantHill();

            Stopwatch sw = new Stopwatch();
            long total_ms = 0;

            for (byte i_1 = 0; i_1 < AlphabetLength; i_1++)
            {
                sw.Restart();

                for (byte i_2 = 0; i_2 < AlphabetLength; i_2++)
                {
                    for (byte i_3 = 0; i_3 < AlphabetLength; i_3++)
                    {
                        for (byte i_4 = 0; i_4 < AlphabetLength; i_4++)
                        {
                            if (i_1 * i_4 == i_2 * i_3) continue;

                            var new_key = new byte[] { i_1, i_2, i_3, i_4 };

                            var decoded = Hill_Decrypt(Encoded, new_key);

                            if (decoded != null)
                            {
                                var new_var = new VariantHill(new_key, GetFitness(decoded));

                                if (new_var.Fitness < 80)
                                {
                                    best = new_var;

                                    total_ms += sw.ElapsedMilliseconds;

                                    goto END;
                                }
                                else if (new_var.Fitness < best.Fitness)
                                {
                                    best = new_var;
                                }
                            }
                        }
                    }
                }

                total_ms += sw.ElapsedMilliseconds;

                Console.WriteLine($"[{i_1 + 1,2}/{AlphabetLength}] {sw.ElapsedMilliseconds / 1000.0} sec");
            }

        END:
            Console.WriteLine();
            Console.WriteLine($"[Total time(sec)]     {total_ms / 1000}");
            Console.WriteLine($"[Best fitness]        {best.Fitness}");
            Console.WriteLine($"[Key]                 [{string.Concat(best.Key.Select((x, i) => x.ToString() + (i < best.Key.Length - 1 ? " " : string.Empty)))}] or [{new string(best.Key.Select(x => CharByPos[x]).ToArray())}]");

            return ByteArrToString(Hill_Decrypt(Encoded, best.Key));
        }

        class VariantHill
        {
            public readonly byte[] Key;
            public readonly double Fitness;

            public VariantHill()
            {
                Fitness = int.MaxValue;
            }

            public VariantHill(byte[] key, double fitness)
            {
                Key = key;
                Fitness = fitness;
            }
        }

        #endregion

        static void Main(string[] args)
        {
            InitFitness();

            ViewQuadFrequencyDistribution(); // Распределение частот квадрограм

            //Dictionary<int, char> symbols = new Dictionary<int, char>();

            //using (var reader = new StreamReader(EncodedPath))
            //{
            //    Encoded = ClearStringAndGetWrongSymbols(reader.ReadToEnd(), symbols);
            //}

            //using (var writer = new StreamWriter(DecodedPath))
            //{
            //    //writer.Write(RepairString(Hack_Affine(), symbols));
            //    writer.Write(RepairString(SimpleReplacement_Hack(), symbols));
            //    //writer.Write(RepairString(Hack_Hill(), symbols));
            //}

            Console.Read();
        }
    }
}