using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace projetDGM
{
    public partial class Travelers : Form
    {
        MySqlConnection con = new MySqlConnection("server=127.0.0.1; port=3306; uid=root; database=db_dgm");
        MySqlCommand cmd = new MySqlCommand();
        public Travelers()
        {
            InitializeComponent();
        }


        private void trav()
        {
            try
            {
                con.Open();
                cmd.Connection = con;
                cmd.CommandText = "select Name as 'NAME',LName as 'LAST NAME',Nname as 'NICK NAME',DOB as 'DATE OF BIRHT',Country as 'NATIONALITY', Gender as 'GENDER', NoID as 'PASSPORT', Occupation as 'OCCUPATION', Address as 'ADDRESS', Reason as 'TRAVELING REASON', Destination as 'DESTINATION', Mouvement as 'MOUVEMENT', Date as 'DATE & TIME', Operator as 'OPERATOR' from passengers_registration where Border = '"+ login.Post+"'";
                cmd.ExecuteNonQuery();
                DataTable dt = new DataTable();
                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                da.Fill(dt);
                metroGrid1.DataSource = dt;
                con.Close();
                int counto = metroGrid1.RowCount;
                double countN = counto - 1;
                gunaLabel14.Text = countN.ToString();
            }
            catch (Exception)
            {
                MetroFramework.MetroMessageBox.Show(this, "Erreur, vérifier vos paramètres", "Message", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void gunaPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void Travelers_Load(object sender, EventArgs e)
        {
            trav();
            pst.Text = login.Post;

            this.reportViewer1.RefreshReport();
            this.reportViewer1.RefreshReport();
        }

        private void gunaAdvenceButton6_Click(object sender, EventArgs e)
        {

        }

        private void gunaAdvenceButton2_Click(object sender, EventArgs e)
        {

        }

        private void gunaAdvenceButton1_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void gunaTextBox1_TextChanged(object sender, EventArgs e)
        {
            if (gunaTextBox1.Text == string.Empty)
            {
                gunaCircleButton1.Visible = true;
            }
            else
            {
                gunaCircleButton1.Visible = false;
            }

            SearchTrav(gunaTextBox1.Text);
        }

        private void gunaTextBox1_Click(object sender, EventArgs e)
        {
           
        }
        public void SearchTrav(string gunaTextBox1)
        {
            try
            {
                string searchQuery = "select Name as 'NAME', LName as 'LAST NAME', Nname as 'NICK NAME', DOB as 'DATE OF BIRTH', Country as 'NATIONALITY', Gender as 'GENDER', NoID as 'PASSPORT', Occupation as 'OCCUPATION', Address as 'ADDRESS', Reason as 'TRAVELING REASON', Destination as 'DESTINATION', Mouvement as 'Mouvement', Date as 'DATE & TIME', Operator as 'OPERATOR' from passengers_registration where Border = '"+ login.Post +"' and CONCAT(Name,LName, Nname, DOB, Country, Gender, NoID, Occupation, Address, Reason, Destination, Mouvement, Date, Operator) LIKE '%" + gunaTextBox1 + "%'";
                MySqlDataAdapter da = new MySqlDataAdapter(searchQuery, con);
                DataTable dt = new DataTable();
                da.Fill(dt);
                metroGrid1.DataSource = dt;
                con.Close();
                int counto = metroGrid1.RowCount;
                double countN = counto - 1;
                gunaLabel14.Text = countN.ToString();
            }
            catch (Exception)
            { }
        }
    }
}
