using System;

namespace Fraction
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            Fraction FirstFraction = new Fraction(13, 16);
            Fraction SecondFraction = new Fraction(11, 11);

            Console.WriteLine("Fractions");
            Console.WriteLine("First fraction:  " + FirstFraction);
            Console.WriteLine("Second fraction: " + SecondFraction);

            Console.WriteLine();

            Console.WriteLine("Arithmetic operators");
            Console.WriteLine("Summ:           " + (FirstFraction + SecondFraction));
            Console.WriteLine("Difference:     " + (FirstFraction - SecondFraction));
            Console.WriteLine("Multiplication: " + (FirstFraction * SecondFraction));
            Console.WriteLine("Division:       " + (FirstFraction / SecondFraction));

            Console.WriteLine();

            Console.WriteLine("Comparative operators");
            Console.WriteLine("First == Second ? " + (FirstFraction == SecondFraction));
            Console.WriteLine("First != Second ? " + (FirstFraction != SecondFraction));
            Console.WriteLine("First >  Second ? " + (FirstFraction > SecondFraction));
            Console.WriteLine("First >= Second ? " + (FirstFraction >= SecondFraction));
            Console.WriteLine("First <  Second ? " + (FirstFraction < SecondFraction));
            Console.WriteLine("First <= Second ? " + (FirstFraction <= SecondFraction));

            Console.ReadKey();
        }
    }

    internal class Fraction
    {
        private int _numerator = 0, _denumerator = 1;

        public int Numerator
        {
            get;
            set;
        }

        public int Denumerator
        {
            get => _denumerator;
        }


        public Fraction(int Num, int Denum)
        {
            Reduction(Num, Denum);
        }

        private Fraction(long Num, long Denum)
        {
            Reduction(Num, Denum);
        }


        public override string ToString()
        {
            return string.Format("{0} / {1}", _numerator, _denumerator);
        }

        private void Reduction(long Num, long Denum)
        {
            long NOD = Fraction.NOD(Num, Denum);

            if (Num == 0)
            {
                _numerator = 0;
                _denumerator = 1;
                return;
            }

            if (Denum < 0)
            {
                Num = -Num;
                Denum = -Denum;
            }

            checked
            {
                _numerator = (int)(Num / NOD);
                _denumerator = (int)(Denum / NOD);
            }
        }

        private static long NOD(long A, long B)
        {
            if (B == 0) throw new DivideByZeroException();
            if (A == 0 || A == 1 || B == 1) return 1;

            A = Math.Abs(A);
            B = Math.Abs(B);

            while (A != B)
            {
                if (A > B)
                    A -= B;

                else
                    B -= A;
            }

            return A;
        }


        #region Arithmetic operators
        public static Fraction operator -(Fraction Operand)
        {
            return new Fraction(-Operand._numerator, Operand._denumerator);
        }

        public static Fraction operator +(Fraction LeftOperand, Fraction RightOperand)
        {
            return new Fraction((long)LeftOperand._numerator * RightOperand._denumerator + (long)LeftOperand._denumerator * RightOperand._numerator, (long)LeftOperand._denumerator * RightOperand._denumerator);
        }

        public static Fraction operator -(Fraction LeftOperand, Fraction RightOperand)
        {
            return new Fraction((long)LeftOperand._numerator * RightOperand._denumerator - (long)LeftOperand._denumerator * RightOperand._numerator, (long)LeftOperand._denumerator * RightOperand._denumerator);
        }

        public static Fraction operator *(Fraction LeftOperand, Fraction RightOperand)
        {
            return new Fraction((long)LeftOperand._numerator * RightOperand._numerator, (long)LeftOperand._denumerator * RightOperand._denumerator);
        }

        public static Fraction operator /(Fraction LeftOperand, Fraction RightOperand)
        {
            return new Fraction((long)LeftOperand._numerator * RightOperand._denumerator, (long)LeftOperand._denumerator * RightOperand._numerator);
        }
        #endregion

        #region Comparative operators

        public static bool operator ==(Fraction LeftOperand, Fraction RightOperand)
        {
            return LeftOperand.Equals(RightOperand);
        }

        public static bool operator !=(Fraction LeftOperand, Fraction RightOperand)
        {
            return !(LeftOperand == RightOperand);
        }

        public static bool operator >(Fraction LeftOperand, Fraction RightOperand)
        {
            return ((double)LeftOperand._numerator / LeftOperand._denumerator).CompareTo((double)RightOperand._numerator / RightOperand._denumerator) > 0;
        }

        public static bool operator >=(Fraction LeftOperand, Fraction RightOperand)
        {
            return (LeftOperand > RightOperand) || (LeftOperand == RightOperand);
        }

        public static bool operator <(Fraction LeftOperand, Fraction RightOperand)
        {
            return !((LeftOperand == RightOperand) || (LeftOperand > RightOperand));
        }

        public static bool operator <=(Fraction LeftOperand, Fraction RightOperand)
        {
            return !(LeftOperand > RightOperand);
        }

        public override bool Equals(object ObjectOperand)
        {
            if (ObjectOperand == null)
                throw new NullReferenceException();

            if (!(ObjectOperand is Fraction))
                throw new ArgumentException("Argument should be Fraction type");

            return _numerator == (ObjectOperand as Fraction)._numerator && _denumerator == (ObjectOperand as Fraction)._denumerator;
        }

        public override int GetHashCode()
        {
            return _numerator.GetHashCode() + _denumerator.GetHashCode();
        }

        #endregion
    }
}
