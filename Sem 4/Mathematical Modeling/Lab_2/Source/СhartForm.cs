using System;
using System.Linq;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace Lab_2
{
    public partial class СhartForm : Form
    {
        public СhartForm(int n, double[] arr_1, double p, double[] arr_2, double lambda)
        {
            InitializeComponent();

            #region Chart_2_1

            Chart_2_1.Series[0].Points.AddXY(0, arr_1.Count(x => x == 0));
            Chart_2_1.Series[0].Points.AddXY(1, arr_1.Count(x => x == 1));

            Chart_2_1.Series[1].Points.AddXY(0, (1 - p) * n);
            Chart_2_1.Series[1].Points.AddXY(1, p * n);

            #endregion


            #region Chart_2_2

            double
                e = Math.Exp(-lambda),
                fact = 1;

            for (int i = 0; i < 10; i++, fact *= i)
            {
                Chart_2_2.Series[0].Points.AddXY(i, arr_2.Count(x => (int)Math.Round(x, MidpointRounding.AwayFromZero) == i));
                Chart_2_2.Series[1].Points.AddXY(i, ((e * Math.Pow(lambda, i)) / fact) * n);
            }

            #endregion


            #region Chart_3_1

            Chart_3_1.ChartAreas[0].AxisY.Minimum = 0;
            Chart_3_1.ChartAreas[0].AxisY.Maximum = 2;

            Chart_3_1.ChartAreas[0].AxisX.Minimum = -1;
            Chart_3_1.ChartAreas[0].AxisX.Maximum = 2;


            Chart_3_1.Series[0].ChartType = SeriesChartType.Line;
            Chart_3_1.Series[0].BorderWidth = 3;

            Chart_3_1.Series[0].Points.AddXY(-1, 0);
            Chart_3_1.Series[0].Points.AddXY(0, 0);
            Chart_3_1.Series[0].Points.AddXY(0, (double)arr_1.Count(x => x == 0) / n);
            Chart_3_1.Series[0].Points.AddXY(1, (double)arr_1.Count(x => x == 0) / n);
            Chart_3_1.Series[0].Points.AddXY(1, 1);
            Chart_3_1.Series[0].Points.AddXY(2, 1);

            #endregion


            #region Chart_3_2

            Chart_3_2.Series[0].ChartType = SeriesChartType.Line;
            Chart_3_2.Series[0].BorderWidth = 3;

            for (int i = -2; i < 11; i++)
                Chart_3_2.Series[0].Points.AddXY(i, (double)arr_2.Count(x => x <= i) / n);

            #endregion
        }
    }
}