using System;
using System.Threading;

namespace Threads_CS
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            string[]
                BufferStringArray;

            string
                First_string = string.Empty,
                Second_string = string.Empty,
                Third_string = string.Empty,
                Fourth_string = string.Empty,
                Fifth_string = string.Empty;

            uint
                Counter = 0,
                First_count = 1, First_delta = 1000,
                Second_count = 1, Second_delta = 1000,
                Third_count = 1, Third_delta = 1000,
                Fourth_count = 1, Fourth_delta = 1000,
                Fifth_count = 1, Fifth_delta = 1000;

            #region Input

            Console.WriteLine("Max records: ");
            BufferStringArray = Console.ReadLine().Split();
            uint.TryParse(BufferStringArray[0], out Counter);
            Writer.RecordsLeft = Counter;

            Console.WriteLine();
            Console.WriteLine("Write 5 lines using the following format: <string uint uint>");

            BufferStringArray = Console.ReadLine().Split();
            First_string = BufferStringArray[0];
            uint.TryParse(BufferStringArray[1], out First_count);
            uint.TryParse(BufferStringArray[2], out First_delta);

            BufferStringArray = Console.ReadLine().Split();
            Second_string = BufferStringArray[0];
            uint.TryParse(BufferStringArray[1], out Second_count);
            uint.TryParse(BufferStringArray[2], out Second_delta);

            BufferStringArray = Console.ReadLine().Split();
            Third_string = BufferStringArray[0];
            uint.TryParse(BufferStringArray[1], out Third_count);
            uint.TryParse(BufferStringArray[2], out Third_delta);

            BufferStringArray = Console.ReadLine().Split();
            Fourth_string = BufferStringArray[0];
            uint.TryParse(BufferStringArray[1], out Fourth_count);
            uint.TryParse(BufferStringArray[2], out Fourth_delta);

            BufferStringArray = Console.ReadLine().Split();
            Fifth_string = BufferStringArray[0];
            uint.TryParse(BufferStringArray[1], out Fifth_count);
            uint.TryParse(BufferStringArray[2], out Fifth_delta);

            #endregion

            //First_string = "1"; First_count = 20; First_delta = 1000;
            //Second_string = "2"; Second_count = 15; Second_delta = 1500;
            //Third_string = "3"; Third_count = 15; Third_delta = 2000;
            //Fourth_string = "4"; Fourth_count = 10; Fourth_delta = 2500;
            //Fifth_string = "5"; Fifth_count = 5; Fifth_delta = 3000;

            Writer
                FirstWriter = new Writer(First_string, First_count, First_delta),
                SecondWriter = new Writer(Second_string, Second_count, Second_delta),
                ThirdWriter = new Writer(Third_string, Third_count, Third_delta),
                FourthWriter = new Writer(Fourth_string, Fourth_count, Fourth_delta),
                FifthWriter = new Writer(Fifth_string, Fifth_count, Fifth_delta);

            Thread
                FirstThread = new Thread(FirstWriter.StartWriting),
                SecondThread = new Thread(SecondWriter.StartWriting),
                ThirdThread = new Thread(ThirdWriter.StartWriting),
                FourthThread = new Thread(FourthWriter.StartWriting),
                FifthThread = new Thread(FifthWriter.StartWriting);

            FirstThread.Start();
            SecondThread.Start();
            ThirdThread.Start();
            FourthThread.Start();
            FifthThread.Start();

            Console.ReadKey();
        }
    }

    public class Writer
    {
        private static readonly object _locker = new object();

        public static uint
            RecordsLeft;

        private string
            _stringToWrite;

        private int
            _count,
            _delta;

        public Writer(string stringToWrite, uint count, uint delta)
        {
            _stringToWrite = stringToWrite;
            _count = count > int.MaxValue ? int.MaxValue : (int)count;
            _delta = delta > int.MaxValue ? int.MaxValue : (int)delta;
        }

        public void StartWriting()
        {
            while (_count != 0 && RecordsLeft != 0)
            {
                lock (_locker)
                {
                    if (RecordsLeft == 0) return;

                    else
                    {
                        --RecordsLeft;
                        --_count;
                    }
                }

                Thread.Sleep(_delta);
                Console.WriteLine(_stringToWrite);
            }
        }
    }
}
