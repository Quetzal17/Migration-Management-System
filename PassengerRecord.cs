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
using ZXing;
using ZXing.Aztec;
using MySql.Data.MySqlClient;
using System.Drawing.Imaging;



namespace projetDGM
{
    public partial class PassengerRecord : Form
    {
        VideoCaptureDevice frame;
        FilterInfoCollection Devices;

        public static string qr;
        public PassengerRecord()
        {
            InitializeComponent();
        }
        MySqlConnection con = new MySqlConnection("server=127.0.0.1; port=3306; uid=root; database=db_dgm");
        MySqlCommand cmd = new MySqlCommand();
        private String quotes(String str)
        {
            return str.Replace("'", "''");
        }

        void Start_cam()
        {
            Devices = new FilterInfoCollection(FilterCategory.VideoInputDevice);
            frame = new VideoCaptureDevice(Devices[1].MonikerString);
            frame.NewFrame += new AForge.Video.NewFrameEventHandler(NewFrame_event);
            frame.Start();
        }
        void Start_cam1()
        {
            Devices = new FilterInfoCollection(FilterCategory.VideoInputDevice);
            frame = new VideoCaptureDevice(Devices[1].MonikerString);
            frame.NewFrame += new AForge.Video.NewFrameEventHandler(NewFrame_event1);
            frame.Start();
        }

        void NewFrame_event1(object send, NewFrameEventArgs e)
        {
            try
            {
                gunaPictureBox3.Image = (Image)e.Frame.Clone();
            }
            catch (Exception ex)
            {

            }
        }
        private void CaptureDevice_NewFrame(object sender, NewFrameEventArgs eventArgs)
        {
            gunaPictureBox3.Image = (Bitmap)eventArgs.Frame.Clone();
        }
        string output;
        void NewFrame_event(object send, NewFrameEventArgs e)
        {
            try
            {
                gunaPictureBox1.Image = (Image)e.Frame.Clone();
            }
            catch(Exception ex)
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

        private void pass()
        {
            try
            {
               

                con.Open();
                cmd.Connection = con;
                cmd.CommandText = "select Picture as 'PICTURE', Name as 'NAME',LName as 'LAST NAME',Nname as 'NICK NAME',DOB as 'DATE OF BIRHT',Country as 'NATIONALITY', Gender as 'GENDER', NoID as 'PASSPORT', Occupation as 'OCCUPATION', Address as 'ADDRESS', Reason as 'TRAVELING REASON', Destination as 'DESTINATION', Mouvement as 'MOUVEMENT', Date as 'DATE & TIME', Operator as 'OPERATOR', Border as 'BORDER POST', Province as 'PROVINCE', Picture as 'PICTURE' from passengers_registration ";
                cmd.ExecuteNonQuery();
                DataTable dt = new DataTable();
                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                da.Fill(dt);
                metroGrid1.DataSource = dt;
                con.Close();
                int counto = metroGrid1.RowCount;
                double countN = counto - 1;
                gunaLabel1.Text = countN.ToString();

            }
            catch (Exception)
            {
                MetroFramework.MetroMessageBox.Show(this, "Error, PLEASE VERIFY YOUR PARAMETERS", "Message", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
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

                // MemoryStream ms = new MemoryStream();
                //gunaPictureBox1.Image.Save(ms, gunaPictureBox1.Image.RawFormat);
                //byte[] img = ms.ToArray();
                byte[] imageBt = null;
                FileStream fstream = new FileStream(this.gunaTextBox12.Text, FileMode.Open, FileAccess.Read);
                BinaryReader br = new BinaryReader(fstream);
                imageBt = br.ReadBytes((int)fstream.Length);
                

                
                con.Open();

                cmd.Connection = con;
                cmd.Parameters.Add(new MySqlParameter("@IMG", imageBt));
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "insert into passengers_registration(Picture, Name, LName, Nname, DOB, Country, Gender, NoID, Occupation, Address, Reason, Destination, Mouvement, Operator, Border, Province) values (@IMG,'" + quotes(gunaTextBox1.Text) + "','" + quotes(gunaTextBox2.Text) + "', '" + quotes(gunaTextBox3.Text) + "', '" + quotes(gunaDateTimePicker1.Text) + "', '" + quotes(gunaTextBox4.Text) + "', '" + quotes(gunaComboBox1.Text) + "', '" + quotes(gunaTextBox5.Text) + "', '" + quotes(gunaTextBox6.Text) + "', '" + quotes(gunaTextBox7.Text) + "', '" + quotes(gunaTextBox8.Text) + "', '" + quotes(gunaTextBox9.Text) + "', '" + quotes(gunaComboBox2.Text) + "', '" + quotes(mat.Text) + "', '" + quotes(pst.Text) + "',  '" + quotes(prov.Text) + "')";
                cmd.ExecuteNonQuery();
                con.Close(); pass();
                MetroFramework.MetroMessageBox.Show(this, "Information successfully recorded", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                
                gunaTextBox1.Text = ""; gunaTextBox2.Text = ""; gunaTextBox3.Text = ""; gunaTextBox4.Text = ""; gunaTextBox5.Text = ""; gunaComboBox1.Text = "";
                gunaTextBox6.Text = ""; gunaTextBox7.Text = ""; gunaTextBox8.Text = ""; gunaTextBox9.Text = ""; gunaTextBox10.Text = ""; gunaComboBox2.Text = "";
                gunaPictureBox1.Image = null; gunaTextBox12.Text = "";
                

            }
            catch (Exception)
            {
                MetroFramework.MetroMessageBox.Show(this, "Error, data not inserted", "Message", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
         
        }

        private void PassengerRecord_Load(object sender, EventArgs e)
        {
            pass();
            mat.Text = login.Matricule;
            pst.Text = login.Post;
            prov.Text = login.Province;
            gunaTextBox10.Text = QRcode.qr;
        }

        public void SearchPass(string gunaTextBox10)
        {
            try
            {
                string searchQuery = "select Picture as 'PICTURE', Name as 'NAME', LName as 'LAST NAME', Nname as 'NICK NAME', DOB as 'DATE OF BIRTH', Country as 'NATIONALITY', Gender as 'GENDER', NoID as 'PASSPORT', Occupation as 'OCCUPATION', Address as 'ADDRESS', Reason as 'TRAVELING REASON', Destination as 'DESTINATION', Mouvement as 'Mouvement', Date as 'DATE & TIME', Operator as 'OPERATOR', Border as 'BORDER', Province as 'PROVINCE' from passengers_registration where CONCAT(Name,LName, Nname, DOB, Country, Gender, NoID, Occupation, Address, Reason, Destination, Mouvement, Date, Operator, Border, Province) LIKE '%" + gunaTextBox10 + "%'";
                MySqlDataAdapter da = new MySqlDataAdapter(searchQuery, con);
                DataTable dt = new DataTable();
                da.Fill(dt);
                metroGrid1.DataSource = dt;
                con.Close();
                int counto = metroGrid1.RowCount;
                double countN = counto - 1;
                gunaLabel1.Text = countN.ToString();
            }
            catch (Exception)
            { }
        }
        private void metroGrid1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void gunaComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void metroGrid1_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            try
            {
               
                gunaTextBox1.Text = metroGrid1.CurrentRow.Cells["NAME"].Value.ToString();
                gunaTextBox2.Text = metroGrid1.CurrentRow.Cells["LAST NAME"].Value.ToString();
                gunaTextBox3.Text = metroGrid1.CurrentRow.Cells["NICK NAME"].Value.ToString();
              //  gunaDateTimePicker1.Text = metroGrid1.CurrentRow.Cells["DATE OF BIRTH"].Value.ToString();
                gunaTextBox4.Text = metroGrid1.CurrentRow.Cells["NATIONALITY"].Value.ToString();
                gunaComboBox1.Text = metroGrid1.CurrentRow.Cells["GENDER"].Value.ToString();
                gunaTextBox5.Text = metroGrid1.CurrentRow.Cells["PASSPORT"].Value.ToString();
                gunaTextBox6.Text = metroGrid1.CurrentRow.Cells["OCCUPATION"].Value.ToString();
                gunaTextBox7.Text = metroGrid1.CurrentRow.Cells["ADDRESS"].Value.ToString();
                gunaTextBox8.Text = metroGrid1.CurrentRow.Cells["TRAVELING REASON"].Value.ToString();
                gunaTextBox9.Text = metroGrid1.CurrentRow.Cells["DESTINATION"].Value.ToString();
                gunaComboBox2.Text = metroGrid1.CurrentRow.Cells["MOUVEMENT"].Value.ToString();
                gunaTextBox11.Text = metroGrid1.CurrentRow.Cells["DATE & TIME"].Value.ToString();
                //gunaDateTimePicker1.Text = metroGrid1.CurrentRow.Cells["DATE OF BIRTH"].Value.ToString();
                //gunaTextBox4.Text = metroGrid1.CurrentRow.Cells["NATIONALITY"].Value.ToString();
                Byte[] img = (Byte[])metroGrid1.CurrentRow.Cells["PICTURE"].Value;
                MemoryStream ms = new MemoryStream(img);


                gunaPictureBox1.Image = Image.FromStream(ms);
            }
            catch (Exception)
            {
                
            }
        }

        private void gunaTextBox10_TextChanged(object sender, EventArgs e)
        {
            SearchPass(gunaTextBox10.Text);
        }

        private void gunaAdvenceButton3_Click(object sender, EventArgs e)
        {
            gunaTextBox1.Text = ""; gunaTextBox2.Text = ""; gunaTextBox3.Text = ""; gunaTextBox4.Text = ""; gunaTextBox5.Text = ""; gunaComboBox1.Text = "";
            gunaTextBox6.Text = ""; gunaTextBox7.Text = ""; gunaTextBox8.Text = ""; gunaTextBox9.Text = ""; gunaTextBox10.Text = ""; gunaComboBox2.Text = "";
            gunaPictureBox1.Image = null; gunaTextBox12.Text = "";
        }

        private void gunaAdvenceButton5_Click(object sender, EventArgs e)
        {
            try
            {
                
                    
                    using (var waitform = new loarding(wait1))
                    {
                        waitform.ShowDialog(this);
                    }
                    String SelectQuery = "Select * from utilisateur where Matricule ='" + quotes(mat.Text) + "' ";
                  //  String SelectQuery = "Select * from passengers_registration where NoID='" + quotes(gunaTextBox5.Text) + "' and Date='" + quotes(gunaTextBox11.Text) + "'";
                    MySqlCommand cmd1 = new MySqlCommand(SelectQuery, con);
                    DataTable dt = new DataTable();
                    MySqlDataAdapter da = new MySqlDataAdapter(cmd1);
                    da.Fill(dt);
                    int i = Convert.ToInt32(dt.Rows.Count.ToString());
                    if (i == 1)
                    {
                        con.Open();
                        cmd.Connection = con;
                        cmd.CommandType = CommandType.Text;
                        cmd.CommandText = "delete from passengers_registration where NoID='" + quotes(gunaTextBox5.Text) + "' and Date ='" + quotes(gunaTextBox11.Text) + "'";
                        cmd.ExecuteNonQuery();
                        con.Close(); pass();
                        MetroFramework.MetroMessageBox.Show(this, "Information Successfully deleted", "Message", MessageBoxButtons.OK, MessageBoxIcon.Question);
                        
                    }
                    else
                    {
                        MetroFramework.MetroMessageBox.Show(this, "Sorry, the operation has failed", "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                
            }
            catch (Exception)
            {
                MetroFramework.MetroMessageBox.Show(this, "Not Connected to the Server. ERR_INTERNET_DISCONNECTED", "Message", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void gunaAdvenceButton1_Click(object sender, EventArgs e)
        {

        }

        private void gunaCircleButton1_Click(object sender, EventArgs e)
        {
            Start_cam();
        }

        private void gunaCircleButton4_Click(object sender, EventArgs e)
        {
            gunaPictureBox1.Image = null;
            gunaTextBox12.Text = "";
           
                frame.Stop();
           
            
        }

        private void gunaCircleButton3_Click(object sender, EventArgs e)
        {
            //using (OpenFileDialog ofd = new OpenFileDialog() { Filter = "JPEG|*.jpg", ValidateNames = true })
            //{
            //    if (ofd.ShowDialog() == DialogResult.OK)
            //    {
            //        gunaPictureBox1.Image = Image.FromFile(ofd.FileName);
                    
            //    }
            //}

            OpenFileDialog opf = new OpenFileDialog();
            opf.Filter = "Choose Image(*.JPG; *.PNG; *.GIF)|*.jpg; *.png; *.gif";
                 if (opf.ShowDialog() == DialogResult.OK)
                     {
                           string picpath = opf.FileName.ToString();
                           gunaTextBox12.Text = picpath;   
                           gunaPictureBox1.Image = Image.FromFile(opf.FileName);

                     }

            //// folderBrowserDialog1.ShowDialog();
            ////gunaTextBox12.Text = folderBrowserDialog1.SelectedPath;
            ////output = folderBrowserDialog1.SelectedPath;
        }

        private void gunaCircleButton2_Click(object sender, EventArgs e)
        {
            // if (frame.IsRunning)
            //  {
            try
            {
                if (output != "" && gunaPictureBox1 != null)
                {
                    gunaPictureBox1.Image.Save(output + "\\Image.jpg");
                }
                else
                {
                    MetroFramework.MetroMessageBox.Show(this, "Sorry, You can't save a picture when the camera is not started", "Message", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                }
            }
            catch (Exception)
            {


            }
          //  }
        }

        private void PassengerRecord_FormClosed(object sender, FormClosedEventArgs e)
        {

            try
            {
                if (frame.IsRunning)
                {
                    frame.Stop();
                }
                else
                {
                    frame.Stop();
                }
            }
            catch (Exception)
            {

            }
            
        }

       

        private void gunaAdvenceButton1_Click_1(object sender, EventArgs e)
        {
            this.Hide();
            PassengerDash pd = new PassengerDash();
            pd.ShowDialog();
        }

        private void gunaAdvenceButton7_Click(object sender, EventArgs e)
        {
            //  QRcode qr = new QRcode();
            //qr.ShowDialog();
            if(gunaShadowPanel4.Visible == false)
            { 
                gunaTransition1.ShowSync(gunaShadowPanel4);
            }
            else
            {
                gunaTransition1.HideSync(gunaShadowPanel4);
               // gunaShadowPanel4.Width = 0;
                

            }
        }

        private void gunaTextBox12_TextChanged(object sender, EventArgs e)
        {

        }

        private void gunaAdvenceButton8_Click(object sender, EventArgs e)
        {
            try
            {
                Start_cam1();
                timer1.Start();
                gunaTextBox10.Text = "";
            }
            catch (Exception)
            {

            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (gunaPictureBox3.Image != null)
            {
                try
                {
                    BarcodeReader barcodeReader = new BarcodeReader();
                    Result result = barcodeReader.Decode((Bitmap)gunaPictureBox3.Image);
                    if (result != null)
                    {
                       //MessageBox.Show(result.ToString());
                        gunaTextBox10.Text = Convert.ToString(result);
                        timer1.Stop();
                       // qr = gunaTextBox10.Text;

                        if (frame.IsRunning)
                        {
                            frame.Stop();
                        }
                        // this.Hide();
                        gunaTransition1.HideSync(gunaShadowPanel4);
                        gunaPictureBox3.Image = null;
                        
                        // PassengerRecord pr = new PassengerRecord();
                        //pr.ShowDialog();

                    }
                }
                catch (Exception)
                {


                }
            }
        }
    }
}
