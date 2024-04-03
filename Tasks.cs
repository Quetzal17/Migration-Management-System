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
using Microsoft.Reporting.WinForms;
using MySql.Data.MySqlClient;

namespace projetDGM
{
    public partial class Tasks : Form
    {
        public Tasks()
        {
            InitializeComponent();
        }

        MySqlConnection con = new MySqlConnection("server=127.0.0.1; port=3306; uid=root; database=db_dgm");
        MySqlCommand cmd = new MySqlCommand();
        private String quotes(String str)
        {
            return str.Replace("'", "''");
        }

        void wait1()
        {
            for (int i = 0; i <= 100; i++)
            {
                Thread.Sleep(3);
            }
        }

        private void users()
        {
            try
            {
                con.Open();
                cmd.Connection = con;
                cmd.CommandText = "select Matricule as 'REG NUMBER',Nom as 'FULL NAME',Fonction as 'FUNCTION',Acces as 'ACCESS',username as 'USER NAME', password as 'PASSWORD' from utilisateur where Post ='"+ login.Post+"' ";
                cmd.ExecuteNonQuery();
                DataTable dt = new DataTable();
                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                da.Fill(dt);
                metroGrid1.DataSource = dt;
                con.Close();
                int counto = metroGrid1.RowCount;
                double countN = counto - 1;
                gunaLabel22.Text = countN.ToString();
            }
            catch (Exception)
            {
                MetroFramework.MetroMessageBox.Show(this, "Erreur, vérifier vos paramètres", "Message", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void SelectTack()
        {
            try
            {
                con.Open();
                cmd.Connection = con;
                cmd.CommandText = "select Task as 'BORDER TASK' from border_tasks";
                cmd.ExecuteNonQuery();
                DataTable dt = new DataTable();
                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                da.Fill(dt);
                metroGrid2.DataSource = dt;
                con.Close();
            }
            catch (Exception)
            {
            }
        }

        private void Addtask()
        {
            try
            {
                gunaComboBox1.Items.Clear();
                con.Open();
                cmd.Connection = con;
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "SELECT * FROM border_tasks";
                MySqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    gunaComboBox1.Items.Add(dr["Task"]);
                }
                con.Close(); dr.Dispose();
            }
            catch (Exception)
            {
            }
        }
        private void total()
        {
            con.Open();
            cmd.Connection = con;
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "select count(*) from passengers_registration";
            double value = Convert.ToInt64(cmd.ExecuteScalar());
            gunaLabel18.Text = Convert.ToString(value);
            con.Close();
        }
        private void stud()
        {
            string student = "Student";
            con.Open();
            cmd.Connection = con;
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "select count(*) from passengers_registration where Occupation = '" + student.ToString() + "'";
            double valstud = Convert.ToInt64(cmd.ExecuteScalar());
            gunaLabel20.Text  = Convert.ToString(valstud);
            con.Close();

            if (gunaLabel18.Text == string.Empty || gunaLabel20.Text == string.Empty)
            {

            }
            else
            {
                double tot, st,percent ;
                tot = Convert.ToDouble(gunaLabel18.Text);
                st = Convert.ToDouble(gunaLabel20.Text);

                percent = st * 100 / tot;
                //circularProgressBar1.Value = circularProgressBar1.Text;
                circularProgressBar1.Text = Convert.ToString(Math.Round(percent, 1)) ;
              //  gunaLabel21.Text = Convert.ToString(Math.Round(percent, 1));
            }
        }

        private void fem()
        {
            string female = "Female";
            con.Open();
            cmd.Connection = con;
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "select count(*) from passengers_registration where Gender = '" + female.ToString() + "'";
            double valstud = Convert.ToInt64(cmd.ExecuteScalar());
            gunaLabel21.Text = Convert.ToString(valstud);
            con.Close();

            if (gunaLabel18.Text == string.Empty || gunaLabel21.Text == string.Empty)
            {

            }
            else
            {
                double tot, fema, percent;
                tot = Convert.ToDouble(gunaLabel18.Text);
                fema = Convert.ToDouble(gunaLabel21.Text);

                percent = fema * 100 / tot;
                //circularProgressBar1.Value = circularProgressBar1.Text;
                circularProgressBar2.Text = Convert.ToString(Math.Round(percent, 1));
                //  gunaLabel21.Text = Convert.ToString(Math.Round(percent, 1));
            }
        }

        private void bord()
        {
            string bord = login.Post;
            con.Open();
            cmd.Connection = con;
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "select count(*) from passengers_registration where Border = '" + bord.ToString() + "'";
            double valstude = Convert.ToInt64(cmd.ExecuteScalar());
            gunaLabel24.Text = Convert.ToString(valstude);
            con.Close();

            if (gunaLabel18.Text == string.Empty || gunaLabel24.Text == string.Empty)
            {

            }
            else
            {
                double tot, borde, percent;
                tot = Convert.ToDouble(gunaLabel18.Text);
                borde = Convert.ToDouble(gunaLabel24.Text);

                percent = borde * 100 / tot;
                //circularProgressBar1.Value = circularProgressBar1.Text;
                circularProgressBar4.Text = Convert.ToString(Math.Round(percent, 1));
                //  gunaLabel21.Text = Convert.ToString(Math.Round(percent, 1));
            }
        }

        private void docu()
        {
            string doc = "TLP";
            con.Open();
            cmd.Connection = con;
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "select count(*) from documents1 where DocType = '" + doc.ToString() + "'";
            double tdoc = Convert.ToInt64(cmd.ExecuteScalar());
            gunaLabel23.Text = Convert.ToString(tdoc);
            con.Close();

            if (gunaLabel18.Text == string.Empty || gunaLabel23.Text == string.Empty)
            {

            }
            else
            {
                double tot, docum, percent;
                tot = Convert.ToDouble(gunaLabel18.Text);
                docum = Convert.ToDouble(gunaLabel23.Text);

                percent = docum * 100 / tot;
                //circularProgressBar1.Value = circularProgressBar1.Text;
                circularProgressBar3.Text = Convert.ToString(Math.Round(percent, 1));
                //  gunaLabel21.Text = Convert.ToString(Math.Round(percent, 1));
            }
        }

        private void metroGrid1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void RegNo()
        {
            try
            {
                string num = "1K223344556677889900A";
                int len = num.Length;
                string opt = string.Empty;
                int optdigit = 5;
                string final;
                int getindex;
                for (int i = 0; i < optdigit; i++)
                {
                    do
                    {
                        getindex = new Random().Next(0, len);
                        final = num.ToCharArray()[getindex].ToString();
                    }
                    while (opt.IndexOf(final) != -1);
                    opt += final;
                }
                gunaTextBox1.Text = (opt);
            }
            catch (Exception)
            { }
        }

            private void metroGrid1_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            try
            {
                gunaTextBox1.Text = metroGrid1.CurrentRow.Cells["REG NUMBER"].Value.ToString();
                gunaTextBox2.Text = metroGrid1.CurrentRow.Cells["FULL NAME"].Value.ToString();
                gunaComboBox1.Text = metroGrid1.CurrentRow.Cells["FUNCTION"].Value.ToString();
                gunaComboBox2.Text = metroGrid1.CurrentRow.Cells["ACCESS"].Value.ToString();
                gunaTextBox3.Text = metroGrid1.CurrentRow.Cells["USER NAME"].Value.ToString();
                gunaTextBox6.Text = metroGrid1.CurrentRow.Cells["PASSWORD"].Value.ToString();
            }
            catch (Exception)
            {
                gunaTextBox1.Text = ""; gunaTextBox2.Text = ""; gunaTextBox3.Text = ""; gunaLabel1.Text = "";
                gunaTextBox4.Text = ""; gunaTextBox6.Text = ""; gunaLabel2.Text = "";
            }
        }

        private void Tasks_Load(object sender, EventArgs e)
        {
            users(); RegNo();
            SelectTack(); Addtask();

             total(); stud(); fem(); docu(); bord();

            pst.Text = login.Post;

            this.reportViewer1.RefreshReport();


        }

        private void gunaAdvenceButton1_Click(object sender, EventArgs e)
        {
            try
            {
                if (gunaTextBox4.Text == string.Empty)
                {
                    gunaCircleButton1.Visible = true;
                }
                else
                {
                    gunaCircleButton1.Visible = false;
                    using (var waitform = new loarding(wait1))
                    {
                        waitform.ShowDialog(this);
                    }
                    String SelectQuery = "Select * from border_tasks where Task='" + quotes(gunaTextBox4.Text) + "'";
                    MySqlCommand cmd1 = new MySqlCommand(SelectQuery, con);
                    DataTable dt = new DataTable();
                    MySqlDataAdapter da = new MySqlDataAdapter(cmd1);
                    da.Fill(dt);
                    int i = Convert.ToInt32(dt.Rows.Count.ToString());
                    if (i == 1)
                    {
                        MetroFramework.MetroMessageBox.Show(this, "This task has been already setted in this system", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        con.Open();
                        cmd.Connection = con;
                        cmd.CommandType = CommandType.Text;
                        cmd.CommandText = "insert into border_tasks(Task) values ('" + quotes(gunaTextBox4.Text) + "')";
                        cmd.ExecuteNonQuery();
                        con.Close(); SelectTack(); Addtask();
                        MetroFramework.MetroMessageBox.Show(this, "New Task successfully Added", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        gunaTextBox4.Text = "";
                    }
                }
            }
            catch (Exception)
            {
                MetroFramework.MetroMessageBox.Show(this, "Error, This operation has failed ", "Message", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void gunaAdvenceButton7_Click(object sender, EventArgs e)
        {
            try
            {
                if (gunaTextBox2.Text == string.Empty || gunaComboBox2.Text == string.Empty || gunaTextBox3.Text == string.Empty || gunaTextBox6.Text == string.Empty)
                {
                    MetroFramework.MetroMessageBox.Show(this, "You have to fill all the information except the Registration Number, only this one will come automatically ", "Message", MessageBoxButtons.OK, MessageBoxIcon.Question);

                }
                else
                {
                    
                    using (var waitform = new loarding(wait1))
                    {
                        waitform.ShowDialog(this);
                    }
                    String SelectQuery = "Select * from utilisateur where Matricule='" + quotes(gunaTextBox1.Text) + "'";
                    MySqlCommand cmd1 = new MySqlCommand(SelectQuery, con);
                    DataTable dt = new DataTable();
                    MySqlDataAdapter da = new MySqlDataAdapter(cmd1);
                    da.Fill(dt);
                    int i = Convert.ToInt32(dt.Rows.Count.ToString());
                    if (i == 1)
                    {
                        if (MetroFramework.MetroMessageBox.Show(this, "Do you want to modify the information of this user?", "Message", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                        {
                            con.Open();
                            cmd.Connection = con;
                            cmd.CommandType = CommandType.Text;
                            cmd.CommandText = "update utilisateur set Nom='" + quotes(gunaTextBox2.Text) + "',Fonction='" + quotes(gunaComboBox1.Text) + "',Acces='" + quotes(gunaComboBox2.Text) + "',username='" + quotes(gunaTextBox3.Text) + "',password='" + quotes(gunaTextBox6.Text) + "' where Matricule='" + quotes(gunaTextBox1.Text) + "'";
                            cmd.ExecuteNonQuery();
                            con.Close(); RegNo(); users();
                            MetroFramework.MetroMessageBox.Show(this, "User information successfully updated", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            // usero();
                            gunaTextBox1.Text = ""; gunaTextBox2.Text = ""; gunaTextBox3.Text = ""; gunaTextBox6.Text = ""; gunaComboBox2.Text = ""; gunaComboBox1.Text = "";
                        }
                        else
                        {
                           // usero();
                            MetroFramework.MetroMessageBox.Show(this, "Operation Cancelled", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                    else
                    {
                        if (MetroFramework.MetroMessageBox.Show(this, "Do you want to add a new user?", "Message", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                        {
                            con.Open();
                            cmd.Connection = con;
                            cmd.CommandType = CommandType.Text;
                            cmd.CommandText = "insert into utilisateur(Nom, Fonction, Acces, username, password, Matricule, Post, Province) values ('" + quotes(gunaTextBox2.Text) + "','" + quotes(gunaComboBox1.Text) + "','" + quotes(gunaComboBox2.Text) + "','" + quotes(gunaTextBox3.Text) + "','" + quotes(gunaTextBox6.Text) + "','" + quotes(gunaTextBox1.Text) + "','" + login.Post + "','" + login.Province + "')";
                            cmd.ExecuteNonQuery();
                            con.Close(); RegNo(); users();
                            MetroFramework.MetroMessageBox.Show(this, "New user successfully created", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            // usero();

                            gunaTextBox1.Text = ""; gunaTextBox2.Text = ""; gunaTextBox3.Text = ""; gunaTextBox6.Text = ""; gunaComboBox2.Text = ""; gunaComboBox1.Text = "";
                        }
                        else
                        {
                           // usero();
                            MetroFramework.MetroMessageBox.Show(this, "Operation Cancelled", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                }
            }
            catch (Exception)
            {
                MetroFramework.MetroMessageBox.Show(this, "Error, PLEASE VERIFY YOUR CONNECTION PARAMETERS", "Message", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void gunaAdvenceButton2_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void gunaAdvenceButton8_Click(object sender, EventArgs e)
        {
            try
            {
                if (gunaTextBox2.Text == string.Empty || gunaComboBox2.Text == string.Empty || gunaTextBox3.Text == string.Empty || gunaTextBox6.Text == string.Empty)
                {
                    MetroFramework.MetroMessageBox.Show(this, "You have to fill all the information except the Registration Number, only this one will come automatically ", "Message", MessageBoxButtons.OK, MessageBoxIcon.Question);

                }

                else
                {
                   
                    using (var waitform = new loarding(wait1))
                    {
                        waitform.ShowDialog(this);
                    }
                    String SelectQuery = "Select * from utilisateur where Matricule='" + quotes(gunaTextBox1.Text) + "'";
                    MySqlCommand cmd1 = new MySqlCommand(SelectQuery, con);
                    DataTable dt = new DataTable();
                    MySqlDataAdapter da = new MySqlDataAdapter(cmd1);
                    da.Fill(dt);
                    int i = Convert.ToInt32(dt.Rows.Count.ToString());
                    if (i == 1)
                    {
                        if (MetroFramework.MetroMessageBox.Show(this, "Do you Want to Delete this user?", "Message", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                        {
                            con.Open();
                            cmd.Connection = con;
                            cmd.CommandType = CommandType.Text;
                            cmd.CommandText = "delete from utilisateur where Matricule='" + quotes(gunaTextBox1.Text) + "'";
                            cmd.ExecuteNonQuery();
                            con.Close(); RegNo(); users();
                            MetroFramework.MetroMessageBox.Show(this, "User information successfully deleted", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                          //  usero();
                        }
                        else
                        {
                          //  usero();
                            MetroFramework.MetroMessageBox.Show(this, "Operation Cancelled", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                    else
                    {
                        MetroFramework.MetroMessageBox.Show(this, "the user haven't been deleted", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            catch (Exception)
            {
                MetroFramework.MetroMessageBox.Show(this, "Error, VERIFY YOUR PARAMETERS", "Message", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void gunaAdvenceButton3_Click(object sender, EventArgs e)
        {
            if (gunaTextBox1.Text == string.Empty && gunaTextBox2.Text == string.Empty && gunaComboBox1.Text == string.Empty && gunaComboBox2.Text == string.Empty && gunaTextBox3.Text == string.Empty && gunaTextBox6.Text == string.Empty)
            {
                RegNo();
            }
            else
            {
                gunaTextBox1.Text = ""; gunaTextBox2.Text = ""; gunaTextBox3.Text = ""; gunaTextBox6.Text = ""; gunaComboBox2.Text = ""; gunaComboBox1.Text = "";
                RegNo();
            }
        }

        private void gunaAdvenceButton6_Click(object sender, EventArgs e)
        {
            String SelectQuery = "select Nom, Fonction, Acces, Username, Matricule from utilisateur where Post = '"+ login.Post +"'";
            MySqlCommand cmd = new MySqlCommand(SelectQuery, con);
            MySqlDataAdapter da = new MySqlDataAdapter(cmd);
            DataSet1 ds = new DataSet1();
            da.Fill(ds, "TRAVELERS");
            ReportDataSource dataSource = new ReportDataSource("DataSet1", ds.Tables[0]);
            this.reportViewer1.LocalReport.DataSources.Clear();
            this.reportViewer1.LocalReport.DataSources.Add(dataSource);
            this.reportViewer1.RefreshReport();
        }

        private void gunaAdvenceButton4_Click(object sender, EventArgs e)
        {
            this.Dispose();
           
        }

        private void gunaAdvenceButton5_Click(object sender, EventArgs e)
        {
            String SelectQuery = "select Nom, Fonction, Acces, Username, Matricule from utilisateur where Acces = '"+ 1 + "' and Post = '" + login.Post + "'";
            MySqlCommand cmd = new MySqlCommand(SelectQuery, con);
            MySqlDataAdapter da = new MySqlDataAdapter(cmd);
            DataSet1 ds = new DataSet1();
            da.Fill(ds, "TRAVELERS");
            ReportDataSource dataSource = new ReportDataSource("DataSet1", ds.Tables[0]);
            this.reportViewer1.LocalReport.DataSources.Clear();
            this.reportViewer1.LocalReport.DataSources.Add(dataSource);
            this.reportViewer1.RefreshReport();
        }

        private void gunaAdvenceButton9_Click(object sender, EventArgs e)
        {
            String SelectQuery = "select Nom, Fonction, Acces, Username, Matricule from utilisateur where Acces = '" + 0 + "' and Post = '" + login.Post + "'";
            MySqlCommand cmd = new MySqlCommand(SelectQuery, con);
            MySqlDataAdapter da = new MySqlDataAdapter(cmd);
            DataSet1 ds = new DataSet1();
            da.Fill(ds, "TRAVELERS");
            ReportDataSource dataSource = new ReportDataSource("DataSet1", ds.Tables[0]);
            this.reportViewer1.LocalReport.DataSources.Clear();
            this.reportViewer1.LocalReport.DataSources.Add(dataSource);
            this.reportViewer1.RefreshReport();
        }

        private void gunaCircleProgressBar1_Click(object sender, EventArgs e)
        {

        }

        private void circularProgressBar1_Click(object sender, EventArgs e)
        {

        }
    }
}
