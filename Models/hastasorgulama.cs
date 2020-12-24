using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HastaneOtomasyonu.Models
{
    public class hastasorgulama
    {
        public double TC_Kimlik { get; set; }
        public double Hasta_No { get; set; }
        public string Hasta { get; set; }
        public DateTime Dogum_Tarihi { get; set; }
        public string Dogum_Yeri { get; set; }
        public string Kurumu { get; set; }
        public string Adres { get; set; }
        public string Uyruk { get; set; }
        public double Telefon { get; set; }
        public string E_Posta { get; set; }
    }
}