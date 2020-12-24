using HastaneOtomasyonu.Models;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;

namespace HastaneOtomasyonu.Controllers
{
    public class Doktor_VezneController : Controller
    {
        public static MySqlDataReader hf;
        //
        // GET: /Hasta_Vezne/
        [HttpPost]
        public ActionResult DoktorVezne(DoktorPuan Puan)
        {
            string connStr = System.Configuration.ConfigurationManager.ConnectionStrings["Merve"].ConnectionString;
            using (MySqlConnection cn = new MySqlConnection(connStr))
            {
                cn.Open();
                MySqlCommand cmd = new MySqlCommand(
"SELECT SUM(doktor_puan.Tedavi_puani) as Toplam FROM personel_kayit INNER JOIN muayene_onay ON personel_kayit.Personel=muayene_onay.Muayene_Doktor INNER JOIN doktor_puan ON muayene_onay.Tedavi=doktor_puan.Tedavi_Adi WHERE personel_kayit.TC_Kimlik=" + Puan.Doktor_TC + "", cn);
                //"SELECT SUM(doktor_puan.Tedavi_puani) as Toplam FROM personel_kayit INNER JOIN muayene_onay ON personel_kayit.Personel=muayene_onay.Muayene_Doktor 
                //INNER JOIN doktor_puan ON muayene_onay.Tedavi=doktor_puan.Tedavi_Adi WHERE personel_kayit.TC_Kimlik=" + Puan.Doktor_TC + ""
                cmd.Connection = cn;
                hf = cmd.ExecuteReader();
                hf.Read();
                Puan.Toplam_Maas = Convert.ToInt32(hf["Toplam"]);
                ViewBag.Maas = Puan.Toplam_Maas * 100;
                RandevuVerme mi = new RandevuVerme();
                mi.DoktorPuan = GetDoktorList(Puan);
                return View(mi);
            }
        }

     
        public ActionResult DoktorVezne()
        {

            string connStr = System.Configuration.ConfigurationManager.ConnectionStrings["Merve"].ConnectionString;
            using (MySqlConnection cn = new MySqlConnection(connStr))
            {
                cn.Open();
                MySqlCommand cmd = new MySqlCommand();
                RandevuVerme mi = new RandevuVerme();
                mi.DoktorPuan = GetDokList();
                return View(mi);
            }
        
        }
       
       [HttpPost]
        public ActionResult Kaydet(DoktorPuan Maas)
        {
            string connStr = System.Configuration.ConfigurationManager.ConnectionStrings["Merve"].ConnectionString;
            using (MySqlConnection cn = new MySqlConnection(connStr))
            {
                cn.Open();
                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandText = @"INSERT INTO doktor_maas_hesap (Doktor_TC,Maas_Ayi,Toplam_Maas) 
                Values('" + Maas.Doktor_TC + "','" + Maas.Maas_Ayi + "','" + Maas.Toplam_Maas + "') ";
                cmd.Connection = cn;
                cmd.ExecuteNonQuery();
                cn.Close();
                Response.Write("Aylık Maaş Kaydedildi!!!");
                Response.Redirect("/Doktor_Vezne/DoktorVezne");
                RandevuVerme mi = new RandevuVerme();
                mi.DoktorPuan = GetDoktorList(Maas);
                return View(mi);
            }
        }


        public List<DoktorPuan> GetDoktorList(DoktorPuan Puan)
        {
            List<DoktorPuan> res = new List<DoktorPuan>();
            string connStr = System.Configuration.ConfigurationManager.ConnectionStrings["Merve"].ConnectionString;
            using (MySqlConnection cn = new MySqlConnection(connStr))
            {
                cn.Open();
                MySqlCommand cmd = new MySqlCommand();
                cmd = new MySqlCommand("SELECT personel_kayit.TC_Kimlik as Doktor_TC,personel_kayit.Personel as Doktorismi,muayene_onay.Tedavi_Tarih as RandevuTarihi,muayene_onay.Tedavi as TedaviAdi,doktor_puan.Tedavi_puani as TedaviPuan FROM personel_kayit INNER JOIN muayene_onay ON personel_kayit.Personel=muayene_onay.Muayene_Doktor INNER JOIN doktor_puan ON muayene_onay.Tedavi=doktor_puan.Tedavi_Adi WHERE personel_kayit.TC_Kimlik='" + Puan.Doktor_TC + "'AND muayene_onay.Tedavi_Tarih>='" + Puan.BaslangicTarihi + "' AND muayene_onay.Tedavi_Tarih<='" + Puan.BitisTarihi + "'", cn);
                MySqlDataReader r = cmd.ExecuteReader();
                while (r.Read())
                {
                    DoktorPuan t = new DoktorPuan();
                    for (int inc = 0; inc < r.FieldCount; inc++)
                    {
                        Type type = t.GetType();
                        PropertyInfo prop = type.GetProperty(r.GetName(inc));
                        //prop.SetValue(t, r.GetValue(inc), null);

                        prop.SetValue(t, Convert.ChangeType(r.GetValue(inc), prop.PropertyType), null);
                    }
                    res.Add(t);
                }
                double id = Puan.Doktor_TC;
                ViewBag.Tc = id;
                return res;
                
            }
        }
        public List<DoktorPuan> GetDokList()
        {
            List<DoktorPuan> res = new List<DoktorPuan>();
            string connStr = System.Configuration.ConfigurationManager.ConnectionStrings["Merve"].ConnectionString;
            using (MySqlConnection cn = new MySqlConnection(connStr))
            {
                cn.Open();
                MySqlCommand cmd = new MySqlCommand();
                cmd = new MySqlCommand("SELECT personel_kayit.TC_Kimlik as Doktor_TC,personel_kayit.Personel as Doktorismi,muayene_onay.Tedavi_Tarih as RandevuTarihi,muayene_onay.Tedavi as TedaviAdi,doktor_puan.Tedavi_puani as TedaviPuan FROM personel_kayit INNER JOIN muayene_onay ON personel_kayit.Personel=muayene_onay.Muayene_Doktor INNER JOIN doktor_puan ON muayene_onay.Tedavi=doktor_puan.Tedavi_Adi", cn);
                MySqlDataReader r = cmd.ExecuteReader();
                while (r.Read())
                {
                    DoktorPuan t = new DoktorPuan();
                    for (int inc = 0; inc < r.FieldCount; inc++)
                    {
                        Type type = t.GetType();
                        PropertyInfo prop = type.GetProperty(r.GetName(inc));
                        //prop.SetValue(t, r.GetValue(inc), null);

                        prop.SetValue(t, Convert.ChangeType(r.GetValue(inc), prop.PropertyType), null);
                    }
                    res.Add(t);
                }
                return res;
            }
        }

        DataTable GetDataFromQuery(string query)
        {
            string connStr = System.Configuration.ConfigurationManager.ConnectionStrings["Merve"].ConnectionString;
            using (MySqlConnection cn = new MySqlConnection(connStr))
            {
                cn.Open();
                MySqlCommand cmd = new MySqlCommand();
                MySqlDataAdapter adap = new MySqlDataAdapter(query, cn);
                DataTable data = new DataTable();
                adap.Fill(data);
                return data;
            }
        }

    }
}
