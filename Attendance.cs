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
    public partial class Attendance : Form
    {
        MySqlConnection con = new MySqlConnection("server=127.0.0.1; port=3306; uid=root; database=db_dgm");
        MySqlCommand cmd = new MySqlCommand();
        public Attendance()
        {
            InitializeComponent();
        }

        private String quotes(String str)
        {
            return str.Replace("'", "''");
        }

        //void wait1()
        //{
        //    for (int i = 0; i <= 100; i++)
        //    {
        //        Thread.Sleep(3);
        //    }
        //}

        private void attend()
        {
            try
            {
                con.Open();
                cmd.Connection = con;
                cmd.CommandText = "select RegNo as 'REG NUMBER',Name as 'NAME', Function as 'FUNCTION',Day as 'DAY',Date as 'DATE', StartTime as 'START TIME', Observation as 'OBSERVATION' from attendance where Post = '" + login.Post + "'";
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

        private void gunaAdvenceButton1_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void Attendance_Load(object sender, EventArgs e)
        {
            attend();
            pst.Text = login.Post;
        }

        private void gunaAdvenceButton1_Click_1(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void metroGrid1_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            try
            {

                gunaTextBox3.Text = metroGrid1.CurrentRow.Cells["REG NUMBER"].Value.ToString();
                gunaTextBox4.Text = metroGrid1.CurrentRow.Cells["NAME"].Value.ToString();
                gunaTextBox5.Text = metroGrid1.CurrentRow.Cells["DATE"].Value.ToString();
               
            }
            catch (Exception)
            {

            }
        }

        private void gunaAdvenceButton3_Click(object sender, EventArgs e)
        {
           
            if (MetroFramework.MetroMessageBox.Show(this, "Do you want to really want to save this comment or Observation ?", "Message", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                con.Open();
                cmd.Connection = con;
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "update attendance set Observation='" + quotes(gunaTextBox2.Text) + "' where RegNo='" + quotes(gunaTextBox3.Text) + "' and Date ='" + quotes(gunaTextBox5.Text) + "'";
                cmd.ExecuteNonQuery();
                con.Close(); attend();
                MetroFramework.MetroMessageBox.Show(this, "Information succesfully Updated", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                attend();

                gunaTextBox2.Text = "";
            }
            else
            {
                attend();
                MetroFramework.MetroMessageBox.Show(this, "Operation Cancelled", "Message", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
               

            }
    }
}
