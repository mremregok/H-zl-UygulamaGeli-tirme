using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Concrete
{
    public abstract class Kisi
    {
        public int Id { get; set; }
        public string TC { get; set; }
        public string Ad { get; set; }
        public string Soyad { get; set; }
        public int Cinsiyet { get; set; }
        public DateTime? DogumTarihi { get; set; }
        public string Sifre { get; set; }
    }
}
