using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Speech;
using System.Speech.Recognition;
using System.Speech.Synthesis;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace projetDGM
{
    public partial class PassengerDash : Form
    {
        SpeechRecognitionEngine recEngine = new SpeechRecognitionEngine();
        SpeechSynthesizer speech = new SpeechSynthesizer();

        public static String  username;
        public PassengerDash()
        {
            

            InitializeComponent();
        }
        MySqlConnection con = new MySqlConnection("server=127.0.0.1; port=3306; uid=root; database=db_dgm");
        MySqlCommand cmd = new MySqlCommand();

        private String quotes(String str)
        {
            return str.Replace("'", "''");
        }
        
        private void gunaAdvenceButton1_Click(object sender, EventArgs e)
        {
            try
            {

                

                String SelectQuery = "Select * from utilisateur where Matricule ='" + quotes(mat.Text) + "' ";
                MySqlCommand cmd = new MySqlCommand(SelectQuery, con);
                DataTable dt = new DataTable();
                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                da.Fill(dt);

                con.Open();
                cmd.Connection = con;
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "insert into attendance(RegNo, Name, Function, Post, Province, Day, Date, StartTime) values ('" + quotes(mat.Text) + "','" + quotes(nam.Text) + "', '" + quotes(fx.Text) + "', '" + quotes(pst.Text) + "', '" + quotes(prov.Text) + "', '" + quotes(LDay.Text) + "', '" + quotes(LDate.Text) + "', '" + quotes(LTime.Text) + "')";
                cmd.ExecuteNonQuery();
                con.Close();


                if (fx.Text == "Recoder T1")
                {
                    PassengerRecord pr = new PassengerRecord();
                    pr.ShowDialog();
                    this.Hide();
                }
                else if (fx.Text == "Border Chief")
                {
                    Border bd = new Border();
                    bd.ShowDialog();
                    this.Hide();
                }
                else if (fx.Text == "Docs Producer")
                {
                    Document doc = new Document();
                    doc.ShowDialog();
                    this.Hide();
                }
                else if (fx.Text == "Admin")
                {
                    AdminDash ads = new AdminDash();
                    ads.ShowDialog();
                    this.Hide();
                }

            }
            catch (Exception)
            {
                MetroFramework.MetroMessageBox.Show(this, "Error, data not inserted", "Message", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
    
        }

        private void PassengerDash_Load(object sender, EventArgs e)
        {
            mat.Text = login.Matricule;
            nam.Text = login.Name;
            pst.Text = login.Post;
            fx.Text = login.Fonction;
            prov.Text = login.Province;


            //speech.SelectVoice("Microsoft Zira Desktop");
            //speech.SpeakAsync("Welcome," + nam.Text + ", My name is Quetzal, I have been developed by VItal TCHIRHOUZA Quetzal, I would like to welcome you to the  General Direction of Migration platform, If you have any need of assistance just let me know, I'm here to help you");

            //speech.SelectVoice("Microsoft Hortense Desktop");
            //speech.SpeakAsync("Bonjour," + nam.Text + " je m'appelle Quetzal, j'ai été dévelopé par Vital TCHIRHOUZA et je voudrais vous souhaitez la bienvenu sur la plateforme de la Direction Générale de migration, si vous avez besoin d'une quelconque assistance en rapport avec ce system, dites-moi, je suis là pour vous aidez");
            speech.SelectVoice("Microsoft Zira Desktop");
            speech.SpeakAsync("r");
            speech.SpeakAsync("...,Welcome," + nam.Text + "");
            speech.SelectVoice("Microsoft Hortense Desktop");
            speech.SpeakAsync("Bienvenu," + nam.Text +"" );

            timer1.Start();
        }

        private void gunaShadowPanel3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void matricule_Click(object sender, EventArgs e)
        {

        }

        private void fx_Click(object sender, EventArgs e)
        {

        }

        private void gunaPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void gunaShadowPanel4_Paint(object sender, PaintEventArgs e)
        {

        }

        private void gunaShadowPanel5_Paint(object sender, PaintEventArgs e)
        {

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            LTime.Text = DateTime.Now.ToString("HH:mm");
            LSecond.Text = DateTime.Now.ToString("ss");
            LDate.Text = DateTime.Now.ToString("dd - MMM - yyyy");
            LDay.Text = DateTime.Now.ToString("dddd");
        }

        private void gunaAdvenceButton2_Click(object sender, EventArgs e)
        {
            if (MetroFramework.MetroMessageBox.Show(this, "Do you really want to be deconnected?", "Message", MessageBoxButtons.YesNo, MessageBoxIcon.Stop) == DialogResult.Yes)
            {
                this.Hide();
                login lg = new login();
                lg.ShowDialog();
            }
            else
            {
                
                MetroFramework.MetroMessageBox.Show(this, "operation cancelled", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void gunaAdvenceButton3_Click(object sender, EventArgs e)
        {
            username = nam.Text;
            About ab = new About();
            ab.ShowDialog();
        }

        private void gunaControlBox1_Click(object sender, EventArgs e)
        {
           
        }

        private void gunaAdvenceButton4_Click(object sender, EventArgs e)
        {
            if (MetroFramework.MetroMessageBox.Show(this, "Do you really want to quit the application", "Message", MessageBoxButtons.YesNo, MessageBoxIcon.Stop) == DialogResult.Yes)
            {
                Application.Exit();
            }
            else
            {

            }
        }
    }
}
