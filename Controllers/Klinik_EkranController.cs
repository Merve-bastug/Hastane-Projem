using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HastaneOtomasyonu.Models;
using System.Reflection;


namespace HastaneOtomasyonu.Controllers
{
    public class Klinik_EkranController : Controller
    {
        //
        // GET: /Klinik_Ekran/
        [HttpPost]
        public ActionResult Klinik(Klinik klinik)
        {
            string connStr = System.Configuration.ConfigurationManager.ConnectionStrings["Merve"].ConnectionString;
            using (MySqlConnection cn = new MySqlConnection(connStr))
            {
                cn.Open();
                MySqlCommand cmd = new MySqlCommand();
                RandevuVerme mi = new RandevuVerme();
                mi.Klinik = GetHastaList(klinik);
                return View(mi);

            }
        }

        public ActionResult Klinik()
        {
            string connStr = System.Configuration.ConfigurationManager.ConnectionStrings["Merve"].ConnectionString;
            using (MySqlConnection cn = new MySqlConnection(connStr))
            {
                cn.Open();
                MySqlCommand cmd = new MySqlCommand();
                RandevuVerme mi = new RandevuVerme();
                mi.Klinik = GetDoktorList();
                return View(mi);

            }
        }

        public List<Klinik> GetDoktorList()
        {
            List<Klinik> res = new List<Klinik>();
            string connStr = System.Configuration.ConfigurationManager.ConnectionStrings["Merve"].ConnectionString;
            using (MySqlConnection cn = new MySqlConnection(connStr))
            {
                cn.Open();
                MySqlCommand cmd = new MySqlCommand();
                cmd = new MySqlCommand("SELECT Personel as Personel FROM personel_kayit WHERE Unvan=1 ", cn);
                MySqlDataReader r = cmd.ExecuteReader();
                while (r.Read())
                {
                    Klinik t = new Klinik();
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

        public List<Klinik> GetHastaList(Klinik klinik)
        {
            List<Klinik> res = new List<Klinik>();
            string connStr = System.Configuration.ConfigurationManager.ConnectionStrings["Merve"].ConnectionString;
            using (MySqlConnection cn = new MySqlConnection(connStr))
            {
                cn.Open();
                MySqlCommand cmd = new MySqlCommand();
                cmd = new MySqlCommand("SELECT hasta_bilgileri.TC as TC_Kimlik,hasta_bilgileri.Hasta_No as Protokol,hasta_bilgileri.Hasta as Hastaİsmi,randevu_verme.Muayene_Adi as Muayene_Adi,randevu_verme.Doktor as Personel,randevu_verme.Randevu_Tarihi as Randevu_Tarihi,randevu_verme.Randevu_Saati as Randevu_Saati FROM hasta_bilgileri JOIN randevu_verme ON randevu_verme.Hasta_TC=hasta_bilgileri.TC WHERE randevu_verme.Doktor='" + klinik.Personel + "' AND randevu_verme.Randevu_Tarihi='" + klinik.Randevu_Tarihi + "' ORDER BY randevu_verme.Randevu_Saati ASC", cn);
                MySqlDataReader r = cmd.ExecuteReader();
                while (r.Read())
                {
                    Klinik t = new Klinik();
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
