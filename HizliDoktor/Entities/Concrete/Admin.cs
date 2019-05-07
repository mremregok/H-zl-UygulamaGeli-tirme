using Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Concrete
{
    public class Admin : IEntity
    {
        public int Id { get; set; }
        public string Adi { get; set; }
        public string Sifre { get; set; }
    }
}
