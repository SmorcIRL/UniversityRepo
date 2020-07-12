using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;

namespace Events
{
    public partial class MainForm : Form
    {
        #region Поля

        private Calculator calculator;
        private Demonstrator demonstrator;

        private Thread
            CalculatorThread,
            DemonstratorThread;

        private int
            Radius,
            Diameter;

        private const int
            RadiusMin = 50,
            RadiusMax = 170;

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

        private bool StartPanelIsActive => panel_StartPanel.Visible;

        #endregion


        #region Методы

        public void Shoot()
        {
            Random random_values = new Random();

            Image.FillEllipse(RedBrush, random_values.Next(Field.Width), random_values.Next(Field.Height), 7, 7);

            Field.Image = Bitmap;
        }

        private int Clamp(int value, int min, int max)
        {
            if (value >= min && value <= max)
                return value;
            else if (value < min)
                return min;
            else
                return max;
        }

        #endregion


        #region Форма

        public MainForm()
        {
            InitializeComponent();
        }

        private void button_Start_Click(object sender, EventArgs e)
        {
            if (!int.TryParse(maskedTextBox_DeltaCInput.Text, out int deltaC) ||
                !int.TryParse(maskedTextBox_DeltaDInput.Text, out int deltaD) ||
                demonstrator != null || calculator != null)
            {
                maskedTextBox_DeltaCInput.ResetText();
                maskedTextBox_DeltaDInput.ResetText();
                return;
            }

            Radius = Clamp(int.Parse(maskedTextBox_RadiusInput.Text), RadiusMin, RadiusMax);

            Diameter = Radius * 2;


            Bitmap = new Bitmap(Field.Width, Field.Height);
            Image = Graphics.FromImage(Bitmap);

            CentralPoint = new Point(Field.Width / 2, Field.Height / 2);

            Image.FillPie(GrayBrush, CentralPoint.X - Radius, CentralPoint.Y - Radius, Diameter, Diameter, 0, 90);
            Image.FillRectangle(GrayBrush, CentralPoint.X - Radius, CentralPoint.Y - Radius, Radius, Radius);
            Image.FillPie(WhiteBrush, CentralPoint.X - Diameter, CentralPoint.Y - Diameter, Diameter, Diameter, 0, 90);

            Image.DrawLine(BlackPen_3, CentralPoint.X, 0, CentralPoint.X, Field.Height);
            Image.DrawLine(BlackPen_3, 0, CentralPoint.Y, Field.Width, CentralPoint.Y);

            Field.Image = Bitmap;



            panel_StartPanel.Visible = false;


            calculator = new Calculator(deltaC);
            CalculatorThread = new Thread(calculator.Calculate);

            demonstrator = new Demonstrator(this, deltaD);
            DemonstratorThread = new Thread(demonstrator.Shoot);


            calculator.CalculationCompleted += demonstrator.Demonstrate;
            demonstrator.StopCalculation += calculator.StopCalculation;

            DemonstratorThread.Start();
            CalculatorThread.Start();
        }

        private void button_StopCalculating_Click(object sender, EventArgs e)
        {
            if (StartPanelIsActive || !calculator.ShouldICalculate) return;

            demonstrator.StopCalculate();
        }

        private void button_StopShooting_Click(object sender, EventArgs e)
        {
            if (StartPanelIsActive || !demonstrator.ShouldIShoot) return;

            if (calculator.ShouldICalculate)
                demonstrator.StopCalculate();

            demonstrator.StopShoot();
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            button_StopCalculating_Click(null, null);
            button_StopShooting_Click(null, null);
        }

        #endregion


        public class Calculator
        {
            public int TimeToSleep { get; private set; }
            public bool ShouldICalculate { get; private set; } = true;


            public Calculator(int sleepDelta)
            {
                TimeToSleep = sleepDelta;
            }

            public void Calculate()
            {
                while (ShouldICalculate)
                {
                    int A = 0, B = 0, Count = 0;

                    while (!(A < B))
                    {
                        Random random = new Random();
                        A = random.Next(2, 10000);
                        B = random.Next(A, 10000);
                    }

                    for (int i = A; i <= B; ++i)
                    {
                        if (!ShouldICalculate) return;

                        if (IsSphenic(i)) ++Count;
                    }


                    CalculationCompleted?.Invoke(A, B, Count);

                    if (!ShouldICalculate) return;
                    Thread.Sleep(TimeToSleep);
                }
            }

            public void StopCalculation() => ShouldICalculate = false;

            public static bool IsSphenic(int number)
            {
                if (number < 30) return false;

                List<int> factors = new List<int>();

                for (int i = 2; number > 1; ++i)
                {
                    var count = 0;

                    while (number % i == 0)
                    {
                        count++;
                        number = number / i;
                    }

                    if (count == 1) factors.Add(i);

                    if (factors.Count == 3) break;
                }

                return factors.Count == 3;
            }


            public event Action<int, int, int> CalculationCompleted;
        }


        public class Demonstrator
        {
            private MainForm OwnerForm;
            public int TimeToSleep { get; private set; }
            public bool ShouldIShoot { get; private set; } = true;

            public Demonstrator(MainForm ownerForm, int sleepDelta)
            {
                OwnerForm = ownerForm;
                TimeToSleep = sleepDelta;
            }

            public void Demonstrate(int A, int B, int Count)
            {
                if (OwnerForm.IsHandleCreated)
                    OwnerForm.Invoke(new Action(() => OwnerForm.dataGridView_Results.Rows.Add(A, B, Count)));

            }
            public void Shoot()
            {
                while (ShouldIShoot)
                {
                    if (OwnerForm.IsHandleCreated) OwnerForm.Invoke(new Action(() => OwnerForm.Shoot()));

                    Thread.Sleep(TimeToSleep);
                }
            }

            public void StopCalculate() => StopCalculation?.Invoke();
            public void StopShoot() => ShouldIShoot = false;

            public event Action StopCalculation;
        }
    }
}
