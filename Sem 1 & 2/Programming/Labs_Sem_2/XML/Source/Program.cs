using System;
using System.Collections.Generic;
using System.Xml;

namespace XML
{
    class Program
    {
        private static void Main()
        {
            Parser parser = new Parser("Files/Family.xml");
            parser.GetInfo();

            Console.ReadKey();
        }
    }

    public class Parser
    {
        private List<Person> people;


        public Parser(string path)
        {
            people = new List<Person>();

            XmlReaderSettings settings = new XmlReaderSettings();
            settings.DtdProcessing = DtdProcessing.Parse;
            settings.ValidationType = ValidationType.DTD;
            settings.IgnoreWhitespace = true;

            using (XmlReader reader = XmlReader.Create(path, settings))
            {
                Person currentPerson = null;
                Marriage currentMarriage = null;

                while (reader.Read())
                {
                    if (reader.NodeType == XmlNodeType.Element)
                    {
                        switch (reader.Name)
                        {
                            case "Person":
                            {
                                currentPerson = new Person(reader.GetAttribute("name"), ParseDateString(reader.GetAttribute("birth_date")));
                                people.Add(currentPerson);

                                break;
                            }

                            case "Marriage":
                            {
                                currentMarriage =
                                new Marriage(reader.GetAttribute("spouse_name"), ParseDateString(reader.GetAttribute("marriage_date")), ParseDateString(reader.GetAttribute("divorce_date")));

                                currentPerson.Marriages.Add(currentMarriage);

                                break;
                            }

                            case "Child":
                            {
                                var child = new Child(reader.GetAttribute("name"));

                                currentMarriage.Childs.Add(child);
                                break;
                            }
                        }
                    }
                }
            }
        }


        public void GetInfo()
        {
            foreach (var person in people)
            {
                Console.WriteLine();
                Console.WriteLine("Person name: " + person.Name + " Birth date: " + person.Birth_date);
                Console.WriteLine("Marriges: ");

                if (person.Marriages.Count == 0)
                {
                    Console.WriteLine("None");
                }
                else
                {
                    foreach (var marriage in person.Marriages)
                    {
                        Console.WriteLine();
                        Console.WriteLine("Spouse name: " + marriage.Spouse_name + " marriage date: " + marriage.Marriage_date + " divorce date: " + marriage.Divorce_date);
                        Console.WriteLine("Childs: ");

                        if (marriage.Childs.Count == 0)
                            Console.WriteLine("None");
                        else
                            foreach (var child in marriage.Childs)
                                Console.WriteLine("Child name: " + child.Name);

                    }
                }

                Console.WriteLine();
                Console.WriteLine("==========================================================================");
            }
        }

        private string ParseDateString(string str)
        {
            if (str == "null") return str;

            DateTime bufferDate;
            if (DateTime.TryParse(str, out bufferDate))
            {
                return string.Format("{0:dd/MM/yyyy}", bufferDate);
            }

            throw new ArgumentException();
        }


        private class Person
        {
            public string Name { get; }
            public string Birth_date { get; }
            public List<Marriage> Marriages { get; }

            public Person(string Name, string Birth_Date)
            {
                this.Name = Name;
                Birth_date = Birth_Date;

                Marriages = new List<Marriage>();
            }
        }

        private class Marriage
        {
            public string Spouse_name { get; }
            public string Marriage_date { get; }
            public string Divorce_date { get; }
            public List<Child> Childs { get; }

            public Marriage(string Spouse_name, string Marriage_date, string Divorce_date)
            {
                this.Spouse_name = Spouse_name;
                this.Marriage_date = Marriage_date;
                this.Divorce_date = Divorce_date;

                Childs = new List<Child>();
            }
        }

        private class Child
        {
            public string Name { get; }

            public Child(string Name)
            {
                this.Name = Name;
            }
        }
    }
}
