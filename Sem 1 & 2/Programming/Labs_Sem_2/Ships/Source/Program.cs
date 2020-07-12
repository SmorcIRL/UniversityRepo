using System;
using System.Collections.Generic;
using System.IO;

namespace Ships
{
    public class Port
    {
        private StreamReader Input;
        private StreamWriter Output;

        private string InputFileName, OutputFileName;

        private Dictionary<int, string> PortState;
        private Queue<string> Raid;

        private int PortCapacity;
        private int FirstEmptyBerth = 1;

        private const string Empty = "Empty";
        private const int UnreachablePortSize = 20;

        public Port(string inputfilename, string outputfilename)
        {
            InputFileName = inputfilename;
            OutputFileName = outputfilename;
        }

        public void ProcessPort()
        {
            try
            {
                Input = new StreamReader(InputFileName);
            }

            catch
            {
                Console.WriteLine("There are some errors with the input file. Input file is not exists or it is empty");
                return;
            }

            Output = new StreamWriter(OutputFileName, false);

            Raid = new Queue<string>();
            PortState = new Dictionary<int, string>();

            PortCapacity = int.Parse(Input.ReadLine());

            for (int i = 1; i <= PortCapacity; i++)
            {
                PortState.Add(i, Empty);
            }

            while (!Input.EndOfStream)
            {
                string BufferString = Input.ReadLine();

                int State = (int)char.GetNumericValue(BufferString[0]);

                switch (State)
                {
                    case 1:
                    {
                        string SubBufferString = BufferString.Remove(0, 2);

                        FindFirstEmptyBerth();

                        if (FirstEmptyBerth != UnreachablePortSize)
                            PortState[FirstEmptyBerth] = SubBufferString;

                        else
                            Raid.Enqueue(SubBufferString);

                        continue;
                    }

                    case 2:
                    {
                        string SubBufferString = BufferString.Remove(0, 2);

                        int IndexToClear = (int)char.GetNumericValue(SubBufferString[0]);

                        PortState[IndexToClear] = Empty;

                        FindFirstEmptyBerth();

                        if (Raid.Count > 0)
                        {
                            PortState[FirstEmptyBerth] = Raid.Dequeue();
                        }

                        continue;
                    }

                    case 3:
                    {
                        Output.WriteLine();
                        Output.WriteLine("Raid state:");

                        int RaidCount = Raid.Count;

                        if (RaidCount == 0)
                        {
                            Output.WriteLine("Raid is empty");
                            continue;
                        }

                        int Iterator = 1;
                        foreach (var ship in Raid)
                        {
                            Output.WriteLine(string.Format("In raid at index <<{0}>> : <<{1}>>", Iterator, ship));
                            Iterator++;
                        }

                        continue;
                    }

                    case 4:
                    {
                        Output.WriteLine();
                        Output.WriteLine("PortState state:");


                        for (int i = 1; i <= PortCapacity; i++)
                        {
                            string OutputBufferString = PortState[i];

                            if (OutputBufferString == Empty)
                                Output.WriteLine(string.Format("Berth with index <<{0}>> is empty", i));

                            else
                                Output.WriteLine(string.Format("Berth with index <<{0}>> : <<{1}>>", i, OutputBufferString));
                        }

                        continue;
                    }
                }
            }

            Console.WriteLine("Completed successfully");


            Output.Close();
            Input.Close();
        }

        private int FindFirstEmptyBerth()
        {
            int MinEmptyIndex = UnreachablePortSize;

            foreach (var berth in PortState.Keys)
            {
                if (PortState[berth] == Empty && berth < MinEmptyIndex)
                    MinEmptyIndex = berth;
            }

            if (MinEmptyIndex != UnreachablePortSize)
                FirstEmptyBerth = MinEmptyIndex;
            else
                FirstEmptyBerth = UnreachablePortSize;

            return FirstEmptyBerth;
        }
    }

    internal class Program
    {
        private static void Main(string[] args)
        {
            string InputFileName, OutputFileName;

            Console.WriteLine("Enter an input file name: ");
            InputFileName = Console.ReadLine();

            Console.WriteLine("Enter an output file name: ");
            OutputFileName = Console.ReadLine();

            Port port = new Port(InputFileName, OutputFileName);
            port.ProcessPort();

            Console.ReadKey();
        }
    }
}
