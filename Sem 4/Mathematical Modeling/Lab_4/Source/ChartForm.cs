using System.Windows.Forms;

namespace Lab_4
{
    public partial class ChartForm : Form
    {
        public ChartForm(double[] arr_1, double[] arr_2)
        {
            InitializeComponent();

            int max_percent = 100;
            Chart_1.ChartAreas[0].AxisY.Maximum = max_percent;
            Chart_2.ChartAreas[0].AxisY.Maximum = max_percent;

            for (int i = 0; i < arr_1.Length; i++)
            {
                Chart_1.Series[0].Points.AddXY(i + 1, arr_1[i]);
            }
            for (int i = 0; i < arr_2.Length; i++)
            {
                Chart_2.Series[0].Points.AddXY(i + 1, arr_2[i]);
            }
        }
    }
}
