using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.OleDb;
using System.IO;

namespace öğrencikayıt
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }
        OleDbConnection baglantim = new OleDbConnection
          ("Provider=Microsoft.Ace.OleDb.12.0;Data Source=öğrenci.accdb");

        private void ogrencileri_goster()
        {
            try
            {
                baglantim.Open();
                OleDbDataAdapter ogrencileri_listele = new OleDbDataAdapter
                 ("select tcno AS[TC KİMLİK NO],adi AS[ADI],soyadi AS[SOYADI],kullaniciadi AS[KULLANICI ADI],parola AS[PAROLA] from kullanicilar order By ad ASC", baglantim);
                DataSet dshafıza = new DataSet();
                ogrencileri_listele.Fill(dshafıza);
                dataGridView1.DataSource = dshafıza.Tables[0];
                baglantim.Close();
            }
            catch (Exception hatamsj)
            {
                MessageBox.Show(hatamsj.Message, "Ogrenci Takıp Programı", MessageBoxButtons.OK
                    , MessageBoxIcon.Error);
                baglantim.Close();
                
            }
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            ogrencileri_goster();
        }
    }
}
