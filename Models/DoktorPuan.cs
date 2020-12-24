using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HastaneOtomasyonu.Models
{
    public class DoktorPuan
    {
        public double Doktor_TC { get; set; }
        public double Toplam_Maas { get; set; }
        public string Doktorismi { get; set; }

        [Display(Name = "BaslangicTarihi")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public string BaslangicTarihi { get; set; }
        [Display(Name = "BitisTarihi")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public string BitisTarihi { get; set; }
        [Display(Name = "RandevuTarihi")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public string RandevuTarihi { get; set; }
        public string TedaviAdi { get; set; }
        public int TedaviPuan { get; set; }
        public string Maas_Ayi { get; set; }
    }
}