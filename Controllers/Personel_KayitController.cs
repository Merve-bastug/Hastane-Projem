using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MySql.Data.MySqlClient;
using HastaneOtomasyonu.Models;

namespace HastaneOtomasyonu.Controllers
{
    public class Personel_KayitController : Controller
    {
        //
        // GET: /Personel_Kayit/

        public ActionResult P_Kayit()
        {
            return View();
        }

        [HttpPost]
        public ActionResult P_Kayit(P__kayit model)
        {
            string connStr = System.Configuration.ConfigurationManager.ConnectionStrings["Merve"].ConnectionString;
            using (MySqlConnection cn = new MySqlConnection(connStr))
            {
                cn.Open();
                MySqlCommand cmd = new MySqlCommand();
                if (model.TC <= 0 && model.Kullanici_Adi == null && model.sifre <= 0)
                {
                    cn.Close();
                    Response.Write("Hiçbir Alan Boş Geçilemez!!!!");
                    return View();
                }
                else
                {
                    cmd.CommandText = @"INSERT INTO personel_kayit (TC_Kimlik,Kullanici_Adi,Sifre,Personel,Brans,Unvan) 
                Values('" + model.TC + "','" + model.Kullanici_Adi + "','" + model.sifre + "','" + model.Personel + "','" + model.Brans + "','" + model.unvan + "') ";

                    cmd.Connection = cn;
                    cmd.ExecuteNonQuery();
                    cn.Close();
                    Response.Write("Kayıt İşlemi Onaylandı!!!");
                    return View();
                }

            }

        }

    }

    }

