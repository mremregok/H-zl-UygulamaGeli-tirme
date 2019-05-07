using Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Concrete
{
    public class Odeme : IEntity
    {
        public int Id { get; set; }
        public int DoktorId { get; set; }
        public decimal Tutar { get; set; }
        public DateTime? Tarih { get; set; }
    }
}
