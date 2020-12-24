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
    public class Klinik_DetayController : Controller
    {
         public static MySqlDataReader hf;
        //
        // GET: /Klinik_Detay/
        [HttpPost]
        public ActionResult KlinikDetay(TedaviDetay tedavidet)
        {
            string connStr = System.Configuration.ConfigurationManager.ConnectionStrings["Merve"].ConnectionString;
            using (MySqlConnection cn = new MySqlConnection(connStr))
            {
                tedavidet.Ucret_Durumu = "Ödenmedi";
                if (tedavidet.Kurumu == "SGK" && tedavidet.Tedavi == "Küretaj" || tedavidet.Kurumu == "SGK" && tedavidet.Tedavi == "Detertraj" || tedavidet.Kurumu == "SGK" && tedavidet.Tedavi == "Diş çekimi")
                {
                    tedavidet.Ucret = 100;
                }
                else if (tedavidet.Kurumu == "SGK" && tedavidet.Tedavi == "Sabit ortidonti" || tedavidet.Kurumu == "SGK" && tedavidet.Tedavi == "Gingivektomi" || tedavidet.Kurumu == "SGK" && tedavidet.Tedavi == "Porselen kron")
                {
                    tedavidet.Ucret = 500;
                }
                else if (tedavidet.Kurumu == "BAĞKUR" && tedavidet.Tedavi == "Küretaj" || tedavidet.Kurumu == "BAĞKUR" && tedavidet.Tedavi == "Detertraj" || tedavidet.Kurumu == "BAĞKUR" && tedavidet.Tedavi == "Diş çekimi")
                {
                    tedavidet.Ucret = 150;
                }
                else if (tedavidet.Kurumu == "BAĞKUR" && tedavidet.Tedavi == "Sabit ortidonti" || tedavidet.Kurumu == "BAĞKUR" && tedavidet.Tedavi == "Gingivektomi" || tedavidet.Kurumu == "BAĞKUR" && tedavidet.Tedavi == "Porselen kron")
                {
                    tedavidet.Ucret = 550;
                }
                else if (tedavidet.Kurumu == "Sigortasız" && tedavidet.Tedavi == "Küretaj" || tedavidet.Kurumu == "Sigortasız" && tedavidet.Tedavi == "Detertraj" || tedavidet.Kurumu == "Sigortasız" && tedavidet.Tedavi == "Diş çekimi")
                {
                    tedavidet.Ucret = 200;
                }
                else if (tedavidet.Kurumu == "Sigortasız" && tedavidet.Tedavi == "Sabit ortidonti" || tedavidet.Kurumu == "Sigortasız" && tedavidet.Tedavi == "Gingivektomi" || tedavidet.Kurumu == "Sigortasız" && tedavidet.Tedavi == "Porselen kron")
                {
                    tedavidet.Ucret = 600;
                }
                else
                {
                    tedavidet.Ucret = 0;
                }
                cn.Open();
                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandText = @"INSERT INTO muayene_onay (Hasta_TC,Muayene_Doktor,Tedavi,cene,Kurumu,Tedavi_Tarih,Tedavi_Saat,Tedavi_Sonuc,Ucret,Ucret_Durumu) 
                Values('" + tedavidet.TC_Kimlik + "','" + tedavidet.Personel + "','" + tedavidet.Tedavi + "','" + tedavidet.cene + "','" 
                          + tedavidet.Kurumu + "',CURDATE(),CURTIME(),'"+ tedavidet.Sonuc + "','" + tedavidet.Ucret + "','" + tedavidet.Ucret_Durumu + "') ";
                cmd.Connection = cn;
                cmd.ExecuteNonQuery();
                cn.Close();
                Response.Write("Muane Onaylandı!!!");
                cn.Open();
                RandevuVerme mi = new RandevuVerme();
                mi.TedaviDetay = GetListe(tedavidet);
                return View(mi);

            }
        }

        public ActionResult Odeme(double id, TedaviDetay tedavidet)
        {

            string connStr = System.Configuration.ConfigurationManager.ConnectionStrings["Merve"].ConnectionString;
            using (MySqlConnection cn = new MySqlConnection(connStr))
            {
                cn.Open();
                MySqlCommand cmd = new MySqlCommand("SELECT Muayene_id,Hasta_TC,Ucret_Durumu FROM muayene_onay WHERE Muayene_id=" + id + "", cn);
                cmd.Connection = cn;
                hf = cmd.ExecuteReader();
                hf.Read();
                tedavidet.Ucret_Durumu = hf["Ucret_Durumu"].ToString();
                if (tedavidet.Ucret_Durumu == "Ödenmedi")
                {
                    cn.Close();
                    cn.Open();
                    cmd = new MySqlCommand("UPDATE muayene_onay SET Ucret_Durumu='Ödendi' WHERE Muayene_id=" + id + "", cn);
                    cmd.Connection = cn;
                    cmd.ExecuteNonQuery();
                    Response.Write("Muane Onaylandı!!!");
                }
                else
                {
                    Response.Redirect("/Klinik_Detay/KlinikDetay");
                }

                Response.Redirect("/Klinik_Detay/KlinikDetay");
                return View();
                
            }
        }

        public ActionResult KlinikDetay()
        {
            string connStr = System.Configuration.ConfigurationManager.ConnectionStrings["Merve"].ConnectionString;
            using (MySqlConnection cn = new MySqlConnection(connStr))
            {
                cn.Open();
                MySqlCommand cmd = new MySqlCommand();
                var routeValue = RouteData.Values["idm"];
                ViewBag.GelenId = routeValue;
                var personelValue = RouteData.Values["id"];
                ViewBag.Personel= personelValue;
                RandevuVerme mi = new RandevuVerme();
                mi.TedaviDetay = GetHListe();
                return View(mi);
            }
        }
 


        public List<TedaviDetay> GetListe(TedaviDetay tedavidet)
        {
            List<TedaviDetay> res = new List<TedaviDetay>();
            string connStr = System.Configuration.ConfigurationManager.ConnectionStrings["Merve"].ConnectionString;
            using (MySqlConnection cn = new MySqlConnection(connStr))
            {
                cn.Open();
                MySqlCommand cmd = new MySqlCommand();
                cmd = new MySqlCommand("SELECT Muayene_id as Muayene_id,Hasta_TC as TC_Kimlik,Muayene_Doktor as Personel,Tedavi as Tedavi,cene as cene,Kurumu as Kurumu,Tedavi_Tarih as Tedavi_Tarihi,Tedavi_Saat as Tedavi_Saati,Tedavi_Sonuc as Sonuc,Ucret as Ucret,Ucret_Durumu as Ucret_Durumu FROM muayene_onay WHERE Hasta_TC='" + tedavidet.TC_Kimlik + "' ORDER BY Tedavi_Saat DESC", cn);
                MySqlDataReader r = cmd.ExecuteReader();
                while (r.Read())
                {
                    TedaviDetay t = new TedaviDetay();
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
    
        public List<TedaviDetay> GetHListe()
        {
            List<TedaviDetay> res = new List<TedaviDetay>();
            string connStr = System.Configuration.ConfigurationManager.ConnectionStrings["Merve"].ConnectionString;
            using (MySqlConnection cn = new MySqlConnection(connStr))
            {
                cn.Open();
                MySqlCommand cmd = new MySqlCommand();
                cmd = new MySqlCommand("SELECT Muayene_id as Muayene_id,Hasta_TC as TC_Kimlik,Muayene_Doktor as Personel,Tedavi as Tedavi,cene as cene,Kurumu as Kurumu,Tedavi_Tarih as Tedavi_Tarihi,Tedavi_Saat as Tedavi_Saati,Tedavi_Sonuc as Sonuc,Ucret as Ucret,Ucret_Durumu as Ucret_Durumu FROM muayene_onay ORDER BY Tedavi_Saat DESC", cn);
                MySqlDataReader r = cmd.ExecuteReader();
                while (r.Read())
                {
                    TedaviDetay t = new TedaviDetay();
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

