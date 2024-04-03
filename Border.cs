using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Speech.Synthesis;
using System.Speech.Recognition;
using System.Speech.AudioFormat;
using System.IO;
using System.Runtime.InteropServices;

namespace projetDGM
{
    public partial class Border : Form
    {
        SpeechRecognitionEngine _r = new SpeechRecognitionEngine();
        SpeechSynthesizer s = new SpeechSynthesizer();
        SpeechRecognitionEngine start = new SpeechRecognitionEngine();

        Random rmd = new Random();
        int RecTimeout = 0;
        DateTime time = DateTime.Now;

        System.Media.SoundPlayer music = new System.Media.SoundPlayer();
        private int blue;

        [DllImport("Gdi32.dll", EntryPoint = "CreateRoundRectRgn")]

        private static extern IntPtr CreateRoundRectRgn
        (
            int nLeftRect,
            int nTopRect,
            int nRightRect,
            int nBottomRect,
            int nWidthEllipse,
            int nHeightEllipse
        );

        public Border()
        {
            InitializeComponent();
        }

        private void Border_Load(object sender, EventArgs e)
        {
            nam.Text = login.Name;
            pst.Text = login.Post;
            prov.Text = login.Province;

            _r.SetInputToDefaultAudioDevice();
            _r.LoadGrammarAsync(new Grammar(new GrammarBuilder(new Choices(File.ReadAllLines("grammar.txt")))));
            _r.SpeechRecognized += new EventHandler<SpeechRecognizedEventArgs>(Default_SpeechRecognized);
            _r.SpeechDetected += new EventHandler<SpeechDetectedEventArgs>(_r_SpeechRecognized);
            _r.RecognizeAsync(RecognizeMode.Multiple);

            start.SetInputToDefaultAudioDevice();
            start.LoadGrammarAsync(new Grammar(new GrammarBuilder(new Choices(File.ReadAllLines("grammar.txt")))));
            start.SpeechRecognized += new EventHandler<SpeechRecognizedEventArgs>(start_SpeechRecognized);

        }

        private void start_SpeechRecognized(object sender, SpeechRecognizedEventArgs e)
        {
            string speech = e.Result.Text;
            if (speech == "Wake up")
            {
                start.RecognizeAsyncCancel();
                s.SpeakAsync("Yes,I am here");
                _r.RecognizeAsync(RecognizeMode.Multiple);
            }

            
            /*
            */
        }

        private void _r_SpeechRecognized(object sender, SpeechDetectedEventArgs e)
        {
            RecTimeout = 0;
        }

        private void Default_SpeechRecognized(object sender, SpeechRecognizedEventArgs e)
        {
            int rum;
            string speech = e.Result.Text;

            if (speech == "Hello")
            {
                s.SelectVoice("Microsoft Zira Desktop");
                //speech.SpeakAsync("r");
                // speech.SpeakAsync("...,Welcome," + nam.Text + "");
                s.SpeakAsync("hi , what can i help");
                //addroom1.BringToFront();
                //addroom1.Visible = true;

            }
            if (speech == "how are you")
            {
                s.SpeakAsync("I am really good");

            }
            if (speech == "what time is it")
            {
                s.SpeakAsync(DateTime.Now.ToString("h mm tt"));
                //customer_registration1.BringToFront();
                //customer_registration1.Visible = true;

            }
            if (speech == "Can I please add rooms")
            {
                s.SpeakAsync("Yes Sir");


            }
            if (speech == "Can I please add a new Customer")
            {
                s.SpeakAsync("Yes, Sir");

            }
            if (speech == "Can I please Check Out")
            {
                s.SpeakAsync("Yes, Sir");


            }
            if (speech == "Can You please open Customer details")
            {
                s.SpeakAsync("Yes, Sir");


            }
            if (speech == "Can i add a new employee")
            {
                s.SpeakAsync("Yes, Sir");


            }
            if (speech == "Stop talking")
            {
                s.SpeakAsyncCancelAll();
                rum = rmd.Next(1, 2);
                if (rum == 1)
                {
                    s.SpeakAsync("Yes, sir");
                }
                if (rum == 2)
                {
                    s.SpeakAsync("I am sorry i will be quiet");
                }


            }
            if (speech == "Stop Listenning")
            {
                s.SpeakAsync("if you need me just ask");
                _r.RecognizeAsyncCancel();
                start.RecognizeAsync(RecognizeMode.Multiple);
            }

            if (speech == "Go to facebook")
            {
                s.SpeakAsync("i've opened it sir");
                System.Diagnostics.Process.Start("https://www.facebook.com/");
                

            }
            if (speech == "Close yandex")
            {
                System.Diagnostics.Process[] close = System.Diagnostics.Process.GetProcessesByName("YANDEX");
                foreach (System.Diagnostics.Process p in close)
                    p.Kill();
                s.SpeakAsync("i've closed it sir");

            }
        }

        private void gunaAdvenceButton1_Click(object sender, EventArgs e)
        {
            this.Hide();
            PassengerDash pd = new PassengerDash();
            pd.ShowDialog();
        }

        private void gunaTileButton1_Click(object sender, EventArgs e)
        {
            Travelers tv = new Travelers();
            tv.ShowDialog();
        }

        private void gunaTileButton3_Click(object sender, EventArgs e)
        {
            Attendance att = new Attendance();
            att.ShowDialog();
        }

        private void gunaTileButton4_Click(object sender, EventArgs e)
        {
            Tasks ts = new Tasks();
            ts.ShowDialog();
        }

        private void gunaTileButton2_Click(object sender, EventArgs e)
        {
            
        }
    }
}
