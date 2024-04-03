using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using AForge.Video;
using AForge.Video.DirectShow;
using Microsoft.Reporting.WinForms;
using System.IO;
using AForge.Video;
using AForge.Video.DirectShow;
using ZXing;
using ZXing.Aztec;
using MySql.Data.MySqlClient;


namespace projetDGM
{
    public partial class QRcode : Form
    {
        public static string qr;
        public QRcode()
        {
            InitializeComponent();
        }
        VideoCaptureDevice frame;
        FilterInfoCollection Devices;

        void Start_cam()
        {
            Devices = new FilterInfoCollection(FilterCategory.VideoInputDevice);
            frame = new VideoCaptureDevice(Devices[1].MonikerString);
            frame.NewFrame += new AForge.Video.NewFrameEventHandler(NewFrame_event);
            frame.Start();
        }

        void NewFrame_event(object send, NewFrameEventArgs e)
        {
            try
            {
                gunaPictureBox3.Image = (Image)e.Frame.Clone();
            }
            catch (Exception ex)
            {

            }
        }
        private void QRcode_Load(object sender, EventArgs e)
        {
          

        }

        private void gunaAdvenceButton7_Click(object sender, EventArgs e)
        {
            Start_cam();
            timer1.Start();
        }

        private void CaptureDevice_NewFrame(object sender, NewFrameEventArgs eventArgs)
        {
            gunaPictureBox3.Image = (Bitmap)eventArgs.Frame.Clone();
        }

        private void QRcode_FormClosing(object sender, FormClosingEventArgs e)
        {
          
          //      frame.Stop();
                this.Dispose();
         
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if(gunaPictureBox3.Image != null)
            {
                try
                {
                    BarcodeReader barcodeReader = new BarcodeReader();
                    Result result = barcodeReader.Decode((Bitmap)gunaPictureBox3.Image);
                    if (result != null)
                    {
                        gunaTextBox9.Text = result.ToString();
                        timer1.Stop();
                        qr = gunaTextBox9.Text;

                        if (frame.IsRunning)
                        {
                            frame.Stop();
                        }
                        this.Hide();

                        PassengerRecord pr = new PassengerRecord();
                        pr.ShowDialog();

                    }
                }
                catch (Exception)
                {

                
                }
            }
        }

        private void gunaAdvenceButton1_Click(object sender, EventArgs e)
        {
            try
            {
                frame.Stop();
                this.Dispose();
            }
            catch (Exception)
            {

            }
        }
    }
}
