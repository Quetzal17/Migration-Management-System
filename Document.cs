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
using System.Speech.Synthesis;
using System.Drawing.Imaging;

using MySql.Data.MySqlClient;

namespace projetDGM
{
    public partial class Document : Form
    {
        public static string number;
        public Document()
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

         string output;

        private void doc()
        {
            try
            {
                con.Open();
                cmd.Connection = con;
                cmd.CommandText = "select Picture as 'PICTURE', DocNo as 'DOC No', Name as 'NAME',LName as 'LAST NAME',Nname as 'NICK NAME',POB as 'PLACE OF BIRTH', DOB as 'DATE OF BIRTH',Nationality as 'NATIONALITY', Profession as 'PROFESSION', Address as 'ADDRESS', IssuedDate as 'ISSUED DATE', ExpDate as 'EXPIRATION', Operator as 'OPERATOR', Post as 'POST', Province as 'PROVINCE', DateTime as 'DATE & TIME', DocType as 'DOCUMENT TYPE', DocNo as 'DOC No' from documents1 ";
                cmd.ExecuteNonQuery();
                DataTable dt = new DataTable();
                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                da.Fill(dt);
                metroGrid1.DataSource = dt;
                con.Close();
                int counto = metroGrid1.RowCount;
                double countN = counto - 1;
                gunaLabel7.Text = countN.ToString();
            }
            catch (Exception)
            {
                MetroFramework.MetroMessageBox.Show(this, "Erreur, vérifier vos paramètres", "Message", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

       
        
      /*  private void DocumentNo()
        {
            try
            {
                con.Open();
                cmd.Connection = con;
                cmd.CommandText = "select MAX(DocNo) from documents1";
                var maxid = cmd.ExecuteScalar();
                double newId = maxid + 1;
                gunaTextBox11.Text = "" + maxid.ToString();


              /*  if (maxid == null)
                {
                    gunaTextBox11.Text = "DGM-";

                }
                else
                {
                    int intval = int.Parse(maxid.Substring(2, 6));
                    intval++;

                    gunaTextBox11.Text = String.Format("E-{0:000000}", intval);

                }
               // cmd.ExecuteNonQuery();

                con.Close();
               
            }
            catch (Exception)
            {
                MetroFramework.MetroMessageBox.Show(this, "Erreur, vérifier vos paramètres", "Message", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }*/
        /// 
        /// </summary>
        /// <param name="gunaTextBox10"></param>

        public void SearchDoc(string gunaTextBox10)
        {
            try
            {
                string searchQuery = "select Picture as 'PICTURE', Name as 'NAME', LName as 'LAST NAME', Nname as 'NICK NAME',POB as 'PLACE OF BIRTH', DOB as 'DATE OF BIRTH', Nationality as 'NATIONALITY', Profession as 'PROFESSION', Address as 'ADDRESS', IssuedDate as 'ISSUED DATE', ExpDate as 'EXPIRATION', Operator as 'OPERATOR', Post as 'POST', Province as 'PROVINCE', DateTime as 'DATE & TIME', DocType as 'DOCUMENT TYPE', DocNo as 'DOC No' from documents1 where CONCAT(Name,LName, Nname, DOB, Nationality, Profession, IssuedDate, ExpDate, Operator, Post, Province, DocType, DocNo) LIKE '%" + gunaTextBox10 + "%'";
                MySqlDataAdapter da = new MySqlDataAdapter(searchQuery, con);
                DataTable dt = new DataTable();
                da.Fill(dt);
                metroGrid1.DataSource = dt;
                con.Close();
                int counto = metroGrid1.RowCount;
                double countN = counto - 1;
                gunaLabel7.Text = countN.ToString();
            }
            catch (Exception)
            { }
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
                gunaTextBox11.Text = (opt);
            }
            catch (Exception)
            { }
        }
        
        private void gunaCircleButton3_Click(object sender, EventArgs e)
        {

        }

        private void gunaAdvenceButton1_Click(object sender, EventArgs e)
        {
            this.Hide();
            PassengerDash pd = new PassengerDash();
            pd.ShowDialog();
        }

        private void gunaControlBox3_Click(object sender, EventArgs e)
        {

        }

        private void gunaAdvenceButton4_Click(object sender, EventArgs e)
        {
            try
            {

                using (var waitform = new loarding(wait1))
                {
                    waitform.ShowDialog(this);
                }

                

                String SelectQuery = "Select * from utilisateur where Matricule ='" + quotes(mat.Text) + "' ";
                MySqlCommand cmd1 = new MySqlCommand(SelectQuery, con);
                DataTable dt = new DataTable();
                MySqlDataAdapter da = new MySqlDataAdapter(cmd1);
                da.Fill(dt);

                byte[] imageBt = null;
                FileStream fstream = new FileStream(this.gunaTextBox8.Text, FileMode.Open, FileAccess.Read);
                BinaryReader br = new BinaryReader(fstream);
                imageBt = br.ReadBytes((int)fstream.Length);

               

                con.Open();
                cmd.Connection = con;
                cmd.Parameters.Add(new MySqlParameter("@IMG", imageBt));
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "insert into documents1(Picture, Name, LName, Nname,POB, DOB, Nationality, Profession, Address, IssuedDate, ExpDate,  Operator, Post, Province, DocType, DocNo) values (@IMG,'" + quotes(gunaTextBox1.Text) + "','" + quotes(gunaTextBox2.Text) + "', '" + quotes(gunaTextBox3.Text) + "', '" + quotes(gunaTextBox4.Text) + "', '" + quotes(gunaDateTimePicker1.Text) + "', '" + quotes(gunaTextBox5.Text) + "', '" + quotes(gunaTextBox6.Text) + "', '" + quotes(gunaTextBox7.Text) + "', '" + quotes(gunaDateTimePicker2.Text) + "', '" + quotes(gunaDateTimePicker3.Text) + "',  '" + quotes(mat.Text) + "', '" + quotes(pst.Text) + "', '" + quotes(prov.Text) + "', '" + quotes(gunaComboBox1.Text) + "', '" + quotes(gunaTextBox11.Text) + "')";
                cmd.ExecuteNonQuery();
                con.Close(); doc();
                MetroFramework.MetroMessageBox.Show(this, "Information successfully recorded", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);

                gunaTextBox1.Text = ""; gunaTextBox2.Text = ""; gunaTextBox3.Text = ""; gunaTextBox4.Text = ""; gunaTextBox5.Text = ""; gunaComboBox1.Text = "";
                gunaTextBox6.Text = ""; gunaTextBox7.Text = "";  gunaTextBox10.Text = "";


            }
            catch (Exception)
            {
                MetroFramework.MetroMessageBox.Show(this, "Error, data not inserted", "Message", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void gunaAdvenceButton7_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog ofd = new OpenFileDialog() { Filter = "JPEG|*.jpg", ValidateNames = true })
            {
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    string picpath = ofd.FileName.ToString();
                    gunaTextBox8.Text = picpath;
                    gunaPictureBox1.Image = Image.FromFile(ofd.FileName);

                }
            }
        }

        private void gunaAdvenceButton2_Click(object sender, EventArgs e)
        {
            if (gunaTextBox11.Text == string.Empty)
            {

            }
            
              else if(gunaPictureBox3.Image == null)
            {
                Zen.Barcode.CodeQrBarcodeDraw qrcode = Zen.Barcode.BarcodeDrawFactory.CodeQr;
                gunaPictureBox3.Image = qrcode.Draw(gunaTextBox11.Text, 20);

            }
            else
            {
                gunaPictureBox3.Image.Save(output + "\\Image.jpg");
            }

            // gunaTextBox11.Text = "";
        }

        private void metroGrid1_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            try
            {
                
                Byte[] img = (Byte[])metroGrid1.CurrentRow.Cells["PICTURE"].Value;
                MemoryStream ms = new MemoryStream(img);

              
                gunaPictureBox1.Image = Image.FromStream(ms);


                gunaTextBox1.Text = metroGrid1.CurrentRow.Cells["NAME"].Value.ToString();
                gunaTextBox2.Text = metroGrid1.CurrentRow.Cells["LAST NAME"].Value.ToString();
                gunaTextBox3.Text = metroGrid1.CurrentRow.Cells["NICK NAME"].Value.ToString();
                //  gunaDateTimePicker1.Text = metroGrid1.CurrentRow.Cells["DATE OF BIRTH"].Value.ToString();
                gunaTextBox4.Text = metroGrid1.CurrentRow.Cells["PLACE OF BIRTH"].Value.ToString();
                gunaTextBox5.Text = metroGrid1.CurrentRow.Cells["NATIONALITY"].Value.ToString();
                gunaTextBox6.Text = metroGrid1.CurrentRow.Cells["PROFESSION"].Value.ToString();
                gunaTextBox7.Text = metroGrid1.CurrentRow.Cells["ADDRESS"].Value.ToString();
                //  gunaTextBox11.Text = metroGrid1.CurrentRow.Cells["DOCUMENT No"].Value.ToString();
                gunaComboBox1.Text = metroGrid1.CurrentRow.Cells["DOCUMENT TYPE"].Value.ToString();
                //gunaDateTimePicker1.Text = metroGrid1.CurrentRow.Cells["DATE OF BIRTH"].Value.ToString();
                gunaTextBox11.Text = metroGrid1.CurrentRow.Cells["DOC No"].Value.ToString();
            }
            catch (Exception)
            {

            }
        }

        private void Document_Load(object sender, EventArgs e)
        {
            doc();
            mat.Text = login.Matricule;
            pst.Text = login.Post;
            prov.Text = login.Province;
        }

        private void gunaAdvenceButton3_Click(object sender, EventArgs e)
        {
            gunaTextBox1.Text = ""; gunaTextBox2.Text = ""; gunaTextBox3.Text = ""; gunaTextBox4.Text = ""; gunaTextBox5.Text = ""; gunaComboBox1.Text = "";
            gunaTextBox6.Text = ""; gunaTextBox7.Text = ""; gunaTextBox10.Text = ""; gunaPictureBox1.Image = null; gunaPictureBox3.Image = null;
            gunaTextBox11.Text = "";
            RegNo();

           // DocumentNo();

        }

        private void gunaAdvenceButton5_Click(object sender, EventArgs e)
        {
            if (gunaTextBox11.Text == string.Empty)
            {

            }
            else
            {
                 number = gunaTextBox11.Text;
                 PReport pr = new PReport();
                 pr.ShowDialog();
            }
            
        }

        private void gunaTextBox10_TextChanged(object sender, EventArgs e)
        {
            SearchDoc(gunaTextBox10.Text);
        }

        private void gunaPanel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
