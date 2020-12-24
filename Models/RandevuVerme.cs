using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HastaneOtomasyonu.Models
{
    public class RandevuVerme
    {
        public List<DoktorPuan> DoktorPuan { get; set; }
        public List<hastasorgulama> Randevu { get; set; }
        public List<RandevuKayit> RandevuKayit { get; set; }
        public List<Klinik> Klinik { get; set; }
        public List<TedaviDetay> TedaviDetay { get; set; }
       
        public double Protokol { get; set; }
        public double Hastaİsmi { get; set; }
        public int Personel_İd { get; set; }
        public double TC_Kimlik { get; set; }
        public string Muayene_Adi { get; set; }
        public string Personel { get; set; }
        public DateTime Randevu_Tarihi { get; set; }
        public DateTime Randevu_Saati { get; set; }
        public string Kurumu { get; set; }
        public int Ucret { get; set; }
        public string Ucret_Durumu { get; set; }
        public int Muayene_id { get; set; }
      
    }
}