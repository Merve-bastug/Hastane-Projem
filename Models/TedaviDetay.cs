using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HastaneOtomasyonu.Models
{
    public class TedaviDetay
    {
        public double TC_Kimlik { get; set; }
        public string Tedavi { get; set; }
        public string cene { get; set; }

        public string Kurumu { get; set; }
        public string Sonuc { get; set; }
        [Display(Name = "Tedavi_Tarihi")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public string Tedavi_Tarihi { get; set; }

        [Display(Name = "Tedavi_Saati")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{HH:mm}", ApplyFormatInEditMode = true)]
        public string Tedavi_Saati { get; set; }
        public int Ucret { get; set; }
        public string Ucret_Durumu { get; set; }
        public double Muayene_id { get; set; }
        public string Personel { get; set; } 

       
    }
}