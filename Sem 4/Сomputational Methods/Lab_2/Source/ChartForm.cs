using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace Lab_2
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
                Chart_1.Series[0].Points.AddXY(5 * (i + 1), arr[i]);
            }
        }
    }
}
