using Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Concrete
{
    public class Rapor : IEntity
    {
        public int Id { get; set; }
        public string Aciklama { get; set; }
        public DateTime? Tarih { get; set; }
    }
}
