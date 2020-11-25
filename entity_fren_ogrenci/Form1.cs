using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace entity_fren_ogrenci
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            // fremework kullanmadan veri çekme 
            InitializeComponent();
        }
        SqlConnection baglanti = new SqlConnection(@"Data Source=DESKTOP-QHTAPA5\MSSQLSERVER01;Initial Catalog=Db_sinav;Integrated Security=True");
        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void btn_ders_listele_Click(object sender, EventArgs e)
        {

            // fremework kullanmadan veri çekme 
            // DERS LİSTELEME
            SqlCommand komut = new SqlCommand("select * from tbl_dersler", baglanti);
            SqlDataAdapter da = new SqlDataAdapter(komut);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;
        }

        private void btn_ogrenci_listele_Click(object sender, EventArgs e)
        {
            // fremework kullanarak veri çekme 
            // ÖĞRENCİ LİSTELEME
            Db_sinavEntities db = new Db_sinavEntities();
            dataGridView1.DataSource = db.tbl_ogrenciler.ToList();
            dataGridView1.Columns[3].Visible = false;  // ÖĞRENCİ TABLOSUNDAKİ 3. SÜTUNU GÖSTERMİYOR
            dataGridView1.Columns[4].Visible = false;  // ÖĞRENCİ TABLOSUNDAKİ 4. SÜTUNU GÖSTERMİYOR
        }

        private void btn_not_listele_Click(object sender, EventArgs e)
        {
            // fremework kullanarak veri çekme 
            // NOT LİSTELEME
            Db_sinavEntities db = new Db_sinavEntities();
            var query = from item in db.tbl_not select new {
                item.not_id,
                item.tbl_ogrenciler.ogrenci_ad, // BU ŞEKİLDE YAZILDIĞINDA ÖĞRENCİ İD  YERİNE İSİM YAZILIR
                item.tbl_ogrenciler.ogrenci_soyad, // BU ŞEKİLDE YAZILDIĞINDA ÖĞRENCİ İD  YERİNE SOYADI YAZILIR
                item.tbl_dersler.ders_ad,          // BU ŞEKİL YAZILDIĞINDA DRS İD YERİNE DERS ADI YAZAR
                item.not_sınav1, 
                item.not_sınav2,
                item.not_sınav3, 
                item.not_ortalama, 
                item.durum };
            dataGridView1.DataSource = query.ToList();
            // İSTENİLEN TABLOLARI GETİRME
        }

        private void btn_kaydet_Click(object sender, EventArgs e)
        {
            // FREMEWORK İLE ÖĞRENCİ EKLEME
            Db_sinavEntities db = new Db_sinavEntities();
            tbl_ogrenciler t = new tbl_ogrenciler();
            t.ogrenci_ad = txt_ogrenci_ad.Text;
            t.ogrenci_soyad = txt_ogrenci_soyad.Text;
            db.tbl_ogrenciler.Add(t);
            db.SaveChanges();                // DEĞİŞİKLİKLERİ KAYDET
            MessageBox.Show("öğrenci listeye eklenmiştir");

        }

        private void btn_sil_Click(object sender, EventArgs e)
        {
            // FREMEWORK İLE ÖĞRENCİ SİLME  Remove Metodu
            Db_sinavEntities db = new Db_sinavEntities();
            int a = Convert.ToInt32(txt_ogrenci_id.Text);
            var x = db.tbl_ogrenciler.Find(a);
            db.tbl_ogrenciler.Remove(x);
            db.SaveChanges();               // DEĞİŞİKLİKLERİ KAYDET
            MessageBox.Show("ÖĞRENCİ SİLİNDİ");
        }

        private void btn_guncelle_Click(object sender, EventArgs e)
        {
            // FREMEWORK İLE ÖĞRENCİ GÜNCELLEME
            Db_sinavEntities db = new Db_sinavEntities();
            int a = Convert.ToInt32(txt_ogrenci_id.Text);
            var x = db.tbl_ogrenciler.Find(a);
            x.ogrenci_ad = txt_ogrenci_ad.Text;
            x.ogrenci_soyad = txt_ogrenci_soyad.Text;
            x.ogrenci_foto = txt_ogrenci_foto.Text;
            db.SaveChanges();                      // DEĞİŞİKLİKLERİ KAYDET
            MessageBox.Show("öğrenci bilgileri güncellendi");

        }

        private void btn_bul_Click(object sender, EventArgs e)
        {
            //FREMEWORK İLE ÖĞRENCİ BULMA
            Db_sinavEntities db = new Db_sinavEntities();
            dataGridView1.DataSource = db.tbl_ogrenciler.Where(x => x.ogrenci_ad == txt_ogrenci_ad.Text).ToList(); // öğrencinin adı yazıldığında öğrenciniz bütün değerlerini getirir.
                                                                                                                   //    dataGridView1.DataSource = db.tbl_ogrenciler.Where(x => x.ogrenci_ad == txt_ogrenci_ad.Text & x.ogrenci_soyad==txt_ogrenci_soyad.Text).ToList();  öğrencinin adı ve soyadı aynıysa getir
                                                                                                                   //    dataGridView1.DataSource = db.tbl_ogrenciler.Where(x => x.ogrenci_ad == txt_ogrenci_ad.Text | x.ogrenci_soyad == txt_ogrenci_soyad.Text).ToList(); öğrencinin adı vaya soyadı aynıysa getir
                                                                                                                   //}
        }

        private void txt_ogrenci_ad_TextChanged(object sender, EventArgs e)
        {
            // Contain metodu ile arama işlemi
            // her hangi bir harfe basıldığında o harfin içeren değerleri getirme
            Db_sinavEntities db = new Db_sinavEntities();
            string aranan = txt_ogrenci_ad.Text;
            var degerler = from s in db.tbl_ogrenciler  where s.ogrenci_ad.Contains(aranan)  select s;
            dataGridView1.DataSource = degerler.ToList();
        }

        private void btn_linq_entitiy_Click(object sender, EventArgs e)
        {
            // entity ile  öğrencileri ada göre sıralama A da Z YE
            Db_sinavEntities db = new Db_sinavEntities();
            if (radioButton1.Checked == true)   // radyo buton işaretli ise
            {
                List<tbl_ogrenciler> liste1 = db.tbl_ogrenciler.OrderBy(p => p.ogrenci_ad).ToList(); // Adan  Zye sırala
                dataGridView1.DataSource = liste1;
            }
            if (radioButton2.Checked == true)
            {
                List<tbl_ogrenciler> liste2 = db.tbl_ogrenciler.OrderByDescending(p => p.ogrenci_ad).ToList();// Z den A ya sırala
                dataGridView1.DataSource = liste2;
            }
            if (radioButton3.Checked == true)
            {
                List<tbl_ogrenciler> liste3 = db.tbl_ogrenciler.OrderBy(p => p.ogrenci_ad).Take(3).ToList(); // ADA GÖRE İLK 3 KAYDI GETİR
                dataGridView1.DataSource = liste3;
            }
            if (radioButton4.Checked == true)
            {
                List<tbl_ogrenciler> liste4 = db.tbl_ogrenciler.Where(p => p.ogrenci_id == 3).ToList(); // İD Sİ 3 OLAN ÖĞRENCİYİ GETİR
                dataGridView1.DataSource = liste4;
            }
            if (radioButton5.Checked == true)
            {
                List<tbl_ogrenciler> liste5 = db.tbl_ogrenciler.Where(p => p.ogrenci_ad.StartsWith("a")).ToList(); // İSMİNİN BAŞ HARFİ A OLAN ÖĞRENCİLERİ GETİR
                dataGridView1.DataSource = liste5;
            }
            if (radioButton6.Checked == true)
            {
                List<tbl_ogrenciler> liste6 = db.tbl_ogrenciler.Where(p => p.ogrenci_ad.EndsWith("a")).ToList(); // İSMİNİN SON HARFİ A OLAN ÖĞRENCİLERİ GETİR
                dataGridView1.DataSource = liste6;
            }
            if (radioButton7.Checked == true)
            {
                bool deger = db.tbl_ogrenciler.Any();
                MessageBox.Show(deger.ToString(), "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);  // TABLODA ÖĞRENCİ OLUP OLMADIĞINI SORGULAR
            }
            if (radioButton8.Checked == true)
            {
                int toplam = db.tbl_ogrenciler.Count();
                MessageBox.Show(toplam.ToString(), "Toplam Öğrenci Sayısı", MessageBoxButtons.OK, MessageBoxIcon.Information); // TOPLAM ÖĞRENCİ SAYISINI BULMA
            }
            if (radioButton9.Checked == true)
            {
                var toplam = db.tbl_not.Sum(p => p.not_sınav1);
                MessageBox.Show("SINAV 1 TOPLAM PUANLAR"+"     " + toplam.ToString()); // 1.SINAV TOPLAM PUAN
            }
            if (radioButton10.Checked==true)
            {
                var ortalama = db.tbl_not.Average(p => p.not_sınav1);
                MessageBox.Show("1. SINAVIN ORTALAMASI" + "   " + ortalama.ToString()); // 1. SINAVIN ORTALAMA PUANI

            }
            if (radioButton11.Checked == true)
            {
                var enyuksek = db.tbl_not.Max(p=>p.not_sınav1);
                MessageBox.Show("1. SINAVIN EN YÜKSEK NOTU " + "  " + enyuksek.ToString()); // 1. SINAVIN EN YÜKSEK PUANI

            }
            if (radioButton12.Checked == true)
            {
                var endusuk = db.tbl_not.Min(p => p.not_sınav1);
                MessageBox.Show("1. SINAVIN EN DÜŞÜK NOTU " + "  " + endusuk.ToString()); // 1. SINAVIN EN DÜŞÜK PUANI

            }
            
            if (radioButton13.Checked == true)
            {
              
                var degerler = db.tbl_not.Where(p => p.not_sınav1 < 50);   // 1. SINAVI 50 DEN KÜÇÜK OLANLARI GETİRİ
                dataGridView1.DataSource = degerler.ToList();
            }
            if (radioButton14.Checked==true)
            {
                var degerler = db.tbl_ogrenciler.Where(p => p.ogrenci_ad == "abdulselam"); // ADI ABDULSELAM OLANLARI GETİRİR  != OLURSA ABDULSELAM DIŞINDAKİLERİ GETİR
                dataGridView1.DataSource = degerler.ToList();
            }
            if (radioButton15.Checked==true)
            {
                var degerler = db.tbl_ogrenciler.Select(x => new { soyad = x.ogrenci_soyad }); // öğrencilerin sadece soy adlarını getirir
                dataGridView1.DataSource = degerler.ToList();
            }
            if (radioButton16.Checked == true)
            {
                var degerler = db.tbl_ogrenciler.Select(x => new 
                {AD=x.ogrenci_ad.ToUpper() ,
                    soyad = x.ogrenci_soyad.ToLower()
                });                                         // ÖĞRENCİLERİN ADINI BÜYÜK SOY ADINI KÜÇÜK GETİRİR
                dataGridView1.DataSource = degerler.ToList();
                
            }
            if (radioButton17.Checked==true)
            {
                //update tbl_not set not_ortalama=(not_sınav1+not_sınav2+not_sınav3)/3   ORTALAMA HESAPLAMA YAZDIRMA.... SQL SERVER
                //  update tbl_not set durum = 0 where not_ortalama<50   geçti kaldıyı yazar... SQL SERVER
                // update tbl_not set durum=1 where not_ortalama>=50....SQL SERVER
                var degerler = db.tbl_not.Select(x => new 
                {
                    ogrenciad = x.tbl_ogrenciler.ogrenci_ad,
                    ortalamsı = x.not_ortalama,                    // ÖĞRENCİLERİN ADINI NOT ORTALAMALARINI VE GEÇİP KALDIKLARINI YAZAR
                    durumu=x.durum==true ? "geçti" : "kaldı"
            });
                dataGridView1.DataSource = degerler.ToList();

            }
            if (radioButton18.Checked==true)
            {
                var degerler = db.tbl_ogrenciler.OrderByDescending(x => x.ogrenci_id).Take(3);  //İD YE GÖRE SON 3 DEĞERİ GÖSTER
                dataGridView1.DataSource = degerler.ToList();
            }
            if (radioButton19.Checked==true)
            {
                var degerler = db.tbl_ogrenciler.OrderBy(x => x.ogrenci_id).Skip(5);  // id ye göre ilk 5 değeri atla
                dataGridView1.DataSource = degerler.ToList();
            }
        }

        private void label6_Click(object sender, EventArgs e)
        {
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Db_sinavEntities db = new Db_sinavEntities();
            label6.Text = db.tbl_not.Max(x=>x.not_ortalama).ToString();  //EN YÜKSEK ORTALAMA

            label15.Text = (from x in db.tbl_ogrenciler orderby x.ogrenci_ad descending select x.ogrenci_ad).First();  // EN YÜKSEK ORTALAMALI ÜRÜN
        }

        private void label14_Click(object sender, EventArgs e)
        {

        }

    }
}