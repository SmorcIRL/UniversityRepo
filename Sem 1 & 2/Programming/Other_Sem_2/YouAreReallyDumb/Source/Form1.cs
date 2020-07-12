using System.Drawing;
using System.Windows.Forms;

namespace You_are_really_dumb
{
    public partial class Form1 : Form
    {
        private const int
            Border = 30,
            FormBorder = 15,
            MoveDelta = 30;

        private int
            ButtonWidth, ButtonHeight;

        private const string
            Code = "QWERT";

        private string
            BufferCode;


        public Form1()
        {
            InitializeComponent();

            ButtonWidth = button2.Size.Width;
            ButtonHeight = button2.Size.Height;

            BufferCode = new string(' ', Code.Length);
        }


        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            BufferCode = BufferCode.Remove(0, 1) + (char)e.KeyValue;

            bool CorrectCode = true;

            for (int i = Code.Length - 1; i >= 0; i--)
            {
                if (BufferCode[i] != Code[i])
                {
                    CorrectCode = false;
                    break;
                }
            }

            if (CorrectCode)
            {
                button2_MouseClick(null, null);
            }
        }

        private void button1_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyCode == Keys.Up || e.KeyCode == Keys.Down || e.KeyCode == Keys.Left || e.KeyCode == Keys.Right)
            {
                e.IsInputKey = true;
            }
        }


        private void label1_MouseMove(object sender, MouseEventArgs e)
        {
            MoveButton2(e.X + (sender as Control).Location.X, e.Y + (sender as Control).Location.Y, sender);
        }

        private void button1_MouseMove(object sender, MouseEventArgs e)
        {
            MoveButton2(e.X + (sender as Control).Location.X, e.Y + (sender as Control).Location.Y, sender);
        }

        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {
            MoveButton2(e.X, e.Y, sender);
        }

        private void MoveButton2(int CursorX, int CursorY, object Sender)
        {
            int
                ButtonX = button2.Location.X,
                ButtonY = button2.Location.Y,
                TriggerX = 0, TriggerY = 0;

            if (CursorX >= ButtonX - Border && CursorX <= ButtonX + ButtonWidth + Border &&
                CursorY >= ButtonY - Border && CursorY <= ButtonY + ButtonHeight + Border)
            {
                button2.Location = new Point(ButtonX + MoveDelta, ButtonY + MoveDelta);

                if (CursorX < ButtonX)
                    TriggerX = 1;
                else if (CursorX > ButtonX + ButtonWidth)
                    TriggerX = -1;

                if (CursorY < ButtonY)
                    TriggerY = 1;
                else if (CursorY > ButtonY + ButtonHeight)
                    TriggerY = -1;

                Point pointMoveTo = new Point(ButtonX + MoveDelta * TriggerX, ButtonY + MoveDelta * TriggerY);


                if (!(Sender is Form))
                {
                    button2.Location = pointMoveTo;
                    return;
                }

                Point screenPointMoveTo = PointToScreen(pointMoveTo);

                TriggerX = TriggerY = 0;

                if (screenPointMoveTo.X < Location.X + FormBorder)
                    TriggerX = 1;
                else if (screenPointMoveTo.X + ButtonWidth > Location.X + Width - FormBorder)
                    TriggerX = -1;

                if (screenPointMoveTo.Y < Location.Y + FormBorder + 20)
                    TriggerY = 1;
                else if (screenPointMoveTo.Y + ButtonHeight > Location.Y + Height - FormBorder)
                    TriggerY = -1;


                switch (TriggerX)
                {
                    case 1:
                    {
                        pointMoveTo.X = Width - ButtonWidth - FormBorder - 15;
                        break;
                    }
                    case -1:
                    {
                        pointMoveTo.X = FormBorder - 5;
                        break;
                    }
                }

                switch (TriggerY)
                {
                    case 1:
                    {
                        pointMoveTo.Y = Height - ButtonHeight - FormBorder - 30;
                        break;
                    }
                    case -1:
                    {
                        pointMoveTo.Y = FormBorder - 5;
                        break;
                    }
                }


                button2.Location = pointMoveTo;
            }
        }


        private void button2_MouseClick(object sender, MouseEventArgs e)
        {
            label1.Text = " Congratulations, you're not so dumb ";
        }
    }
}
