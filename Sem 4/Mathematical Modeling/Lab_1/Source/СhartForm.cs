using System.Linq;
using System.Windows.Forms;

namespace Lab_1
{
    public partial class СhartForm : Form
    {
        public СhartForm(double[] arr_1, double[] arr_2)
        {
            InitializeComponent();

            for (int i = 0; i < 10; i++)
            {
                Chart_1.Series[0].Points.AddY(arr_1.Count(x => x < (i + 1.0) / 10 && x >= (double)i / 10));
                Chart_2.Series[0].Points.AddY(arr_2.Count(x => x < (i + 1.0) / 10 && x >= (double)i / 10));
            }
        }
    }
}
