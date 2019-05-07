using Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Concrete
{
    public class Randevu : IEntity
    {
        public int Id { get; set; }
        public int DoktorId { get; set; }
        public int BolumId { get; set; }
        public int HastaId { get; set; }
        public DateTime? BaslangicTarihi{ get; set; }
        public DateTime? BitisiTarihi { get; set; }
    }
}
