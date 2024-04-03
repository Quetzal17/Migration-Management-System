using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace projetDGM
{
    public partial class progress : Form
    {
        [DllImport("Gdi32.dll", EntryPoint = "CreateRoundRectRgn")]
        private static extern IntPtr RoundCorner(
            int leftRect,
            int topRect,
            int rightRect,
            int bottomRect,
            int widthEllipse,
            int heightEllipse

            );
        public progress()
        {
            InitializeComponent();
            Region = System.Drawing.Region.FromHrgn(RoundCorner(0, 0, Width, Height, 7, 7));
        }

        int counter = 0;
        int len = 0;
        string text;

        private void progress_Load(object sender, EventArgs e)
        {
            text = label1.Text;

            len = text.Length;
            label1.Text = "";
            timer1.Start();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            label1.Text = text.Substring(0, counter);
            ++counter;
            

            if(counter > len)
            {
   
                timer1.Stop();
                timer2.Start();
                //login lg = new login();
                //lg.ShowDialog();
                //this.Dispose();
            }
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            gunaProgressBar1.Increment(1);
            gunaLabel3.Text = gunaProgressBar1.ProgressPercentText;

            if (gunaProgressBar1.Value == 100)
            {
                timer2.Stop();
                this.Hide();
                login lg = new login();
                lg.ShowDialog();
            }
            
        }
    }
}
