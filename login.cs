using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
//using System.Web.WebSockets;
using System.Windows.Forms;
using MySql.Data.MySqlClient;


namespace projetDGM
{
    public partial class login : Form
    {
        public static String Name, Post, Matricule, Fonction, Province;
        
        public login()
        {
            InitializeComponent();
        }

       // MySqlConnection con = new MySqlConnection("SERVER=197.157.193.6;PORT=3306;DATABASE=db_dgm;UID=root; PASSWORD=Spidernet@2021;");
       // MySqlConnection con = new MySqlConnection("server=197.157.193.6; port=3306; uid=crm; PASSWORD= Spidernet@2021; database=db_dgm");
        MySqlConnection con = new MySqlConnection("server=127.0.0.1; port=3306; uid=root; database=db_dgm");
        MySqlCommand cmd = new MySqlCommand();
        private void login_Load(object sender, EventArgs e)
        {
            //gunaAnimateWindow1.Start();
        }

        private String quotes(String str)
        {
            return str.Replace("'", "''");
        }

        private void gunaAdvenceButton2_Click(object sender, EventArgs e)
        {
            if (MetroFramework.MetroMessageBox.Show(this, "Do you really want to quit the application", "Message", MessageBoxButtons.YesNo, MessageBoxIcon.Stop) == DialogResult.Yes)
            {
                Application.Exit();
            }
            else
            {

            }
        }

        void wait1()
        {
            for (int i = 0; i <= 100; i++)
            {
                Thread.Sleep(3);
            }
        }
        private void gunaAdvenceButton1_Click(object sender, EventArgs e)
        {
            try
            {
                if (gunaTextBox1.Text == string.Empty)
                {
                    gunaCircleButton2.Visible = true;
                }
                else if (gunaTextBox2.Text == string.Empty)
                {
                    gunaCircleButton2.Visible = false;
                    gunaCircleButton1.Visible = true;
                }
                else
                {
                    using (var waitform = new loarding(wait1))
                    {
                        waitform.ShowDialog(this);
                    }
                    String SelectQuery = "Select * from utilisateur where username='" + quotes(gunaTextBox1.Text) + "' and password='" + quotes(gunaTextBox2.Text) + "'";
                    MySqlCommand cmd = new MySqlCommand(SelectQuery, con);
                    DataTable dt = new DataTable();
                    MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                    da.Fill(dt);
                    int i = Convert.ToInt32(dt.Rows.Count.ToString());
                    if (i == 1)
                    {
                        string Acces = dt.Rows[0][3].ToString();
                        string function = dt.Rows[0][2].ToString();
                        string name = dt.Rows[0][1].ToString();
                        string matricule = dt.Rows[0][6].ToString();
                        string post = dt.Rows[0][7].ToString();
                        string province = dt.Rows[0][8].ToString();

                        if (Acces =="1")
                        {
                            this.Hide();
                            Name = name;
                            Post = post;
                            Matricule = matricule;
                            Province = province;
                            Fonction = function;

                            PassengerDash ado = new PassengerDash();
                            ado.ShowDialog();
                      
                        }
                        else
                        {
                            MetroFramework.MetroMessageBox.Show(this, "You are no longer Authorized to acces this system", "Message", MessageBoxButtons.OK, MessageBoxIcon.Question);
                        }

                    }
                    else
                    {

                        MetroFramework.MetroMessageBox.Show(this, "Incorrect username or password", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            catch (Exception)
            {
                MetroFramework.MetroMessageBox.Show(this, "Error, THE SYSTEM IS NOT CONNECTED TO THE SERVER", "Message", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void gunaTextBox1_TextChanged(object sender, EventArgs e)
        {
            if (gunaTextBox1.Text == string.Empty)
            {
                gunaCircleButton2.Visible = true;
            }
            else if (gunaTextBox2.Text == string.Empty)
            {
                gunaCircleButton2.Visible = false;
                gunaCircleButton1.Visible = true;
            }
            else
            {
                gunaCircleButton1.Visible = false;
            }
        }

        private void gunaTextBox2_TextChanged(object sender, EventArgs e)
        {
            if (gunaTextBox1.Text == string.Empty)
            {
                gunaCircleButton2.Visible = true;
            }
            else if (gunaTextBox2.Text == string.Empty)
            {
                gunaCircleButton2.Visible = false;
                gunaCircleButton1.Visible = true;
            }
            else
            {
                gunaCircleButton1.Visible = false;
            }
        }
    }
}
