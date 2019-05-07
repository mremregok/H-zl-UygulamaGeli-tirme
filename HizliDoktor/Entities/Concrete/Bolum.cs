using Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Concrete
{
    public class Bolum : IEntity
    {
        public int Id { get; set; }
        public string Adi { get; set; }
        public int HastaneId { get; set; }
    }
}
