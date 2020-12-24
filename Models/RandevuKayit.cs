using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HastaneOtomasyonu.Models
{
    public class RandevuKayit
    {
        public int Personel_İd { get; set; }
        public double TC_Kimlik { get; set; }
        public string Muayene_Adi { get; set; }
        public string Personel { get; set; }

        [Display(Name = "Randevu_Tarihi")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public string Randevu_Tarihi { get; set; }

        [Display(Name = "Randevu_Saati")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{HH:mm}", ApplyFormatInEditMode = true)]
        public string Randevu_Saati { get; set; }
    }
}