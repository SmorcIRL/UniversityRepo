using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace Lab_5
{
    public partial class ChartForm : Form
    {
        public ChartForm(double[] arr)
        {
            InitializeComponent();

            Chart_1.Series[0].ChartType = SeriesChartType.Line;
            Chart_1.Series[0].BorderWidth = 2;
            Chart_1.ChartAreas[0].AxisX.Minimum = 0;

            for (int i = 0; i < arr.Length; i++)
            {
                Chart_1.Series[0].Points.AddXY(100 * i + 1, arr[i]);
            }
        }
    }
}