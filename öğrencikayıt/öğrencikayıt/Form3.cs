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
using System.Text.RegularExpressions;

namespace öğrencikayıt
{
    public partial class Form3 : Form
    {
        public Form3()
        {
            InitializeComponent();
        }
        //veri tabanı dosya yolu ve provider nesnesinin hazırlanması.
        OleDbConnection baglantim = new OleDbConnection
        ("Provider=Microsoft.Ace.OleDb.12.0;Data Source=öğrenci.accdb");

        private void ogrencileri_goster()
        {
            try
            {
                baglantim.Open();
                OleDbDataAdapter ogrencileri_listele = new OleDbDataAdapter
                ("select tcno AS[TC KİMLİK NO] ,adi AS[ADI],soyadi AS[SOYADI],dogumyer AS[DOĞUM YERİ],dogumtarih AS[DOĞUM TARİHİ],sınıf AS[SINIF],anneadı AS[ANNE ADI],babaadı AS[BABA ADI],tel AS[TEL],adres AS[ADRES] from ogrenciler Order By adi ASC", baglantim);    
                DataSet dshafıza = new DataSet();
                ogrencileri_listele.Fill(dshafıza);
                dataGridView1.DataSource = dshafıza.Tables[0];
                baglantim.Close();
            }
            catch (Exception hatamsj)
            {
                MessageBox.Show(hatamsj.Message, "Ogrenci Kayıt Programı", MessageBoxButtons.OK, MessageBoxIcon.Error);
                baglantim.Close();
                
            }
        }
        
        private void Form3_Load(object sender, EventArgs e)
        {
            this.Text = "YÖNETİCİ İŞLEMLERİ";
            textBox1.MaxLength = 11;
            textBox9.MaxLength = 10;
            toolTip1.SetToolTip(this.textBox1, "TC Kimlik No 11 Karekter Olmalı.");
            textBox2.CharacterCasing = CharacterCasing.Upper;
            textBox3.CharacterCasing = CharacterCasing.Upper;
            textBox4.CharacterCasing = CharacterCasing.Upper;
            textBox6.CharacterCasing = CharacterCasing.Upper;
            textBox7.CharacterCasing = CharacterCasing.Upper;
            textBox8.CharacterCasing = CharacterCasing.Upper;
            textBox10.CharacterCasing = CharacterCasing.Upper;
            DateTime zaman = DateTime.Now;
            int yil = int.Parse(zaman.ToString("yyyy"));
            int ay = int.Parse(zaman.ToString("MM"));
            int gun = int.Parse(zaman.ToString("dd"));
            dateTimePicker1.MinDate = new DateTime(1960,1,1);
            dateTimePicker1.MaxDate = new DateTime(yil, ay, gun);
            dateTimePicker1.Format = DateTimePickerFormat.Short;
            ogrencileri_goster();
          
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (textBox1.Text.Length < 11)
                errorProvider1.SetError(textBox1, "TC KİMLİK NO 11 KARAKTER OLMALI");
            else
                errorProvider1.Clear();
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (((int)e.KeyChar >= 48 && (int)e.KeyChar <= 57) || (int)e.KeyChar == 8)
                e.Handled = false;
            else
                e.Handled = true;
        }

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsLetter(e.KeyChar) == true || char.IsControl(e.KeyChar) == true || char.IsSeparator(e.KeyChar) == true)
                e.Handled = false;
            else
                e.Handled = true;
        }

        private void textBox3_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsLetter(e.KeyChar) == true || char.IsControl(e.KeyChar) == true || char.IsSeparator(e.KeyChar) == true)
                e.Handled = false;
            else
                e.Handled = true;

        }

        private void textBox4_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsLetter(e.KeyChar) == true || char.IsControl(e.KeyChar) == true || char.IsSeparator(e.KeyChar) == true)
                e.Handled = false;
            else
                e.Handled = true;
        }

        private void textBox7_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsLetter(e.KeyChar) == true || char.IsControl(e.KeyChar) == true || char.IsSeparator(e.KeyChar) == true)
                e.Handled = false;
            else
                e.Handled = true;
        }

        private void textBox8_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsLetter(e.KeyChar) == true || char.IsControl(e.KeyChar) == true || char.IsSeparator(e.KeyChar) == true)
                e.Handled = false;
            else
                e.Handled = true;
        }

        private void textBox9_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (((int)e.KeyChar >= 48 && (int)e.KeyChar <= 57) || (int)e.KeyChar == 8)
                e.Handled = false;
            else
                e.Handled = true;
        }

        private void textBox9_TextChanged(object sender, EventArgs e)
        {
            if (textBox9.Text.Length < 10)
                errorProvider1.SetError(textBox9, "TELEFON NO 10 KARAKTER OLMALI");
            else
                errorProvider1.Clear();
        }
        private void topPage1_temizle()
        {
            textBox1.Clear();textBox2.Clear();textBox3.Clear();
            textBox4.Clear(); textBox6.Clear(); textBox7.Clear();
            textBox8.Clear(); textBox9.Clear(); textBox10.Clear();
            pictureBox1.Image = null;
            
        }
        private void button1_Click(object sender, EventArgs e)
        {
            bool kayıtkontrol = false;
            baglantim.Open();
            OleDbCommand selectsorgu = new OleDbCommand("select*from ogrenciler where tcno='" + textBox1.Text+"'", baglantim);
            OleDbDataReader kayıtokuma = selectsorgu.ExecuteReader();
            while(kayıtokuma.Read())
            {
                kayıtkontrol = true;
                break;
            }
            baglantim.Close();

            if(kayıtkontrol==false)
            {
                if (pictureBox1.Image == null)
                    button5.ForeColor = Color.Red;
                else
                    button5.ForeColor = Color.Black;

                if (textBox1.Text.Length < 11 || textBox1.Text == "")
                    label1.ForeColor = Color.Red;
                else
                    label1.ForeColor = Color.Black;
                if (textBox2.Text.Length < 2 || textBox2.Text == "")
                    label2.ForeColor = Color.Red;
                else
                    label2.ForeColor = Color.Black;
                if (textBox3.Text.Length < 2 || textBox3.Text == "")
                    label3.ForeColor = Color.Red;
                else
                    label3.ForeColor = Color.Black;
                if (textBox4.Text.Length < 3 || textBox4.Text == "")
                    label5.ForeColor = Color.Red;
                else
                    label5.ForeColor = Color.Black;
                if (textBox6.Text.Length < 1 || textBox6.Text == "")
                    label6.ForeColor = Color.Red;
                else
                    label6.ForeColor = Color.Black;
                if (textBox7.Text.Length < 2 || textBox7.Text == "")
                    label7.ForeColor = Color.Red;
                else
                    label7.ForeColor = Color.Black;
                if (textBox8.Text.Length < 3 || textBox8.Text == "")
                    label8.ForeColor = Color.Red;
                else
                    label8.ForeColor = Color.Black;
                if (textBox9.Text.Length < 10 || textBox9.Text == "")
                    label9.ForeColor = Color.Red;
                else
                    label9.ForeColor = Color.Black;
                if (textBox10.Text.Length < 5 || textBox10.Text == "")
                    label10.ForeColor = Color.Red;
                else
                    label10.ForeColor = Color.Black;

                if (textBox1.Text.Length == 11 && textBox1.Text != " "
                    && textBox2.Text != "" && textBox2.Text.Length > 1
                    && textBox3.Text != "" && textBox3.Text.Length > 1
                    && textBox4.Text != "" && textBox4.Text.Length > 1
                    && textBox6.Text != "" && textBox6.Text.Length > 0
                    && textBox7.Text != "" && textBox7.Text.Length > 1
                    && textBox8.Text != "" && textBox8.Text.Length > 1
                    && textBox9.Text != "" && textBox9.Text.Length > 7
                    && textBox10.Text != "" && textBox10.Text.Length > 1
                    &&pictureBox1.Image != null)
                  


                {
                    try
                    {
                        baglantim.Open();
                        OleDbCommand eklekomut = new OleDbCommand("insert into ogrenciler values('" + textBox1.Text + "','" + textBox2.Text + "','" + textBox3.Text + "','" + dateTimePicker1.Text + "','" + textBox4.Text + "','" + textBox6.Text + "','" + textBox7.Text + "','" + textBox8.Text + "','" + textBox9.Text + "','" + textBox10.Text + "')", baglantim);
                        eklekomut.ExecuteNonQuery();
                        baglantim.Close();
                        if (!Directory.Exists(Application.StartupPath + "\\ogrenciresimleri"))
                        
                            Directory.CreateDirectory(Application.StartupPath + "\\ogrenciresimleri");

                            pictureBox1.Image.Save(Application.StartupPath + "\\ogrenciresimleri\\" + textBox1.Text + ".jpg");

                            
                        MessageBox.Show("Kayıt Oluşturuldu!", "Öğrenci Kayıt Programı "
                            , MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        topPage1_temizle();

                    }
                    catch (Exception hatamsj)
                    {
                        MessageBox.Show(hatamsj.Message);
                        baglantim.Close();
                    }
                }
                else
                {
                    MessageBox.Show("Yazı rengi kırmızı olan alanları yeniden giriniz!",
                      "Öğrenci Kayıt Programı", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

            }
          else
            {
                MessageBox.Show("Girilen TC numarası daha önceden kayıtlıdır!",
                    "Öğrenci Kayıt Programı", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            bool kayıt_aramadurumu = false;
            if(textBox1.Text.Length==11)
            {
                baglantim.Open();
                OleDbCommand selectsorgu = new OleDbCommand("select*from ogrenciler where tcno='" + textBox1.Text + "'", baglantim);
                OleDbDataReader kayıtokuma = selectsorgu.ExecuteReader();
                while(kayıtokuma.Read())
                {
                    kayıt_aramadurumu = true;
                    try
                    {
                        pictureBox1.Image = Image.FromFile(Application.StartupPath + "\\ogrenciresimleri\\" + kayıtokuma.GetValue(0).ToString() + ".jpg");
                    }
                    catch (Exception)
                    {
                        pictureBox1.Image = Image.FromFile(Application.StartupPath + "\\ogrenciresimleri\\resimyok.jpg");
                    }
                    textBox2.Text = kayıtokuma.GetValue(1).ToString();
                    textBox3.Text = kayıtokuma.GetValue(2).ToString();
                    dateTimePicker1.Text = kayıtokuma.GetValue(3).ToString();
                    textBox4.Text = kayıtokuma.GetValue(4).ToString();
                    textBox6.Text = kayıtokuma.GetValue(5).ToString();
                    textBox7.Text = kayıtokuma.GetValue(6).ToString();
                    textBox8.Text = kayıtokuma.GetValue(7).ToString();
                    textBox9.Text = kayıtokuma.GetValue(8).ToString();
                    textBox10.Text = kayıtokuma.GetValue(9).ToString();
                    break;
                }
                if(kayıt_aramadurumu==false)
                
                    MessageBox.Show("Aranan kayıt bulunamadı!", "Öğrenci kayıt programı", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                
                baglantim.Close();
            }
            else
            {
                MessageBox.Show("lütfen 11 haneli bir TC kimlik no giriniz!", "Öğrenci takip programı", MessageBoxButtons.OK, MessageBoxIcon.Error);
                topPage1_temizle();           
            }

        }

        private void button3_Click(object sender, EventArgs e)
        {

                

                if (textBox1.Text.Length < 11 || textBox1.Text == "")
                    label1.ForeColor = Color.Red;
                else
                    label1.ForeColor = Color.Black;
                if (textBox2.Text.Length < 2 || textBox2.Text == "")
                    label2.ForeColor = Color.Red;
                else
                    label2.ForeColor = Color.Black;
                if (textBox3.Text.Length < 2 || textBox3.Text == "")
                    label3.ForeColor = Color.Red;
                else
                    label3.ForeColor = Color.Black;
                if (textBox4.Text.Length < 3 || textBox4.Text == "")
                    label5.ForeColor = Color.Red;
                else
                    label5.ForeColor = Color.Black;
                if (textBox6.Text.Length < 1 || textBox6.Text == "")
                    label6.ForeColor = Color.Red;
                else
                    label6.ForeColor = Color.Black;
                if (textBox7.Text.Length < 3 || textBox7.Text == "")
                    label7.ForeColor = Color.Red;
                else
                    label7.ForeColor = Color.Black;
                if (textBox8.Text.Length < 3 || textBox8.Text == "")
                    label8.ForeColor = Color.Red;
                else
                    label8.ForeColor = Color.Black;
                if (textBox9.Text.Length < 10 || textBox9.Text == "")
                    label9.ForeColor = Color.Red;
                else
                    label9.ForeColor = Color.Black;
                if (textBox10.Text.Length < 5 || textBox10.Text == "")
                    label10.ForeColor = Color.Red;
                else
                    label10.ForeColor = Color.Black;

                if (textBox1.Text.Length == 11 && textBox1.Text != " "
                    && textBox2.Text != "" && textBox2.Text.Length > 1
                    && textBox3.Text != "" && textBox3.Text.Length > 1
                    && textBox4.Text != "" && textBox4.Text.Length > 1
                    && textBox6.Text != "" && textBox6.Text.Length > 0
                    && textBox7.Text != "" && textBox7.Text.Length > 1
                    && textBox8.Text != "" && textBox8.Text.Length > 1
                    && textBox9.Text != "" && textBox9.Text.Length > 7
                    && textBox10.Text != "" && textBox10.Text.Length > 1)
                    
                  
            {
                    try
                    {
                        baglantim.Open();
                    OleDbCommand guncellekomut = new OleDbCommand("update  ogrenciler set adi='" + textBox2.Text + "',soyadi='" + textBox3.Text + "',dogumtarih='" + dateTimePicker1.Text + "',dogumyer='" + textBox4.Text + "',sınıf='" + textBox6.Text + "',anneadı='" + textBox7.Text + "',babaadı='" + textBox8.Text + "',tel='" + textBox9.Text + "',adres='" + textBox10.Text + "'where tcno='" + textBox1.Text + "'", baglantim);
                    guncellekomut.ExecuteNonQuery();

                    baglantim.Close();
                   
                    MessageBox.Show("Öğrenci bilgileri güncellendi!", "Öğrenci Kayıt Programı ", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    ogrencileri_goster();
                  
                    }
                    catch (Exception hatamsj)
                    {
                        MessageBox.Show(hatamsj.Message);
                        baglantim.Close();
                    }
                }
                else
                {
                    MessageBox.Show("Yazı rengi kırmızı olan alanları yeniden giriniz!",
                      "Öğrenci Kayıt Programı", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

            }

        private void button2_Click(object sender, EventArgs e)
        {
            if (textBox1.Text.Length == 11)
            {
                bool kayıt_arama = false;
                baglantim.Open();
                OleDbCommand selectsorgu = new OleDbCommand("select*from ogrenciler where tcno='" + textBox1.Text + "'", baglantim);
                OleDbDataReader kayıtokuma = selectsorgu.ExecuteReader();
                while (kayıtokuma.Read())
                {
                    kayıt_arama = true;
                    OleDbCommand deletesorgu = new OleDbCommand("delete from ogrenciler where tcno='" + textBox1.Text + "'", baglantim);
                    deletesorgu.ExecuteNonQuery();
                    

                        MessageBox.Show("Öğrenci kaydı silindi!", "Öğrenci Takip Programı", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    baglantim.Close();
                    ogrencileri_goster();
                    topPage1_temizle();

                    break;
                }
                if (kayıt_arama = false)
                    MessageBox.Show("SİLİNECEK KAYIT BULUNAMADI!", "Öğrenci Takip Programı", MessageBoxButtons.OK, MessageBoxIcon.Error);
                baglantim.Close();
                topPage1_temizle();
            }
            else
                MessageBox.Show("Lütfen 11 haneden oluşan TCNO giriniz! ", "Öğrenci Takip Programı", MessageBoxButtons.OK, MessageBoxIcon.Error);

        }

        private void button4_Click(object sender, EventArgs e)
        {
            topPage1_temizle();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            OpenFileDialog resimsec = new OpenFileDialog();
            resimsec.Title = "Öğrenci resmi seçiniz.";
            resimsec.Filter = "JPG dosyalar(*.jpg) |*.jpg";
            if(resimsec.ShowDialog()==DialogResult.OK)
            {
                this.pictureBox1.Image = new Bitmap(resimsec.OpenFile());

            }

        }
    }
    
}
