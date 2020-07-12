using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace Shooting
{
    public partial class Form1 : Form
    {
        #region Fields

        private int
            ShotsNumber,
            ShotsMade = 0,
            CorrectShots = 0,
            Radius,
            Diameter;

        private const int
            ShotsMin = 1,
            ShotsMax = 100,
            RadiusMin = 50,
            RadiusMax = 500;

        private Bitmap
            Bitmap;

        private Graphics
            Image;

        private readonly Brush
            WhiteBrush = new SolidBrush(Color.White),
            GrayBrush = new SolidBrush(Color.Gray),
            RedBrush = new SolidBrush(Color.Red);

        private readonly Pen
            GrayPen_2 = new Pen(Color.Gray, 2),
            BlackPen_3 = new Pen(Color.Black, 3);

        private
            Point CentralPoint;


        private List<Point>
            Points = new List<Point>();

        #endregion


        public Form1()
        {
            InitializeComponent();

            FormBorderStyle = FormBorderStyle.FixedSingle;

            panel3.Visible = false;
        }


        private void Form1_Resize(object sender, EventArgs e)
        {
            DrawBackground();
        }


        #region Main logic

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            if (panel2.Visible == true) return;

            Shoot(new Point((MousePosition.X - Location.X - 12) - CentralPoint.X, (MousePosition.Y - Location.Y - 33) - CentralPoint.Y));
        }

        private void Shoot(Point point)
        {
            Points.Add(point);

            Image.FillEllipse(RedBrush, CentralPoint.X + point.X, CentralPoint.Y + point.Y, 7, 7);
            pictureBox1.Image = Bitmap;

            if (point.X > 0 && point.Y > 0)
            {
                if (Math.Sqrt(point.X * point.X + point.Y * point.Y) <= Radius - 3)
                    CorrectShots++;
            }

            else if (point.X < 0 && point.Y < 0)
            {
                if (point.X > -Radius && point.Y > -Radius)
                    if (Math.Sqrt(Math.Pow(Radius + point.X, 2) + Math.Pow(Radius + point.Y, 2)) > Radius - 5)
                        CorrectShots++;
            }

            ShotsMade++;

            label3.Text = CorrectShots.ToString();
            label5.Text = (ShotsMade - CorrectShots).ToString();

            if (ShotsMade == ShotsNumber)
            {
                ShotsMade = CorrectShots = 0;

                panel2.Visible = true;
                panel3.Visible = false;
            }
        }

        private void DrawBackground()
        {
            Bitmap = new Bitmap(Width, Height);
            Image = Graphics.FromImage(Bitmap);

            CentralPoint = new Point(Width / 2, Height / 2);

            Image.FillPie(GrayBrush, CentralPoint.X - Radius, CentralPoint.Y - Radius, Diameter, Diameter, 0, 90);
            Image.FillRectangle(GrayBrush, CentralPoint.X - Radius, CentralPoint.Y - Radius, Radius, Radius);
            Image.FillPie(WhiteBrush, CentralPoint.X - Diameter, CentralPoint.Y - Diameter, Diameter, Diameter, 0, 90);


            Image.DrawLine(BlackPen_3, CentralPoint.X, 0, CentralPoint.X, Height);
            Image.DrawLine(BlackPen_3, 0, CentralPoint.Y, Width, CentralPoint.Y);

            foreach (var point in Points)
                Image.FillEllipse(RedBrush, CentralPoint.X + point.X, CentralPoint.Y + point.Y, 7, 7);


            pictureBox1.Image = Bitmap;
        }

        #endregion


        #region Menu panel

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != Convert.ToChar(8))
            {
                e.Handled = true;
            }
        }

        private void textBox1_Validated(object sender, EventArgs e)
        {
            int Buffer;
            int.TryParse(textBox1.Text, out Buffer);
            textBox1.Text = ((Buffer < ShotsMin) ? ShotsMin : (Buffer > ShotsMax) ? ShotsMax : Buffer).ToString();
        }


        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != Convert.ToChar(8))
            {
                e.Handled = true;
            }
        }

        private void textBox2_Validated(object sender, EventArgs e)
        {
            int Buffer;
            int.TryParse(textBox2.Text, out Buffer);
            textBox2.Text = ((Buffer < RadiusMin) ? RadiusMin : (Buffer > RadiusMax) ? RadiusMax : Buffer).ToString();
        }


        private void button1_Click(object sender, EventArgs e)
        {
            ShotsNumber = int.Parse(textBox1.Text);
            Radius = int.Parse(textBox2.Text);
            Diameter = Radius * 2;

            Points.Clear();


            DrawBackground();

            panel2.Visible = false;
            label3.Text = label5.Text = "0";

            textBox3.Text = "0";
            textBox4.Text = "0";
            panel3.Visible = true;

            FormBorderStyle = FormBorderStyle.Sizable;
        }

        #endregion


        #region Shoot panel

        private void textBox3_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != Convert.ToChar(8) && e.KeyChar != '-')
            {
                e.Handled = true;
            }
        }

        private void textBox3_Validated(object sender, EventArgs e)
        {
            int
                Buffer;

            int.TryParse(textBox3.Text, out Buffer);

            textBox3.Text = ((Buffer < -CentralPoint.X) ? -CentralPoint.X : (Buffer > CentralPoint.X) ? CentralPoint.X : Buffer).ToString();
        }


        private void textBox4_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != Convert.ToChar(8) && e.KeyChar != '-')
            {
                e.Handled = true;
            }
        }

        private void textBox4_Validated(object sender, EventArgs e)
        {
            int
                Buffer;

            int.TryParse(textBox4.Text, out Buffer);

            textBox4.Text = ((Buffer < -CentralPoint.Y) ? -CentralPoint.Y : (Buffer > CentralPoint.Y) ? CentralPoint.Y : Buffer).ToString();
        }


        private void button2_Click(object sender, EventArgs e)
        {
            int X, Y;

            if (textBox3.Text[0] == '-')
                int.TryParse(textBox3.Text.Substring(1, textBox3.Text.Length - 1), out X);
            else
                int.TryParse(textBox3.Text, out X);

            if (textBox4.Text[0] == '-')
                int.TryParse(textBox4.Text.Substring(1, textBox4.Text.Length - 1), out Y);
            else
                int.TryParse(textBox4.Text, out Y);

            if (textBox3.Text[0] == '-') X = -X;
            if (textBox4.Text[0] != '-') Y = -Y;

            Shoot(new Point(X - 3, Y - 3));
        }

        #endregion
    }
}
