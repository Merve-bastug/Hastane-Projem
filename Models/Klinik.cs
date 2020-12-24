using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HastaneOtomasyonu.Models
{
    public class Klinik
    {
        public double TC_Kimlik { get; set; }
        public double Protokol { get; set; }
        public string Hastaİsmi { get; set; }

        [Display(Name = "Randevu_Tarihi")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public string Randevu_Tarihi { get; set; }

        [Display(Name = "Randevu_Saati")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{HH:mm}", ApplyFormatInEditMode = true)]
        public string Randevu_Saati { get; set; }
        public string Muayene_Adi { get; set; }
        public string  Personel { get; set; } 

    }
}