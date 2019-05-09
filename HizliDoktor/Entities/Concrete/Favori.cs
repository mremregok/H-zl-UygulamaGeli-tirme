using Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Concrete
{
    public class Favori: IEntity
    {
        public int Id { get; set; }
        public int DoktorId { get; set; }
        public int DoktorAdı { get; set; }
        public int HastaId { get; set; }
        public DateTime? OlusturulmaTarihi { get; set; }
    }
}
