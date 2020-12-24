using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MySql.Data.MySqlClient;
using HastaneOtomasyonu.Models;
using System.Reflection;
using System.Data;

namespace HastaneOtomasyonu.Controllers
{
    public class Randevu_İslemleriController : Controller
    {
        //
        // GET: /Randevu_İslemleri/

       [HttpPost]
        public ActionResult Randevu(hastasorgulama Ran)
        {
            string connStr = System.Configuration.ConfigurationManager.ConnectionStrings["Merve"].ConnectionString;
            using (MySqlConnection cn = new MySqlConnection(connStr))
            {
                cn.Open();
                MySqlCommand cmd = new MySqlCommand();
                RandevuVerme mi = new RandevuVerme();
                mi.Randevu = GetRandevu(Ran);
                return View(mi);
            }
        }
        public ActionResult Randevu()
        {
               string connStr = System.Configuration.ConfigurationManager.ConnectionStrings["Merve"].ConnectionString;
                using (MySqlConnection cn = new MySqlConnection(connStr))
                {
                    cn.Open();
                    MySqlCommand cmd = new MySqlCommand();
                    RandevuVerme mi = new RandevuVerme();
                    mi.Randevu = GetListe();
                    return View(mi); 
                }
        }
        public List<hastasorgulama> GetRandevu(hastasorgulama Ran)
        {
            List<hastasorgulama> res = new List<hastasorgulama>();
            string connStr = System.Configuration.ConfigurationManager.ConnectionStrings["Merve"].ConnectionString;
            using (MySqlConnection cn = new MySqlConnection(connStr))
            {
                cn.Open();
                MySqlCommand cmd = new MySqlCommand();
                cmd = new MySqlCommand("SELECT TC as TC_Kimlik, Hasta_No as Hasta_No,Hasta as Hasta, Dogum_Tarihi as Dogum_Tarihi, Dogum_Yeri as Dogum_Yeri, Kurumu as Kurumu, Adres as Adres, Uyruk as Uyruk, Telefon as Telefon, E_Posta as E_Posta FROM hasta_bilgileri where TC=" + Ran.TC_Kimlik + "", cn);
                MySqlDataReader r = cmd.ExecuteReader();
                while (r.Read())
                {
                    hastasorgulama t = new hastasorgulama();
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
        public List<hastasorgulama> GetListe()
        {
            List<hastasorgulama> res = new List<hastasorgulama>();
            string connStr = System.Configuration.ConfigurationManager.ConnectionStrings["Merve"].ConnectionString;
            using (MySqlConnection cn = new MySqlConnection(connStr))
            {
                cn.Open();
                MySqlCommand cmd = new MySqlCommand();
                cmd = new MySqlCommand("SELECT TC as TC_Kimlik,Hasta_No as Hasta_No,Hasta as Hasta,Dogum_Tarihi as Dogum_Tarihi,Dogum_Yeri as Dogum_Yeri,Kurumu as Kurumu,Adres as Adres,Uyruk as Uyruk,Telefon as Telefon,E_Posta as E_Posta FROM hasta_bilgileri ", cn);
                MySqlDataReader r = cmd.ExecuteReader();
                while (r.Read())
                {
                    hastasorgulama t = new hastasorgulama();
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
