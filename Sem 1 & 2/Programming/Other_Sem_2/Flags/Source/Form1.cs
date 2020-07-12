using System;
using System.Drawing;
using System.Windows.Forms;

namespace Flags
{
    public partial class Form1 : Form
    {
        #region Поля

        private bool
            CurrentFlag = true;

        private readonly double
            FirstFlagProportion = 0.6d,
            SecondFlagProportion = 0.5d,

            FirstWhiteLineWidth = 0.3125d,
            FirstBlackLineDelta = 0.055d,

            SecondTriangleTop = 0.43d,
            SecondLineWidth = 0.3d,
            SecondRedLineDelta = 0.052d,

            SecondStarPoint_1_PropX = 0.12d,
            SecondStarPoint_1_PropY = 0.35d,

            SecondStarPoint_2_PropX = 0.15365d,
            SecondStarPoint_2_PropY = 0.443d,

            SecondStarPoint_3_PropX = 0.208d,
            SecondStarPoint_3_PropY = 0.40625d,

            SecondStarPoint_4_PropX = 0.1745d,
            SecondStarPoint_4_PropY = 0.5d,

            SecondStarPoint_5_PropX = 0.208d,
            SecondStarPoint_5_PropY = 0.594d,

            SecondStarPoint_6_PropX = 0.15365d,
            SecondStarPoint_6_PropY = 0.557d,

            SecondStarPoint_7_PropX = 0.12d,
            SecondStarPoint_7_PropY = 0.646d,

            SecondStarPoint_8_PropX = 0.12d,
            SecondStarPoint_8_PropY = 0.5365d,

            SecondStarPoint_9_PropX = 0.065d,
            SecondStarPoint_9_PropY = 0.5d,

            SecondStarPoint_10_PropX = 0.12d,
            SecondStarPoint_10_PropY = 0.4635d;

        #endregion

        private readonly Brush
            WhiteBrush = new SolidBrush(Color.White),
            BlackBrush = new SolidBrush(Color.Black),
            GreenBrush = new SolidBrush(Color.Green),
            YellowBrush = new SolidBrush(Color.Yellow),
            FirstBackgroundBrush = new SolidBrush(Color.FromArgb(218, 25, 53)),
            SecondTriangleWhiteBrush = new SolidBrush(Color.FromArgb(15, 71, 175));


        private Bitmap Bitmap;
        private Graphics Image;

        public Form1()
        {
            InitializeComponent();

            DrawFlag();
        }

        private void Form1_Resize(object sender, EventArgs e)
        {
            DrawFlag();
        }

        private void DrawFlag()
        {
            double
                FormProportion = (double)Height / Width;

            if (CurrentFlag)
            {
                if (FormProportion > FirstFlagProportion)
                    Bitmap = new Bitmap(Width, (int)(Width * FirstFlagProportion));
                else
                    Bitmap = new Bitmap((int)(Height / FirstFlagProportion), Height);

                Console.WriteLine(FormProportion);
                Console.WriteLine(Width + "/" + Height + "  " + Bitmap.Width + "/" + Bitmap.Height);
                Console.WriteLine();
                Console.WriteLine();

                Point[] WhiteLine =
                {
                    new Point(0, 0),
                    new Point(Bitmap.Width - (int)(FirstWhiteLineWidth * Bitmap.Width), Bitmap.Height),
                    new Point(Bitmap.Width, Bitmap.Height),
                    new Point((int)(FirstWhiteLineWidth * Bitmap.Width), 0)
                };
                Point[] BlackLine =
                {
                    new Point((int)(FirstBlackLineDelta * Bitmap.Width), 0),
                    new Point(Bitmap.Width - (int)(FirstWhiteLineWidth * Bitmap.Width) + (int)(FirstBlackLineDelta * Bitmap.Width), Bitmap.Height),
                    new Point(Bitmap.Width - (int)(FirstBlackLineDelta * Bitmap.Width), Bitmap.Height),
                    new Point((int)((FirstWhiteLineWidth - FirstBlackLineDelta) * Bitmap.Width) , 0)
                };

                Image = Graphics.FromImage(Bitmap);
                Image.FillRectangle(FirstBackgroundBrush, 0, 0, Bitmap.Width, Bitmap.Height);
                Image.FillPolygon(WhiteBrush, WhiteLine);
                Image.FillPolygon(BlackBrush, BlackLine);
            }

            else
            {
                if (FormProportion > SecondFlagProportion)
                    Bitmap = new Bitmap(Width, (int)(Width * SecondFlagProportion));
                else
                    Bitmap = new Bitmap((int)(Height / SecondFlagProportion), Height);

                Point[] BlackLine =
                {
                    new Point(0,0),
                    new Point(0, (int)(SecondLineWidth * Bitmap.Height)),
                    new Point(Bitmap.Width, (int)(SecondLineWidth * Bitmap.Height)),
                    new Point(Bitmap.Width, 0)
                };
                Point[] RedLine =
                {
                    new Point(0,(int)((SecondLineWidth + SecondRedLineDelta) * Bitmap.Height)),
                    new Point(0,(int)((1 - SecondLineWidth - SecondRedLineDelta) * Bitmap.Height)),
                    new Point(Bitmap.Width,(int)((1 - SecondLineWidth - SecondRedLineDelta) * Bitmap.Height)),
                    new Point(Bitmap.Width, (int)((SecondLineWidth + SecondRedLineDelta) * Bitmap.Height))
                };
                Point[] GreenLine =
                {
                    new Point(0, Bitmap.Height - (int)(SecondLineWidth * Bitmap.Height)),
                    new Point(0,Bitmap.Height),
                    new Point(Bitmap.Width,Bitmap.Height),
                    new Point(Bitmap.Width, Bitmap.Height - (int)(SecondLineWidth * Bitmap.Height))
                };
                Point[] Triangle =
                {
                    new Point(0, 0),
                    new Point((int)(SecondTriangleTop * Bitmap.Width), Bitmap.Height/2),
                    new Point(0, Bitmap.Height),
                };
                Point[] Star =
                {
                    new Point((int)(SecondStarPoint_1_PropX * Bitmap.Width), (int)(SecondStarPoint_1_PropY * Bitmap.Height)),
                    new Point((int)(SecondStarPoint_2_PropX * Bitmap.Width), (int)(SecondStarPoint_2_PropY * Bitmap.Height)),
                    new Point((int)(SecondStarPoint_3_PropX * Bitmap.Width), (int)(SecondStarPoint_3_PropY * Bitmap.Height)),
                    new Point((int)(SecondStarPoint_4_PropX * Bitmap.Width), (int)(SecondStarPoint_4_PropY * Bitmap.Height)),
                    new Point((int)(SecondStarPoint_5_PropX * Bitmap.Width), (int)(SecondStarPoint_5_PropY * Bitmap.Height)),
                    new Point((int)(SecondStarPoint_6_PropX * Bitmap.Width), (int)(SecondStarPoint_6_PropY * Bitmap.Height)),
                    new Point((int)(SecondStarPoint_7_PropX * Bitmap.Width), (int)(SecondStarPoint_7_PropY * Bitmap.Height)),
                    new Point((int)(SecondStarPoint_8_PropX * Bitmap.Width), (int)(SecondStarPoint_8_PropY * Bitmap.Height)),
                    new Point((int)(SecondStarPoint_9_PropX * Bitmap.Width), (int)(SecondStarPoint_9_PropY * Bitmap.Height)),
                    new Point((int)(SecondStarPoint_10_PropX * Bitmap.Width), (int)(SecondStarPoint_10_PropY * Bitmap.Height))
                };

                Image = Graphics.FromImage(Bitmap);
                Image.FillRectangle(WhiteBrush, 0, 0, Bitmap.Width, Bitmap.Height);
                Image.FillPolygon(BlackBrush, BlackLine);
                Image.FillPolygon(FirstBackgroundBrush, RedLine);
                Image.FillPolygon(GreenBrush, GreenLine);
                Image.FillPolygon(SecondTriangleWhiteBrush, Triangle);
                Image.FillPolygon(YellowBrush, Star);
            }

            picture.Image = Bitmap;
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Space)
            {
                CurrentFlag = !CurrentFlag;
                DrawFlag();
            }
        }
    }
}
