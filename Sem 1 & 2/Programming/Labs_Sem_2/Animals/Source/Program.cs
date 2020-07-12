using System;
using System.Collections.Generic;
using System.IO;


namespace Animals
{
    class Program
    {
        private static StreamReader Input;
        private static Dictionary<string, AnimalOwner> AnimalOwners;
        private static Dictionary<string, AnimalTypeData> AnimalsTypes;

        private static void Main(string[] args)
        {
            try
            {
                Input = new StreamReader("Files/input.txt");
            }
            catch
            {
                Console.WriteLine("There are some errors with the input file. Input file is not exists or it is empty");
                Console.ReadKey();
                return;
            }

            AnimalOwners = new Dictionary<string, AnimalOwner>();
            AnimalsTypes = new Dictionary<string, AnimalTypeData>();

            while (!Input.EndOfStream)
            {
                string[] BufferStringArray = Input.ReadLine().Split(',');

                if (!AnimalOwners.ContainsKey(BufferStringArray[0]))
                    AnimalOwners[BufferStringArray[0]] = new AnimalOwner(BufferStringArray[0]);

                if (!AnimalsTypes.ContainsKey(BufferStringArray[1]))
                    AnimalsTypes[BufferStringArray[1]] = new AnimalTypeData();


                Animal BufferAnimal = new Animal
                            (
                                BufferStringArray[1],
                                uint.Parse(BufferStringArray[3]),
                                AnimalOwners[BufferStringArray[0]],
                                BufferStringArray[2] == string.Empty ? Animal.Nameless : BufferStringArray[2]
                            );


                AnimalOwners[BufferStringArray[0]].OwnAnimals.Add(BufferAnimal);

                var CachedTypeData = AnimalsTypes[BufferStringArray[1]];
                CachedTypeData.Animals.Add(BufferAnimal);
                CachedTypeData.Owners.Add(BufferStringArray[0]);
                CachedTypeData.TypeAge.UpdateAge(BufferAnimal.Age);
            }

            Console.WriteLine("Type 1/2/3/4 to continue");
            var Key = Console.ReadKey().KeyChar;
            Console.WriteLine();

            switch (Key)
            {
                case '1':
                {
                    foreach (var animalOwner in AnimalOwners.Values)
                    {
                        int counter = 0;
                        HashSet<string> bufferTypes = new HashSet<string>();

                        foreach (var animal in animalOwner.OwnAnimals)
                            if (bufferTypes.Add(animal.Name))
                                counter++;

                        Console.WriteLine(string.Format("[{0}] has animals of [{1}] different types", animalOwner.Name, counter));
                    }

                    break;
                }

                case '2':
                {
                    Console.WriteLine("Enter an animal type");
                    string bufferTypeString = Console.ReadLine();

                    if (!AnimalsTypes.ContainsKey(bufferTypeString))
                    {
                        Console.WriteLine(string.Format("There are no animals of the selected type"));
                        break;
                    }

                    foreach (var owner in AnimalsTypes[bufferTypeString].Owners)
                    {
                        Console.WriteLine(string.Format("Animal owner: [{0}]", owner));

                        foreach (var animal in AnimalOwners[owner].OwnAnimals)
                            if (animal.Type == bufferTypeString)
                                Console.WriteLine(string.Format("   Animal name: [{0}]", animal.Name));

                        Console.WriteLine();
                    }

                    break;
                }

                case '3':
                {
                    Console.WriteLine("Enter an animal name");
                    string bufferString = Console.ReadLine();

                    HashSet<string> bufferTypes = new HashSet<string>();

                    foreach (var animals in AnimalsTypes.Values)
                        foreach (var animal in animals.Animals)
                            if (animal.Name == bufferString)
                            {
                                bufferTypes.Add(animal.Type);
                                break;
                            }


                    Console.WriteLine(string.Format("Such name have animals of [{0}] different types", bufferTypes.Count));

                    break;
                }

                case '4':
                {
                    foreach (var type in AnimalsTypes.Keys)
                        Console.WriteLine(string.Format("Type [{0,8}]: MIN age = [{1}], MAX age = [{2}]",
                            type,
                            AnimalsTypes[type].TypeAge.Min,
                            AnimalsTypes[type].TypeAge.Max));
                    break;
                }

                default:
                {
                    Console.WriteLine("Wrong input");
                    break;
                }
            }


            Console.ReadKey();
        }


        public class AnimalOwner
        {
            public string Name { get; }
            public List<Animal> OwnAnimals { get; }

            public AnimalOwner(string name)
            {
                Name = name;
                OwnAnimals = new List<Animal>();
            }
        }

        public class Animal
        {
            public const string Nameless = "[NAMELESS]";

            public string Type { get; }
            public uint Age { get; }
            public string Name { get; }
            public AnimalOwner Owner { get; }

            public Animal(string animalType, uint age, AnimalOwner animalOwner, string animalName = Nameless)
            {
                Type = animalType;
                Age = age;
                Owner = animalOwner;
                Name = animalName;
            }
        }

        public class AnimalTypeData
        {
            public HashSet<string> Owners { get; }
            public AnimalAge TypeAge { get; }
            public List<Animal> Animals { get; }

            public AnimalTypeData()
            {
                TypeAge = new AnimalAge();
                Animals = new List<Animal>();
                Owners = new HashSet<string>();
            }

            public class AnimalAge
            {
                public uint Min { get; private set; } = uint.MaxValue;
                public uint Max { get; private set; } = uint.MinValue;

                public void UpdateAge(uint age)
                {
                    if (age < Min) Min = age;
                    if (age >= Max) Max = age;
                }
            }
        }
    }
}
