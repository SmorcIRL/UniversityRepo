using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace Lab_3
{
    public partial class ChartForm : Form
    {
        public ChartForm(double[] w_arr, int[] iter_arr)
        {
            InitializeComponent();

            Chart_1.ChartAreas[0].AxisX.Minimum = 0;
            Chart_1.ChartAreas[0].AxisY.Maximum = 500;

            for (int i = 0; i < w_arr.Length; i++)
            {
                Chart_1.Series[0].Points.AddXY(w_arr[i], iter_arr[i]);
            }
        }

        public ChartForm(Dictionary<double, List<Tuple<int, double>>> data)
        {
            InitializeComponent();

            Chart_1.ChartAreas[0].AxisX.Minimum = 0;
            Chart_1.ChartAreas[0].AxisX.Maximum = 90;
            Chart_1.ChartAreas[0].AxisY.Maximum = 0.0001;

            int line_index = 0;

            foreach (var key in data.Keys)
            {
                for (int i = 0; i < data[key].Count; i++)
                {
                    Chart_1.Series[line_index].Points.AddXY(data[key][i].Item1, data[key][i].Item2);
                }

                line_index++;
            }
        }

        public ChartForm(double[] ms_arr)
        {
            InitializeComponent();

            Chart_1.ChartAreas[0].AxisX.Minimum = 0;

            Chart_1.Series[0].ChartType = SeriesChartType.Column;
            Chart_1.Series[0].BorderWidth = 40;

            for (int i = 0; i < 6; i++)
            {
                Chart_1.Series[0].Points.AddXY(i + 1, ms_arr[i]);
            }
        }
    }
}
