using Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Concrete
{
    public class Hasta : IEntity
    {
        public int Id { get; set; }
        public int Tc { get; set; }
        public string Adi { get; set; }
        public string Soyadi { get; set; }
        public string Adres { get; set; }
        public string Hastalik { get; set; }
        public DateTime? DogumTarihi { get; set; }
        public string DogumYeri { get; set; }
        public string Cinsiyet { get; set; }
        public int RandevuDurumu { get; set; }
        public string Sifre { get; set; }
    }
}
