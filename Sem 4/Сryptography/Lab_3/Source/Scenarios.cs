using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static Lab_3.MagmaСipher;

namespace Lab_3
{
    static class Scenarios
    {
        static Random random = new Random(Guid.NewGuid().GetHashCode());
        const int N = 15625;
        public enum SpecialTextKeyVariant
        {
            LowHWTextRandomKey,
            HighHWTextRandomKey,
            RandomTextLowHWKey,
            RandomTextHighHWKey
        }

        public static ulong[] RandomTextKey()
        {
            return Encode(FillText(GetRandomBlock), GetRandomKey());
        }
        public static ulong[] SpecialTextKey(SpecialTextKeyVariant variant)
        {
            ulong[] text;
            BitArray key;

            if (variant == SpecialTextKeyVariant.LowHWTextRandomKey || variant == SpecialTextKeyVariant.HighHWTextRandomKey)
            {
                key = GetRandomKey();

                if (variant == SpecialTextKeyVariant.LowHWTextRandomKey)
                {
                    text = FillText(GetRandomBlock_HW_LOW);
                }
                else
                {
                    text = FillText(GetRandomBlock_HW_HIGH);
                }
            }
            else if (variant == SpecialTextKeyVariant.RandomTextLowHWKey || variant == SpecialTextKeyVariant.RandomTextHighHWKey)
            {
                text = FillText(GetRandomBlock);

                if (variant == SpecialTextKeyVariant.RandomTextLowHWKey)
                {
                    key = GetRandomKey_HW_LOW();
                }
                else
                {
                    key = GetRandomKey_HW_HIGH();
                }
            }
            else
            {
                throw new ArgumentException();
            }

            return Encode(text, key);
        }
        public static ulong[] ErrorPropagationKey()
        {
            ulong text = 0;
            BitArray[] keys = new BitArray[N].Select(_ => GetRandomKey()).ToArray();
            ulong[,] result = new ulong[N, KeyBitLength];

            Parallel.For(0, N, i =>
            {
                for (int j = 0; j < KeyBitLength; j++)
                {
                    BitArray delta_key = new BitArray(KeyBitLength);

                    delta_key[j] = true;

                    result[i, j] = Encode(text, keys[i]) ^ Encode(text, ((BitArray)keys[i].Clone()).Xor(delta_key));
                }
            });

            return result.Cast<ulong>().ToArray();
        }
        public static ulong[] ErrorPropagationText()
        {
            ulong[] text = FillText(GetRandomBlock);
            uint[] key = new uint[KeySegmentsCount];
            ulong[,] result = new ulong[N, BlockBitLength];

            Parallel.For(0, N, i =>
            {
                Parallel.For(0, BlockBitLength, j =>
                {
                    result[i, j] = Encode(text[i], key) ^ Encode(text[i] ^ (0 | (uint)1 << (j % BlockBitLength)), key);
                });
            });

            return result.Cast<ulong>().ToArray();
        }
        public static ulong[] TextsCorrelation()
        {
            ulong[] text = FillText(GetRandomBlock);
            BitArray key = GetRandomKey();
            ulong[] encoded = Encode(text, key);

            Parallel.For(0, text.Length, i => text[i] ^= encoded[i]);

            return text;
        }
        public static ulong[] ProbabilisticProperties()
        {
            ulong[] result = new ulong[N];
            BitArray key = GetRandomKey();

            for (int i = 1; i < result.Length; i++)
            {
                result[i] = Encode(result[i - 1], key);
            }

            return result;
        }

        static BitArray GetRandomKey()
        {
            return new BitArray(new bool[KeyBitLength].Select(x => random.NextDouble() >= 0.5).ToArray());
        }
        static BitArray GetRandomKey_HW_LOW()
        {
            var key = new BitArray(KeyBitLength, false);

            int count = 0;

            while (count < 3)
            {
                int index = random.Next(KeyBitLength);

                if (!key[index])
                {
                    key[index] = true;
                    count++;
                }
            }

            return key;
        }
        static BitArray GetRandomKey_HW_HIGH()
        {
            return new BitArray(KeyBitLength, true).Xor(GetRandomKey_HW_LOW());
        }

        static ulong GetRandomBlock()
        {
            ulong result = 0;
            ulong bit = 1;

            for (int i = 0; i < BlockBitLength; i++)
            {
                if (random.NextDouble() >= 0.5)
                {
                    result |= bit;
                }

                bit <<= 1;
            }

            return result;
        }
        static ulong GetRandomBlock_HW_LOW()
        {
            ulong result = 0;

            int count = random.Next(3);

            while (count > 0)
            {
                int index = random.Next(BlockBitLength);

                ulong mask = (ulong)1 << index;

                if ((result & mask) == 0)
                {
                    result |= mask;
                    count--;
                }
            }

            return result;
        }
        static ulong GetRandomBlock_HW_HIGH()
        {
            ulong result = ulong.MaxValue;

            int count = random.Next(3);

            while (count > 0)
            {
                int index = random.Next(BlockBitLength);

                ulong mask = (ulong)1 << index;

                if ((result & mask) != 0)
                {
                    result ^= mask;
                    count--;
                }
            }

            return result;
        }

        static ulong[] FillText(Func<ulong> func, int n = N)
        {
            return new ulong[n].Select(_ => func()).ToArray();
        }
    }
}