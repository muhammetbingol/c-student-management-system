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

namespace öğrencikayıt
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        //  Veri tabanı dosya yolu ve provider nesnesinin belirlenmesi.
        OleDbConnection baglantim = new OleDbConnection("Provider=Microsoft.Ace.OleDb.12.0;Data Source=öğrenci.accdb");

        //formlar arası veri aktarımında kullanılıcak değişkenler
        public static string tcno, adi, soyadi;

        //yerel yani yanlızca bu formda geçerli olacak değişkenler
        int hak = 3; bool durum = false;
        private void Form1_Load(object sender, EventArgs e)
        {
            this.Text = "kullanıcı Girişi..";
            this.AcceptButton = button1;
            this.CancelButton = button2;
            label4.Text = Convert.ToString(hak);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.FixedToolWindow;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (hak != 0)
            {

                baglantim.Open();
                OleDbCommand selectsorgu = new OleDbCommand("select* from kullanicilar", baglantim);
                OleDbDataReader kayıtokuma = selectsorgu.ExecuteReader();
                while (kayıtokuma.Read())
                {
                    if (kayıtokuma["kullaniciadi"].ToString() == textBox1.Text && kayıtokuma["parola"].ToString() == textBox2.Text)
                    {

                        durum = true;
                        tcno = kayıtokuma.GetValue(0).ToString();
                        adi = kayıtokuma.GetValue(1).ToString();
                        soyadi = kayıtokuma.GetValue(2).ToString();
                        this.Hide();
                        Form3 frm3 = new Form3();
                        frm3.Show();
                        break;

                    }

                }

                if (durum == false)
                    hak--;
                baglantim.Close();
            }
            label4.Text = Convert.ToString(hak);
            if (hak == 0)
            {
                button1.Enabled = false;
                MessageBox.Show("Giriş hakkı kalmadı!", "Ogrenci Kayıt Programı", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
            }
        }       
    }
}
