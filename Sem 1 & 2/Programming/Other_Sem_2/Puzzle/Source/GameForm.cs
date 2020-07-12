using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Windows.Forms;

namespace Puzzle
{
    public partial class GameForm : Form
    {
        private int[,]
            Matrix = new int[4, 4],
            BufferMatrix = new int[4, 4];
        private int
            Counter = 0,
            Ticker = 0;
        private bool
            Won = false;

        private readonly List<int> InitializationList = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16 };
        private const string
            DefaultName = "Player",
            SavePath = "save.gamedata";

        private List<GameData> Games;
        private GameData CurrentGameData;


        public GameForm()
        {
            //BinaryFormatter formatter = new BinaryFormatter();

            //using (FileStream fs = new FileStream("save.gamedata", FileMode.OpenOrCreate))
            //{
            //    formatter.Serialize(fs, new List<GameData>()
            //    {
            //        new GameData("Player_1", new DateTime(2000,5,20,20,20,20), 100,2342),
            //        new GameData("Player_1", new DateTime(1999,1,20,20,20,20), 56,23423),
            //        new GameData("Player_1", new DateTime(2000,5,20,20,20,20), 200,222),
            //        new GameData("Player_1", new DateTime(2004,2,20,20,20,20), 200,234),
            //        new GameData("Player_1", new DateTime(2000,2,20,20,20,20), 200,4435),
            //        new GameData("Player_1", new DateTime(2000,5,20,20,20,20), 200,5435),
            //        new GameData("Player_2", new DateTime(2000,4,20,20,20,20), 60,766),
            //        new GameData("Player_2", new DateTime(2342,5,20,20,20,20), 200,66),
            //        new GameData("Player_2", new DateTime(2004,5,20,20,20,20), 200,65),
            //        new GameData("Player_2", new DateTime(2000,5,20,20,20,20), 11,755),
            //        new GameData("Player_2", new DateTime(2000,5,20,20,20,20), 223,65),
            //        new GameData("Player_4", new DateTime(2000,5,20,20,20,20), 200,567),
            //        new GameData("Player_3", new DateTime(2000,6,20,20,20,20), 200,55),
            //        new GameData("Player_5", new DateTime(2004,5,20,20,20,20), 1232,257),
            //        new GameData("Player_4", new DateTime(2000,5,20,20,20,20), 200,546),
            //        new GameData("Player_1", new DateTime(2014,5,20,20,20,20), 200,278),
            //        new GameData("Player_4", new DateTime(2000,5,20,20,20,20), 223,200),
            //        new GameData("Player_1", new DateTime(2012,5,20,20,20,20), 200,855),
            //        new GameData("Player_5", new DateTime(2000,5,20,20,20,20), 200,878),
            //        new GameData("Player_1", new DateTime(2000,5,20,20,20,20), 200,200),
            //        new GameData("Player_3", new DateTime(2014,5,20,20,20,20), 40,200),
            //        new GameData("Player_2", new DateTime(2000,7,20,20,20,20), 323,200),
            //        new GameData("Player_3", new DateTime(2000,5,20,20,20,20), 200,200),
            //        new GameData("Player_5", new DateTime(2000,5,20,20,20,20), 200,77),
            //        new GameData("Player_3", new DateTime(2000,5,20,20,20,20), 322,782),
            //        new GameData("Player_2", new DateTime(2015,5,20,20,20,20), 200,7),
            //        new GameData("Player_2", new DateTime(2000,7,20,20,20,20), 200,200),
            //        new GameData("Player_1", new DateTime(2000,5,20,20,20,20), 1333,200),
            //        new GameData("Player_4", new DateTime(2019,5,20,20,20,20), 200,333),
            //        new GameData("Player_4", new DateTime(2015,8,20,20,20,20), 3433,34200),
            //        new GameData("Player_5", new DateTime(2017,5,20,20,20,20), 222,53),
            //    });
            //}


            InitializeComponent();

            DeserializeGames();

            InitializeMatrix(false);
        }

        private void InitializeMatrix(bool GetFromBuffer)
        {
            CurrentGameData = new GameData();

            Won = false;
            timer1.Enabled = true;
            panel_GZ.Visible = false;
            playerNameTextBox.Text = DefaultName;
            CloseRecords(null, null);

            if (GetFromBuffer)
            {
                Array.Copy(BufferMatrix, 0, Matrix, 0, Matrix.Length);
            }

            else
            {
                Random random = new Random();

                while (true)
                {
                    List<int> BufferInitializationList = new List<int>(InitializationList);

                    for (int i = 0; i < 4; ++i)
                        for (int g = 0; g < 4; ++g)
                        {
                            int randomListIndex = random.Next(BufferInitializationList.Count);
                            Matrix[i, g] = BufferInitializationList[randomListIndex];
                            BufferInitializationList.RemoveAt(randomListIndex);
                        }


                    int N = 0;

                    for (int i = 0; i < 4; ++i)
                        for (int g = 0; g < 4; ++g)
                        {
                            if (Matrix[i, g] != 16)
                            {
                                int k = g;

                                for (int l = i; l < 4; ++l)
                                {
                                    for (; k < 4; ++k)
                                        if (Matrix[l, k] < Matrix[i, g]) ++N;

                                    k = 0;
                                }

                            }

                            else
                                N += 1 + i;
                        }


                    if (N % 2 == 0) break;
                }

                Array.Copy(Matrix, 0, BufferMatrix, 0, Matrix.Length);
            }

            for (int i = 0; i < 4; ++i)
                for (int g = 0; g < 4; ++g)
                    gridPanel.GetControlFromPosition(g, i).Text = Matrix[i, g] == 16 ? string.Empty : Matrix[i, g].ToString();

            Counter = 0;
            label1.Text = Counter.ToString();

            Ticker = 0;
            Timer_Tick(null, null);

            playerNameTextBox.Text = DefaultName;
        }


        private void TableButtonClick(object sender, MouseEventArgs e)
        {
            if (Won) return;

            var button = sender as Button;

            int
                ButtonIndex = button.TabIndex,
                CurrentColumn = gridPanel.GetColumn(button),
                CurrentRow = gridPanel.GetRow(button);

            bool Replaced = false;
            Control bufferButton = null;

            if (CurrentRow > 0 && !Replaced)
            {
                bufferButton = gridPanel.GetControlFromPosition(CurrentColumn, CurrentRow - 1);

                if (bufferButton.Text == string.Empty)
                {
                    Matrix[CurrentRow - 1, CurrentColumn] = Matrix[CurrentRow, CurrentColumn];
                    Matrix[CurrentRow, CurrentColumn] = 16;

                    bufferButton.Text = button.Text;
                    button.Text = string.Empty;

                    Replaced = true;
                }
            }

            if (CurrentRow < 3 && !Replaced)
            {
                bufferButton = gridPanel.GetControlFromPosition(CurrentColumn, CurrentRow + 1);

                if (bufferButton.Text == string.Empty)
                {
                    Matrix[CurrentRow + 1, CurrentColumn] = Matrix[CurrentRow, CurrentColumn];
                    Matrix[CurrentRow, CurrentColumn] = 16;

                    bufferButton.Text = button.Text;
                    button.Text = string.Empty;

                    Replaced = true;
                }
            }

            if (CurrentColumn > 0 && !Replaced)
            {
                bufferButton = gridPanel.GetControlFromPosition(CurrentColumn - 1, CurrentRow);

                if (bufferButton.Text == string.Empty)
                {
                    Matrix[CurrentRow, CurrentColumn - 1] = Matrix[CurrentRow, CurrentColumn];
                    Matrix[CurrentRow, CurrentColumn] = 16;

                    bufferButton.Text = button.Text;
                    button.Text = string.Empty;

                    Replaced = true;
                }
            }

            if (CurrentColumn < 3 && !Replaced)
            {
                bufferButton = gridPanel.GetControlFromPosition(CurrentColumn + 1, CurrentRow);

                if (bufferButton.Text == string.Empty)
                {
                    Matrix[CurrentRow, CurrentColumn + 1] = Matrix[CurrentRow, CurrentColumn];
                    Matrix[CurrentRow, CurrentColumn] = 16;

                    bufferButton.Text = button.Text;
                    button.Text = string.Empty;

                    Replaced = true;
                }
            }

            if (Replaced)
            {
                Counter++;
                label1.Text = Counter.ToString();
            }


            for (int i = 0; i < 4; i++)
                for (int g = 0; g < 4; g++)
                    if (Matrix[i, g] - 1 != i * 4 + g) return;

            ToWin();
        }

        private void NewGameBtnMainClick(object sender, EventArgs e)
        {
            InitializeMatrix(true);
        }

        private void NewGameBtnFromGzPanelClick(object sender, EventArgs e)
        {
            CurrentGameData.PlayerName = string.IsNullOrEmpty(playerNameTextBox.Text) ? DefaultName : playerNameTextBox.Text;

            InitializeMatrix(false);
        }


        private void Timer_Tick(object sender, EventArgs e)
        {
            label4.Text = Ticker.ToString();
            Ticker++;

            if (Won) timer1.Enabled = false;
        }

        private void ToWin()
        {
            Won = true;

            label7.Text = Ticker.ToString();
            label9.Text = Counter.ToString();

            CurrentGameData.SessionDurationInSeconds = Ticker;
            CurrentGameData.StepCount = Counter;

            Games.Add(CurrentGameData);

            panel_GZ.Visible = true;
        }


        private void OpenRecords(object sender, EventArgs e)
        {
            panel_Score.Visible = !panel_Score.Visible;
            dateTimePicker1.Value = DateTime.Now;

            DisplayTop(null, null);
        }

        private void CloseRecords(object sender, EventArgs e)
        {
            panel_Score.Visible = false;
        }

        private void DisplayTop(object sender, EventArgs e)
        {
            if (radioButton1.Checked)
                Games.Sort((a, b) => a.SessionDurationInSeconds.CompareTo(b.SessionDurationInSeconds));
            else if (radioButton2.Checked)
                Games.Sort((a, b) => a.StepCount.CompareTo(b.StepCount));
            else if (radioButton3.Checked)
            {
                Games.Sort((a, b) => a.StartTime.CompareTo(b.StartTime));
                Games.Reverse();
            }

            scoreTable.Rows.Clear();

            for (int i = 0; i < Games.Count && i < 10; ++i)
            {
                scoreTable.Rows.Add(i + 1, Games[i].PlayerName, Games[i].StartTime, Games[i].SessionDurationInSeconds, Games[i].StepCount);
            }

        }

        private void ClearRecordsByTime(object sender, EventArgs e)
        {
            Games.RemoveAll((x) => DateTime.Compare(x.StartTime, dateTimePicker1.Value) > 0 ? true : false);

            DisplayTop(null, null);
        }


        private void DeserializeGames()
        {
            BinaryFormatter formatter = new BinaryFormatter();

            using (FileStream fs = new FileStream(SavePath, FileMode.OpenOrCreate))
            {
                if (fs.Length != 0)
                    Games = (List<GameData>)formatter.Deserialize(fs);
                else
                    Games = new List<GameData>();
            }
        }

        private void SerializeGames(object sender, FormClosingEventArgs e)
        {
            BinaryFormatter formatter = new BinaryFormatter();

            using (FileStream fs = new FileStream(SavePath, FileMode.OpenOrCreate))
            {
                formatter.Serialize(fs, Games);
            }
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