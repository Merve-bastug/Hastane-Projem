using HastaneOtomasyonu.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MySql.Data.MySqlClient;
using System.Reflection;
using System.Data;

namespace HastaneOtomasyonu.Controllers
{
    public class Randevu_VermeController : Controller
    {
        //
        // GET: /Randevu_Verme/
        [HttpPost]
        public ActionResult RandevuVer(RandevuKayit doktor)
        {
            string connStr = System.Configuration.ConfigurationManager.ConnectionStrings["Merve"].ConnectionString;
            using (MySqlConnection cn = new MySqlConnection(connStr))
            {
                cn.Open();
                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandText = @"INSERT INTO randevu_verme (Hasta_TC,Muayene_Adi,Doktor,Randevu_Tarihi,Randevu_Saati) 
                       Values('" + doktor.TC_Kimlik + "','" + doktor.Muayene_Adi + "','" + doktor.Personel + "','" + doktor.Randevu_Tarihi + "','" + doktor.Randevu_Saati + "') ";
                cmd.Connection = cn;
                cmd.ExecuteNonQuery();
                cn.Close();
                Response.Write("Kayıt İşlemi Onaylandı!!!");
                RandevuVerme mi = new RandevuVerme();
                mi.RandevuKayit = GetDoktorList();
                return View(mi);

            }
        }

     public ActionResult RandevuVer()
     {
         string connStr = System.Configuration.ConfigurationManager.ConnectionStrings["Merve"].ConnectionString;
         using (MySqlConnection cn = new MySqlConnection(connStr))
         {
             cn.Open();
             MySqlCommand cmd = new MySqlCommand();
             var routeValue = RouteData.Values["id"];
             ViewBag.GelenId = routeValue;
             RandevuVerme mi = new RandevuVerme();
             mi.RandevuKayit = GetDoktorList();
             return View(mi);
         }
     }
   
     public List<RandevuKayit> GetDoktorList()
     {
         List<RandevuKayit> res = new List<RandevuKayit>();
         string connStr = System.Configuration.ConfigurationManager.ConnectionStrings["Merve"].ConnectionString;
         using (MySqlConnection cn = new MySqlConnection(connStr))
         {
             cn.Open();
             MySqlCommand cmd = new MySqlCommand();
             cmd = new MySqlCommand("SELECT  Personel_id as Personel_İd,Personel as Personel,Brans as Muayene_Adi FROM personel_kayit WHERE Unvan=1 ORDER BY Brans ", cn);

             MySqlDataReader r = cmd.ExecuteReader();
             while (r.Read())
             {
                 RandevuKayit t = new RandevuKayit();
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
