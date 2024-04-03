using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace projetDGM
{
    public partial class tchelo : Form
    {
        public tchelo()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            double num1, num2, num3;
            num1 = Convert.ToDouble(textBox1.Text);
            num2 = Convert.ToDouble(textBox2.Text);

            num3 = num1 * 100 / num2;
            textBox3.Text = Convert.ToString(Math.Round(num3, 0));
        }
    }
}
