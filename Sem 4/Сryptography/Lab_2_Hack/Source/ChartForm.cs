using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lab_3
{
    public partial class ChartForm : Form
    {
        public ChartForm(double[] arr)
        {
            InitializeComponent();

            chart1.ChartAreas[0].AxisX.Maximum = arr.Count();

            for (int i = 0; i < arr.Length; i++)
            {
                chart1.Series[0].Points.AddXY(i, arr[i]);
            }
        }
    }
}
