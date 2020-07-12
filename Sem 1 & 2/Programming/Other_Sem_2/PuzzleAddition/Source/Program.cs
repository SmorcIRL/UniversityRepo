using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace PuzzleHelper
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            BinaryFormatter formatter = new BinaryFormatter();

            using (FileStream fs = new FileStream("save.gamedata", FileMode.OpenOrCreate))
            {
                formatter.Serialize(fs, new List<GameData>()
                {
                    new GameData("Player_1", new DateTime(2000,5,20,20,20,20), 100,2342),
                    new GameData("Player_1", new DateTime(1999,1,20,20,20,20), 56,23423),
                    new GameData("Player_1", new DateTime(2000,5,20,20,20,20), 200,222),
                    new GameData("Player_1", new DateTime(2004,2,20,20,20,20), 200,234),
                    new GameData("Player_1", new DateTime(2000,2,20,20,20,20), 200,4435),
                    new GameData("Player_1", new DateTime(2000,5,20,20,20,20), 200,5435),
                    new GameData("Player_2", new DateTime(2000,4,20,20,20,20), 60,766),
                    new GameData("Player_2", new DateTime(2342,5,20,20,20,20), 200,66),
                    new GameData("Player_2", new DateTime(2004,5,20,20,20,20), 200,65),
                    new GameData("Player_2", new DateTime(2000,5,20,20,20,20), 11,755),
                    new GameData("Player_2", new DateTime(2000,5,20,20,20,20), 223,65),
                    new GameData("Player_4", new DateTime(2000,5,20,20,20,20), 200,567),
                    new GameData("Player_3", new DateTime(2000,6,20,20,20,20), 200,55),
                    new GameData("Player_5", new DateTime(2004,5,20,20,20,20), 1232,257),
                    new GameData("Player_4", new DateTime(2000,5,20,20,20,20), 200,546),
                    new GameData("Player_1", new DateTime(2014,5,20,20,20,20), 200,278),
                    new GameData("Player_4", new DateTime(2000,5,20,20,20,20), 223,200),
                    new GameData("Player_1", new DateTime(2012,5,20,20,20,20), 200,855),
                    new GameData("Player_5", new DateTime(2000,5,20,20,20,20), 200,878),
                    new GameData("Player_1", new DateTime(2000,5,20,20,20,20), 200,200),
                    new GameData("Player_3", new DateTime(2014,5,20,20,20,20), 40,200),
                    new GameData("Player_2", new DateTime(2000,7,20,20,20,20), 323,200),
                    new GameData("Player_3", new DateTime(2000,5,20,20,20,20), 200,200),
                    new GameData("Player_5", new DateTime(2000,5,20,20,20,20), 200,77),
                    new GameData("Player_3", new DateTime(2000,5,20,20,20,20), 322,782),
                    new GameData("Player_2", new DateTime(2015,5,20,20,20,20), 200,7),
                    new GameData("Player_2", new DateTime(2000,7,20,20,20,20), 200,200),
                    new GameData("Player_1", new DateTime(2000,5,20,20,20,20), 1333,200),
                    new GameData("Player_4", new DateTime(2019,5,20,20,20,20), 200,333),
                    new GameData("Player_4", new DateTime(2015,8,20,20,20,20), 3433,34200),
                    new GameData("Player_5", new DateTime(2017,5,20,20,20,20), 222,53),
                });
            }
        }


        [Serializable]
        public class GameData
        {
            public GameData()
            {
                StartTime = DateTime.Now;
                PlayerName = "Player";
            }

            public GameData(string name, DateTime dateTime, int dur, int steps)
            {
                StartTime = dateTime;
                PlayerName = name;
                SessionDurationInSeconds = dur;
                StepCount = steps;
            }

            public string PlayerName { get; set; }
            public DateTime StartTime { get; set; }
            public int SessionDurationInSeconds { get; set; }
            public int StepCount { get; set; }
        }
    }
}
