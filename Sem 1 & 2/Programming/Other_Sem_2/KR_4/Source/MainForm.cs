using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace KR_4
{
    public partial class MainForm : Form
    {
        private const string
            file_fail = "Input file not found.",
            file_success = "Input file found.";

        private Bitmap Bitmap;

        private Graphics Image;

        private readonly Brush
            WhiteBrush = new SolidBrush(Color.White),
            GrayBrush = new SolidBrush(Color.Gray),
            RedBrush = new SolidBrush(Color.Red);

        private readonly Pen
            GrayPen_2 = new Pen(Color.Gray, 2),
            BlackPen_3 = new Pen(Color.Black, 3);

        private Random random = new Random();


        public MainForm()
        {
            InitializeComponent();

            DrawBackground();

            if (FindFile()) ProccessFile();
        }

        public void DrawBackground()
        {
            Bitmap = new Bitmap(Field.Width, Field.Height);
            Image = Graphics.FromImage(Bitmap);

            Image.DrawLine(BlackPen_3, Field.Width / 2, 0, Field.Width / 2, Field.Height);
            Image.DrawLine(BlackPen_3, 0, Field.Height / 2, Field.Width, Field.Height / 2);

            float y = Field.Height / 2;
            float x = 500 / y;

            for (float i = x; i < 1000; i += 0.001f)
            {
                //if (y > Field.Height / 2 || x > Field.Width / 2) break;

                x = i;
                y = 500 / x;

                Image.FillRectangle(RedBrush, Field.Width / 2 + x + 50, Field.Height / 2 - y - 50, 2, 2);
            }

            Field.Image = Bitmap;
        }

        public bool FindFile()
        {
            if (File.Exists("Files/INPUT.TXT"))
            {
                label_status.Text = file_success;
                label_status.ForeColor = Color.Green;

                return true;
            }

            else
            {
                label_status.Text = file_fail;
                label_status.ForeColor = Color.Red;

                return false;
            }
        }

        private void ProccessFile()
        {
            List<Point2D> points = new List<Point2D>();

            using (StreamReader Input = new StreamReader("Files/INPUT.TXT"))
            {
                while (!Input.EndOfStream)
                {
                    string InputString = Input.ReadLine();
                    if (InputString == string.Empty) break;

                    var StrArray = InputString.Split(' ');

                    points.Add(new Point2D(int.Parse(StrArray[0]), int.Parse(StrArray[1])));
                }
            }

            points.Sort();
            points.Reverse();

            using (StreamWriter Output = new StreamWriter("Files/OUTPUT.TXT"))
            {
                var Rows = pointTable.Rows;

                foreach (var point in points)
                {
                    Rows.Add(point.Module, point.X, point.Y);
                    DrawPoint2D(point);
                    Output.WriteLine(point);
                }
            }

            label_fourth_1.Text += Point2D.CounterFourth_1;
            label_fourth_2.Text += Point2D.CounterFourth_2;
            label_fourth_3.Text += Point2D.CounterFourth_3;
            label_fourth_4.Text += Point2D.CounterFourth_4;

            label_axis_x.Text += Point2D.CounterAxis_X;
            label_axis_y.Text += Point2D.CounterAxis_Y;
        }

        public void DrawPoint2D(Point2D point)
        {
            Image.FillEllipse(new SolidBrush(Color.FromArgb(random.Next(256), random.Next(256), random.Next(256))),
                Field.Width / 2 + point.X / 4 - 3.5f, Field.Height / 2 + point.Y / 4 - 3.5f, 7, 7);

            Field.Image = Bitmap;
        }


        public class Point2D : IComparable<Point2D>
        {
            public int X { get; private set; }
            public int Y { get; private set; }
            public double Module { get; private set; }

            public bool OnAxis_X => X == 0;
            public bool OnAxis_Y => Y == 0;

            public static int
                CounterFourth_1,
                CounterFourth_2,
                CounterFourth_3,
                CounterFourth_4,
                CounterAxis_X,
                CounterAxis_Y;

            public Point2D(int x, int y)
            {
                X = x;
                Y = y;
                Module = Math.Sqrt(Math.Pow(X, 2) + Math.Pow(Y, 2));

                if (OnAxis_X) ++CounterAxis_X;
                if (OnAxis_Y) ++CounterAxis_Y;

                if (!OnAxis_X && !OnAxis_Y)
                {
                    if (X > 0 && Y > 0) ++CounterFourth_1;
                    else if (X < 0 && Y > 0) ++CounterFourth_2;
                    else if (X < 0 && Y < 0) ++CounterFourth_3;
                    else if (X > 0 && Y < 0) ++CounterFourth_4;
                }
            }

            public override string ToString()
            {
                return "X: " + X + "  Y: " + Y + "  Module: " + Module;
            }

            public int CompareTo(Point2D other)
            {
                return Module.CompareTo(other.Module);
            }

            ~Point2D()
            {
                if (OnAxis_X) --CounterAxis_X;
                if (OnAxis_Y) --CounterAxis_Y;

                if (!OnAxis_X && !OnAxis_Y)
                {
                    if (X > 0 && Y > 0) --CounterFourth_1;
                    else if (X < 0 && Y > 0) --CounterFourth_2;
                    else if (X < 0 && Y < 0) --CounterFourth_3;
                    else if (X > 0 && Y < 0) --CounterFourth_4;
                }
            }
        }
    }
}