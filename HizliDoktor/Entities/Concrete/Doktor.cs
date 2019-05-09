using Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Concrete
{
    public class Doktor : Kisi, IEntity
    {
        public string Unvan { get; set; }
        public int HastaneId { get; set; }
        public int BolumId { get; set; }
    }
}
