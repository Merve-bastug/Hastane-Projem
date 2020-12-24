using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MySql.Data.MySqlClient;
using HastaneOtomasyonu.Models;

namespace HastaneOtomasyonu.Controllers
{
    public class loginController : Controller
    {
        //
        // GET: /login/
        public static MySqlDataReader hf;
        [HttpPost]
        public ActionResult giris(P__kayit log)
        {
            string connStr = System.Configuration.ConfigurationManager.ConnectionStrings["Merve"].ConnectionString;
            using (MySqlConnection cn = new MySqlConnection(connStr))
            {   cn.Open();
                MySqlCommand cmd = new MySqlCommand();
                cmd = new MySqlCommand("select Kullanici_Adi,Sifre,Unvan from personel_kayit where Kullanici_Adi=@Kullaniciadi and Sifre=@sifre", cn);
                cmd.Parameters.Add("@Kullaniciadi", MySqlDbType.VarChar).Value = log.Kullanici_Adi;
                cmd.Parameters.Add("@sifre", MySqlDbType.VarChar).Value = log.sifre;
                hf = cmd.ExecuteReader();
                if (hf.Read())
                {   var unvan = (hf["Unvan"]).ToString();
                    if (unvan == "4")
                    { Response.Redirect("/Personel_Kayit/P_Kayit");  }
                    else if (unvan == "3")
                    { Response.Redirect("/Randevu_İslemleri/Randevu");  }
                    else if (unvan == "6")
                    {  Response.Redirect("/Doktor_Vezne/DoktorVezne");  }
                    else
                    { Response.Write("Böyle bir kayıt bulunmamaktadır !");  }

                }
                else
                {
                    Response.Redirect("/login/giris");
                    Response.Write("Kullanıcı adı ve şifre hatalı !");
                }
                cmd.Connection = cn;
                cn.Close();
                return View();
            }
           
        }
        public ActionResult giris()
        {
            return View();
        }

    }
}
