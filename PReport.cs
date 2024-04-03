using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Reporting.WinForms;
using QRCoder;
using MySql.Data.MySqlClient;
using System.IO;
using System.Drawing.Imaging;
using Microsoft.TeamFoundation.Client.Reporting;

namespace projetDGM
{
    public partial class PReport : Form
    {
        
        public PReport()
        {
            InitializeComponent();
        }
        MySqlConnection con = new MySqlConnection("server=127.0.0.1; port=3306; uid=root; database=db_dgm");
        MySqlCommand cmd = new MySqlCommand();

        private void PReport_Load(object sender, EventArgs e)
        {
            gunaLabel1.Text = Document.number;
           // this.cepgl.RefreshReport();
            this.cepgl.LocalReport.EnableExternalImages = true;
        }

        private void gunaAdvenceButton6_Click(object sender, EventArgs e)
        {
            String SelectQuery = "select Picture, Name, Lname, Nname, POB, DOB, Nationality, Profession, Address, IssuedDate, ExpDate, DocNo from documents1 where DocNo = '"+ gunaLabel1.Text + "'";
            MySqlCommand cmd = new MySqlCommand(SelectQuery, con);
            MySqlDataAdapter da = new MySqlDataAdapter(cmd);
            DataSet2 ds = new DataSet2();
            da.Fill(ds, "DOCUMENT");

            ReportDataSource rds = new ReportDataSource("DataSet2", ds.Tables[0]);
            this.cepgl.LocalReport.DataSources.Clear();
            this.cepgl.LocalReport.DataSources.Add(rds);
            // this.cepgl.LocalReport.ReportPath = "CEPGL.rdlc";
            this.cepgl.RefreshReport();


            /* QRCoder.QRCodeGenerator qRCodeGenerator = new QRCodeGenerator();
             QRCoder.QRCodeData qRCodeData = qRCodeGenerator.CreateQrCode(gunaLabel1.Text, QRCoder.QRCodeGenerator.ECCLevel.Q);
             QRCoder.QRCode qRCode = new QRCoder.QRCode(qRCodeData);
             Bitmap bmp = qRCode.GetGraphic(7);*/
            QRCoder.QRCodeGenerator qRCodeGenerator = new QRCoder.QRCodeGenerator();
            QRCoder.QRCodeData qRCodeData = qRCodeGenerator.CreateQrCode(gunaLabel1.Text, QRCoder.QRCodeGenerator.ECCLevel.Q);
            QRCoder.QRCode qRCode = new QRCoder.QRCode(qRCodeData);
            Bitmap bmp = qRCode.GetGraphic(5);

            using (MemoryStream ms = new MemoryStream())
            {
                bmp.Save(ms, ImageFormat.Bmp);
                DataSet2 reportData = new DataSet2();
                DataSet2.QrCodeRow qRCodeRow = reportData.QrCode.NewQrCodeRow();
                qRCodeRow.Image = ms.ToArray();
                reportData.QrCode.AddQrCodeRow(qRCodeRow);

                ReportDataSource reportDataSource = new ReportDataSource();
                reportDataSource.Name = "DataSet1";
                reportDataSource.Value = reportData.QrCode;
              //  cepgl.LocalReport.DataSources.Clear();
                cepgl.LocalReport.DataSources.Add(reportDataSource);
                cepgl.RefreshReport();
              
            }

          
           

        }

        private void reportViewer1_Load(object sender, EventArgs e)
        {

        }

        private void gunaAdvenceButton1_Click(object sender, EventArgs e)
        {
            this.Hide();
        }
    }
}
