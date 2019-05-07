using Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Concrete
{
    public class Doktor : IEntity
    {
        public int Id { get; set; }
        public string Adi { get; set; }
        public string Soyadi { get; set; }
        public string Cinsiyet { get; set; }
        public string Unvan { get; set; }
        public int HastaneId { get; set; }
        public int BolumId { get; set; }
        public int Tc { get; set; }
        public DateTime? CalismaSaatiBaslangic { get; set; }
        public DateTime? AraSaati { get; set; }
        public DateTime? CalismaSaatiBitis { get; set; }
        public string Sifre { get; set; }
    }
}
